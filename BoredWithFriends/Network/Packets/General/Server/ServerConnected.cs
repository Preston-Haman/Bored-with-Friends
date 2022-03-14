using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Sent by the server in response to <see cref="Client.ClientConnect"/>.
	/// </summary>
	[Packet(typeof(ServerConnected), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerConnected, ConnectionState.Handshook)]
	internal class ServerConnected : ServerPacket
	{
		private const string BORED_WITH_FRIENDS_SERVER_MAGIC = "Is to deny them battle.";

		private string boredWithFriendsMagic = BORED_WITH_FRIENDS_SERVER_MAGIC;

		protected override void ReadImpl()
		{
			boredWithFriendsMagic = ReadString();
		}

		protected override void RunImpl(Connection con)
		{
			if (boredWithFriendsMagic == BORED_WITH_FRIENDS_SERVER_MAGIC)
			{
				con.SetConnectionState(ConnectionState.Authed);
				Network.Client.RaiseEvent(GeneralEvent.LoginReady, this);
			}
			else
			{
				Network.Client.StopClient(this);
			}
		}

		protected override void RunImpl()
		{
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			WriteString(BORED_WITH_FRIENDS_SERVER_MAGIC);
		}
	}
}
