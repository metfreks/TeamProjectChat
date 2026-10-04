using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class ServerManager
    {
        private TcpListener _listener;
        private bool _isRunning;
        private readonly Dictionary<string, TcpClient> _connectedClients = new();
        public event Action<List<User>> UsersUpdated;
        public event Action<string> StatusChanged;
    }
}
