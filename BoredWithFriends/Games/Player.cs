namespace BoredWithFriends.Games
{
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
		public TurnBasedPlayer(string name) : base(name)
		{
			//Nothing to do for now.
		}

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
	/// The most basic representation of a Player. This class holds a reference to the player's
	/// username and their ID -- both from the database (except when the player is a guest).
	/// </summary>
	internal class Player
	{
		/// <summary>
		/// This is just a blank object that can be locked on; it will be locked
		/// on when <see cref="nextGuestID"/> is being decremented.
		/// </summary>
		private static readonly object lockableObject = new();

		/// <summary>
		/// The next ID to use for guest players. When a guest player is created,
		/// this will be decremented so guest ID's can be unique.
		/// </summary>
		private static int nextGuestID = -1;

		/// <summary>
		/// The player's PlayerID from the database; if this is set to a negative value,
		/// then this player is a guest (not stored in the database).
		/// </summary>
		public int PlayerID { get; }

		/// <summary>
		/// The username of the player in the database. This name is unique to this player
		/// so long as they are not a guest. When set as a guest, the player's name should
		/// still be unique; but it will not be enforced by the database.
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Checks if this Player is a guest or not. Guests are marked with negative ID values,
		/// but they are still unique.
		/// </summary>
		public bool IsGuest
		{
			get
			{
				return PlayerID < 0;
			}
		}

		/// <summary>
		/// Creates a new player with the specified name as a guest.
		/// </summary>
		/// <param name="name">The name to display for this guest player.</param>
		public Player(string name)
		{
			lock (lockableObject)
			{
				PlayerID = nextGuestID--;
				Name = name;
			}
		}

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

		/// <summary>
		/// This method returns the <see cref="PlayerID"/> which uniquely identifies this player.
		/// </summary>
		/// <returns>The <see cref="PlayerID"/>.</returns>
		public override int GetHashCode()
		{
			return PlayerID;
		}

		public override bool Equals(object? obj)
		{
			//TODO: Not sure if I have to override the == operator or not
			if (obj is Player player)
			{
				//The names should match, too; but this is the only
				//defined way for players to be equal.
				return PlayerID == player.PlayerID;
			}
			return false;
		}
	}
}
