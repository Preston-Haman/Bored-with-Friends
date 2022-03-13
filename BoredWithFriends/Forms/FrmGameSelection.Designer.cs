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
            this.btMatchFourStart.Location = new System.Drawing.Point(265, 138);
            this.btMatchFourStart.Name = "btMatchFourStart";
            this.btMatchFourStart.Size = new System.Drawing.Size(94, 89);
            this.btMatchFourStart.TabIndex = 0;
            this.btMatchFourStart.UseVisualStyleBackColor = true;
            this.btMatchFourStart.Click += new System.EventHandler(this.btMatchFourStart_Click);
            // 
            // lblGameSelectTitle
            // 
            this.lblGameSelectTitle.AutoSize = true;
            this.lblGameSelectTitle.Font = new System.Drawing.Font("Sigmar One", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblGameSelectTitle.Location = new System.Drawing.Point(58, 9);
            this.lblGameSelectTitle.Name = "lblGameSelectTitle";
            this.lblGameSelectTitle.Size = new System.Drawing.Size(569, 99);
            this.lblGameSelectTitle.TabIndex = 0;
            this.lblGameSelectTitle.Text = "Select A Game!";
            // 
            // lblMatchFourTitle
            // 
            this.lblMatchFourTitle.AutoSize = true;
            this.lblMatchFourTitle.Font = new System.Drawing.Font("Sigmar One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMatchFourTitle.Location = new System.Drawing.Point(253, 99);
            this.lblMatchFourTitle.Name = "lblMatchFourTitle";
            this.lblMatchFourTitle.Size = new System.Drawing.Size(118, 25);
            this.lblMatchFourTitle.TabIndex = 2;
            this.lblMatchFourTitle.Text = "Match Four";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 409);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(78, 29);
            this.btnBack.TabIndex = 99;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FrmGameSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblMatchFourTitle);
            this.Controls.Add(this.lblGameSelectTitle);
            this.Controls.Add(this.btMatchFourStart);
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