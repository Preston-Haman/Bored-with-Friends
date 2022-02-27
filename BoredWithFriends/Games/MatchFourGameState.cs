using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Games
{
	internal class MatchFourGameState : GameState
	{
		public enum BoardToken : byte
		{
			Empty,
			Blue,
			Red
		}

		private const int MAX_NUMBER_OF_PLAYERS = 2;

		/// <summary>
		/// A player competing in this match.
		/// </summary>
		private TurnBasedPlayer player1, player2;

		/// <summary>
		/// Represents the board as a 2D array. It's important to note that
		/// this is rows, and then columns. In terms of coordinates, that's
		/// y, and then x. To help match screen coordinates when we display
		/// this information later, the first row is the top row of the board.
		/// </summary>
		private readonly BoardToken[,] board;

		public int Rows { get; private set; }

		public int Columns { get; private set; }

		private int tokenPlayCount = 0;

		public MatchFourGameState(int rows, int columns, int player1ID = -1, string player1Name = "Guest1", int player2ID = -1, string player2Name = "Guest2")
		{
			Rows = rows;
			Columns = columns;

			board = new BoardToken[rows, columns];
			for (int y = 0; y < rows; y++)
			{
				for (int x = 0; x < columns; x++)
				{
					board[y, x] = BoardToken.Empty;
				}
			}

			player1 = new TurnBasedPlayer(player1ID, player1Name);
			player2 = new TurnBasedPlayer(player2ID, player2Name);

			AddCompetingPlayers(player1, player2);

			player1.IsPlayerTurn = true;
			GameHasStarted = true;
		}

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
					//Game is over!
					GameHasEnded = true;
				}
				player.IsPlayerTurn = false;
				opponent.IsPlayerTurn = true;
				return true;
			}
			return false;
		}

		private void CheckWinCondition(Player lastPlayerTurn, BoardToken playerToken, int playedColumn, int playedRow)
		{
			bool CheckWinConditionVertical()
			{
				int connectedCount = 0;
				//Look Up & Down
				for (int x = playedColumn, y = Rows - 1; y >= 0; y--)
				{
					if (board[y, x] == BoardToken.Empty) break;
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
					if (x < 0) continue;
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
					if (x < 0) continue;
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
		}

		public BoardToken GetTokenAt(int column, int row)
		{
			return board[row, column];
		}

		public override int GetMaxPlayers()
		{
			return MAX_NUMBER_OF_PLAYERS;
		}

		public override TurnBasedPlayer getCurrentPlayer()
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
