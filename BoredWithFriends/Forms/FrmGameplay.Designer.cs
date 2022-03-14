namespace BoredWithFriends.Forms
{
	partial class FrmGameplay
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
			this.ctrlPlayerList1 = new BoredWithFriends.Controls.CtrlPlayerList();
			this.SuspendLayout();
			// 
			// ctrlPlayerList1
			// 
			this.ctrlPlayerList1.LocalMode = false;
			this.ctrlPlayerList1.Location = new System.Drawing.Point(0, 0);
			this.ctrlPlayerList1.Name = "ctrlPlayerList1";
			this.ctrlPlayerList1.Size = new System.Drawing.Size(212, 486);
			this.ctrlPlayerList1.TabIndex = 1;
			// 
			// FrmGameplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(710, 488);
			this.Controls.Add(this.ctrlPlayerList1);
			this.IsMdiContainer = true;
			this.Name = "FrmGameplay";
			this.Text = "Bored with Friends";
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.CtrlPlayerList ctrlPlayerList1;
	}
}
