using BoredWithFriends.Games;
using BoredWithFriends.Network.Packets.MatchFour.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BoredWithFriends.Games.MatchFourGameState;

namespace BoredWithFriends.Network.Packets.MatchFour.Server
{
	/// <summary>
	/// Broadcast by the server when a player has made a move.
	/// </summary>
	[Packet(typeof(ServerTokenPlayed), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ServerTokenPlayed)]
	internal class ServerTokenPlayed : ServerPacket
	{
		private int playerID;

		private int turnCount;

		private int row;

		private int column;

		private BoardToken token;

		public ServerTokenPlayed(Player player, int turnCount, int row, int column, BoardToken token)
		{
			playerID = player.PlayerID;
			this.turnCount = turnCount;
			this.row = row;
			this.column = column;
			this.token = token;
		}

		protected override void ReadImpl()
		{
			playerID = ReadInt();
			turnCount = ReadInt();
			row = ReadInt();
			column = ReadInt();
			token = (BoardToken) ReadByte();
		}

		protected override void RunImpl()
		{
			GetClientGameState<MatchFourGameState>(out MatchFourGameState game);
			TurnBasedPlayer player = game.GetPlayerByID(playerID, out _);

			bool validPlay = game.PlayGamePiece(player, column, out int playedRow);
			validPlay &= row == playedRow;
			validPlay &= game.GetTokenAt(row, column) == token;
			validPlay &= game.GetTurnCount() == turnCount;

			if (!validPlay)
			{
				//Something is wrong with our board!
				PacketSendUtility.SendPacket(new ClientRequestBoardState());
			}
		}

		protected override void WriteImpl()
		{
			WriteInt(playerID);
			WriteInt(turnCount);
			WriteInt(row);
			WriteInt(column);
			WriteByte((byte) token);
		}
	}
}
