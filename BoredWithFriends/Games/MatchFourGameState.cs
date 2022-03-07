using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Games
{
	/// <summary>
	/// A GameState that represents Match Four (more commonly known as Connect Four). In
	/// a standard game, there are six rows and seven columns on the board. This implementation
	/// allows the client to specify a larger custom board size, but not a smaller one.
	/// </summary>
	internal class MatchFourGameState : GameState
	{
		/// <summary>
		/// A simple enum to represent states of the holes of the board.
		/// </summary>
		public enum BoardToken : byte
		{
			/// <summary>
			/// If a position on the board is empty, then a player may place a token there.
			/// </summary>
			Empty,

			/// <summary>
			/// Represents the token of Player 1.
			/// </summary>
			Blue,

			/// <summary>
			/// Represents the token of Player 2.
			/// </summary>
			Red
		}

		/// <summary>
		/// The maximum number of competing players.
		/// </summary>
		private const int MAX_NUMBER_OF_PLAYERS = 2;

		/// <summary>
		/// A player competing in this match.
		/// </summary>
		private readonly TurnBasedPlayer player1, player2;

		/// <summary>
		/// Represents the board as a 2D array. It's important to note that
		/// this is rows, and then columns. In terms of coordinates, that's
		/// y, and then x. To help match screen coordinates when we display
		/// this information later, the first row is the top row of the board.
		/// </summary>
		private readonly BoardToken[,] board;

		/// <summary>
		/// The number of rows available on this board. In a standard game, there are six.
		/// </summary>
		public int Rows { get; private set; } = 6;

		/// <summary>
		/// The number of columns available on this board. In a standard game, there are seven.
		/// </summary>
		public int Columns { get; private set; } = 7;

		/// <summary>
		/// The number of tokens that have been played by the competing players.
		/// </summary>
		private int tokenPlayCount = 0;

		/// <summary>
		/// Creates a new game of Match Four with the given number of rows and columns. The given
		/// players will be the competing players for this game.
		/// <br></br><br></br>
		/// To allow the player of this game a fair chance of competition, the number of rows and
		/// columns may not be less than that of a standard game.
		/// </summary>
		/// <param name="rows">The number of board rows to use for this game.</param>
		/// <param name="columns">The number of board columns to use for this game.</param>
		/// <param name="player1">The player that will be using Blue tokens.</param>
		/// <param name="player2">The player that will be using Red tokens.</param>
		public MatchFourGameState(int rows, int columns, Player player1 = null!, Player player2 = null!)
		{
			Rows = rows < 6 ? 6 : rows;
			Columns = columns < 7 ? 7 : columns;

			board = new BoardToken[rows, columns];
			for (int y = 0; y < rows; y++)
			{
				for (int x = 0; x < columns; x++)
				{
					board[y, x] = BoardToken.Empty;
				}
			}

			if (player1 is null)
			{
				this.player1 = new TurnBasedPlayer(-1, "Guest1");
			}
			else
			{
				this.player1 = new TurnBasedPlayer(player1);
			}

			if (player2 is null)
			{
				this.player2 = new TurnBasedPlayer(-1, "Guest2");
			}
			else
			{
				this.player2 = new TurnBasedPlayer(player2);
			}

			AddCompetingPlayers(this.player1, this.player2);

			this.player1.IsPlayerTurn = true;
			GameHasStarted = true;
		}

		/// <summary>
		/// Plays a token representing the given player onto the board into the given column.
		/// Returns true when the player's turn was successful, false if the player cannot make
		/// the given move.
		/// <br></br><br></br>
		/// By playng a token, the game may come to an end. Check <see cref="GameState.GameHasEnded"/> to see
		/// if this move resulted in ending the game. The game can end here for two reasons; either the player
		/// who made this move has won, or the board has been completely filled.
		/// </summary>
		/// <param name="player">The player making this game move.</param>
		/// <param name="playedColumn">The column to play a token in.</param>
		/// <returns></returns>
		public bool PlayGamePiece(TurnBasedPlayer player, int playedColumn)
		{
			if (player.IsPlayerTurn & GameHasStarted & !GameHasEnded)
			{
				TurnBasedPlayer opponent = player == player1 ? player2 : player1;
				BoardToken playerToken = player == player1 ? BoardToken.Blue : BoardToken.Red;

				bool tokenPlayed = false;
				int playedRow = Rows - 1;
				for (; playedRow >= 0; playedRow--)
				{
					if (board[playedRow, playedColumn] == BoardToken.Empty)
					{
						board[playedRow, playedColumn] = playerToken;
						tokenPlayed = true;
						break;
					}
				}

				if (!tokenPlayed)
				{
					return false;
				}

				tokenPlayCount++;
				CheckWinCondition(player, playerToken, playedColumn, playedRow);
				if (tokenPlayCount == Rows * Columns)
				{
					//Game is over because board is full!
					GameHasEnded = true;
				}
				player.IsPlayerTurn = false;
				opponent.IsPlayerTurn = true;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Analyzes the game board to determine if the last player to make a move has won.
		/// In the case that the game has been won, the last player will be set as the winner,
		/// their opponent will be set as the loser, and <see cref="GameState.GameHasEnded"/>
		/// will be set to true.
		/// </summary>
		/// <param name="lastPlayerTurn">The player who played the last token.</param>
		/// <param name="playerToken">The type of token the player played.</param>
		/// <param name="playedColumn">The column in which the token was played.</param>
		/// <param name="playedRow">The row in which the token was played.</param>
		private void CheckWinCondition(Player lastPlayerTurn, BoardToken playerToken, int playedColumn, int playedRow)
		{
			//Analyze board for winning state; mark player as winner if won, and other player as loser.
			bool playerWon = CheckWinConditionVertical() || CheckWinConditionHorizontal()
								|| CheckWinConditionDiagonalUp() || CheckWinConditionDiagonalDown();

			if (playerWon)
			{
				TurnBasedPlayer opponent = lastPlayerTurn == player1 ? player2 : player1;
				PlayerLoses(opponent);
				PlayerWins(lastPlayerTurn);
				GameHasEnded = true;
			}

			bool CheckWinConditionVertical()
			{
				int connectedCount = 0;
				//Look Up & Down
				for (int x = playedColumn, y = Rows - 1; y >= 0; y--)
				{
					if (board[y, x] == BoardToken.Empty)
					{
						break;
					}
					if (board[y, x] == playerToken)
					{
						if (++connectedCount == 4)
						{
							return true;
						}
					}
					else
					{
						connectedCount = 0;
					}
				}
				return false;
			}
			
			bool CheckWinConditionHorizontal()
			{
				int connectedCount = 0;
				//Look Horizontally
				for (int x = 0, y = playedRow; x < Columns; x++)
				{
					if (board[y, x] == playerToken)
					{
						if (++connectedCount == 4)
						{
							return true;
						}
					}
					else
					{
						connectedCount = 0;
					}
				}
				return false;
			}

			bool CheckWinConditionDiagonalUp()
			{
				int connectedCount = 0;
				//Look Diagonally from bottom left to upper right
				for (int x = playedColumn - (Rows - 1 - playedRow), y = Rows - 1; x < Columns & y >= 0; x++, y--)
				{
					if (x < 0)
					{
						continue;
					}
					if (board[y, x] == playerToken)
					{
						if (++connectedCount == 4)
						{
							return true;
						}
					}
					else
					{
						connectedCount = 0;
					}
				}
				return false;
			}

			bool CheckWinConditionDiagonalDown()
			{
				int connectedCount = 0;
				//Look Diagonally from upper left to bottom right
				for (int x = playedColumn - playedRow, y = 0; x < Columns & y < Rows; x++, y++)
				{
					if (x < 0)
					{
						continue;
					}
					if (board[y, x] == playerToken)
					{
						if (++connectedCount == 4)
						{
							return true;
						}
					}
					else
					{
						connectedCount = 0;
					}
				}
				return false;
			}
		}

		/// <summary>
		/// Retrieves the token at the specified row and column of the game board.
		/// </summary>
		/// <param name="row">The board row of interest.</param>
		/// <param name="column">The board column of interest.</param>
		/// <returns>An enum representing what type of token was found at the given location on the board.</returns>
		public BoardToken GetTokenAt(int row, int column)
		{
			return board[row, column];
		}

		/// <inheritdoc/>
		public override int GetMaxPlayers()
		{
			return MAX_NUMBER_OF_PLAYERS;
		}

		/// <inheritdoc/>
		public override TurnBasedPlayer GetCurrentPlayer()
		{
			if (player1.IsPlayerTurn)
			{
				return player1;
			}
			else
			{
				return player2;
			}
		}
	}
}
