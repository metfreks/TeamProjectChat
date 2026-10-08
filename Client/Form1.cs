using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client
{
    public partial class Client : Form
    {
        private readonly ClientNetwork _network;

        public Client()
        {
            InitializeComponent();
            _network = new();
            _network.TextReceived += Network_TextReceived;
            _network.ConnectionChanged += Network_ConnectionChange;
            _network.SystemMSG += Network_SystemMessage;
            _network.ErrorMSG += Network_ErrMessage;
            _network.FileReceived += Network_FileHandler;
            _network.UsersUpdated += Network_UsersUpdate;
            tbPassword.PasswordChar = '*';

        }

        private async void loginBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }
            rtbChat.Text = "";
            if (tbLogin.Text.Contains('|') || tbPassword.Text.Contains('|'))
            {
                MessageBox.Show("The | character cannot be used.");
                return;
            }
            await _network.Login(tbLogin.Text, tbPassword.Text);
            btnSend.Enabled = true;
            btnFileSend.Enabled = true;
        }

        private async void registerBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }
            if (tbLogin.Text.Contains('|') || tbPassword.Text.Contains('|'))
            {
                MessageBox.Show("The | character cannot be used.");
                return;
            }
            await _network.Register(tbLogin.Text, tbPassword.Text);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbIP.Text))
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

        private void Network_TextReceived(string sender, string recipients, string msg, DateTime time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Network_TextReceived(sender, recipients, msg, time)));
                return;
            }
            var status = (string.IsNullOrEmpty(recipients) || recipients == "ALL") ? "PUBLIC: " : "PRIVATE: ";
            rtbChat.AppendText($"{Environment.NewLine}{time.ToShortTimeString()} {status}{sender}: {msg}");
            rtbChat.ScrollToCaret();
        }

        private void Network_SystemMessage(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Network_SystemMessage(msg)));
                return;
            }
            rtbChat.AppendText($"{Environment.NewLine}{DateTime.Now:HH:mm:ss} SYSTEM: {msg}");
            rtbChat.ScrollToCaret();
        }

        private void Network_ErrMessage(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Network_ErrMessage(msg)));
                return;
            }
            rtbChat.AppendText($"{Environment.NewLine}{DateTime.Now:HH:mm:ss} ERROR: {msg}");
            rtbChat.ScrollToCaret();
            MessageBox.Show(msg);
        }

        private void Network_FileHandler(string sender, string file, byte[] bytes)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Network_FileHandler(sender, file, bytes)));
                return;
            }
            rtbChat.AppendText($"{Environment.NewLine}{DateTime.Now:HH:mm:ss} {sender} sent the file - {file}");
            rtbChat.ScrollToCaret();
            if (sender == _network.Name)
            {
                return;
            }
            var res = MessageBox.Show($"Received {file} from {sender}. Save it to your disk?", "File alert", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                saveFileDialog.FileName = file;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, bytes);
                    MessageBox.Show("File saved.");
                }
            }
        }

        private void Network_UsersUpdate(List<string> users)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Network_UsersUpdate(users)));
                return;
            }
            lbUsers.Items.Clear();
            lbUsers.Items.Add("ALL");
            foreach (var u in users)
            {
                if (u != _network.Name)
                {
                    lbUsers.Items.Add(u);
                }
            }

        }

        private void Network_ConnectionChange(bool check)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Network_ConnectionChange(check)));
                return;
            }
            if (check)
            {
                disconnectBtn.Enabled = true;
                loginBtn.Enabled = true;
                registerBtn.Enabled = true;
                connectBtn.Enabled = false;
                tbIP.Enabled = false;
                tbPort.Enabled = false;
                tbMsg.Enabled = true;
            }
            else
            {
                disconnectBtn.Enabled = false;
                loginBtn.Enabled = false;
                registerBtn.Enabled = false;
                connectBtn.Enabled = true;
                tbIP.Enabled = true;
                tbPort.Enabled = true;
                tbMsg.Enabled = false;
            }
        }


        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (_network.IsConnected == false)
            {
                return;
            }
            var recipient = GetSelectedRecipients();
            if (tbMsg.Text.Contains('|'))
            {
                MessageBox.Show("The | character cannot be used.");
                return;
            }
            string text = tbMsg.Text;
            await _network.SendTextAsync(recipient, text);
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
                if ((string)item == "ALL")
                {
                    return "ALL";
                }
                recipients.Add(item.ToString()!);
            }

            return string.Join(",", recipients);
        }

        private async void btnFileSend_Click(object sender, EventArgs e)
        {
            if (!_network.IsConnected)
            {
                MessageBox.Show("Not connected to the server.");
                return;
            }
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                string recipient = GetSelectedRecipients();
                await _network.SendFile(recipient, openFileDialog.FileName);
                rtbChat.AppendText($"{Environment.NewLine}{DateTime.Now:HH:mm:ss} You sent the file: {Path.GetFileName(openFileDialog.FileName)}");
                rtbChat.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _network.Disconnect();
        }

        private async void connectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                await _network.ConnectAsync(tbIP.Text, int.Parse(tbPort.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            _network.Disconnect();
            btnSend.Enabled = false;
            btnFileSend.Enabled = false;
        }

    }
}