using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	[Packet(typeof(ServerAddPlayer), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerAddPlayer)]
	internal class ServerAddPlayer : ServerPacket
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
