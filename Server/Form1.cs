namespace Server
{
    public partial class Form1 : Form
    {
        private ServerManager _server;
        public Form1()
        {
            InitializeComponent();
            _server = new();
            _server.UsersUpdated += ServerUpdated;
            _server.StatusChanged += ServerChanged;
            _server.Log += Log;
        }

        private async void startBtn_Click(object sender, EventArgs e)
        {
            int port = int.Parse(tbPort.Text);
            startBtn.Enabled = false;
            stopBtn.Enabled = true;
            approveBtn.Enabled = true;
            deleteBtn.Enabled = true;
            banBtn.Enabled = true;
            refreshBtn.Enabled = true;
            await _server.StartServer(port);

        }

        private void ServerUpdated(List<User> users)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ServerUpdated(users)));
                return;
            }
            lbUsers.DataSource = null;
            lbUsers.DataSource = users;
        }

        private void ServerChanged(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ServerChanged(status)));
                return;
            }
            label2.Text = status;
        }

        private void Log(string log)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Log(log)));
                return;
            }

            tbLog.AppendText($"{Environment.NewLine}{DateTime.Now:HH:mm:ss} {log}");
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            _server.StopServer();
            startBtn.Enabled = true;
            stopBtn.Enabled = false;
            approveBtn.Enabled = false;
            deleteBtn.Enabled = false;
            banBtn.Enabled = false;
            refreshBtn.Enabled = false;
        }

        private async void approveBtn_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem is User selectedUser)
            {
                await _server.ApproveUser(selectedUser.Id);
            }
            else
            {
                MessageBox.Show("Pick a user from the list.");
            }
        }

        private async void banBtn_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem is User selectedUser)
            {
                if (tbBan.Text != "")
                {
                    int banTime = int.Parse(tbBan.Text);
                    await _server.BanUser(selectedUser.Id, banTime);
                }
                else
                {
                    int banTime = 60;
                    await _server.BanUser(selectedUser.Id, banTime);
                }
            }
            else
            {
                MessageBox.Show("Pick a user from the list.");
            }
        }

        private async void deleteBtn_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem is User selectedUser)
            {
                await _server.DeleteUser(selectedUser.Id);
            }
            else
            {
                MessageBox.Show("Pick a user from the list.");
            }
        }

        private async void refreshBtn_Click(object sender, EventArgs e)
        {
            await _server.RefreshUsersList();
        }
    }
}
