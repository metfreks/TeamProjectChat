namespace Client
{
    public partial class Form1 : Form
    {
        private readonly ClientNetwork _network;

        private string _currentUser = "";

        public Form1()
        {
            InitializeComponent();

            _network = new ClientNetwork();
            _network.MessageReceived += Network_MessageReceived;

            tbIp.Text = "127.0.0.1";
            tbPort.Text = "3456";

            tbPassword.PasswordChar = '*';

            lbUsers.SelectionMode =
                SelectionMode.MultiExtended;
        }

        private async void loginBtn_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                await ConnectToServer();

                string message =
                    $"LOGIN|{tbLogin.Text}|{tbPassword.Text}";

                await _network.SendAsync(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void registerBtn_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                await ConnectToServer();

                string message =
                    $"REGISTER|{tbLogin.Text}|{tbPassword.Text}";

                await _network.SendAsync(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbIp.Text))
            {
                MessageBox.Show("Enter server IP.");
                return false;
            }

            if (!int.TryParse(tbPort.Text, out int port))
            {
                MessageBox.Show("Enter a valid port.");
                return false;
            }

            if (port < 1 || port > 65535)
            {
                MessageBox.Show("Port must be between 1 and 65535.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                MessageBox.Show("Enter login.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Enter password.");
                return false;
            }

            return true;
        }

        private async Task ConnectToServer()
        {
            if (_network.IsConnected)
            {
                return;
            }

            int port = int.Parse(tbPort.Text);

            await _network.ConnectAsync(
                tbIp.Text,
                port);
        }

        private void Network_MessageReceived(
            string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                    Network_MessageReceived(message)));

                return;
            }

            ProcessServerMessage(message);
        }

        private void ProcessServerMessage(
            string message)
        {
            string[] parts = message.Split('|');

            if (parts.Length == 0)
            {
                return;
            }

            switch (parts[0])
            {
                case "SYSTEM":
                    ProcessSystemMessage(parts);
                    break;

                case "ERROR":
                    ProcessErrorMessage(parts);
                    break;

                case "USERS":
                    ProcessUsersMessage(parts);
                    break;

                case "MSG":
                    ProcessChatMessage(parts);
                    break;

                case "FILE":
                    ProcessFileMessage(parts);
                    break;
            }
        }

        private void ProcessSystemMessage(
            string[] parts)
        {
            if (parts.Length < 2)
            {
                return;
            }

            string message = parts[1];

            if (message == "Login successful.")
            {
                _currentUser = tbLogin.Text;

                loginBtn.Enabled = false;
                registerBtn.Enabled = false;

                tbIp.Enabled = false;
                tbPort.Enabled = false;
                tbLogin.Enabled = false;
                tbPassword.Enabled = false;

                AddChatMessage(
                    "SYSTEM: Login successful.");
            }
            else
            {
                AddChatMessage(
                    $"SYSTEM: {message}");
            }
        }

        private void ProcessErrorMessage(
            string[] parts)
        {
            if (parts.Length < 2)
            {
                return;
            }

            MessageBox.Show(
                parts[1],
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void ProcessUsersMessage(
            string[] parts)
        {
            lbUsers.Items.Clear();

            if (parts.Length < 2)
            {
                return;
            }

            string[] users =
                parts[1].Split(
                    ',',
                    StringSplitOptions.RemoveEmptyEntries);

            foreach (string user in users)
            {
                if (user != _currentUser)
                {
                    lbUsers.Items.Add(user);
                }
            }
        }

        private void ProcessChatMessage(
            string[] parts)
        {
            if (parts.Length < 4)
            {
                return;
            }

            string sender = parts[1];
            string text = parts[3];

            AddChatMessage(
                $"{sender}: {text}");
        }

        private void ProcessFileMessage(
            string[] parts)
        {
            if (parts.Length < 5)
            {
                return;
            }

            string sender = parts[1];
            string fileName = parts[3];
            string base64 = parts[4];

            try
            {
                byte[] fileData =
                    Convert.FromBase64String(base64);

                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() ==
                    DialogResult.OK)
                {
                    File.WriteAllBytes(
                        saveFileDialog.FileName,
                        fileData);

                    AddChatMessage(
                        $"{sender} sent file: {fileName}");
                }
            }
            catch
            {
                MessageBox.Show(
                    "Failed to receive file.",
                    "File Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddChatMessage(
            string message)
        {
            lbChat.Items.Add(message);

            if (lbChat.Items.Count > 0)
            {
                lbChat.TopIndex =
                    lbChat.Items.Count - 1;
            }
        }

        private async void btnSend_Click(
            object sender,
            EventArgs e)
        {
            if (!_network.IsConnected)
            {
                MessageBox.Show(
                    "Not connected to the server.");

                return;
            }

            if (string.IsNullOrWhiteSpace(tbMsg.Text))
            {
                return;
            }

            if (tbMsg.Text.Contains('|'))
            {
                MessageBox.Show(
                    "The | character cannot be used.");

                return;
            }

            string recipients =
                GetSelectedRecipients();

            string message =
                $"MSG|{_currentUser}|{recipients}|{tbMsg.Text}";

            await _network.SendAsync(message);

            tbMsg.Clear();
        }

        private string GetSelectedRecipients()
        {
            if (lbUsers.SelectedItems.Count == 0)
            {
                return "ALL";
            }

            List<string> recipients = new();

            foreach (object item in lbUsers.SelectedItems)
            {
                recipients.Add(item.ToString()!);
            }

            return string.Join(",", recipients);
        }

        private async void btnFileSend_Click(
            object sender,
            EventArgs e)
        {
            if (!_network.IsConnected)
            {
                MessageBox.Show(
                    "Not connected to the server.");

                return;
            }

            if (openFileDialog.ShowDialog() !=
                DialogResult.OK)
            {
                return;
            }

            try
            {
                string fileName =
                    Path.GetFileName(
                        openFileDialog.FileName);

                byte[] fileData =
                    await File.ReadAllBytesAsync(
                        openFileDialog.FileName);

                string base64 =
                    Convert.ToBase64String(fileData);

                string recipients =
                    GetSelectedRecipients();

                string message =
                    $"FILE|{_currentUser}|{recipients}|{fileName}|{base64}";

                await _network.SendAsync(message);

                AddChatMessage(
                    $"You sent file: {fileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "File Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            _network.Disconnect();
        }
    }
}