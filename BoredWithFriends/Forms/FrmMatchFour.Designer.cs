namespace BoredWithFriends.Forms
{
	partial class FrmMatchFour
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
			this.ctrlMatchFourBoard = new BoredWithFriends.Controls.CtrlMatchFourBoard();
			this.btnClearBoard = new System.Windows.Forms.Button();
			this.btnColumn1 = new System.Windows.Forms.Button();
			this.btnColumn3 = new System.Windows.Forms.Button();
			this.btnColumn4 = new System.Windows.Forms.Button();
			this.btnColumn5 = new System.Windows.Forms.Button();
			this.btnColumn6 = new System.Windows.Forms.Button();
			this.btnColumn7 = new System.Windows.Forms.Button();
			this.btnColumn2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ctrlMatchFourBoard
			// 
			this.ctrlMatchFourBoard.BackColor = System.Drawing.Color.Yellow;
			this.ctrlMatchFourBoard.Location = new System.Drawing.Point(12, 41);
			this.ctrlMatchFourBoard.Name = "ctrlMatchFourBoard";
			this.ctrlMatchFourBoard.Size = new System.Drawing.Size(470, 402);
			this.ctrlMatchFourBoard.TabIndex = 0;
			// 
			// btnClearBoard
			// 
			this.btnClearBoard.Location = new System.Drawing.Point(12, 449);
			this.btnClearBoard.Name = "btnClearBoard";
			this.btnClearBoard.Size = new System.Drawing.Size(469, 23);
			this.btnClearBoard.TabIndex = 1;
			this.btnClearBoard.Text = "Clear Board";
			this.btnClearBoard.UseVisualStyleBackColor = true;
			this.btnClearBoard.Click += new System.EventHandler(this.btnClearBoard_Click);
			// 
			// btnColumn1
			// 
			this.btnColumn1.Location = new System.Drawing.Point(12, 12);
			this.btnColumn1.Name = "btnColumn1";
			this.btnColumn1.Size = new System.Drawing.Size(62, 23);
			this.btnColumn1.TabIndex = 2;
			this.btnColumn1.Tag = "0";
			this.btnColumn1.UseVisualStyleBackColor = true;
			this.btnColumn1.Click += new System.EventHandler(this.ColumnButton_Click);
			// 
			// btnColumn3
			// 
			this.btnColumn3.Location = new System.Drawing.Point(148, 12);
			this.btnColumn3.Name = "btnColumn3";
			this.btnColumn3.Size = new System.Drawing.Size(62, 23);
			this.btnColumn3.TabIndex = 4;
			this.btnColumn3.Tag = "2";
			this.btnColumn3.UseVisualStyleBackColor = true;
			this.btnColumn3.Click += new System.EventHandler(this.ColumnButton_Click);
			// 
			// btnColumn4
			// 
			this.btnColumn4.Location = new System.Drawing.Point(216, 12);
			this.btnColumn4.Name = "btnColumn4";
			this.btnColumn4.Size = new System.Drawing.Size(62, 23);
			this.btnColumn4.TabIndex = 5;
			this.btnColumn4.Tag = "3";
			this.btnColumn4.UseVisualStyleBackColor = true;
			this.btnColumn4.Click += new System.EventHandler(this.ColumnButton_Click);
			// 
			// btnColumn5
			// 
			this.btnColumn5.Location = new System.Drawing.Point(284, 12);
			this.btnColumn5.Name = "btnColumn5";
			this.btnColumn5.Size = new System.Drawing.Size(62, 23);
			this.btnColumn5.TabIndex = 6;
			this.btnColumn5.Tag = "4";
			this.btnColumn5.UseVisualStyleBackColor = true;
			this.btnColumn5.Click += new System.EventHandler(this.ColumnButton_Click);
			// 
			// btnColumn6
			// 
			this.btnColumn6.Location = new System.Drawing.Point(352, 12);
			this.btnColumn6.Name = "btnColumn6";
			this.btnColumn6.Size = new System.Drawing.Size(62, 23);
			this.btnColumn6.TabIndex = 7;
			this.btnColumn6.Tag = "5";
			this.btnColumn6.UseVisualStyleBackColor = true;
			this.btnColumn6.Click += new System.EventHandler(this.ColumnButton_Click);
			// 
			// btnColumn7
			// 
			this.btnColumn7.Location = new System.Drawing.Point(420, 12);
			this.btnColumn7.Name = "btnColumn7";
			this.btnColumn7.Size = new System.Drawing.Size(62, 23);
			this.btnColumn7.TabIndex = 8;
			this.btnColumn7.Tag = "6";
			this.btnColumn7.UseVisualStyleBackColor = true;
			this.btnColumn7.Click += new System.EventHandler(this.ColumnButton_Click);
			// 
			// btnColumn2
			// 
			this.btnColumn2.Location = new System.Drawing.Point(80, 12);
			this.btnColumn2.Name = "btnColumn2";
			this.btnColumn2.Size = new System.Drawing.Size(62, 23);
			this.btnColumn2.TabIndex = 3;
			this.btnColumn2.Tag = "1";
			this.btnColumn2.UseVisualStyleBackColor = true;
			this.btnColumn2.Click += new System.EventHandler(this.ColumnButton_Click);
			// 
			// FrmMatchFour
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 484);
			this.Controls.Add(this.btnColumn7);
			this.Controls.Add(this.btnColumn6);
			this.Controls.Add(this.btnColumn5);
			this.Controls.Add(this.btnColumn4);
			this.Controls.Add(this.btnColumn3);
			this.Controls.Add(this.btnColumn2);
			this.Controls.Add(this.btnColumn1);
			this.Controls.Add(this.btnClearBoard);
			this.Controls.Add(this.ctrlMatchFourBoard);
			this.Name = "FrmMatchFour";
			this.Text = "Match Four";
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.CtrlMatchFourBoard ctrlMatchFourBoard;
		private Button btnClearBoard;
		private Button btnColumn1;
		private Button btnColumn3;
		private Button btnColumn4;
		private Button btnColumn5;
		private Button btnColumn6;
		private Button btnColumn7;
		private Button btnColumn2;
	}
}
