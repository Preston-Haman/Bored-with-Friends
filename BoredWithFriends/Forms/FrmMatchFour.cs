using BoredWithFriends.Games;
using BoredWithFriends.Network;
using BoredWithFriends.Network.Packets.MatchFour.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoredWithFriends.Forms
{
	internal partial class FrmMatchFour : Form
	{
		/// <summary>
		/// Creates a playable Match Four GUI.
		/// </summary>
		public FrmMatchFour(MatchFourGameState game)
		{
			InitializeComponent();
			this.ApplyGeneralTheme();

			game.GameGui = ctrlMatchFourBoard;
		}

		private void btnClearBoard_Click(object sender, EventArgs e)
		{
			string confirmMessage = "This will forfeit the game if it's in progress; are you sure?";
			DialogResult result = MessageBox.Show(this, confirmMessage, "Confirm Clear Board", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

			if (result == DialogResult.Yes)
			{
				PacketSendUtility.SendPacket(new ClientClearBoard());
			}
		}

		/// <summary>
		/// Called when the user clicks on a column button to play a piece on the board.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ColumnButton_Click(object sender, EventArgs e)
		{
			//The sender object should be a Button with a int string Tag for the column index (zero based).
			if (sender is Button btn && btn.Tag is string strTag)
			{
				int columnIndex = int.Parse(strTag);

				try
				{
					PacketSendUtility.SendPacket(new ClientPlayToken(columnIndex));
				}
				catch (FailedPlayException)
				{
					//Wasn't our turn or that column was filled.
					//We can do the following, but it could be spammy if the player keeps clicking when it's not their turn...
					//PacketSendUtility.SendPacket(new ClientRequestBoardState());
				}
			}
		}
	}
}
