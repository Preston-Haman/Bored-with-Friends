namespace BoredWithFriends.Forms
{
	partial class FrmAccountCreation
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
            this.btnBackToLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateAccount = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWarningInvalidName = new System.Windows.Forms.Label();
            this.lblWarningInvalidPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBackToLogin
            // 
            this.btnBackToLogin.BackColor = System.Drawing.SystemColors.Control;
            this.btnBackToLogin.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnBackToLogin.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBackToLogin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBackToLogin.Location = new System.Drawing.Point(20, 267);
            this.btnBackToLogin.Name = "btnBackToLogin";
            this.btnBackToLogin.Size = new System.Drawing.Size(75, 37);
            this.btnBackToLogin.TabIndex = 5;
            this.btnBackToLogin.Text = "Back";
            this.btnBackToLogin.UseVisualStyleBackColor = false;
            this.btnBackToLogin.Click += new System.EventHandler(this.btnBackToLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(163, 131);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(242, 27);
            this.txtPassword.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(20, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 33);
            this.label3.TabIndex = 13;
            this.label3.Text = "Password";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(15, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 33);
            this.label2.TabIndex = 12;
            this.label2.Text = "User Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCreateAccount
            // 
            this.btnCreateAccount.BackColor = System.Drawing.SystemColors.Control;
            this.btnCreateAccount.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCreateAccount.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCreateAccount.Location = new System.Drawing.Point(302, 238);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(130, 66);
            this.btnCreateAccount.TabIndex = 4;
            this.btnCreateAccount.Text = "Create New User";
            this.btnCreateAccount.UseVisualStyleBackColor = false;
            this.btnCreateAccount.Click += new System.EventHandler(this.btnCreateAccount_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(163, 76);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(242, 27);
            this.txtUserName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sigmar One", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(45, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(427, 56);
            this.label1.TabIndex = 9;
            this.label1.Text = "Please enter a user name and password\r\nthen click create new user\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(163, 189);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(242, 27);
            this.txtConfirmPassword.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(20, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 66);
            this.label4.TabIndex = 17;
            this.label4.Text = "Confirm \r\nPassword";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblWarningInvalidName
            // 
            this.lblWarningInvalidName.AutoSize = true;
            this.lblWarningInvalidName.ForeColor = System.Drawing.Color.Red;
            this.lblWarningInvalidName.Location = new System.Drawing.Point(163, 106);
            this.lblWarningInvalidName.Name = "lblWarningInvalidName";
            this.lblWarningInvalidName.Size = new System.Drawing.Size(0, 20);
            this.lblWarningInvalidName.TabIndex = 18;
            // 
            // lblWarningInvalidPassword
            // 
            this.lblWarningInvalidPassword.AutoSize = true;
            this.lblWarningInvalidPassword.ForeColor = System.Drawing.Color.Red;
            this.lblWarningInvalidPassword.Location = new System.Drawing.Point(163, 161);
            this.lblWarningInvalidPassword.Name = "lblWarningInvalidPassword";
            this.lblWarningInvalidPassword.Size = new System.Drawing.Size(0, 20);
            this.lblWarningInvalidPassword.TabIndex = 19;
            // 
            // FrmAccountCreation
            // 
            this.AcceptButton = this.btnCreateAccount;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(517, 316);
            this.Controls.Add(this.lblWarningInvalidPassword);
            this.Controls.Add(this.lblWarningInvalidName);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBackToLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCreateAccount);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.Name = "FrmAccountCreation";
            this.Text = "FrmAccountCreation";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Button btnBackToLogin;
		private Button btnLogin;
		private TextBox txtPassword;
		private Label label3;
		private Label label2;
		private Button btnCreateAccount;
		private TextBox txtUserName;
		private Label label1;
		private TextBox txtConfirmPassword;
		private Label label4;
		private Label lblWarningInvalidName;
		private Label lblWarningInvalidPassword;
	}
}
