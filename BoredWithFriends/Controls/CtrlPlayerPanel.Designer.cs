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
			this.ctrlPlayerPanel = new BoredWithFriends.Controls.CtrlPlayerListPanel();
			this.ctrlSpectatorPanel = new BoredWithFriends.Controls.CtrlPlayerListPanel();
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
			// ctrlPlayerPanel
			// 
			this.ctrlPlayerPanel.AutoScroll = true;
			this.ctrlPlayerPanel.LocalMode = false;
			this.ctrlPlayerPanel.Location = new System.Drawing.Point(3, 38);
			this.ctrlPlayerPanel.Name = "ctrlPlayerPanel";
			this.ctrlPlayerPanel.Size = new System.Drawing.Size(206, 175);
			this.ctrlPlayerPanel.TabIndex = 2;
			// 
			// ctrlSpectatorPanel
			// 
			this.ctrlSpectatorPanel.AutoScroll = true;
			this.ctrlSpectatorPanel.LocalMode = false;
			this.ctrlSpectatorPanel.Location = new System.Drawing.Point(3, 279);
			this.ctrlSpectatorPanel.Name = "ctrlSpectatorPanel";
			this.ctrlSpectatorPanel.Size = new System.Drawing.Size(206, 175);
			this.ctrlSpectatorPanel.TabIndex = 3;
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
			this.Controls.Add(this.ctrlSpectatorPanel);
			this.Controls.Add(this.ctrlPlayerPanel);
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
		private CtrlPlayerListPanel ctrlPlayerPanel;
		private CtrlPlayerListPanel ctrlSpectatorPanel;
		private Button btnAddPlayer;
		private Button btnLeave;
	}
}
