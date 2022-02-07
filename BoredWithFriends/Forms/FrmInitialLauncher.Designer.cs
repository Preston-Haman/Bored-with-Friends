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
            this.btnLocalGameLaunch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMultiplayerGameLaunch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLocalGameLaunch
            // 
            this.btnLocalGameLaunch.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLocalGameLaunch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocalGameLaunch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLocalGameLaunch.Location = new System.Drawing.Point(57, 103);
            this.btnLocalGameLaunch.Name = "btnLocalGameLaunch";
            this.btnLocalGameLaunch.Size = new System.Drawing.Size(175, 104);
            this.btnLocalGameLaunch.TabIndex = 0;
            this.btnLocalGameLaunch.Text = "Local Play\r\n(Hot Seat)\r\n";
            this.btnLocalGameLaunch.UseVisualStyleBackColor = false;
            this.btnLocalGameLaunch.Click += new System.EventHandler(this.btnLocalGameLaunch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sigmar One", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(495, 66);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bored with Friends";
            // 
            // btnMultiplayerGameLaunch
            // 
            this.btnMultiplayerGameLaunch.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnMultiplayerGameLaunch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMultiplayerGameLaunch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMultiplayerGameLaunch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnMultiplayerGameLaunch.Location = new System.Drawing.Point(290, 103);
            this.btnMultiplayerGameLaunch.Name = "btnMultiplayerGameLaunch";
            this.btnMultiplayerGameLaunch.Size = new System.Drawing.Size(175, 104);
            this.btnMultiplayerGameLaunch.TabIndex = 3;
            this.btnMultiplayerGameLaunch.Text = "Online\r\nMultiplayer\r\n";
            this.btnMultiplayerGameLaunch.UseVisualStyleBackColor = false;
            // 
            // FrmInitialLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(519, 282);
            this.Controls.Add(this.btnMultiplayerGameLaunch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLocalGameLaunch);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "FrmInitialLauncher";
            this.Text = "Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private Button btnLocalGameLaunch;
        private Label label1;
        private Button btnMultiplayerGameLaunch;
    }
}
