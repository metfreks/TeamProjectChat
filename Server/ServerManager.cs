using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class ServerManager
    {
        private TcpListener? _listener;
        private bool _isRunning;
        private readonly Dictionary<string, TcpClient> _connectedClients = new();
        public event Action<List<User>>? UsersUpdated;
        public event Action<string>? StatusChanged;
        public event Action<string>? Log;

        public async Task StartServer(int port = 3456)
        {
            if (_isRunning)
            {
                return;
            }
            using (var db = new ServerDBContext())
            {
                await db.Database.EnsureCreatedAsync();
            }
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRunning = true;
            StatusChanged?.Invoke($"Server started on port #{port}.");
            Log?.Invoke($"Server started on port #{port}.");
            await AcceptClients();
        }

        public void StopServer()
        {
            if (!_isRunning)
            {
                return;
            }

            _isRunning = false;
            _listener?.Stop();
            lock (_connectedClients)
            {
                foreach (var client in _connectedClients.Values)
                {
                    client.Close();
                }
                _connectedClients.Clear();
            }

            StatusChanged?.Invoke("Server stopped.");
            Log?.Invoke("Server stopped.");
        }

        private async Task AcceptClients()
        {
            while (_isRunning)
            {
                try
                {
                    var client = await _listener!.AcceptTcpClientAsync();
                    _ = HandleClientAsync(client);
                }
                catch
                {
                    break;
                }
            }
        }

        private async Task SendMessageAsync(TcpClient client, string message)
        {
            try
            {
                var sw = new StreamWriter(client.GetStream());
                sw.AutoFlush = true;
                await sw.WriteLineAsync(message);
            }
            catch
            {

            }
        }

        private async Task RouteMessage(string raw, string sender, string recipientsStr)
        {
            List<TcpClient> targets = new List<TcpClient>();
            lock (_connectedClients)
            {
                if (string.IsNullOrEmpty(recipientsStr) || recipientsStr == "ALL")
                {
                    targets.AddRange(_connectedClients.Values);
                }
                else
                {
                    var recipients = recipientsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var recipient in recipients)
                    {
                        if (_connectedClients.TryGetValue(recipient, out var c))
                        {
                            targets.Add(c);
                        }
                    }
                    if (_connectedClients.TryGetValue(sender, out var senderClient) && !targets.Contains(senderClient))
                    {
                        targets.Add(senderClient);
                    }
                }
            }

            foreach (var client in targets)
            {
                await SendMessageAsync(client, raw);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            var stream = client.GetStream();
            using var sr = new StreamReader(stream);
            string user = "";
            while (client.Connected && _isRunning)
            {
                try
                {
                    string? raw = await sr.ReadLineAsync()!;
                    if (raw == null)
                    {
                        break;
                    }
                    await ProcessReq(raw, client, user, name => user = name);
                }
                catch
                {
                    break;
                }
            }

            if (!string.IsNullOrEmpty(user))
            {
                lock (_connectedClients)
                {
                    _connectedClients.Remove(user);
                }
                Log?.Invoke($"User {user} disconnected.");
                await BroadcastUserListAsync();
            }
        }

        private async Task BroadcastUserListAsync()
        {
            List<string> onlineUsers;
            lock (_connectedClients)
            {
                onlineUsers = _connectedClients.Keys.ToList();
            }
            string command = "USERS|" + string.Join(",", onlineUsers);
            List<TcpClient> clients;
            lock (_connectedClients)
            {
                clients = _connectedClients.Values.ToList();
            }

            foreach (var client in clients)
            {
                await SendMessageAsync(client, command);
            }
        }

        public async Task RefreshUsersList()
        {
            using (var db = new ServerDBContext())
            {
                var users = await db.Users.Select(u => new User { Id = u.Id, Name = u.Name, IsApproved = u.IsApproved, IsBanned = u.IsBanned, Ban = u.Ban }).ToListAsync();
                foreach (var u in users)
                {
                    if (u.IsBanned && u.Ban < DateTime.Now)
                    {
                        u.IsBanned = false;
                    }
                }
                UsersUpdated?.Invoke(users);
            }
        }


        private async Task ProcessReq(string raw, TcpClient client, string currentUsername, Action<string> setUsername)
        {
            var parts = raw.Split(new[] { '|' });
            if (parts.Length < 2)
            {
                return;
            }

            var request = parts[0];
            using (var db = new ServerDBContext())
            {
                switch (request)
                {
                    case "REGISTER":
                        var regUser = parts[1];
                        var regPass = parts[2];

                        var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Name == regUser);
                        if (existingUser == null)
                        {
                            db.Users.Add(new User { Name = regUser, Password = regPass, IsApproved = false });
                            await db.SaveChangesAsync();
                            Log?.Invoke($"Registration from {regUser}.");
                            await SendMessageAsync(client, "SYSTEM|Registration completed. Awaiting approval.");
                            await RefreshUsersList();
                        }
                        else
                        {
                            await SendMessageAsync(client, "ERROR|User already exists.");
                        }
                        break;
                    case "LOGIN":
                        var loginUser = parts[1];
                        var loginPass = parts[2];

                        var user = await db.Users.FirstOrDefaultAsync(u => u.Name == loginUser && u.Password == loginPass);
                        if (user == null)
                        {
                            await SendMessageAsync(client, $"ERROR|Incorrect login or password.");
                        }
                        else if (!user.IsApproved)
                        {
                            await SendMessageAsync(client, $"ERROR|The user is not approved by the admin.");
                        }
                        else if (user.IsBanned && user.Ban > DateTime.Now)
                        {
                            await SendMessageAsync(client, $"ERROR|You are banned until: {user.Ban}");
                        }
                        else
                        {
                            setUsername(user.Name!);
                            lock (_connectedClients)
                            {
                                _connectedClients[user.Name!] = client;
                            }
                            Log?.Invoke($"User {user.Name} logged in.");
                            await SendMessageAsync(client, $"SYSTEM|Login successful.");
                            await SendUserHistoryAsync(client, user.Name!);
                            await BroadcastUserListAsync();
                        }
                        break;
                    case "MSG":
                        string sender = parts[1];
                        string recipientsStr = parts[2];
                        string text = parts[3];
                        var userBan = await db.Users.FirstOrDefaultAsync(u => u.Name == sender);
                        if (userBan!.IsBanned && userBan.Ban > DateTime.Now)
                        {
                            await SendMessageAsync(client, $"ERROR|You are banned until: {userBan.Ban}");
                            break;
                        }

                        db.Messages.Add(new MSG { User = sender, Text = text, Recipients = string.IsNullOrEmpty(recipientsStr) ? "ALL" : recipientsStr });
                        await db.SaveChangesAsync();
                        Log?.Invoke($"Message from {sender} to {(string.IsNullOrEmpty(recipientsStr) ? "ALL" : recipientsStr)}: {text}");
                        await RouteMessage(raw, sender, recipientsStr);
                        break;
                    case "FILE":
                        string fSender = parts[1];
                        string fRecipients = parts[2];
                        string fileName = parts[3];
                        var fileBytes = Convert.FromBase64String(parts[4]);

                        db.Messages.Add(new MSG { User = fSender, FileName = fileName, FileData = fileBytes, Recipients = string.IsNullOrEmpty(fRecipients) ? "ALL" : fRecipients });
                        await db.SaveChangesAsync();
                        Log?.Invoke($"File sent from {fSender} to {(string.IsNullOrEmpty(fRecipients) ? "ALL" : fRecipients)}: {fileName}");
                        await RouteMessage(raw, fSender, fRecipients);
                        break;
                }
            }
        }

        public async Task ApproveUser(int userId)
        {
            using (var db = new ServerDBContext())
            {
                var user = await db.Users.FindAsync(userId);
                if (user != null)
                {
                    user.IsApproved = true;
                    Log?.Invoke($"User approved - {user.Name}.");
                    await db.SaveChangesAsync();
                }
            }
            await RefreshUsersList();
        }

        public async Task BanUser(int userId, int minutes = 60)
        {
            using (var db = new ServerDBContext())
            {
                var user = await db.Users.FindAsync(userId);
                if (user != null)
                {
                    user.IsBanned = true;
                    user.Ban = DateTime.Now.AddMinutes(minutes);
                    await db.SaveChangesAsync();
                    Log?.Invoke($"User {user.Name} is banned for {minutes}.");
                }
            }
            await RefreshUsersList();
        }

        public async Task DeleteUser(int userId)
        {
            using (var db = new ServerDBContext())
            {
                var user = await db.Users.FindAsync(userId);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    Log?.Invoke($"Removed user {user.Name}.");
                }
            }
            await RefreshUsersList();
        }
        private async Task SendUserHistoryAsync(TcpClient client, string username)
        {
            using (var db = new ServerDBContext())
            {
                var userMessages = await db.Messages
                    .Where(m => m.Recipients == "ALL" || m.User == username || m.Recipients.Contains(username))
                    .OrderBy(m => m.Id).ToListAsync();

                foreach (var msg in userMessages)
                {
                    if (!string.IsNullOrEmpty(msg.FileName) && msg.FileData != null)
                    {
                        string base64 = Convert.ToBase64String(msg.FileData);
                        await SendMessageAsync(client, $"HISTORY_FILE|{msg.User}|{msg.Recipients}|{msg.FileName}|{base64}");
                    }
                    else
                    {
                        await SendMessageAsync(client, $"HISTORY_MSG|{msg.User}|{msg.Recipients}|{msg.Text}");
                    }
                }
            }
        }

    }
}
