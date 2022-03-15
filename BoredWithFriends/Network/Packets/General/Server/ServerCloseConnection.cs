using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Sent by the server to indicate that the connection to the client has been closed.
	/// </summary>
	[Packet(typeof(ServerCloseConnection), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerCloseConnection)]
	internal class ServerCloseConnection : ServerPacket
	{
		protected override void ReadImpl()
		{
			//Nothing to do.
		}

		protected override void RunImpl(Connection con)
		{
			con.Close();
		}

		protected override void RunImpl()
		{
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			//Nothing to do.
		}
	}
}
