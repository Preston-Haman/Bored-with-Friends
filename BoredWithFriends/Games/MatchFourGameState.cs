using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Games
{
	/// <summary>
	/// An interface of callback methods intended for the GUI consumer of a <see cref="MatchFourGameState"/>.
	/// </summary>
	internal interface IMatchFourGui
	{
		/// <summary>
		/// Called when the board of a Match Four game has undergone a change. Implementors
		/// may use this method to display the new state of the board on the GUI.
		/// </summary>
		/// <param name="game">The game in which the board state has changed.</param>
		public void UpdateBoardDisplay(MatchFourGameState game);
	}

	/// <summary>
	/// An empty implementation of <see cref="IMatchFourGui"/> that does nothing.
	/// </summary>
	internal class NoGuiMatchFour : IMatchFourGui
	{
		public void UpdateBoardDisplay(MatchFourGameState game)
		{
			//Do nothing.
		}
	}

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
		/// /// <summary>
		/// Users of this gamestate must provide an implementation of this interface so changes
		/// to the gamestate can be reflected upon the GUI.
		/// </summary>
		public IMatchFourGui GameGui { get; set; } = new NoGuiMatchFour();

		/// <summary>
		/// A player competing in this match.
		/// </summary>
		private readonly TurnBasedPlayer player1, player2;

		/// <summary>
		/// Represents the board as a 2D array.
		/// <br></br><br></br>
		/// It's important to note that this is rows, and then columns.
		/// In terms of coordinates, that's y, and then x. To help match screen
		/// coordinates when we display this information later, the first row is
		/// the top row of the board.
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
		/// To allow the players of this game a fair chance of competition, the number of rows and
		/// columns may not be less than that of a standard game.
		/// </summary>
		/// <param name="rows">The number of board rows to use for this game.</param>
		/// <param name="columns">The number of board columns to use for this game.</param>
		/// <param name="player1">The player that will be using Blue tokens.</param>
		/// <param name="player2">The player that will be using Red tokens.</param>
		/// <param name="generalGameGui">See <see cref="GameState.generalGameGui"/>.</param>
		/// <param name="gameGui">See <see cref="GameGui"/>.</param>
		public MatchFourGameState(int rows, int columns, Player player1 = null!, Player player2 = null!, IGeneralGameGui generalGameGui = null!):
			base(generalGameGui is not null ? generalGameGui : new NoGuiGame())
		{
			Rows = rows < 6 ? 6 : rows;
			Columns = columns < 7 ? 7 : columns;

			this.player1 = player1 is null ? new TurnBasedPlayer("Guest") : new TurnBasedPlayer(player1);
			this.player2 = player2 is null ? new TurnBasedPlayer("Guest") : new TurnBasedPlayer(player2);
			
			this.player1.IsPlayerTurn = true;
			
			AddCompetingPlayers(this.player1, this.player2);

			board = new BoardToken[rows, columns];
			ResetBoard(); //I'm not actually sure if this is necessary.

			GameHasStarted = true;
		}

		/// <summary>
		/// Iterates through <see cref="board"/> and sets every value to <see cref="BoardToken.Empty"/>.
		/// </summary>
		private void ResetBoard()
		{
			//TODO: If a newly constructed board defaults to the Empty value, just make a new one instead.
			for (int y = 0; y < Rows; y++)
			{
				for (int x = 0; x < Columns; x++)
				{
					board[y, x] = BoardToken.Empty;
				}
			}
			tokenPlayCount = 0;
			GameGui.UpdateBoardDisplay(this);
		}

		public void SetBoardState(int playerTurnID, BoardToken[,] newBoard)
		{
			if (newBoard.GetLength(0) != board.GetLength(0) || newBoard.GetLength(0) != board.GetLength(0))
			{
				throw new ArgumentException("The new board state must be of the same size as the current one.");
			}

			tokenPlayCount = 0;
			for (int y = 0; y < Rows; y++)
			{
				for (int x = 0; x < Columns; x++)
				{
					board[y, x] = newBoard[y, x];
					if (board[y, x] != BoardToken.Empty)
					{
						tokenPlayCount++;
					}
				}
			}
			GameGui.UpdateBoardDisplay(this);
			SetPlayerTurn(GetPlayerByID(playerTurnID, out _));
		}

		/// <summary>
		/// Sets the given <paramref name="player"/> as the active player and calls the appropriate
		/// callback.
		/// </summary>
		/// <param name="player">The player to provide a turn to.</param>
		private void SetPlayerTurn(Player player)
		{
			bool isPlayer1Turn = player == player1;
			player1.IsPlayerTurn = isPlayer1Turn;
			player2.IsPlayerTurn = !isPlayer1Turn;
			generalGameGui.UpdateActivePlayer(this, isPlayer1Turn ? player1 : player2);
		}

		/// <summary>
		/// Checks if playing in the given <paramref name="column"/> is possible and returns true if so.
		/// This method outputs the row that would be played if a token was played in the given <paramref name="column"/>.
		/// </summary>
		/// <param name="player">The player making the play.</param>
		/// <param name="column">The column to play in.</param>
		/// <param name="playedRow">The row that would be played if the play was made.</param>
		/// <returns>True if the play is possible.</returns>
		public bool CheckPlayIsPossible(TurnBasedPlayer player, int column, out int playedRow)
		{
			playedRow = -1;
			if (player != player1 && player != player2)
			{
				System.Diagnostics.Debug.WriteLine("A spectator of Match Four attempted to make a play!");
				return false;
			}

			if (!player.IsPlayerTurn | !GameHasStarted | GameHasEnded)
			{
				return false;
			}

			bool tokenPlayable = false;
			playedRow = Rows - 1;
			for (; playedRow >= 0; playedRow--)
			{
				if (board[playedRow, column] == BoardToken.Empty)
				{
					tokenPlayable = true;
					break;
				}
			}

			if (!tokenPlayable)
			{
				playedRow = -1;
			}

			return tokenPlayable;
		}

		/// <summary>
		/// Plays a token representing the given player onto the board into the given column.
		/// Returns true when the player's turn was successful, false if the player cannot make
		/// the given move. If the move cannot be made, <paramref name="playedRow"/> will be set
		/// to -1; if it can, then it will be set to the row of the play.
		/// <br></br><br></br>
		/// By playing a token, the game may come to an end. Check <see cref="GameState.GameHasEnded"/> to see
		/// if this move resulted in ending the game. The game can end here for two reasons; either the player
		/// who made this move has won, or the board has been completely filled.
		/// </summary>
		/// <param name="player">The player making this game move.</param>
		/// <param name="playedColumn">The column to play a token in.</param>
		/// <param name="playedRow">The row the game piece was played in, or -1 if the play failed.</param>
		/// <returns>True if the play was successful, false otherwise.</returns>
		public bool PlayGamePiece(TurnBasedPlayer player, int playedColumn, out int playedRow)
		{
			if (CheckPlayIsPossible(player, playedColumn, out playedRow))
			{
				BoardToken playerToken = board[playedRow, playedColumn] = player == player1 ? BoardToken.Blue : BoardToken.Red;
				tokenPlayCount++;

				GameGui.UpdateBoardDisplay(this);
				
				//Give a turn to the opponent player
				SetPlayerTurn(player == player1 ? player2 : player1);

				CheckWinCondition(player, playerToken, playedColumn, playedRow);
				if (tokenPlayCount == Rows * Columns)
				{
					//Game is over because board is full!
					GameHasEnded = true;
				}
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
		/// Returns the number of tokens that have been played during this match.
		/// </summary>
		/// <returns>The <see cref="tokenPlayCount"/>.</returns>
		public int GetTurnCount()
		{
			return tokenPlayCount;
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
		public override TurnBasedPlayer GetPlayerByID(int playerID, out bool isSpectator)
		{
			Player player = base.GetPlayerByID(playerID, out isSpectator);
			if (!isSpectator && player is TurnBasedPlayer turnPlayer)
			{
				return turnPlayer;
			}

			return new(player);
		}

		/// <summary>
		/// Removes the given <paramref name="player"/> from the game. If the player
		/// is competing in this game, they will forfeit the match if <see cref="tokenPlayCount"/>
		/// is greater than one.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="spectator"></param>
		public override void PlayerLeaves(Player player, bool spectator = false)
		{
			if (!spectator && tokenPlayCount > 1)
			{
				PlayerForfeit(player);
			}
			base.PlayerLeaves(player, spectator);
		}

		protected override void PlayerForfeit(Player player)
		{
			if (tokenPlayCount > 0)
			{
				base.PlayerForfeit(player);
			}
		}

		/// <inheritdoc/>
		public override void ResetGame()
		{
			lock (this)
			{
				GameHasEnded = false;
				Winners.Clear();
				Losers.Clear();

				ResetBoard();
				SetPlayerTurn(player1);
			}
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
