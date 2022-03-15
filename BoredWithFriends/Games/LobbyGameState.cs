using BoredWithFriends.Network.Packets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Games
{
	internal class LobbyGameState : GameState
	{
		public readonly BoredWithFriendsProtocol game;

		public readonly ConcurrentQueue<Player> players = new();

		public LobbyGameState(BoredWithFriendsProtocol game, IGeneralGameGui generalGameGui) : base(generalGameGui)
		{
			this.game = game;
		}

		public void QueuePlayer(Player player)
		{
			players.Enqueue(player);
			AddCompetingPlayers(player);
		}

		public Player GetNextPlayer()
		{
			if (players.TryDequeue(out Player? player))
			{
				return player;
			}
			throw new InvalidOperationException("There are no players waiting in this lobby.");
		}

		public bool CanStartGame()
		{
			return game.RequiredPlayerCount() <= players.Count;
		}

		public override void PlayerLeaves(Player player, bool spectator = false)
		{
			base.PlayerLeaves(player, spectator);

			//Might need a different approach...
			lock (players)
			{
				for (int i = 0; i < players.Count; i++)
				{
					if (players.TryDequeue(out Player? p)) {
						if (p != player)
						{
							players.Enqueue(p);
						}
					}
				}
			}
		}

		public override Player GetCurrentPlayer()
		{
			throw new NotImplementedException();
		}

		public override int GetMaxPlayers()
		{
			//Arbitrary value I never expect to hit.
			return short.MaxValue;
		}

		public override void ResetGame()
		{
			//Nothing to reset.
		}
	}
}
