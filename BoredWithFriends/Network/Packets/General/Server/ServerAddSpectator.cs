using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Joining a game as a spectator is not currently supported by the GUI.
	/// <br></br><br></br>
	/// Sent by the server to indicate that a spectator has joined the client's game as a spectator.
	/// </summary>
	[Packet(typeof(ServerAddSpectator), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerAddSpectator)]
	internal class ServerAddSpectator : ServerPacket
	{
		private int playerID;

		private string playerName;

		public ServerAddSpectator(Player player)
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
			GetClientGameState<GameState>(out GameState game);
			game.AddSpectatingPlayers(new Player(playerID, playerName));
		}

		protected override void WriteImpl()
		{
			WriteInt(playerID);
			WriteString(playerName);
		}
	}
}
