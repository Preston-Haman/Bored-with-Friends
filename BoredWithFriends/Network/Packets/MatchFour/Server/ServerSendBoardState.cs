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

		public ServerSendBoardState(MatchFourGameState matchFourGame)
		{
			playerTurnID = matchFourGame.GetCurrentPlayer().PlayerID;
			rows = matchFourGame.Rows;
			columns = matchFourGame.Columns;
			board = new BoardToken[rows, columns];
			for (int row = 0; row < rows; row++)
			{
				for (int column = 0; column < columns; column++)
				{
					board[row, column] = matchFourGame.GetTokenAt(row, column);
				}
			}
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
			GetClientGameState<MatchFourGameState>(out MatchFourGameState game);

			game.SetBoardState(playerTurnID, board);
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
