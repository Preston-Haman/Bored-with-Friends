using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour.Server
{
	[Packet(typeof(ServerSendBoardState), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ServerSendBoardState)]
	internal class ServerSendBoardState : ServerPacket
	{
		protected override void ReadImpl()
		{
			throw new NotImplementedException();
		}

		protected override void RunImpl()
		{
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			throw new NotImplementedException();
		}
	}
}
