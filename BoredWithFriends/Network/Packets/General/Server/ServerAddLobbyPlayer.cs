using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Sent by the server when a different player has joined the client's lobby.
	/// </summary>
	[Packet(typeof(ServerAddLobbyPlayer), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerAddLobbyPlayer)]
	internal class ServerAddLobbyPlayer : ServerPacket
	{
		private int playerID;

		private string playerName;

		public ServerAddLobbyPlayer(Player player)
		{
			playerID = player.PlayerID;
			playerName = player.Name;
		}

		protected override void ReadImpl()
		{
			playerID = ReadInt();
			playerName = ReadString();
		}

		protected override void RunImpl()
		{
			GetClientGameState<LobbyGameState>(out LobbyGameState game);
			game.QueuePlayer(new Player(playerID, playerName));
		}

		protected override void WriteImpl()
		{
			WriteInt(playerID);
			WriteString(playerName);
		}
	}
}
