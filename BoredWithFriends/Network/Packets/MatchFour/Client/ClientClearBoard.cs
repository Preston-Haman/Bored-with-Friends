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
	/// Sent by the client when a user clears the board.
	/// </summary>
	[Packet(typeof(ClientClearBoard), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ClientClearBoard)]
	internal class ClientClearBoard : ClientPacket
	{
		private bool playerForfeited;

		public ClientClearBoard()
		{
			playerForfeited = Network.Client.LocalGameState!.GameHasEnded;
		}

		protected override void ReadImpl()
		{
			playerForfeited = ReadBool();
		}

		protected override void RunImpl(Connection con)
		{
			GetPlayerConnectionAndGameState<MatchFourGameState>(con, out PlayerConnection pcon, out MatchFourGameState game);
			
			if (playerForfeited |= !game.GameHasEnded)
			{
				game.PlayerForfeit(pcon.PlayerID);
			}

			PacketSendUtility.BroadcastPacket(pcon.Player, new ServerBoardCleared(pcon.Player, playerForfeited));
		}

		protected override void WriteImpl()
		{
			WriteBool(playerForfeited);
		}
	}
}
