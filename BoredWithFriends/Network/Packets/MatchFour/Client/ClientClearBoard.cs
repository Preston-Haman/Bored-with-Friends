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

		public ClientClearBoard(bool playerForfeited)
		{
			this.playerForfeited = playerForfeited;
		}

		protected override void ReadImpl()
		{
			playerForfeited = ReadBool();
		}

		protected override void RunImpl(Connection con)
		{
			//TODO: Get gamestate for this packet and forfeit this player; reply with BoardCleared
			//If the player is not forfeiting, make sure that the game is over!
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			WriteBool(playerForfeited);
		}
	}
}
