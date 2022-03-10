using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour.Server
{
	/// <summary>
	/// Sent by the server when the Match Four board has been cleared by one of the players.
	/// </summary>
	[Packet(typeof(ServerBoardCleared), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ServerBoardCleared)]
	internal class ServerBoardCleared : ServerPacket
	{
		private int playerID;

		private bool playerForfeited;

		public ServerBoardCleared(Player player, bool playerForfeited)
		{
			playerID = player.PlayerID;
			this.playerForfeited = playerForfeited;
		}

		protected override void ReadImpl()
		{
			playerID = ReadInt();
			playerForfeited = ReadBool();
		}

		protected override void RunImpl()
		{
			/*TODO:
			 * Clear the board in the GameState (make a new game).
			 * Announce that the specified player forfeited the game.
			 */
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			WriteInt(playerID);
			WriteBool(playerForfeited);
		}
	}
}
