using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	[Packet(typeof(ServerStartGame), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerStartGame)]
	internal class ServerStartGame : ServerPacket
	{
		private BoredWithFriendsProtocol protocol;

		private List<Player> players;

		public ServerStartGame(BoredWithFriendsProtocol protocol, List<Player> players)
		{
			this.protocol = protocol;
			this.players = players;
		}

		protected override void ReadImpl()
		{
			protocol = (BoredWithFriendsProtocol) ReadShort();
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
			Network.Client.RaiseEvent(GeneralEvent.GameStart, new GameStartResult(protocol, players));
		}

		protected override void WriteImpl()
		{
			WriteShort((short) protocol);
			WriteInt(players.Count);
			foreach (Player p in players)
			{
				WriteInt(p.PlayerID);
				WriteString(p.Name);
			}
		}
	}

	internal class GameStartResult
	{
		public BoredWithFriendsProtocol Protocol { get; }

		public List<Player> Players { get; }

		public GameStartResult(BoredWithFriendsProtocol protocol, List<Player> players)
		{
			Protocol = protocol;
			Players = players;
		}

	}
}
