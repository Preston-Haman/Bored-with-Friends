using BoredWithFriends.Network.Packets.General.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Client
{
	/// <summary>
	/// Sent by the client during initial connection to the server.
	/// </summary>
	[Packet(typeof(ClientConnect), BoredWithFriendsProtocol.General, (short) GeneralOps.ClientConnect, ConnectionState.Unknown)]
	internal class ClientConnect : ClientPacket
	{
		private const string BORED_WITH_FRIENDS_CLIENT_MAGIC = "The only way to win...";

		private string boredWithFriendsMagic = BORED_WITH_FRIENDS_CLIENT_MAGIC;

		protected override void ReadImpl()
		{
			boredWithFriendsMagic = ReadString();
		}

		protected override void RunImpl(Connection con)
		{
			if (boredWithFriendsMagic == BORED_WITH_FRIENDS_CLIENT_MAGIC)
			{
				con.SetConnectionState(ConnectionState.Handshook);
				PacketSendUtility.SendPacket(con, new ServerConnected());
			}
			else
			{
				con.Close();
			}
		}

		protected override void WriteImpl()
		{
			WriteString(BORED_WITH_FRIENDS_CLIENT_MAGIC);
		}
	}
}
