using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	[Packet(typeof(ServerApprovePasswordUpdate), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerApprovePasswordUpdate)]
	internal class ServerApprovePasswordUpdate : ServerPacket
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
