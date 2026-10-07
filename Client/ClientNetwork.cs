using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class ClientNetwork
    {
        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;

        public bool IsConnected =>
            _client != null && _client.Connected;

        public event Action<string>? MessageReceived;

        public async Task ConnectAsync(string ip, int port)
        {
            if (IsConnected)
            {
                return;
            }

            _client = new TcpClient();

            await _client.ConnectAsync(ip, port);

            NetworkStream stream = _client.GetStream();

            _reader = new StreamReader(
                stream,
                Encoding.UTF8);

            _writer = new StreamWriter(
                stream,
                Encoding.UTF8)
            {
                AutoFlush = true
            };

            _ = ReceiveMessagesAsync();
        }

        public async Task SendAsync(string message)
        {
            if (_writer == null)
            {
                return;
            }

            await _writer.WriteLineAsync(message);
        }

        private async Task ReceiveMessagesAsync()
        {
            if (_reader == null)
            {
                return;
            }

            try
            {
                while (_client != null && _client.Connected)
                {
                    string? message =
                        await _reader.ReadLineAsync();

                    if (message == null)
                    {
                        break;
                    }

                    MessageReceived?.Invoke(message);
                }
            }
            catch
            {
            }
        }

        public void Disconnect()
        {
            try
            {
                _reader?.Close();
                _writer?.Close();
                _client?.Close();
            }
            catch
            {
            }
        }
    }
}