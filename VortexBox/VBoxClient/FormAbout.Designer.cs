namespace VBoxClient
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            label1 = new Label();
            label2 = new Label();
            labelNorealist = new Label();
            labelKM = new Label();
            label3 = new Label();
            labelClientLink = new Label();
            labelServerLink = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 267);
            label1.Name = "label1";
            label1.Size = new Size(88, 21);
            label1.TabIndex = 7;
            label1.Text = "Версия: 1.0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(12, 288);
            label2.Name = "label2";
            label2.Size = new Size(114, 21);
            label2.TabIndex = 8;
            label2.Text = "Разработчики:";
            // 
            // labelNorealist
            // 
            labelNorealist.AutoSize = true;
            labelNorealist.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelNorealist.Location = new Point(36, 309);
            labelNorealist.Name = "labelNorealist";
            labelNorealist.Size = new Size(147, 21);
            labelNorealist.TabIndex = 9;
            labelNorealist.Text = "* Клиент: norealist";
            labelNorealist.Click += labelNorealist_Click;
            // 
            // labelKM
            // 
            labelKM.AutoSize = true;
            labelKM.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelKM.Location = new Point(36, 330);
            labelKM.Name = "labelKM";
            labelKM.Size = new Size(168, 21);
            labelKM.TabIndex = 10;
            labelKM.Text = "* Сервер: KM-090105";
            labelKM.Click += labelKM_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(12, 363);
            label3.Name = "label3";
            label3.Size = new Size(64, 21);
            label3.TabIndex = 11;
            label3.Text = "Github: ";
            // 
            // labelClientLink
            // 
            labelClientLink.AutoSize = true;
            labelClientLink.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelClientLink.Location = new Point(36, 384);
            labelClientLink.Name = "labelClientLink";
            labelClientLink.Size = new Size(302, 21);
            labelClientLink.TabIndex = 12;
            labelClientLink.Text = "https://github.com/norealist/vortexbox";
            labelClientLink.Click += labelClientLink_Click;
            // 
            // labelServerLink
            // 
            labelServerLink.AutoSize = true;
            labelServerLink.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelServerLink.Location = new Point(36, 405);
            labelServerLink.Name = "labelServerLink";
            labelServerLink.Size = new Size(354, 21);
            labelServerLink.TabIndex = 13;
            labelServerLink.Text = "https://github.com/norealist/vortexbox-server";
            labelServerLink.Click += labelServerLink_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.vbox_logo_4;
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(400, 240);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 14;
            pictureBox2.TabStop = false;
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(425, 445);
            Controls.Add(pictureBox2);
            Controls.Add(labelServerLink);
            Controls.Add(labelClientLink);
            Controls.Add(label3);
            Controls.Add(labelKM);
            Controls.Add(labelNorealist);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormAbout";
            Text = "О программе";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label labelNorealist;
        private Label labelKM;
        private Label label3;
        private Label labelClientLink;
        private Label labelServerLink;
        private PictureBox pictureBox2;
    }
}