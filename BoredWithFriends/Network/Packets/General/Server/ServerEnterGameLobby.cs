using BoredWithFriends.Games;
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

		private List<Player> players;

		public ServerEnterGameLobby(LobbyGameState lobby)
		{
			gameChoice = lobby.game;
			players = lobby.Players;
		}

		protected override void ReadImpl()
		{
			gameChoice = (BoredWithFriendsProtocol) ReadShort();
			int playerCount = ReadInt();
			players = new(playerCount);
			for (int i = 0; i < playerCount; i++)
			{
				Player player = new(ReadInt(), ReadString());
				players.Add(player);
			}
		}

		protected override void RunImpl()
		{
			Network.Client.RaiseEvent(GeneralEvent.EnteredGameLobby, new LobbyResult(gameChoice, players));
		}

		protected override void WriteImpl()
		{
			WriteShort((short) gameChoice);
			WriteInt(players.Count);
			foreach (Player p in players)
			{
				WriteInt(p.PlayerID);
				WriteString(p.Name);
			}
		}
	}

	internal class LobbyResult
	{
		public BoredWithFriendsProtocol GameChoice { get; }

		public List<Player> Players { get; }

		public LobbyResult(BoredWithFriendsProtocol gameChoice, List<Player> players)
		{
			GameChoice = gameChoice;
			Players = players;
		}

	}
}
