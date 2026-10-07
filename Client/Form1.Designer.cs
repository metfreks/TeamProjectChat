namespace Client
{
    partial class Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            btnFileSend = new Button();
            btnSend = new Button();
            tbMsg = new TextBox();
            rtbChat = new RichTextBox();
            lbUsers = new ListBox();
            connectBtn = new Button();
            disconnectBtn = new Button();
            tbPort = new TextBox();
            tbIP = new TextBox();
            label3 = new Label();
            label4 = new Label();
            loginBtn = new Button();
            registerBtn = new Button();
            label2 = new Label();
            tbPassword = new TextBox();
            label1 = new Label();
            tbLogin = new TextBox();
            SuspendLayout();
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            openFileDialog.Title = "Select a file";
            // 
            // saveFileDialog
            // 
            saveFileDialog.Title = "Save received file";
            // 
            // btnFileSend
            // 
            btnFileSend.Font = new Font("Segoe UI", 16F);
            btnFileSend.Location = new Point(700, 346);
            btnFileSend.Name = "btnFileSend";
            btnFileSend.Size = new Size(169, 72);
            btnFileSend.TabIndex = 33;
            btnFileSend.Text = "Send File";
            btnFileSend.UseVisualStyleBackColor = true;
            btnFileSend.Click += btnFileSend_Click;
            // 
            // btnSend
            // 
            btnSend.Font = new Font("Segoe UI", 16F);
            btnSend.Location = new Point(525, 346);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(169, 72);
            btnSend.TabIndex = 32;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // tbMsg
            // 
            tbMsg.Location = new Point(37, 346);
            tbMsg.Multiline = true;
            tbMsg.Name = "tbMsg";
            tbMsg.Size = new Size(471, 72);
            tbMsg.TabIndex = 31;
            // 
            // rtbChat
            // 
            rtbChat.Location = new Point(37, 156);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(471, 184);
            rtbChat.TabIndex = 30;
            rtbChat.Text = "";
            // 
            // lbUsers
            // 
            lbUsers.FormattingEnabled = true;
            lbUsers.Location = new Point(525, 156);
            lbUsers.Name = "lbUsers";
            lbUsers.Size = new Size(344, 184);
            lbUsers.TabIndex = 29;
            // 
            // connectBtn
            // 
            connectBtn.Font = new Font("Segoe UI", 16F);
            connectBtn.Location = new Point(525, 16);
            connectBtn.Name = "connectBtn";
            connectBtn.Size = new Size(169, 61);
            connectBtn.TabIndex = 28;
            connectBtn.Text = "Connect";
            connectBtn.UseVisualStyleBackColor = true;
            connectBtn.Click += connectBtn_Click;
            // 
            // disconnectBtn
            // 
            disconnectBtn.Font = new Font("Segoe UI", 16F);
            disconnectBtn.Location = new Point(700, 16);
            disconnectBtn.Name = "disconnectBtn";
            disconnectBtn.Size = new Size(169, 61);
            disconnectBtn.TabIndex = 27;
            disconnectBtn.Text = "Disconnect";
            disconnectBtn.UseVisualStyleBackColor = true;
            disconnectBtn.Click += disconnectBtn_Click;
            // 
            // tbPort
            // 
            tbPort.Location = new Point(116, 50);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(392, 27);
            tbPort.TabIndex = 26;
            tbPort.Text = "3456";
            // 
            // tbIP
            // 
            tbIP.Location = new Point(116, 17);
            tbIP.Name = "tbIP";
            tbIP.Size = new Size(392, 27);
            tbIP.TabIndex = 25;
            tbIP.Text = "127.0.0.1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 52);
            label3.Name = "label3";
            label3.Size = new Size(38, 20);
            label3.TabIndex = 24;
            label3.Text = "Port:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(37, 17);
            label4.Name = "label4";
            label4.Size = new Size(24, 20);
            label4.TabIndex = 23;
            label4.Text = "IP:";
            // 
            // loginBtn
            // 
            loginBtn.Font = new Font("Segoe UI", 16F);
            loginBtn.Location = new Point(525, 89);
            loginBtn.Name = "loginBtn";
            loginBtn.Size = new Size(169, 61);
            loginBtn.TabIndex = 22;
            loginBtn.Text = "Login";
            loginBtn.UseVisualStyleBackColor = true;
            loginBtn.Click += loginBtn_Click;
            // 
            // registerBtn
            // 
            registerBtn.Font = new Font("Segoe UI", 16F);
            registerBtn.Location = new Point(700, 89);
            registerBtn.Name = "registerBtn";
            registerBtn.Size = new Size(169, 61);
            registerBtn.TabIndex = 21;
            registerBtn.Text = "Register";
            registerBtn.UseVisualStyleBackColor = true;
            registerBtn.Click += registerBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 126);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 20;
            label2.Text = "Password:";
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(116, 123);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(392, 27);
            tbPassword.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 93);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 18;
            label1.Text = "Login:";
            // 
            // tbLogin
            // 
            tbLogin.Location = new Point(116, 90);
            tbLogin.Name = "tbLogin";
            tbLogin.Size = new Size(392, 27);
            tbLogin.TabIndex = 17;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 437);
            Controls.Add(btnFileSend);
            Controls.Add(btnSend);
            Controls.Add(tbMsg);
            Controls.Add(rtbChat);
            Controls.Add(lbUsers);
            Controls.Add(connectBtn);
            Controls.Add(disconnectBtn);
            Controls.Add(tbPort);
            Controls.Add(tbIP);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(loginBtn);
            Controls.Add(registerBtn);
            Controls.Add(label2);
            Controls.Add(tbPassword);
            Controls.Add(label1);
            Controls.Add(tbLogin);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Client";
            Text = "Client";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Button btnFileSend;
        private Button btnSend;
        private TextBox tbMsg;
        private RichTextBox rtbChat;
        private ListBox lbUsers;
        private Button connectBtn;
        private Button disconnectBtn;
        private TextBox tbPort;
        private TextBox tbIP;
        private Label label3;
        private Label label4;
        private Button loginBtn;
        private Button registerBtn;
        private Label label2;
        private TextBox tbPassword;
        private Label label1;
        private TextBox tbLogin;
    }
}
