using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BoredWithFriends.Games.MatchFourGameState;

namespace BoredWithFriends.Network.Packets.MatchFour.Server
{
	/// <summary>
	/// Sent by the server to inform the client of the current board state.
	/// </summary>
	[Packet(typeof(ServerSendBoardState), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ServerSendBoardState)]
	internal class ServerSendBoardState : ServerPacket
	{
		private int playerTurnID;

		private int rows;

		private int columns;

		private BoardToken[,] board;

		public ServerSendBoardState(Player activePlayer, BoardToken[,] board)
		{
			playerTurnID = activePlayer.PlayerID;
			rows = board.GetLength(0);
			columns = board.GetLength(1);
			this.board = board;
		}

		protected override void ReadImpl()
		{
			playerTurnID = ReadInt();
			rows = ReadInt();
			columns = ReadInt();
			board = new BoardToken[rows, columns];
			for (int row = 0; row < rows; row++)
			{
				for (int column = 0; column < columns; column++)
				{
					board[row, column] = (BoardToken) ReadByte();
				}
			}
		}

		protected override void RunImpl()
		{
			/*TODO
			 * Set the board state as given by the server.
			 * Change the turn to be the player specified.
			 */
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			WriteInt(playerTurnID);
			WriteInt(rows);
			WriteInt(columns);
			for (int row = 0; row < rows; row++)
			{
				for (int column = 0; column < columns; column++)
				{
					WriteByte((byte) board[row, column]);
				}
			}
		}
	}
}
