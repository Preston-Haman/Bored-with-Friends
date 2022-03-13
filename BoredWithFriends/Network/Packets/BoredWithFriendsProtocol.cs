using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets
{
	/// <summary>
	/// The named protocols implemented by this application's network handling.
	/// </summary>
	internal enum BoredWithFriendsProtocol : short
	{
		General,
		MatchFour
	}

	internal static class BoredWithFriendsProtocolExtension
	{
		public static int RequiredPlayerCount(this BoredWithFriendsProtocol protocol)
		{
			switch (protocol)
			{
				case BoredWithFriendsProtocol.General:
					return 0;
				case BoredWithFriendsProtocol.MatchFour:
					return 2;
				default:
					throw new NotImplementedException();
			}
		}

		//This approach is likely going to be removed in the future...
		public static bool TryCreateNewGame(this BoredWithFriendsProtocol protocol, LobbyGameState lobby, out GameState? game)
		{
			game = null;

			if (lobby.game != protocol)
			{
				throw new InvalidOperationException($"The given lobby is not meant for this {nameof(BoredWithFriendsProtocol)}.");
			}

			if (!lobby.CanStartGame())
			{
				return false;
			}

			switch (protocol)
			{
				case BoredWithFriendsProtocol.General:
					throw new InvalidOperationException($"No gamestate exists for the {nameof(BoredWithFriendsProtocol.General)} protocol.");
				case BoredWithFriendsProtocol.MatchFour:
					game = new MatchFourGameState(6, 7, lobby.GetNextPlayer(), lobby.GetNextPlayer());
					return true;
				default:
					throw new NotImplementedException();
			}
		}
	}
}
