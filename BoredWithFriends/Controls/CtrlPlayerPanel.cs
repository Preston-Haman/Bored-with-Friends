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
				ctrlPlayerPanel.LocalMode = value;
				ctrlSpectatorPanel.LocalMode = value;
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
				ctrlSpectatorPanel.AddPlayer(players);
			}
			else
			{
				ctrlPlayerPanel.AddPlayer(players);
			}
		}

		public void RemovePlayer(Player player, bool spectator = false)
		{
			if (spectator)
			{
				ctrlSpectatorPanel.RemovePlayer(player);
			}
			else
			{
				ctrlPlayerPanel.RemovePlayer(player);
			}
		}

		public void SetActivePlayer(Player player)
		{
			ctrlPlayerPanel.SetActivePlayer(player);
		}

		public void Clear()
		{
			ctrlPlayerPanel.Clear();
			ctrlSpectatorPanel.Clear();
		}
	}
}
