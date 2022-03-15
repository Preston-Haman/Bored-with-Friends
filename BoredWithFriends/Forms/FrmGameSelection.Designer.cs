namespace BoredWithFriends.Forms
{
	partial class FrmGameSelection
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGameSelection));
			this.btMatchFourStart = new System.Windows.Forms.Button();
			this.lblGameSelectTitle = new System.Windows.Forms.Label();
			this.lblMatchFourTitle = new System.Windows.Forms.Label();
			this.btnBack = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btMatchFourStart
			// 
			this.btMatchFourStart.Image = ((System.Drawing.Image)(resources.GetObject("btMatchFourStart.Image")));
			this.btMatchFourStart.Location = new System.Drawing.Point(200, 104);
			this.btMatchFourStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btMatchFourStart.Name = "btMatchFourStart";
			this.btMatchFourStart.Size = new System.Drawing.Size(82, 67);
			this.btMatchFourStart.TabIndex = 0;
			this.btMatchFourStart.UseVisualStyleBackColor = true;
			this.btMatchFourStart.Click += new System.EventHandler(this.btMatchFourStart_Click);
			// 
			// lblGameSelectTitle
			// 
			this.lblGameSelectTitle.AutoSize = true;
			this.lblGameSelectTitle.Font = new System.Drawing.Font("Sigmar One", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblGameSelectTitle.Location = new System.Drawing.Point(19, 7);
			this.lblGameSelectTitle.Name = "lblGameSelectTitle";
			this.lblGameSelectTitle.Size = new System.Drawing.Size(456, 79);
			this.lblGameSelectTitle.TabIndex = 0;
			this.lblGameSelectTitle.Text = "Select A Game!";
			// 
			// lblMatchFourTitle
			// 
			this.lblMatchFourTitle.AutoSize = true;
			this.lblMatchFourTitle.Font = new System.Drawing.Font("Sigmar One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblMatchFourTitle.Location = new System.Drawing.Point(189, 74);
			this.lblMatchFourTitle.Name = "lblMatchFourTitle";
			this.lblMatchFourTitle.Size = new System.Drawing.Size(95, 20);
			this.lblMatchFourTitle.TabIndex = 2;
			this.lblMatchFourTitle.Text = "Match Four";
			// 
			// btnBack
			// 
			this.btnBack.Location = new System.Drawing.Point(12, 451);
			this.btnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(68, 22);
			this.btnBack.TabIndex = 99;
			this.btnBack.Text = "Back";
			this.btnBack.UseVisualStyleBackColor = true;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// FrmGameSelection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 484);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.lblMatchFourTitle);
			this.Controls.Add(this.lblGameSelectTitle);
			this.Controls.Add(this.btMatchFourStart);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "FrmGameSelection";
			this.Text = "FrmGameSelection";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button btMatchFourStart;
		private Label lblGameSelectTitle;
		private Label lblMatchFourTitle;
		private Button btnBack;
	}
}