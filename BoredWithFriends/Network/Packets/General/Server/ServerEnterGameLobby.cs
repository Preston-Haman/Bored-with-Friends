using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Sent by the server when the client has entered into a game lobby.
	/// </summary>
	[Packet(typeof(ServerEnterGameLobby), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerEnterGameLobby)]
	internal class ServerEnterGameLobby : ServerPacket
	{
		private BoredWithFriendsProtocol gameChoice;

		public ServerEnterGameLobby(BoredWithFriendsProtocol gameChoice)
		{
			this.gameChoice = gameChoice;
		}

		protected override void ReadImpl()
		{
			gameChoice = (BoredWithFriendsProtocol) ReadShort();
		}

		protected override void RunImpl()
		{
			Network.Client.RaiseEvent(GeneralEvent.EnteredGameLobby, gameChoice);
		}

		protected override void WriteImpl()
		{
			WriteShort((short) gameChoice);
		}
	}
}
