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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowConnect));
            buttonConnect = new Button();
            textBox_addr = new TextBox();
            labelAddr = new Label();
            textBox_login = new TextBox();
            textBox_password = new TextBox();
            labelLogin = new Label();
            labelPassword = new Label();
            buttonReg = new Button();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // buttonConnect
            // 
            buttonConnect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonConnect.Location = new Point(21, 400);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(216, 57);
            buttonConnect.TabIndex = 0;
            buttonConnect.Text = "Войти";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // textBox_addr
            // 
            textBox_addr.BorderStyle = BorderStyle.FixedSingle;
            textBox_addr.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_addr.Location = new Point(144, 272);
            textBox_addr.Name = "textBox_addr";
            textBox_addr.Size = new Size(285, 29);
            textBox_addr.TabIndex = 1;
            // 
            // labelAddr
            // 
            labelAddr.AutoSize = true;
            labelAddr.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelAddr.Location = new Point(21, 272);
            labelAddr.Name = "labelAddr";
            labelAddr.Size = new Size(117, 21);
            labelAddr.TabIndex = 2;
            labelAddr.Text = "Адрес сервера:";
            // 
            // textBox_login
            // 
            textBox_login.BorderStyle = BorderStyle.FixedSingle;
            textBox_login.Font = new Font("Segoe UI", 12F);
            textBox_login.Location = new Point(144, 307);
            textBox_login.Name = "textBox_login";
            textBox_login.Size = new Size(285, 29);
            textBox_login.TabIndex = 5;
            // 
            // textBox_password
            // 
            textBox_password.BorderStyle = BorderStyle.FixedSingle;
            textBox_password.Font = new Font("Segoe UI", 12F);
            textBox_password.Location = new Point(144, 342);
            textBox_password.Name = "textBox_password";
            textBox_password.PasswordChar = 'x';
            textBox_password.Size = new Size(285, 29);
            textBox_password.TabIndex = 6;
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Font = new Font("Segoe UI", 12F);
            labelLogin.Location = new Point(81, 310);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(57, 21);
            labelLogin.TabIndex = 7;
            labelLogin.Text = "Логин:";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Segoe UI", 12F);
            labelPassword.Location = new Point(72, 345);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(66, 21);
            labelPassword.TabIndex = 8;
            labelPassword.Text = "Пароль:";
            // 
            // buttonReg
            // 
            buttonReg.Font = new Font("Segoe UI", 12F);
            buttonReg.Location = new Point(243, 400);
            buttonReg.Name = "buttonReg";
            buttonReg.Size = new Size(186, 57);
            buttonReg.TabIndex = 9;
            buttonReg.Text = "Зарегистрироваться";
            buttonReg.UseVisualStyleBackColor = true;
            buttonReg.Click += buttonReg_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.vbox_logo_4;
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(445, 240);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // WindowConnect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(469, 479);
            Controls.Add(pictureBox2);
            Controls.Add(buttonReg);
            Controls.Add(labelPassword);
            Controls.Add(labelLogin);
            Controls.Add(textBox_password);
            Controls.Add(textBox_login);
            Controls.Add(labelAddr);
            Controls.Add(textBox_addr);
            Controls.Add(buttonConnect);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "WindowConnect";
            Text = "VortexBox : Connect";
            Load += WindowConnect_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonConnect;
        private TextBox textBox_addr;
        private Label labelAddr;
        private TextBox textBox_login;
        private TextBox textBox_password;
        private Label labelLogin;
        private Label labelPassword;
        private Button buttonReg;
        private PictureBox pictureBox2;
    }
}