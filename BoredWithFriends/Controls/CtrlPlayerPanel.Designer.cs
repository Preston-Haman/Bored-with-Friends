namespace BoredWithFriends.Controls
{
	partial class CtrlPlayerList
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.playerListPanel1 = new BoredWithFriends.Controls.CtrlPlayerListPanel();
			this.playerListPanel2 = new BoredWithFriends.Controls.CtrlPlayerListPanel();
			this.btnAddPlayer = new System.Windows.Forms.Button();
			this.btnLeave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 26);
			this.label1.TabIndex = 0;
			this.label1.Text = "Players:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Sigmar One", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(3, 250);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(129, 26);
			this.label2.TabIndex = 1;
			this.label2.Text = "Spectators:";
			// 
			// playerListPanel1
			// 
			this.playerListPanel1.AutoScroll = true;
			this.playerListPanel1.LocalMode = false;
			this.playerListPanel1.Location = new System.Drawing.Point(3, 38);
			this.playerListPanel1.Name = "playerListPanel1";
			this.playerListPanel1.Size = new System.Drawing.Size(206, 175);
			this.playerListPanel1.TabIndex = 2;
			// 
			// playerListPanel2
			// 
			this.playerListPanel2.AutoScroll = true;
			this.playerListPanel2.LocalMode = false;
			this.playerListPanel2.Location = new System.Drawing.Point(3, 279);
			this.playerListPanel2.Name = "playerListPanel2";
			this.playerListPanel2.Size = new System.Drawing.Size(206, 175);
			this.playerListPanel2.TabIndex = 3;
			// 
			// btnAddPlayer
			// 
			this.btnAddPlayer.Location = new System.Drawing.Point(3, 460);
			this.btnAddPlayer.Name = "btnAddPlayer";
			this.btnAddPlayer.Size = new System.Drawing.Size(110, 23);
			this.btnAddPlayer.TabIndex = 4;
			this.btnAddPlayer.Text = "Add Player";
			this.btnAddPlayer.UseVisualStyleBackColor = true;
			// 
			// btnLeave
			// 
			this.btnLeave.Location = new System.Drawing.Point(119, 460);
			this.btnLeave.Name = "btnLeave";
			this.btnLeave.Size = new System.Drawing.Size(90, 23);
			this.btnLeave.TabIndex = 5;
			this.btnLeave.Text = "Leave";
			this.btnLeave.UseVisualStyleBackColor = true;
			// 
			// CtrlPlayerList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnLeave);
			this.Controls.Add(this.btnAddPlayer);
			this.Controls.Add(this.playerListPanel2);
			this.Controls.Add(this.playerListPanel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CtrlPlayerList";
			this.Size = new System.Drawing.Size(212, 486);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label label1;
		private Label label2;
		private CtrlPlayerListPanel playerListPanel1;
		private CtrlPlayerListPanel playerListPanel2;
		private Button btnAddPlayer;
		private Button btnLeave;
	}
}
