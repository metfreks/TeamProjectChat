using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class ServerManager
    {
        private bool _isRunning;
        private readonly Dictionary<string, TcpClient> _connectedClients = new();
        public event Action<List<User>> UsersUpdated;
        public event Action<string> StatusChanged;

        //public ServerManager()
        //{

        //}

        public async Task StartServer(int port = 3456)
        {
            if (_isRunning)
            {
                return;
            }
            using (var db = new ServerDBContext())
            {
                db.Database.EnsureCreated();
            }
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRunning = true;
            StatusChanged?.Invoke($"Server started in port #{port}.");
            await AcceptClients();
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

        private async Task HandleClientAsync(TcpClient client)
        {
            
        }

    }
}
