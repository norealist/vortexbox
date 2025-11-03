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
            VBoxClientText = new Label();
            VBoxText = new Label();
            label1 = new Label();
            label2 = new Label();
            labelNorealist = new Label();
            labelKM = new Label();
            label3 = new Label();
            labelClientLink = new Label();
            labelServerLink = new Label();
            SuspendLayout();
            // 
            // VBoxClientText
            // 
            VBoxClientText.AutoSize = true;
            VBoxClientText.Font = new Font("OCR A Extended", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            VBoxClientText.Location = new Point(274, 94);
            VBoxClientText.Name = "VBoxClientText";
            VBoxClientText.Size = new Size(75, 20);
            VBoxClientText.TabIndex = 6;
            VBoxClientText.Text = "Client";
            // 
            // VBoxText
            // 
            VBoxText.AutoSize = true;
            VBoxText.Font = new Font("Agency FB", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            VBoxText.Location = new Point(92, 26);
            VBoxText.Name = "VBoxText";
            VBoxText.Size = new Size(257, 77);
            VBoxText.TabIndex = 5;
            VBoxText.Text = "VortexBox";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 140);
            label1.Name = "label1";
            label1.Size = new Size(88, 21);
            label1.TabIndex = 7;
            label1.Text = "Версия: 1.0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(12, 161);
            label2.Name = "label2";
            label2.Size = new Size(114, 21);
            label2.TabIndex = 8;
            label2.Text = "Разработчики:";
            // 
            // labelNorealist
            // 
            labelNorealist.AutoSize = true;
            labelNorealist.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelNorealist.Location = new Point(36, 182);
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
            labelKM.Location = new Point(36, 203);
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
            label3.Location = new Point(12, 236);
            label3.Name = "label3";
            label3.Size = new Size(64, 21);
            label3.TabIndex = 11;
            label3.Text = "Github: ";
            // 
            // labelClientLink
            // 
            labelClientLink.AutoSize = true;
            labelClientLink.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelClientLink.Location = new Point(36, 257);
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
            labelServerLink.Location = new Point(36, 278);
            labelServerLink.Name = "labelServerLink";
            labelServerLink.Size = new Size(354, 21);
            labelServerLink.TabIndex = 13;
            labelServerLink.Text = "https://github.com/norealist/vortexbox-server";
            labelServerLink.Click += labelServerLink_Click;
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 322);
            Controls.Add(labelServerLink);
            Controls.Add(labelClientLink);
            Controls.Add(label3);
            Controls.Add(labelKM);
            Controls.Add(labelNorealist);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(VBoxClientText);
            Controls.Add(VBoxText);
            Name = "FormAbout";
            Text = "О программе";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label VBoxClientText;
        private Label VBoxText;
        private Label label1;
        private Label label2;
        private Label labelNorealist;
        private Label labelKM;
        private Label label3;
        private Label labelClientLink;
        private Label labelServerLink;
    }
}