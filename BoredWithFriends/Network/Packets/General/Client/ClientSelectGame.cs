using BoredWithFriends.Network.Packets.General.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Client
{
	/// <summary>
	/// Sent by the client when the user selects a game to play.
	/// </summary>
	[Packet(typeof(ClientSelectGame), BoredWithFriendsProtocol.General, (short) GeneralOps.ClientSelectGame)]
	internal class ClientSelectGame : ClientPacket
	{
		private BoredWithFriendsProtocol gameChoice;

		public ClientSelectGame(BoredWithFriendsProtocol gameChoice)
		{
			this.gameChoice = gameChoice;
		}

		protected override void ReadImpl()
		{
			gameChoice = (BoredWithFriendsProtocol) ReadShort();
		}

		protected override void RunImpl(Connection con)
		{
			//TODO: Place the client into a server queue for the selected game
			//match them with the next player that connects.
			//Reply with PacketSendUtility.SendPacket(GetPlayerConnection(con).Player, new ServerEnterGameLobby());
			//if they are queued, or handle starting the game for them with their matched players.
			throw new NotImplementedException();
			
		}

		protected override void WriteImpl()
		{
			WriteShort((short) gameChoice);
		}
	}
}
