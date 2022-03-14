namespace BoredWithFriends.Forms
{
	partial class FrmInitialLauncher
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
			this.lblTitle = new System.Windows.Forms.Label();
			this.cbxPlayOnline = new System.Windows.Forms.CheckBox();
			this.lblServerIP = new System.Windows.Forms.Label();
			this.lblPort = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblPlayOffline = new System.Windows.Forms.Label();
			this.btnCreateAccount = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.txtServerIP = new System.Windows.Forms.TextBox();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnGuest = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Sigmar One", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTitle.Location = new System.Drawing.Point(18, 1);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(395, 53);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Bored with Friends";
			// 
			// cbxPlayOnline
			// 
			this.cbxPlayOnline.AutoSize = true;
			this.cbxPlayOnline.Font = new System.Drawing.Font("Sigmar One", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cbxPlayOnline.Location = new System.Drawing.Point(38, 62);
			this.cbxPlayOnline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cbxPlayOnline.Name = "cbxPlayOnline";
			this.cbxPlayOnline.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.cbxPlayOnline.Size = new System.Drawing.Size(131, 28);
			this.cbxPlayOnline.TabIndex = 3;
			this.cbxPlayOnline.Text = "Play Online";
			this.cbxPlayOnline.UseVisualStyleBackColor = true;
			this.cbxPlayOnline.CheckedChanged += new System.EventHandler(this.cbxPlayOnline_CheckedChanged);
			// 
			// lblServerIP
			// 
			this.lblServerIP.AutoSize = true;
			this.lblServerIP.Font = new System.Drawing.Font("Sigmar One", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblServerIP.Location = new System.Drawing.Point(38, 98);
			this.lblServerIP.Name = "lblServerIP";
			this.lblServerIP.Size = new System.Drawing.Size(94, 24);
			this.lblServerIP.TabIndex = 4;
			this.lblServerIP.Text = "Server IP";
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Font = new System.Drawing.Font("Sigmar One", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblPort.Location = new System.Drawing.Point(298, 98);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(53, 24);
			this.lblPort.TabIndex = 5;
			this.lblPort.Text = "Port";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Font = new System.Drawing.Font("Sigmar One", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblUsername.Location = new System.Drawing.Point(37, 142);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(94, 24);
			this.lblUsername.TabIndex = 6;
			this.lblUsername.Text = "Username";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Font = new System.Drawing.Font("Sigmar One", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblPassword.Location = new System.Drawing.Point(38, 166);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(98, 24);
			this.lblPassword.TabIndex = 7;
			this.lblPassword.Text = "Password";
			// 
			// lblPlayOffline
			// 
			this.lblPlayOffline.AutoSize = true;
			this.lblPlayOffline.Font = new System.Drawing.Font("Sigmar One", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblPlayOffline.Location = new System.Drawing.Point(91, 235);
			this.lblPlayOffline.Name = "lblPlayOffline";
			this.lblPlayOffline.Size = new System.Drawing.Size(231, 48);
			this.lblPlayOffline.TabIndex = 8;
			this.lblPlayOffline.Text = "OR\r\nContinue Offline as Guest";
			this.lblPlayOffline.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// btnCreateAccount
			// 
			this.btnCreateAccount.Location = new System.Drawing.Point(38, 199);
			this.btnCreateAccount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnCreateAccount.Name = "btnCreateAccount";
			this.btnCreateAccount.Size = new System.Drawing.Size(121, 22);
			this.btnCreateAccount.TabIndex = 9;
			this.btnCreateAccount.Text = "Create Account";
			this.btnCreateAccount.UseVisualStyleBackColor = true;
			this.btnCreateAccount.Click += new System.EventHandler(this.btnCreateAccount_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(232, 199);
			this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(82, 22);
			this.btnLogin.TabIndex = 10;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// txtServerIP
			// 
			this.txtServerIP.Enabled = false;
			this.txtServerIP.Location = new System.Drawing.Point(138, 97);
			this.txtServerIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtServerIP.Name = "txtServerIP";
			this.txtServerIP.Size = new System.Drawing.Size(139, 23);
			this.txtServerIP.TabIndex = 11;
			// 
			// txtPort
			// 
			this.txtPort.Enabled = false;
			this.txtPort.Location = new System.Drawing.Point(359, 98);
			this.txtPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(83, 23);
			this.txtPort.TabIndex = 12;
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(138, 142);
			this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(176, 23);
			this.txtUsername.TabIndex = 13;
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(138, 167);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(176, 23);
			this.txtPassword.TabIndex = 14;
			// 
			// btnGuest
			// 
			this.btnGuest.Location = new System.Drawing.Point(350, 255);
			this.btnGuest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnGuest.Name = "btnGuest";
			this.btnGuest.Size = new System.Drawing.Size(91, 22);
			this.btnGuest.TabIndex = 15;
			this.btnGuest.Text = "Play as Guest";
			this.btnGuest.UseVisualStyleBackColor = true;
			this.btnGuest.Click += new System.EventHandler(this.btnGuest_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(18, 255);
			this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(54, 22);
			this.btnExit.TabIndex = 16;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// FrmInitialLauncher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(465, 301);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnGuest);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.txtPort);
			this.Controls.Add(this.txtServerIP);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.btnCreateAccount);
			this.Controls.Add(this.lblPlayOffline);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.lblPort);
			this.Controls.Add(this.lblServerIP);
			this.Controls.Add(this.cbxPlayOnline);
			this.Controls.Add(this.lblTitle);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "FrmInitialLauncher";
			this.Text = "Launcher";
			this.Click += new System.EventHandler(this.FrmInitialLauncher_Click);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion
        private Label lblTitle;
		private CheckBox cbxPlayOnline;
		private Label lblServerIP;
		private Label lblPort;
		private Label lblUsername;
		private Label lblPassword;
		private Label lblPlayOffline;
		private Button btnCreateAccount;
		private Button btnLogin;
		private TextBox txtServerIP;
		private TextBox txtPort;
		private TextBox txtUsername;
		private TextBox txtPassword;
		private Button btnGuest;
		private Button btnExit;
	}
}
