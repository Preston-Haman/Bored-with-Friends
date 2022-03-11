namespace BoredWithFriends.Games
{
	/// <summary>
	/// An interface of callback methods intended for the GUI consumer of a <see cref="GameState"/>.
	/// <br></br><br></br>
	/// The methods of this interface are as follows:<br></br>
	/// <list type="bullet"><see cref="AddPlayer"/></list>
	/// <list type="bullet"><see cref="RemovePlayer"/></list>
	/// <list type="bullet"><see cref="UpdateActivePlayer"/></list>
	/// <list type="bullet"><see cref="DeclareGameEnd"/></list>
	/// </summary>
	internal interface IGeneralGameGui
	{
		/// <summary>
		/// Called when a player has been added to a gamestate; implementors may use
		/// this method to display the player addition on the GUI.
		/// </summary>
		/// <param name="game">The game that has been affected by this change.</param>
		/// <param name="player">The player that has been added to the game.</param>
		/// <param name="spectator">Whether or not this player is a spectator.</param>
		public void AddPlayer(GameState game, Player player, bool spectator = false);

		/// <summary>
		/// Called when a player is no longer part of a gamestate. Implementors may
		/// use this method to display the player removal on the GUI.
		/// </summary>
		/// <param name="game">The game that has been affected by this change.</param>
		/// <param name="player">The player that is no longer part of the game.</param>
		/// <param name="spectator">Whether or not the removed player was a spectator.</param>
		public void RemovePlayer(GameState game, Player player, bool spectator = false);

		/// <summary>
		/// Called when a new player has become the active player. Implementors may
		/// use this method to display player turns. The active player is the player
		/// who is currently being allotted a turn.
		/// <br></br><br></br>
		/// For games that do not support turns, this method will never be called.
		/// </summary>
		/// <param name="game">The game that has been affected by this change.</param>
		/// <param name="player">The new active player.</param>
		public void UpdateActivePlayer(GameState game, Player player);

		/// <summary>
		/// Called when a game ends. Implementors may use this method to display
		/// any information necessary about the ending state of the game on the GUI.
		/// </summary>
		/// <param name="game">The game that has been affected by this change.</param>
		public void DeclareGameEnd(GameState game);
	}

	/// <summary>
	/// An empty implementation of <see cref="IGeneralGameGui"/> that does nothing.
	/// </summary>
	internal class NoGuiGame : IGeneralGameGui
	{
		public void AddPlayer(GameState game, Player player, bool spectator = false)
		{
			//Do nothing.
		}

		public void DeclareGameEnd(GameState game)
		{
			//Do nothing.
		}

		public void RemovePlayer(GameState game, Player player, bool spectator = false)
		{
			//Do nothing.
		}

		public void UpdateActivePlayer(GameState game, Player player)
		{
			//Do nothing.
		}
	}
}
