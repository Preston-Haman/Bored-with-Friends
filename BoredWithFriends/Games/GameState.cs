using BoredWithFriends.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Games
{
	/// <summary>
	/// GameStates represent an entire Game that can be played by Players. Each game is
	/// different, so this abstract class merely outlines things that are common to most games.
	/// </summary>
	internal abstract class GameState
	{
		/// <summary>
		/// Users of this gamestate must provide an implementation of this interface so changes
		/// to the gamestate can be reflected upon the GUI.
		/// </summary>
		protected readonly IGeneralGameGui generalGameGui;

		/// <summary>
		/// The bool value for <see cref="GameHasStarted"/>.
		/// </summary>
		private bool gameHasStarted = false;

		/// <summary>
		/// The bool value for <see cref="GameHasEnded"/>.
		/// </summary>
		private bool gameHasEnded = false;

		/// <summary>
		/// The list of players involved in competition with this game.
		/// </summary>
		public List<Player> Players { get; } = new();

		/// <summary>
		/// The list of players that are spectating this game.
		/// </summary>
		public List<Player> Spectators { get; } = new();

		/// <summary>
		/// A list containing Players who have won this game.
		/// This list will not be populated before the game ends.
		/// </summary>
		public List<Player> Winners { get; } = new();

		/// <summary>
		/// A list containing Players who have lost this game.
		/// It's possible that players may end up in this list before the game
		/// ends.
		/// </summary>
		public List<Player> Losers { get; } = new();

		/// <summary>
		/// If this game has started, then this should be true.
		/// </summary>
		protected bool GameHasStarted
		{
			get { return gameHasStarted; }

			set
			{
				gameHasStarted = value;
				try
				{
					Player currentPlayer = GetCurrentPlayer();
					generalGameGui.UpdateActivePlayer(this, currentPlayer);
				}
				catch (Exception)
				{
					//Ignore; this game doesn't have turns.
				}
			}
		}

		/// <summary>
		/// If this game is over, then this should be true.
		/// </summary>
		public bool GameHasEnded
		{
			get { return gameHasEnded; }

			protected set
			{
				gameHasEnded = value;
				if (gameHasEnded)
				{
					generalGameGui.DeclareGameEnd(this);
				}
			}
		}

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
		/// Creates a new gamestate with the specified GUI handler.
		/// </summary>
		/// <param name="generalGameGUI">The general GUI handler for this game.</param>
		public GameState(IGeneralGameGui generalGameGui)
		{
			this.generalGameGui = generalGameGui;
			generalGameGui.GameStateChanged(this);
		}

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
				generalGameGui.AddPlayer(this, p);
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
				generalGameGui.AddPlayer(this, p, true);
			}
		}

		/// <summary>
		/// Iterates the list of <see cref="Players"/> and <see cref="Spectators"/> to find the
		/// <see cref="Player"/> with the given <paramref name="playerID"/> and returns it.
		/// </summary>
		/// <param name="playerID">The PlayerID of the player to retrieve.</param>
		/// <returns>The <see cref="Player"/> with the given <paramref name="playerID"/>.</returns>
		/// <exception cref="ArgumentException">If there is no such player.</exception>
		public virtual Player GetPlayerByID(int playerID, out bool isSpectator)
		{
			//Check competing players.
			foreach (Player player in Players)
			{
				if (player.PlayerID == playerID)
				{
					isSpectator = false;
					return player;
				}
			}

			//Check spectators
			foreach (Player player in Spectators)
			{
				if (player.PlayerID == playerID)
				{
					isSpectator = true;
					return player;
				}
			}

			throw new ArgumentException("No such player is registered with this gamestate.");
		}

		/// <summary>
		/// Removes the given player from the gamestate.
		/// <br></br><br></br>
		/// Subclasses should override this method with an implementation that
		/// forfeits the player when necessary. The default implementation only
		/// removes the given <paramref name="player"/> from either
		/// <see cref="Players"/> or <see cref="Spectators"/> based on the value
		/// of <paramref name="spectator"/>.
		/// </summary>
		/// <param name="player">The <see cref="Player"/> leaving the game.</param>
		/// <param name="spectator">Whether or not <paramref name="player"/> is a spectator.</param>
		public virtual void PlayerLeaves(Player player, bool spectator = false)
		{
			//Might have to do this based on how Remove works...
			//player = GetPlayerByID(player.PlayerID);

			if (spectator)
			{
				Players.Remove(player);
			}
			else
			{
				Spectators.Remove(player);
			}

			generalGameGui.RemovePlayer(this, player, spectator);
		}

		/// <summary>
		/// Retrieves the player with the given <paramref name="playerID"/> and calls
		/// <see cref="PlayerForfeit(Player)"/>.
		/// </summary>
		/// <param name="playerID">The ID of the player who wishes to give up.</param>
		public void PlayerForfeit(int playerID)
		{
			PlayerForfeit(GetPlayerByID(playerID, out _));
		}

		/// <summary>
		/// Sets this player as a loser of the game by player choice.
		/// <br></br><br></br>
		/// If a subclass allows for Player teams, or supports more than one competing player,
		/// it must override this method to determine if this forfeiture ends the game.
		/// </summary>
		/// <param name="player">The player who wishes to give up.</param>
		protected virtual void PlayerForfeit(Player player)
		{
			if (GameHasEnded)
			{
				return;
			}

			//TODO: This logic could be better...
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
				foreach (Player winner in players)
				{
					PlayerWins(winner);
				}
				//The game is over!
				GameHasEnded = true;
			}
		}

		/// <summary>
		/// Retrieves the player with the given <paramref name="playerID"/> and calls
		/// <see cref="PlayerWins(Player)"/>.
		/// </summary>
		/// <param name="playerID">The player ID of a player who has won this game.</param>
		protected void PlayerWins(int playerID)
		{
			PlayerWins(GetPlayerByID(playerID, out _));
		}

		/// <summary>
		/// Sets this player as a winner of the game. Players win when they
		/// makes a move that ends the game in their favour, or if there are no
		/// other competing players left to challenge them.
		/// 
		/// <br></br><br></br>
		/// For a list of winners, see <seealso cref="Winners"/>.
		/// </summary>
		/// <param name="player">A player who has won this game.</param>
		protected virtual void PlayerWins(Player player)
		{
			Winners.Add(player);
			if (!player.IsGuest)
			{
				DatabaseContext.AddWin(player);
			}
		}

		/// <summary>
		/// Retrieves the player with the given <paramref name="playerID"/> and calls
		/// <see cref="PlayerLoses(Player)"/>.
		/// </summary>
		/// <param name="playerID"></param>
		protected void PlayerLoses(int playerID)
		{
			PlayerLoses(GetPlayerByID(playerID, out _));
		}

		/// <summary>
		/// Sets this player as a loser of the game. Players lose upon forfeiture
		/// (see <see cref="PlayerForfeit"/>), or when another player has ended
		/// the game in their favour.
		/// 
		/// <br></br><br></br>
		/// For a list of losers, see <seealso cref="Losers"/>.
		/// </summary>
		/// <param name="player">A player who has lost this game.</param>
		protected virtual void PlayerLoses(Player player)
		{
			Losers.Add(player);
			if (!player.IsGuest)
			{
				DatabaseContext.AddLoss(player);
			}
		}

		/// <summary>
		/// Returns this GameState to its original starting point as if the Game had just been
		/// created. The current players and spectators are kept.
		/// </summary>
		public abstract void ResetGame();

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
		public abstract Player GetCurrentPlayer();
	}
}
