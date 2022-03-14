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
	internal partial class CtrlPlayerList : UserControl
	{
		private bool localMode = false;

		public bool LocalMode
		{
			get
			{
				return localMode;
			}

			set
			{
				localMode = value;
				btnAddPlayer.Enabled = value;
				playerListPanel1.LocalMode = value;
				playerListPanel2.LocalMode = value;
			}
		}

		public CtrlPlayerList()
		{
			InitializeComponent();
		}

		public void AddPlayer(bool spectator = false, params Player[] players)
		{
			if (spectator)
			{
				playerListPanel2.AddPlayer(players);
			}
			else
			{
				playerListPanel1.AddPlayer(players);
			}
		}

		public void RemovePlayer(Player player, bool spectator = false)
		{
			if (spectator)
			{
				playerListPanel2.RemovePlayer(player);
			}
			else
			{
				playerListPanel1.RemovePlayer(player);
			}
		}

		public void SetActivePlayer(Player player)
		{
			playerListPanel1.SetActivePlayer(player);
		}

		public void Clear()
		{
			playerListPanel1.Clear();
			playerListPanel2.Clear();
		}
	}
}
