using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour.Server
{
	[Packet(typeof(ServerBoardCleared), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.BoardCleared)]
	internal class ServerBoardCleared : ServerPacket
	{
		protected override void ReadImpl()
		{
			//TODO: Read playerID
			throw new NotImplementedException();
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
			//TODO: Write PlayerID
			throw new NotImplementedException();
		}
	}
}
