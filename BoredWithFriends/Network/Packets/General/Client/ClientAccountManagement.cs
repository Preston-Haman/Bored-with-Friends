using BoredWithFriends.Games;
using BoredWithFriends.Network.Packets.General.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Client
{
	/// <summary>
	/// Sent by the client when the user wants to enter into Account Management.
	/// </summary>
	[Packet(typeof(ClientAccountManagement), BoredWithFriendsProtocol.General, (short) GeneralOps.ClientAccountManagement)]
	internal class ClientAccountManagement : ClientPacket
	{
		protected override void ReadImpl()
		{
			//Nothing to do.
		}

		protected override void RunImpl(Connection con)
		{
			PlayerConnection pcon = GetPlayerConnection(con);
			Player player = pcon.Player;
			PacketSendUtility.SendPacket(player, new ServerSendAccountDetails(player));
		}

		protected override void WriteImpl()
		{
			//Nothing to do.
		}
	}
}
