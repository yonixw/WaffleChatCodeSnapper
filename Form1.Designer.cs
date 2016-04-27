namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.ebMain = new System.Windows.Forms.WebBrowser();
            this.lstChat = new System.Windows.Forms.ListBox();
            this.tmrChat = new System.Windows.Forms.Timer(this.components);
            this.txtAuth = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ebMain
            // 
            this.ebMain.Location = new System.Drawing.Point(12, 52);
            this.ebMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.ebMain.Name = "ebMain";
            this.ebMain.Size = new System.Drawing.Size(791, 616);
            this.ebMain.TabIndex = 0;
            this.ebMain.Url = new System.Uri("https://www.greybox.com/en/account-settings/redeem-code/", System.UriKind.Absolute);
            // 
            // lstChat
            // 
            this.lstChat.FormattingEnabled = true;
            this.lstChat.Location = new System.Drawing.Point(810, 52);
            this.lstChat.Name = "lstChat";
            this.lstChat.Size = new System.Drawing.Size(297, 615);
            this.lstChat.TabIndex = 1;
            // 
            // tmrChat
            // 
            this.tmrChat.Enabled = true;
            this.tmrChat.Tick += new System.EventHandler(this.tmrChat_Tick);
            // 
            // txtAuth
            // 
            this.txtAuth.Location = new System.Drawing.Point(12, 19);
            this.txtAuth.Name = "txtAuth";
            this.txtAuth.Size = new System.Drawing.Size(791, 20);
            this.txtAuth.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(823, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start Listening after ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 680);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtAuth);
            this.Controls.Add(this.lstChat);
            this.Controls.Add(this.ebMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser ebMain;
        private System.Windows.Forms.ListBox lstChat;
        private System.Windows.Forms.Timer tmrChat;
        private System.Windows.Forms.TextBox txtAuth;
        private System.Windows.Forms.Button button1;
    }
}

