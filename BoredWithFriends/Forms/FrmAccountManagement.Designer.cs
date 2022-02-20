namespace BoredWithFriends.Forms
{
	partial class FrmAccountManagement
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
			this.lblUsername = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lblPlayTime = new System.Windows.Forms.Label();
			this.lblLosses = new System.Windows.Forms.Label();
			this.lblWins = new System.Windows.Forms.Label();
			this.lblRoundsPlayed = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btnChangePassword = new System.Windows.Forms.Button();
			this.txtConfirmNewPassword = new System.Windows.Forms.TextBox();
			this.txtCurrentPassword = new System.Windows.Forms.TextBox();
			this.txtNewPassword = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.btnDeleteAccount = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Font = new System.Drawing.Font("Sigmar One", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblUsername.Location = new System.Drawing.Point(12, 9);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(213, 53);
			this.lblUsername.TabIndex = 0;
			this.lblUsername.Text = "Username";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(87, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154, 26);
			this.label1.TabIndex = 1;
			this.label1.Text = "New Password";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(6, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(235, 26);
			this.label2.TabIndex = 2;
			this.label2.Text = "Confirm New Password";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(47, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(194, 26);
			this.label3.TabIndex = 3;
			this.label3.Text = "Current Password";
			// 
			// tabControl1
			// 
			this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(12, 65);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(501, 163);
			this.tabControl1.TabIndex = 4;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lblPlayTime);
			this.tabPage1.Controls.Add(this.lblLosses);
			this.tabPage1.Controls.Add(this.lblWins);
			this.tabPage1.Controls.Add(this.lblRoundsPlayed);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Location = new System.Drawing.Point(4, 27);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(493, 132);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Game Stats";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// lblPlayTime
			// 
			this.lblPlayTime.AutoSize = true;
			this.lblPlayTime.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblPlayTime.Location = new System.Drawing.Point(184, 81);
			this.lblPlayTime.Name = "lblPlayTime";
			this.lblPlayTime.Size = new System.Drawing.Size(106, 26);
			this.lblPlayTime.TabIndex = 8;
			this.lblPlayTime.Text = "Play Time";
			// 
			// lblLosses
			// 
			this.lblLosses.AutoSize = true;
			this.lblLosses.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblLosses.Location = new System.Drawing.Point(184, 55);
			this.lblLosses.Name = "lblLosses";
			this.lblLosses.Size = new System.Drawing.Size(74, 26);
			this.lblLosses.TabIndex = 7;
			this.lblLosses.Text = "Losses";
			// 
			// lblWins
			// 
			this.lblWins.AutoSize = true;
			this.lblWins.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblWins.Location = new System.Drawing.Point(184, 29);
			this.lblWins.Name = "lblWins";
			this.lblWins.Size = new System.Drawing.Size(60, 26);
			this.lblWins.TabIndex = 6;
			this.lblWins.Text = "Wins";
			// 
			// lblRoundsPlayed
			// 
			this.lblRoundsPlayed.AutoSize = true;
			this.lblRoundsPlayed.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblRoundsPlayed.Location = new System.Drawing.Point(184, 3);
			this.lblRoundsPlayed.Name = "lblRoundsPlayed";
			this.lblRoundsPlayed.Size = new System.Drawing.Size(85, 26);
			this.lblRoundsPlayed.TabIndex = 5;
			this.lblRoundsPlayed.Text = "Rounds";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label7.Location = new System.Drawing.Point(6, 81);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(172, 26);
			this.label7.TabIndex = 3;
			this.label7.Text = "Total Play Time:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label6.Location = new System.Drawing.Point(98, 55);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 26);
			this.label6.TabIndex = 2;
			this.label6.Text = "Losses:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label5.Location = new System.Drawing.Point(112, 29);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 26);
			this.label5.TabIndex = 1;
			this.label5.Text = "Wins:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.Location = new System.Drawing.Point(14, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(164, 26);
			this.label4.TabIndex = 0;
			this.label4.Text = "Rounds Played:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btnChangePassword);
			this.tabPage2.Controls.Add(this.txtConfirmNewPassword);
			this.tabPage2.Controls.Add(this.txtCurrentPassword);
			this.tabPage2.Controls.Add(this.txtNewPassword);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Location = new System.Drawing.Point(4, 27);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(493, 132);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Password";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// btnChangePassword
			// 
			this.btnChangePassword.Location = new System.Drawing.Point(247, 88);
			this.btnChangePassword.Name = "btnChangePassword";
			this.btnChangePassword.Size = new System.Drawing.Size(231, 23);
			this.btnChangePassword.TabIndex = 7;
			this.btnChangePassword.Text = "Change Password";
			this.btnChangePassword.UseVisualStyleBackColor = true;
			this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
			// 
			// txtConfirmNewPassword
			// 
			this.txtConfirmNewPassword.Location = new System.Drawing.Point(247, 33);
			this.txtConfirmNewPassword.Name = "txtConfirmNewPassword";
			this.txtConfirmNewPassword.Size = new System.Drawing.Size(231, 23);
			this.txtConfirmNewPassword.TabIndex = 6;
			this.txtConfirmNewPassword.UseSystemPasswordChar = true;
			// 
			// txtCurrentPassword
			// 
			this.txtCurrentPassword.Location = new System.Drawing.Point(247, 59);
			this.txtCurrentPassword.Name = "txtCurrentPassword";
			this.txtCurrentPassword.Size = new System.Drawing.Size(231, 23);
			this.txtCurrentPassword.TabIndex = 5;
			this.txtCurrentPassword.UseSystemPasswordChar = true;
			// 
			// txtNewPassword
			// 
			this.txtNewPassword.Location = new System.Drawing.Point(247, 7);
			this.txtNewPassword.Name = "txtNewPassword";
			this.txtNewPassword.Size = new System.Drawing.Size(231, 23);
			this.txtNewPassword.TabIndex = 4;
			this.txtNewPassword.UseSystemPasswordChar = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.btnDeleteAccount);
			this.tabPage3.Location = new System.Drawing.Point(4, 27);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(493, 132);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Account Status";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// btnDeleteAccount
			// 
			this.btnDeleteAccount.Location = new System.Drawing.Point(6, 6);
			this.btnDeleteAccount.Name = "btnDeleteAccount";
			this.btnDeleteAccount.Size = new System.Drawing.Size(154, 42);
			this.btnDeleteAccount.TabIndex = 0;
			this.btnDeleteAccount.Text = "Delete Account";
			this.btnDeleteAccount.UseVisualStyleBackColor = true;
			this.btnDeleteAccount.Click += new System.EventHandler(this.btnDeleteAccount_Click);
			// 
			// FrmAccountManagement
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(525, 240);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.lblUsername);
			this.Name = "FrmAccountManagement";
			this.Text = "FrmAccountManagement";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lblUsername;
		private Label label1;
		private Label label2;
		private Label label3;
		private TabControl tabControl1;
		private TabPage tabPage1;
		private Label label6;
		private Label label5;
		private Label label4;
		private TabPage tabPage2;
		private Button btnChangePassword;
		private TextBox txtConfirmNewPassword;
		private TextBox txtCurrentPassword;
		private TextBox txtNewPassword;
		private TabPage tabPage3;
		private Label lblPlayTime;
		private Label lblLosses;
		private Label lblWins;
		private Label lblRoundsPlayed;
		private Label label7;
		private Button btnDeleteAccount;
	}
}
