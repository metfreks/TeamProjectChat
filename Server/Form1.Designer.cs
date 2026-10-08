namespace Server
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
            startBtn = new Button();
            banBtn = new Button();
            stopBtn = new Button();
            label1 = new Label();
            tbPort = new TextBox();
            lbUsers = new ListBox();
            approveBtn = new Button();
            tbLog = new TextBox();
            label2 = new Label();
            deleteBtn = new Button();
            tbBan = new TextBox();
            refreshBtn = new Button();
            SuspendLayout();
            // 
            // startBtn
            // 
            startBtn.Font = new Font("Segoe UI", 16F);
            startBtn.Location = new Point(252, 12);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(244, 58);
            startBtn.TabIndex = 0;
            startBtn.Text = "Start";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += startBtn_Click;
            // 
            // banBtn
            // 
            banBtn.Enabled = false;
            banBtn.Font = new Font("Segoe UI", 12F);
            banBtn.Location = new Point(253, 330);
            banBtn.Name = "banBtn";
            banBtn.Size = new Size(113, 52);
            banBtn.TabIndex = 1;
            banBtn.Text = "Ban";
            banBtn.UseVisualStyleBackColor = true;
            banBtn.Click += banBtn_Click;
            // 
            // stopBtn
            // 
            stopBtn.Enabled = false;
            stopBtn.Font = new Font("Segoe UI", 16F);
            stopBtn.Location = new Point(502, 12);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new Size(244, 58);
            stopBtn.TabIndex = 2;
            stopBtn.Text = "Stop";
            stopBtn.UseVisualStyleBackColor = true;
            stopBtn.Click += stopBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(50, 25);
            label1.Name = "label1";
            label1.Size = new Size(52, 28);
            label1.TabIndex = 3;
            label1.Text = "Port:";
            // 
            // tbPort
            // 
            tbPort.Location = new Point(108, 27);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(125, 27);
            tbPort.TabIndex = 4;
            tbPort.Text = "3456";
            // 
            // lbUsers
            // 
            lbUsers.FormattingEnabled = true;
            lbUsers.Location = new Point(50, 87);
            lbUsers.Name = "lbUsers";
            lbUsers.Size = new Size(316, 204);
            lbUsers.TabIndex = 5;
            // 
            // approveBtn
            // 
            approveBtn.Enabled = false;
            approveBtn.Font = new Font("Segoe UI", 12F);
            approveBtn.Location = new Point(49, 330);
            approveBtn.Name = "approveBtn";
            approveBtn.Size = new Size(96, 52);
            approveBtn.TabIndex = 6;
            approveBtn.Text = "Approve";
            approveBtn.UseVisualStyleBackColor = true;
            approveBtn.Click += approveBtn_Click;
            // 
            // tbLog
            // 
            tbLog.Location = new Point(372, 76);
            tbLog.Multiline = true;
            tbLog.Name = "tbLog";
            tbLog.ReadOnly = true;
            tbLog.ScrollBars = ScrollBars.Both;
            tbLog.Size = new Size(374, 215);
            tbLog.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F);
            label2.Location = new Point(423, 318);
            label2.Name = "label2";
            label2.Size = new Size(270, 37);
            label2.TabIndex = 10;
            label2.Text = "Server status: Inactive";
            // 
            // deleteBtn
            // 
            deleteBtn.Enabled = false;
            deleteBtn.Font = new Font("Segoe UI", 12F);
            deleteBtn.Location = new Point(151, 330);
            deleteBtn.Name = "deleteBtn";
            deleteBtn.Size = new Size(96, 52);
            deleteBtn.TabIndex = 11;
            deleteBtn.Text = "Delete";
            deleteBtn.UseVisualStyleBackColor = true;
            deleteBtn.Click += deleteBtn_Click;
            // 
            // tbBan
            // 
            tbBan.Location = new Point(50, 297);
            tbBan.Name = "tbBan";
            tbBan.Size = new Size(316, 27);
            tbBan.TabIndex = 12;
            // 
            // refreshBtn
            // 
            refreshBtn.Enabled = false;
            refreshBtn.Location = new Point(21, 87);
            refreshBtn.Name = "refreshBtn";
            refreshBtn.Size = new Size(23, 27);
            refreshBtn.TabIndex = 13;
            refreshBtn.Text = "R";
            refreshBtn.UseVisualStyleBackColor = true;
            refreshBtn.Click += refreshBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 392);
            Controls.Add(refreshBtn);
            Controls.Add(tbBan);
            Controls.Add(deleteBtn);
            Controls.Add(label2);
            Controls.Add(tbLog);
            Controls.Add(approveBtn);
            Controls.Add(lbUsers);
            Controls.Add(tbPort);
            Controls.Add(label1);
            Controls.Add(stopBtn);
            Controls.Add(banBtn);
            Controls.Add(startBtn);
            Name = "Form1";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startBtn;
        private Button banBtn;
        private Button stopBtn;
        private Label label1;
        private TextBox tbPort;
        private ListBox lbUsers;
        private Button approveBtn;
        private TextBox tbLog;
        private Label label2;
        private Button deleteBtn;
        private TextBox tbBan;
        private Button refreshBtn;
    }
}
