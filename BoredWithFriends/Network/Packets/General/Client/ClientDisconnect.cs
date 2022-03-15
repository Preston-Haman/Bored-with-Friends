using BoredWithFriends.Network.Packets.General.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Client
{
	/// <summary>
	/// Sent by the client when the user disconnects.
	/// </summary>
	[Packet(typeof(ClientDisconnect), BoredWithFriendsProtocol.General, (short) GeneralOps.ClientDisconnect)]
	internal class ClientDisconnect : ClientPacket
	{
		protected override void ReadImpl()
		{
			//Nothing to do.
		}

		protected override void RunImpl(Connection con)
		{
			PacketSendUtility.SendPacket(con, new ServerCloseConnection());
		}

		protected override void WriteImpl()
		{
			//Nothing to do.
		}
	}
}
