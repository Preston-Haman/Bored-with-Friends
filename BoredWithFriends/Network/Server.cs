using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	/// <summary>
	/// Holds state information for when this application is running as a server.
	/// That also includes when this application is running in a local setting.
	/// </summary>
	internal static class Server
	{
		/// <summary>
		/// The active player connections mapped by Player.
		/// </summary>
		private static readonly Dictionary<Player, PlayerConnection> playerConnections = new();

		/// <summary>
		/// The active games mapped by Player.
		/// </summary>
		private static readonly Dictionary<Player, GameState> playerGames = new();

		/// <summary>
		/// The network handler in charge of sending and receiving packets.
		/// </summary>
		private static NetworkHandler serverNetworkHandler = null!;

		/// <summary>
		/// If the server has started or not.
		/// </summary>
		private static bool isStarted = false;

		/// <summary>
		/// If the server is running in a local context or not.
		/// </summary>
		private static bool isLocal = false;

		/// <summary>
		/// The currently active GameState for this application.
		/// </summary>
		public static GameState? LocalGameState { get; set; }

		/// <summary>
		/// Starts the server functionality of this application with the given network handler.
		/// </summary>
		/// <param name="handler"></param>
		public static void StartServer(NetworkHandler handler)
		{
			if (!isStarted)
			{
				isLocal = handler is LocalNetworkHandler;

				serverNetworkHandler = handler;
				serverNetworkHandler.Start();

				PacketSendUtility.NetHandler = handler;

				isStarted = true;
			}
		}

		/// <summary>
		/// Removes the given inactive <paramref name="con"/> from <see cref="playerConnections"/>,
		/// and <see cref="playerGames"/>.
		/// </summary>
		/// <param name="con">The connection that is no longer active.</param>
		public static void PlayerDisconnected(PlayerConnection con)
		{
			playerConnections.Remove(con.Player);
			playerGames.Remove(con.Player);
		}

		/// <summary>
		/// Adds the given <paramref name="con"/> to <see cref="playerConnections"/>.
		/// </summary>
		/// <param name="con">The connection to register with this server.</param>
		public static void AddPlayerConnection(PlayerConnection con)
		{
			playerConnections.Add(con.Player, con);
		}

		/// <summary>
		/// Adds the given <paramref name="gameState"/> to <see cref="playerGames"/>.
		/// </summary>
		/// <param name="gameState">The game to register with this server.</param>
		public static void RegisterGameState(GameState gameState)
		{
			foreach (Player p in gameState.Players)
			{
				playerGames.Add(p, gameState);
			}
		}

		/// <summary>
		/// Registers the given <paramref name="gameState"/> to the given <paramref name="player"/>
		/// in <see cref="playerGames"/>.
		/// </summary>
		/// <param name="player">The player of interest.</param>
		/// <param name="gameState">The game the given <paramref name="player"/> is a part of.</param>
		public static void RegisterGameState(Player player, GameState gameState)
		{
			playerGames.Add(player, gameState);
		}

		/// <summary>
		/// Returns the registered gamestate for the given player.
		/// If this server is running in a local context, then the <see cref="LocalGameState"/>
		/// is returned.
		/// </summary>
		/// <param name="player">The player taking part in the game of interest.</param>
		/// <returns>The gamestate the given <paramref name="player"/> is a part of.</returns>
		/// <exception cref="ArgumentException">If the player has no registered gamestate.</exception>
		public static GameState GetGameState(Player player)
		{
			if (isLocal)
			{
				if (LocalGameState is not null)
				{
					return LocalGameState;
				}
			}
			else if (playerGames.TryGetValue(player, out GameState? gameState))
			{
				return gameState;
			}
			throw new ArgumentException($"No GameState exists for the given {nameof(player)}.");
		}

		/// <summary>
		/// Returns the registered connection for the given player from <see cref="playerConnections"/>.
		/// </summary>
		/// <param name="player">The player to get the connection for.</param>
		/// <returns>The connection for the given <paramref name="player"/>.</returns>
		/// <exception cref="ArgumentException">If the given player has no registered connection</exception>
		public static PlayerConnection GetPlayerConnection(Player player)
		{
			if (playerConnections.TryGetValue(player, out PlayerConnection? con))
			{
				return con;
			}
			throw new ArgumentException($"No such {nameof(player)} has been registered.");
		}

		/// <summary>
		/// Calls <see cref="GetPlayerConnection(Player)"/> and returns the connection
		/// cast as a <see cref="LocalConnection"/>.
		/// </summary>
		/// <param name="player">The player to get the connection for.</param>
		/// <returns>The <see cref="LocalConnection"/> for the given <paramref name="player"/>.</returns>
		/// <exception cref="ArgumentException">If the player does not have a <see cref="LocalConnection"/>
		/// registered.</exception>
		public static LocalConnection GetLocalConnection(Player player)
		{
			if (GetPlayerConnection(player) is LocalConnection con)
			{
				return con;
			}
			throw new ArgumentException($"No such {nameof(LocalConnection)} for the {nameof(player)} has been registered.");
		}
	}
}
