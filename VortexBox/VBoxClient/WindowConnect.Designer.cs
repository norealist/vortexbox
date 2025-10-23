namespace VBoxClient
{
    partial class WindowConnect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowConnect));
            buttonConnect = new Button();
            textBox_addr = new TextBox();
            labelAddr = new Label();
            VBoxText = new Label();
            VBoxClientText = new Label();
            textBox_login = new TextBox();
            textBox_password = new TextBox();
            labelLogin = new Label();
            labelPassword = new Label();
            buttonReg = new Button();
            notifyIcon = new NotifyIcon(components);
            SuspendLayout();
            // 
            // buttonConnect
            // 
            buttonConnect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonConnect.Location = new Point(22, 261);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(216, 57);
            buttonConnect.TabIndex = 0;
            buttonConnect.Text = "Войти";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // textBox_addr
            // 
            textBox_addr.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_addr.Location = new Point(145, 133);
            textBox_addr.Name = "textBox_addr";
            textBox_addr.Size = new Size(285, 29);
            textBox_addr.TabIndex = 1;
            // 
            // labelAddr
            // 
            labelAddr.AutoSize = true;
            labelAddr.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelAddr.Location = new Point(22, 133);
            labelAddr.Name = "labelAddr";
            labelAddr.Size = new Size(117, 21);
            labelAddr.TabIndex = 2;
            labelAddr.Text = "Адрес сервера:";
            // 
            // VBoxText
            // 
            VBoxText.AutoSize = true;
            VBoxText.Font = new Font("Agency FB", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            VBoxText.Location = new Point(97, 25);
            VBoxText.Name = "VBoxText";
            VBoxText.Size = new Size(257, 77);
            VBoxText.TabIndex = 3;
            VBoxText.Text = "VortexBox";
            // 
            // VBoxClientText
            // 
            VBoxClientText.AutoSize = true;
            VBoxClientText.Font = new Font("OCR A Extended", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            VBoxClientText.Location = new Point(279, 93);
            VBoxClientText.Name = "VBoxClientText";
            VBoxClientText.Size = new Size(75, 20);
            VBoxClientText.TabIndex = 4;
            VBoxClientText.Text = "Client";
            // 
            // textBox_login
            // 
            textBox_login.Font = new Font("Segoe UI", 12F);
            textBox_login.Location = new Point(145, 168);
            textBox_login.Name = "textBox_login";
            textBox_login.Size = new Size(285, 29);
            textBox_login.TabIndex = 5;
            // 
            // textBox_password
            // 
            textBox_password.Font = new Font("Segoe UI", 12F);
            textBox_password.Location = new Point(145, 203);
            textBox_password.Name = "textBox_password";
            textBox_password.PasswordChar = 'x';
            textBox_password.Size = new Size(285, 29);
            textBox_password.TabIndex = 6;
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Font = new Font("Segoe UI", 12F);
            labelLogin.Location = new Point(82, 171);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(57, 21);
            labelLogin.TabIndex = 7;
            labelLogin.Text = "Логин:";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Segoe UI", 12F);
            labelPassword.Location = new Point(73, 206);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(66, 21);
            labelPassword.TabIndex = 8;
            labelPassword.Text = "Пароль:";
            // 
            // buttonReg
            // 
            buttonReg.Font = new Font("Segoe UI", 12F);
            buttonReg.Location = new Point(244, 261);
            buttonReg.Name = "buttonReg";
            buttonReg.Size = new Size(186, 57);
            buttonReg.TabIndex = 9;
            buttonReg.Text = "Зарегистрироваться";
            buttonReg.UseVisualStyleBackColor = true;
            buttonReg.Click += buttonReg_Click;
            // 
            // notifyIcon
            // 
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "VortexBox";
            notifyIcon.Visible = true;
            // 
            // WindowConnect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 336);
            Controls.Add(buttonReg);
            Controls.Add(labelPassword);
            Controls.Add(labelLogin);
            Controls.Add(textBox_password);
            Controls.Add(textBox_login);
            Controls.Add(VBoxClientText);
            Controls.Add(VBoxText);
            Controls.Add(labelAddr);
            Controls.Add(textBox_addr);
            Controls.Add(buttonConnect);
            Name = "WindowConnect";
            Text = "VortexBox : Connect";
            Load += WindowConnect_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonConnect;
        private TextBox textBox_addr;
        private Label labelAddr;
        private Label VBoxText;
        private Label VBoxClientText;
        private TextBox textBox_login;
        private TextBox textBox_password;
        private Label labelLogin;
        private Label labelPassword;
        private Button buttonReg;
        private NotifyIcon notifyIcon;
    }
}