namespace BoredWithFriends.Forms
{
	partial class FrmAccountConfirmation
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccountConfirmation));
			this.lblConfirmationNotice = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnOk = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// lblConfirmationNotice
			// 
			this.lblConfirmationNotice.AutoSize = true;
			this.lblConfirmationNotice.Font = new System.Drawing.Font("Sigmar One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblConfirmationNotice.Location = new System.Drawing.Point(10, 9);
			this.lblConfirmationNotice.Name = "lblConfirmationNotice";
			this.lblConfirmationNotice.Size = new System.Drawing.Size(233, 60);
			this.lblConfirmationNotice.TabIndex = 0;
			this.lblConfirmationNotice.Text = "Account successfully created!\r\nYou may now use this login at\r\nthe login screen.";
			this.lblConfirmationNotice.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(267, 9);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(79, 67);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// btnOk
			// 
			this.btnOk.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnOk.Location = new System.Drawing.Point(85, 76);
			this.btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(108, 22);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Confirm";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// FrmAccountConfirmation
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(367, 107);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.lblConfirmationNotice);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "FrmAccountConfirmation";
			this.Text = "FrmAccountConfirmation";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lblConfirmationNotice;
		private PictureBox pictureBox1;
		private Button btnOk;
	}
}
