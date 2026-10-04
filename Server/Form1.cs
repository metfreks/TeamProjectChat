namespace Server
{
    public partial class Form1 : Form
    {
        private ServerManager _server;
        public Form1()
        {
            InitializeComponent();
            _server = new();
        }

        private async void startBtn_Click(object sender, EventArgs e)
        {
            await _server.StartServer();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {

        }

        private void approveBtn_Click(object sender, EventArgs e)
        {

        }

        private void banBtn_Click(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
