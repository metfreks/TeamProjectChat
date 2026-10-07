namespace Client
{
    partial class Form1
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
            labelPassword = new Label();
            labelPort = new Label();
            labelIP = new Label();
            labelLogin = new Label();
            label5 = new Label();
            tbIp = new TextBox();
            tbLogin = new TextBox();
            tbPort = new TextBox();
            tbPassword = new TextBox();
            loginBtn = new Button();
            registerBtn = new Button();
            lbUsers = new ListBox();
            labelUsers = new Label();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            lblChat = new Label();
            lbChat = new ListBox();
            lblMessage = new Label();
            tbMsg = new TextBox();
            btnSend = new Button();
            btnFileSend = new Button();
            SuspendLayout();
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(33, 74);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(60, 15);
            labelPassword.TabIndex = 0;
            labelPassword.Text = "Password:";
            // 
            // labelPort
            // 
            labelPort.AutoSize = true;
            labelPort.Location = new Point(197, 18);
            labelPort.Name = "labelPort";
            labelPort.Size = new Size(32, 15);
            labelPort.TabIndex = 1;
            labelPort.Text = "Port:";
            // 
            // labelIP
            // 
            labelIP.AutoSize = true;
            labelIP.Location = new Point(33, 15);
            labelIP.Name = "labelIP";
            labelIP.Size = new Size(20, 15);
            labelIP.TabIndex = 2;
            labelIP.Text = "IP:";
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Location = new Point(33, 45);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(40, 15);
            labelLogin.TabIndex = 3;
            labelLogin.Text = "Login:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(77, 45);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 4;
            // 
            // tbIp
            // 
            tbIp.Location = new Point(59, 12);
            tbIp.Name = "tbIp";
            tbIp.Size = new Size(100, 23);
            tbIp.TabIndex = 5;
            tbIp.Text = "127.0.0.1";
            // 
            // tbLogin
            // 
            tbLogin.Location = new Point(77, 41);
            tbLogin.Name = "tbLogin";
            tbLogin.Size = new Size(100, 23);
            tbLogin.TabIndex = 6;
            // 
            // tbPort
            // 
            tbPort.Location = new Point(235, 15);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(100, 23);
            tbPort.TabIndex = 7;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(99, 71);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(100, 23);
            tbPassword.TabIndex = 9;
            // 
            // loginBtn
            // 
            loginBtn.Location = new Point(33, 112);
            loginBtn.Name = "loginBtn";
            loginBtn.Size = new Size(75, 23);
            loginBtn.TabIndex = 10;
            loginBtn.Text = "Login";
            loginBtn.UseVisualStyleBackColor = true;
            // 
            // registerBtn
            // 
            registerBtn.Location = new Point(124, 112);
            registerBtn.Name = "registerBtn";
            registerBtn.Size = new Size(75, 23);
            registerBtn.TabIndex = 11;
            registerBtn.Text = "Register";
            registerBtn.UseVisualStyleBackColor = true;
            // 
            // lbUsers
            // 
            lbUsers.FormattingEnabled = true;
            lbUsers.HorizontalScrollbar = true;
            lbUsers.Location = new Point(33, 172);
            lbUsers.Name = "lbUsers";
            lbUsers.SelectionMode = SelectionMode.MultiExtended;
            lbUsers.Size = new Size(120, 94);
            lbUsers.TabIndex = 12;
            // 
            // labelUsers
            // 
            labelUsers.AutoSize = true;
            labelUsers.Location = new Point(33, 154);
            labelUsers.Name = "labelUsers";
            labelUsers.Size = new Size(35, 15);
            labelUsers.TabIndex = 13;
            labelUsers.Text = "Users";
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
            // lblChat
            // 
            lblChat.AutoSize = true;
            lblChat.Location = new Point(191, 154);
            lblChat.Name = "lblChat";
            lblChat.Size = new Size(32, 15);
            lblChat.TabIndex = 14;
            lblChat.Text = "Chat";
            // 
            // lbChat
            // 
            lbChat.FormattingEnabled = true;
            lbChat.HorizontalScrollbar = true;
            lbChat.IntegralHeight = false;
            lbChat.Location = new Point(191, 172);
            lbChat.Name = "lbChat";
            lbChat.Size = new Size(120, 96);
            lbChat.TabIndex = 15;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(33, 300);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(56, 15);
            lblMessage.TabIndex = 16;
            lblMessage.Text = "Message:";
            // 
            // tbMsg
            // 
            tbMsg.Location = new Point(33, 318);
            tbMsg.Name = "tbMsg";
            tbMsg.Size = new Size(278, 23);
            tbMsg.TabIndex = 17;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(317, 317);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 18;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            // 
            // btnFileSend
            // 
            btnFileSend.Location = new Point(317, 356);
            btnFileSend.Name = "btnFileSend";
            btnFileSend.Size = new Size(75, 23);
            btnFileSend.TabIndex = 19;
            btnFileSend.Text = "Send File";
            btnFileSend.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnFileSend);
            Controls.Add(btnSend);
            Controls.Add(tbMsg);
            Controls.Add(lblMessage);
            Controls.Add(lbChat);
            Controls.Add(lblChat);
            Controls.Add(labelUsers);
            Controls.Add(lbUsers);
            Controls.Add(registerBtn);
            Controls.Add(loginBtn);
            Controls.Add(tbPassword);
            Controls.Add(tbPort);
            Controls.Add(tbLogin);
            Controls.Add(tbIp);
            Controls.Add(label5);
            Controls.Add(labelLogin);
            Controls.Add(labelIP);
            Controls.Add(labelPort);
            Controls.Add(labelPassword);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPassword;
        private Label labelPort;
        private Label labelIP;
        private Label labelLogin;
        private Label label5;
        private TextBox tbIp;
        private TextBox tbLogin;
        private TextBox tbPort;
        private TextBox tbPassword;
        private Button loginBtn;
        private Button registerBtn;
        private ListBox lbUsers;
        private Label labelUsers;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Label lblChat;
        private ListBox lbChat;
        private Label lblMessage;
        private TextBox tbMsg;
        private Button btnSend;
        private Button btnFileSend;
    }
}
