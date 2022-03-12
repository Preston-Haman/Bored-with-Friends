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
	[Packet(typeof(ServerAddPlayer), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerAddPlayer)]
	internal class ServerAddPlayer : ServerPacket
	{
		private int playerID;

		private string playerName;

		public ServerAddPlayer(Player player)
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
			throw new NotImplementedException();

			//TODO: Get lobby gamestate, add player to it
			//GetClientGameState<LobbyGameState>(out LobbyGameState game);
			//game.AddLobbyPlayer(new Player(playerID, playerName));
		}

		protected override void WriteImpl()
		{
			WriteInt(playerID);
			WriteString(playerName);
		}
	}
}
