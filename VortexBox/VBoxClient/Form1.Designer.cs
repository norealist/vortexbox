namespace VBoxClient
{
    partial class MainWindow
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
            listFiles = new ListBox();
            buttonDownload = new Button();
            buttonUpload = new Button();
            fileInfo = new TextBox();
            encryptFileOrNot = new CheckBox();
            buttonDelete = new Button();
            SuspendLayout();
            // 
            // listFiles
            // 
            listFiles.BackColor = SystemColors.InactiveBorder;
            listFiles.BorderStyle = BorderStyle.FixedSingle;
            listFiles.Font = new Font("YouTube Sans Light 48pt", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listFiles.FormattingEnabled = true;
            listFiles.Items.AddRange(new object[] { "...", "(DIR) Work", "forest.png", "video.mp4", "prog.exe", "secret.zip.enc" });
            listFiles.Location = new Point(12, 12);
            listFiles.Name = "listFiles";
            listFiles.Size = new Size(400, 386);
            listFiles.TabIndex = 0;
            // 
            // buttonDownload
            // 
            buttonDownload.Location = new Point(431, 269);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(262, 39);
            buttonDownload.TabIndex = 1;
            buttonDownload.Text = "Скачать";
            buttonDownload.UseVisualStyleBackColor = true;
            // 
            // buttonUpload
            // 
            buttonUpload.Location = new Point(431, 314);
            buttonUpload.Name = "buttonUpload";
            buttonUpload.Size = new Size(262, 39);
            buttonUpload.TabIndex = 2;
            buttonUpload.Text = "Загрузить";
            buttonUpload.UseVisualStyleBackColor = true;
            // 
            // fileInfo
            // 
            fileInfo.BorderStyle = BorderStyle.FixedSingle;
            fileInfo.Font = new Font("Cascadia Mono SemiLight", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            fileInfo.Location = new Point(431, 12);
            fileInfo.Multiline = true;
            fileInfo.Name = "fileInfo";
            fileInfo.ReadOnly = true;
            fileInfo.Size = new Size(262, 196);
            fileInfo.TabIndex = 3;
            fileInfo.Text = "### forest.png\r\n\r\nСоздан: 12.02.2029 13:51:10\r\nРазмер: 5 кб";
            // 
            // encryptFileOrNot
            // 
            encryptFileOrNot.AutoSize = true;
            encryptFileOrNot.Location = new Point(431, 227);
            encryptFileOrNot.Name = "encryptFileOrNot";
            encryptFileOrNot.Size = new Size(182, 19);
            encryptFileOrNot.TabIndex = 4;
            encryptFileOrNot.Text = "Шифровать перед загрузкой";
            encryptFileOrNot.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(431, 359);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(262, 39);
            buttonDelete.TabIndex = 5;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(712, 414);
            Controls.Add(buttonDelete);
            Controls.Add(encryptFileOrNot);
            Controls.Add(fileInfo);
            Controls.Add(buttonUpload);
            Controls.Add(buttonDownload);
            Controls.Add(listFiles);
            Name = "MainWindow";
            Text = "VortexBox";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listFiles;
        private Button buttonDownload;
        private Button buttonUpload;
        private TextBox fileInfo;
        private CheckBox encryptFileOrNot;
        private Button buttonDelete;
    }
}
