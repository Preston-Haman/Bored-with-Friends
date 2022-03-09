using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	[Packet(typeof(ServerAddSpectator), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerAddSpectator)]
	internal class ServerAddSpectator : ClientPacket
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
