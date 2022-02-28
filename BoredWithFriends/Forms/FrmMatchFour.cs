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

namespace BoredWithFriends.Forms
{
	internal partial class FrmMatchFour : Form
	{
		private readonly TurnBasedPlayer? activePlayer;

		public FrmMatchFour()
		{
			//For testing purposes.
			InitializeComponent();
			this.ApplyGeneralTheme();
			ctrlMatchFourBoard.NewGame(new MatchFourGameState(6, 7));
			activePlayer = null;
		}

		/// <summary>
		/// Creates a playable Match Four GUI for one player. When using local play,
		/// a separate instance must be used to track other players.
		/// </summary>
		/// <param name="activePlayer"></param>
		public FrmMatchFour(TurnBasedPlayer activePlayer, MatchFourGameState gameState)
		{
			InitializeComponent();
			this.ApplyGeneralTheme();
			this.activePlayer = activePlayer;
			ctrlMatchFourBoard.NewGame(gameState);
		}

		private void btnClearBoard_Click(object sender, EventArgs e)
		{
			MatchFourGameState gameState = ctrlMatchFourBoard.GameState;
			
			//TODO: Warn the player that this will forfeit the match if it's not over, yet.
			gameState.PlayerForfeit(activePlayer is null ? gameState.getCurrentPlayer() : activePlayer);

			//TODO: When online play is supported, we will have to call this from the server
			//side of things and tell the clients to reset vs the user triggering it like this.

			List<Player> players = gameState.Players;
			List<Player> spectators = gameState.Spectators;

			gameState = new MatchFourGameState(gameState.Rows, gameState.Columns, players[0].PlayerID, players[0].Name, players[1].PlayerID, players[1].Name);

			foreach (Player p in spectators)
			{
				gameState.AddSpectatingPlayer(p);
			}

			ctrlMatchFourBoard.NewGame(gameState);
			Refresh();
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

				MatchFourGameState gameState = ctrlMatchFourBoard.GameState;

				if (gameState.PlayGamePiece(activePlayer is null ? gameState.getCurrentPlayer() : activePlayer, columnIndex))
				{
					Refresh();
				}

				if (gameState.GameHasEnded)
				{
					//TODO: Disable column buttons, declare game end, declare winner (if there was one).
					if (gameState.GameHasWinner)
					{
						//Declare winner
					}
					else
					{
						//Declare cat's game
					}
				}
			}
		}
	}
}
