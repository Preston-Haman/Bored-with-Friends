using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoredWithFriends.Controls
{
	internal partial class CtrlPlayerListPanel : UserControl
	{
		public bool LocalMode { get; set; }

		public CtrlPlayerListPanel()
		{
			//For the designer
			InitializeComponent();
		}

		public void AddPlayer(params Player[] players)
		{
			flowLayoutPanel1.SuspendLayout();

			foreach (Player player in players)
			{
				Label lblPlayer = new();
				lblPlayer.ForeColor = SystemColors.ActiveCaption;
				lblPlayer.Font = new Font("Sigmar One", 10, FontStyle.Regular, GraphicsUnit.Point);
				lblPlayer.Text = player.Name;
				lblPlayer.Tag = player;
				
				if (LocalMode)
				{
					//TODO: Add method of logging in...
				}

				flowLayoutPanel1.Controls.Add(lblPlayer);
			}

			flowLayoutPanel1.ResumeLayout();
			flowLayoutPanel1.PerformLayout();
		}

		public void RemovePlayer(Player player)
		{
			flowLayoutPanel1.SuspendLayout();

			for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
			{
				Control c = flowLayoutPanel1.Controls[i];
				if (c is Label lbl && lbl.Tag is Player p && player.PlayerID == p.PlayerID)
				{
					flowLayoutPanel1.Controls.RemoveAt(i--);
				}
			}

			flowLayoutPanel1.ResumeLayout();
			flowLayoutPanel1.PerformLayout();
		}

		public void Clear()
		{
			flowLayoutPanel1.SuspendLayout();
			flowLayoutPanel1.Controls.Clear();
			flowLayoutPanel1.ResumeLayout();
			flowLayoutPanel1.PerformLayout();
		}

		public void SetActivePlayer(Player player)
		{
			for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
			{
				Control c = flowLayoutPanel1.Controls[i];
				if (c is Label lbl)
				{
					if (lbl.Tag is Player p && player.PlayerID == p.PlayerID)
					{
						lbl.Font = new Font(lbl.Font, FontStyle.Underline);
					}
					else
					{
						lbl.Font = new Font(lbl.Font, FontStyle.Regular);
					}
				}
			}
		}
	}
}
