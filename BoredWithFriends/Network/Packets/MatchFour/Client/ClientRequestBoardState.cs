using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour.Client
{
	[Packet(typeof(ClientRequestBoardState), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ClientRequestBoardState)]
	internal class ClientRequestBoardState : ClientPacket
	{
		public ClientRequestBoardState()
		{
			//For reflection
		}

		protected override void ReadImpl()
		{
			//Nothing to do.
		}

		protected override void RunImpl()
		{
			//TODO: Reply with SendBoardState
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			//Nothing to do.
		}
	}
}
