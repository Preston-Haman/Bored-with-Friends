using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour.Client
{
	[Packet(typeof(ClientClearBoard), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ClearBoard)]
	internal class ClientClearBoard : ClientPacket
	{
		public ClientClearBoard()
		{
			//For reflection
		}

		protected override void ReadImpl()
		{
			//Nothing to do.
		}

		protected override void RunImpl()
		{
			//TODO: Get gamestate for this packet and forfeit this player; reply with BoardCleared
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			//Nothing to do.
		}
	}
}
