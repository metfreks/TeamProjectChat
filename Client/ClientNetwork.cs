using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Client
{
    internal class ClientNetwork
    {
        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;
        public string? Name { get; set; }
        public bool IsConnected => _client != null && _client.Connected;
        public event Action<bool>? ConnectionChanged;
        public event Action<string>? ErrorMSG;
        public event Action<string>? SystemMSG;
        public event Action<List<string>>? UsersUpdated;
        public event Action<string, string, string>? TextReceived;
        public event Action<string, string, byte[]>? FileReceived;

        public async Task ConnectAsync(string ip, int port)
        {
            if (IsConnected)
            {
                return;
            }
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(ip, port);
                var stream = _client.GetStream();
                _reader = new StreamReader(stream);
                _writer = new StreamWriter(stream);
                _writer.AutoFlush = true;
                ConnectionChanged?.Invoke(true);
                _ = ReceiveMessagesAsync();
            }
            catch (Exception ex)
            {
                ErrorMSG?.Invoke($"Error. {ex.Message}");
                Disconnect();
            }
        }

        private async Task SendRequest(string req)
        {
            if (_writer != null && IsConnected)
            {
                try
                {
                    await _writer.WriteLineAsync(req);
                }
                catch (Exception ex)
                {
                    ErrorMSG?.Invoke($"Error. {ex.Message}");
                }
            }
        }

        public async Task SendTextAsync(string recipient, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string target = string.IsNullOrEmpty(recipient) ? "ALL" : recipient;
            await SendRequest($"MSG|{Name}|{target}|{message}");
        }

        private async Task ReceiveMessagesAsync()
        {
            if (_reader == null)
            {
                return;
            }
            while (IsConnected)
            {
                try
                {
                    var raw = await _reader.ReadLineAsync()!;
                    if (raw == null)
                    {
                        break;
                    }
                    ProcessReqClientAsync(raw);
                }
                catch
                {
                    break;
                }
            }
            Disconnect();
        }

        public async Task Login(string name, string password)
        {
            Name = name;
            await SendRequest($"LOGIN|{name}|{password}");
        }

        public async Task Register(string name, string password)
        {
            Name = name;
            await SendRequest($"REGISTER|{name}|{password}");
        }

        public async Task SendFile(string recipient, string filePath)
        {
            if (!File.Exists(filePath))
            {
                ErrorMSG?.Invoke("File does not exist.");
                return;
            }
            try
            {
                var fileBytes = await File.ReadAllBytesAsync(filePath);
                string fileName = Path.GetFileName(filePath);
                var b = Convert.ToBase64String(fileBytes);
                string target = string.IsNullOrEmpty(recipient) ? "ALL" : recipient;
                await SendRequest($"FILE|{Name}|{target}|{fileName}|{b}");
            }
            catch (Exception ex)
            {
                ErrorMSG?.Invoke($"Error. {ex.Message}");
            }
        }

        private void ProcessReqClientAsync(string req)
        {
            var parts = req.Split(new[] { '|' });
            if (parts.Length < 2)
            {
                return;
            }
            var request = parts[0];
            switch (request)
            {
                case "SYSTEM":
                    SystemMSG?.Invoke(parts[1]);
                    break;
                case "ERROR":
                    ErrorMSG?.Invoke(parts[1]);
                    break;
                case "USERS":
                    List<string> users = new();
                    if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1]))
                    {
                        users.AddRange(parts[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    UsersUpdated?.Invoke(users);
                    break;
                case "MSG":
                    if (parts.Length >= 4)
                    {
                        TextReceived?.Invoke(parts[1], parts[2], parts[3]);
                    }
                    break;
                case "FILE":
                    if (parts.Length >= 5)
                    {
                        var bytes = Convert.FromBase64String(parts[4]);
                        FileReceived?.Invoke(parts[1], parts[3], bytes);
                    }
                    break;
                case "HISTORY_MSG":
                    if (parts.Length >= 4)
                    {
                        TextReceived?.Invoke(parts[1], parts[2], parts[3]);
                    }
                    break;

                case "HISTORY_FILE":
                    if (parts.Length >= 5)
                    {
                        string sender = parts[1];
                        string fileName = parts[3];
                        SystemMSG?.Invoke($"{sender} send the file {fileName}");
                    }
                    break;
            }
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }
            _client?.Close();
            _reader?.Dispose();
            _writer?.Dispose();
            ConnectionChanged?.Invoke(false);
        }
    }
}