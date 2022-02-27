using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Games
{
	internal class Player
	{
		public int PlayerID { get; }

		public string Name { get; protected set; }

		public Player(int playerID, string name)
		{
			PlayerID = playerID;
			Name = name;
		}

		public override string ToString()
		{
			return Name;
		}
	}

	internal class TurnBasedPlayer : Player
	{
		public bool IsPlayerTurn { get; set; }

		public TurnBasedPlayer(int playerID, string name) : base(playerID, name)
		{
			//Nothing else to do for now.
		}
	}

	internal abstract class GameState
	{
		/// <summary>
		/// The list of players involved in competition with this game.
		/// </summary>
		public List<Player> Players { get; } = new();

		/// <summary>
		/// The list of players that are spectating this game.
		/// </summary>
		public List<Player> Spectators { get; } = new();

		/// <summary>
		/// If this game has started, then this should be true.
		/// </summary>
		protected bool GameHasStarted { get; set; } = false;

		/// <summary>
		/// If this game is over, then this should be true.
		/// </summary>
		public bool GameHasEnded { get; protected set; } = false;

		public bool GameHasWinner { get; protected set; } = false;

		public List<Player> Winners { get; } = new();

		public List<Player> Losers { get; } = new();

		protected virtual void AddCompetingPlayers(params Player[] competingPlayers)
		{
			if (competingPlayers.Length > GetMaxPlayers() - Players.Count)
			{
				throw new ArgumentException("Cannot add more than the maximum number of competing players.");
			}

			foreach (Player p in competingPlayers)
			{
				Players.Add(p);
			}
		}

		public virtual void AddSpectatingPlayer(Player player)
		{
			Spectators.Add(player);
		}

		public virtual void PlayerForfeit(Player player)
		{
			Losers.Add(player);
			//Treating negative 1 as guest players
			if (player.PlayerID > 0)
			{
				//TODO: Call database code that adds a loss to this player's stats.

			}
		}

		protected virtual void PlayerWins(Player player)
		{
			GameHasWinner = true;
			Winners.Add(player);
			//Treating negative 1 as guest players
			if (player.PlayerID > 0)
			{
				//TODO: Call database code that adds a win to this player's stats.

			}
		}

		public virtual void PlayerLoses(Player player)
		{
			Losers.Add(player);
			//Treating negative 1 as guest players
			if (player.PlayerID > 0)
			{
				//TODO: Call database code that adds a loss to this player's stats.

			}
		}

		/// <summary>
		/// Returns the maximum number of competing players for this game.
		/// </summary>
		/// <returns>The maximum number of competing players</returns>
		public abstract int GetMaxPlayers();

		public abstract Player getCurrentPlayer();

	}
}
