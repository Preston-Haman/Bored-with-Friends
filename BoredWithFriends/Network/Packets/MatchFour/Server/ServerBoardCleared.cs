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
			GetClientGameState<MatchFourGameState>(out MatchFourGameState game);
			
			if (playerForfeited && !game.GameHasEnded)
			{
				//Check if game is over for local play... TODO: needs a better check.
				game.PlayerForfeit(playerID);
			}

			game.ResetGame();
		}

		protected override void WriteImpl()
		{
			WriteInt(playerID);
			WriteBool(playerForfeited);
		}
	}
}
