using BoredWithFriends.Games;
using BoredWithFriends.Network.Packets.MatchFour.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour.Client
{
	/// <summary>
	/// Sent by the client when the player plays a token.
	/// </summary>
	[Packet(typeof(ClientPlayToken), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ClientPlayToken)]
	internal class ClientPlayToken : ClientPacket
	{
		private int row = -1;

		private int column = -1; //Invalid default value just in case.

		private int turnCount;

		public ClientPlayToken(int column)
		{
			if (Network.Client.LocalGameState is not MatchFourGameState game || Network.Client.ClientPlayer is not TurnBasedPlayer player)
			{
				throw new InvalidOperationException("Cannot play a token on a Match Four board while not playing Match Four.");
			}

			if (!game.CheckPlayIsPossible(player, column, out int row))
			{
				throw new FailedPlayException();
			}

			this.row = row;
			this.column = column;
			turnCount = game.GetTurnCount();
		}

		protected override void ReadImpl()
		{
			row = ReadInt();
			column = ReadInt();
			turnCount = ReadInt();
		}

		protected override void RunImpl(Connection con)
		{
			GetPlayerConnectionAndGameState<MatchFourGameState>(con, out PlayerConnection pcon, out MatchFourGameState game);
			TurnBasedPlayer player = game.GetPlayerByID(pcon.PlayerID, out _);

			bool validPlay = game.GetTurnCount() == turnCount;
			validPlay &= game.CheckPlayIsPossible(player, column, out int playedRow);
			validPlay &= row == playedRow;

			if (validPlay)
			{
				game.PlayGamePiece(player, column, out playedRow);
				ServerPacket packet = new ServerTokenPlayed(player, game.GetTurnCount(), playedRow, column, game.GetTokenAt(playedRow, column));
				PacketSendUtility.BroadcastPacket(player, packet);
			}
			else
			{
				PacketSendUtility.SendPacket(player, new ServerSendBoardState(game));
			}
		}

		protected override void WriteImpl()
		{
			WriteInt(row);
			WriteInt(column);
			WriteInt(turnCount);
		}
	}

	/// <summary>
	/// The attempt to play a token at the given location failed for an unspecified reason.
	/// </summary>
	internal class FailedPlayException : Exception
	{

	}
}
