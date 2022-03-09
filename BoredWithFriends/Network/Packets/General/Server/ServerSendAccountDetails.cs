using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	[Packet(typeof(ServerSendAccountDetails), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerSendAccountDetails)]
	internal class ServerSendAccountDetails : ClientPacket
	{
		protected override void ReadImpl()
		{
			throw new NotImplementedException();
		}

		protected override void RunImpl(Connection con)
		{
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			throw new NotImplementedException();
		}
	}
}
