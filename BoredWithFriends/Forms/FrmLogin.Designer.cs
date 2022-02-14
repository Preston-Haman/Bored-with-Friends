namespace BoredWithFriends.Forms
{
    partial class FrmLogin
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.btnCreateAccount = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.btnBackToInitialLauncher = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(16, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(452, 52);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter your desired user name and password, \r\nthen click login or create new user";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(157, 73);
			this.txtUserName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(289, 23);
			this.txtUserName.TabIndex = 1;
			// 
			// btnCreateAccount
			// 
			this.btnCreateAccount.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCreateAccount.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCreateAccount.Location = new System.Drawing.Point(322, 145);
			this.btnCreateAccount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnCreateAccount.Name = "btnCreateAccount";
			this.btnCreateAccount.Size = new System.Drawing.Size(114, 50);
			this.btnCreateAccount.TabIndex = 3;
			this.btnCreateAccount.Text = "Create New User";
			this.btnCreateAccount.UseVisualStyleBackColor = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(38, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 26);
			this.label2.TabIndex = 4;
			this.label2.Text = "User Name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(41, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 26);
			this.label3.TabIndex = 5;
			this.label3.Text = "Password";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(157, 106);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(289, 23);
			this.txtPassword.TabIndex = 6;
			// 
			// btnLogin
			// 
			this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnLogin.Location = new System.Drawing.Point(165, 145);
			this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(112, 50);
			this.btnLogin.TabIndex = 7;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = false;
			// 
			// btnBackToInitialLauncher
			// 
			this.btnBackToInitialLauncher.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnBackToInitialLauncher.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnBackToInitialLauncher.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnBackToInitialLauncher.Location = new System.Drawing.Point(29, 167);
			this.btnBackToInitialLauncher.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnBackToInitialLauncher.Name = "btnBackToInitialLauncher";
			this.btnBackToInitialLauncher.Size = new System.Drawing.Size(66, 28);
			this.btnBackToInitialLauncher.TabIndex = 8;
			this.btnBackToInitialLauncher.Text = "Back";
			this.btnBackToInitialLauncher.UseVisualStyleBackColor = false;
			this.btnBackToInitialLauncher.Click += new System.EventHandler(this.btnBackToInitialLauncher_Click);
			// 
			// FrmLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 211);
			this.Controls.Add(this.btnBackToInitialLauncher);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCreateAccount);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "FrmLogin";
			this.Text = "Login";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox txtUserName;
        private Button btnCreateAccount;
        private Label label2;
        private Label label3;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnBackToInitialLauncher;
    }
}
