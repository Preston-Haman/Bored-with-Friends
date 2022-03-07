using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Games
{
	/// <summary>
	/// The most basic representation of a Player. This class holds a reference to the player's
	/// username and their ID -- both from the database -- except when the player is a guest.
	/// </summary>
	internal class Player
	{
		/// <summary>
		/// The player's PlayerID from the database; if this is set to -1,
		/// then this player is a guest (not stored in the database).
		/// </summary>
		public int PlayerID { get; } = -1;

		/// <summary>
		/// The username of the player in the database. This name is unique to this player
		/// so long as they are not a guest. When set as a guest, the player's name should
		/// still be unique; but it will not be enforced by the database.
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Creates a simple Player with the specified PlayerID and username. These values
		/// should come from the database. If the player is a guest, then the PlayerID should
		/// be set as -1.
		/// </summary>
		/// <param name="playerID">The PlayerID from the database, or -1 for a guest.</param>
		/// <param name="name">The username of the player; this may display on screen.</param>
		public Player(int playerID, string name)
		{
			PlayerID = playerID;
			Name = name;
		}

		/// <summary>
		/// Returns this player's username for display.
		/// </summary>
		/// <returns>The player's username for display.</returns>
		public override string ToString()
		{
			return Name;
		}
	}

	/// <summary>
	/// A simple Player class that maintains a boolean value for tracking the player's turn.
	/// </summary>
	internal class TurnBasedPlayer : Player
	{
		/// <summary>
		/// For turn based games, this will be true when the player may perform an action;
		/// otherwise, it will be false. A false value indicates that the player should not
		/// be allowed to perform input on the current game state.
		/// </summary>
		public bool IsPlayerTurn { get; set; }

		/// <inheritdoc/>
		public TurnBasedPlayer(int playerID, string name) : base(playerID, name)
		{
			//Nothing else to do for now.
		}

		public TurnBasedPlayer(Player player) : base(player.PlayerID, player.Name)
		{
			//Nothing else to do for now.
		}
	}

	/// <summary>
	/// GameStates represent an entire Game that can be played by Players. Each game is
	/// different, so this absract class merely outlines things that are common to most games.
	/// </summary>
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

		/// <summary>
		/// If this game is over, this will be true only if there was a winner.
		/// Games may end in a draw, so it's necessary to know if someone has won
		/// the game -- this property provides a convenient way to achieve this.
		/// </summary>
		public bool GameHasWinner
		{
			get
			{
				if (GameHasEnded && Winners.Count > 0)
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// A list containing Players who have won this game.
		/// This list will not be populated before the game ends.
		/// </summary>
		public List<Player> Winners { get; } = new();

		/// <summary>
		/// A list containined Players who have lost this game.
		/// It's possible that players may end up in this list before the game
		/// ends.
		/// </summary>
		public List<Player> Losers { get; } = new();

		/// <summary>
		/// Adds Players to this game as competitors (as opposed to a spectators).
		/// </summary>
		/// <param name="competingPlayers">A list of players to add to this game as competitors.</param>
		/// <exception cref="ArgumentException">If this game cannot accept any more players,</exception>
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

		/// <summary>
		/// Adds Players to this game as spectators. Note that the caller is responsible
		/// for preventing the addition of a competing player.
		/// </summary>
		/// <param name="players">A list of players to add to this game as spectators.</param>
		public virtual void AddSpectatingPlayers(params Player[] players)
		{
			foreach (Player p in players)
			{
				Spectators.Add(p);
			}
		}

		/// <summary>
		/// Sets this player as a loser of the game by player choice.
		/// 
		/// If a subclass of GameState allows for Player teams, it must override
		/// this method to determine if this forfeiture ends the game.
		/// </summary>
		/// <param name="player">The player who wishes to give up.</param>
		public virtual void PlayerForfeit(Player player)
		{
			//TODO: Determine if we should track forfeiting separately from losses.
			PlayerLoses(player);

			List<Player> players = new(Players.Count);
			foreach (Player p in Players)
			{
				if (p.PlayerID != player.PlayerID)
				{
					players.Add(p);
				}
			}

			if (players.Count < 2)
			{
				//The game is over!
				GameHasEnded = true;
				foreach (Player winner in players)
				{
					PlayerWins(winner);
				}
			}
		}

		/// <summary>
		/// Sets this player as a winner of the game. Players win when they
		/// makes a move that ends the game in their favour, or if there are no
		/// other competing players left to challenge them.
		/// 
		/// <br></br><br></br>
		/// For a list of winners, see <seealso cref="Winners"/>.
		/// </summary>
		/// <param name="player">A Player who has won this game.</param>
		protected virtual void PlayerWins(Player player)
		{
			Winners.Add(player);
			//Treating negative 1 as guest players
			if (player.PlayerID != -1)
			{
				//TODO: Call database code that adds a win to this player's stats.

			}
		}

		/// <summary>
		/// Sets this player as a loser of the game. Players lose upon forfeiture
		/// (see <see cref="PlayerForfeit"/>), or when another player has ended
		/// the game in their favour.
		/// 
		/// <br></br><br></br>
		/// For a list of losers, see <seealso cref="Losers"/>.
		/// </summary>
		/// <param name="player">A Player who has lost this game.</param>
		public virtual void PlayerLoses(Player player)
		{
			Losers.Add(player);
			//Treating negative 1 as guest players
			if (player.PlayerID != -1)
			{
				//TODO: Call database code that adds a loss to this player's stats.

			}
		}

		/// <summary>
		/// Returns the maximum number of competing players for this game.
		/// </summary>
		/// <returns>The maximum number of competing players</returns>
		public abstract int GetMaxPlayers();

		/// <summary>
		/// For turn based games, this will return the Player who is currently
		/// being allowed a turn. If the game in question does not provide for turns,
		/// then this method should throw an exception.
		/// </summary>
		/// <returns>The Player who is currently being allowed a turn.</returns>
		public abstract Player getCurrentPlayer();

	}
}
