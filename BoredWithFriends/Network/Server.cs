﻿using BoredWithFriends.Games;
using BoredWithFriends.Network.Packets;
using BoredWithFriends.Network.Packets.General.Server;
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
		/// A set of <see cref="LobbyGameState"/>s mapped by the game they are for.
		/// </summary>
		private static readonly Dictionary<BoredWithFriendsProtocol, LobbyGameState> lobbies = new();

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
		/// The value for <see cref="LocalGameState"/>.
		/// </summary>
		private static GameState? localGameState;

		/// <summary>
		/// The currently active GameState for this application.
		/// </summary>
		public static GameState? LocalGameState
		{
			get
			{
				return localGameState;
			}

			set
			{
				localGameState = value;
				if (value is not null)
				{
					RegisterGameState(value);
				}
			}
		}

		/// <summary>
		/// Starts the server functionality of this application with the given network handler.
		/// </summary>
		/// <param name="handler"></param>
		public static void StartServer(bool online = false)
		{
			NetworkHandler handler = online ? new ServerNetworkHandler() : new LocalNetworkHandler();

			if (!isStarted)
			{
				isLocal = !online;

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
			if (playerGames.TryGetValue(con.Player, out GameState? game))
			{
				Player player = game.GetPlayerByID(con.PlayerID, out bool isSpectator);
				game.PlayerLeaves(player, isSpectator);
			}
			playerGames.Remove(con.Player);
		}

		/// <summary>
		/// Adds the given <paramref name="con"/> to <see cref="playerConnections"/>,
		/// then to <see cref="serverNetworkHandler"/> if it is a <see cref="ServerNetworkHandler"/>.
		/// </summary>
		/// <param name="con">The connection to register with this server.</param>
		public static void AddPlayerConnection(PlayerConnection con)
		{
			playerConnections.Add(con.Player, con);

			if (serverNetworkHandler is ServerNetworkHandler serverHandler)
			{
				serverHandler.AddPlayerConnection(con);
			}
		}

		/// <summary>
		/// If <paramref name="con"/> is of type <see cref="ClientConnection"/>, then
		/// the underlying TcpClient is adopted (see <seealso cref="ClientConnection.AdoptClient"/>)
		/// into a new <see cref="PlayerConnection"/> for <paramref name="player"/>, which is then returned.
		/// </summary>
		/// <param name="con">A possible <see cref="ClientConnection"/> that the given <paramref name="player"/>
		/// is using.</param>
		/// <param name="player">The player using the given <paramref name="con"/>.</param>
		public static void AuthConnectionAsPlayer(Connection con, Player player)
		{
			if (con is ClientConnection ccon)
			{
				PlayerConnection pcon = isLocal ? new LocalConnection(player) : new PlayerConnection(ccon.AdoptClient(), player);
				pcon.SetConnectionState(ConnectionState.Authed);
				AddPlayerConnection(pcon);
			}
		}

		/// <summary>
		/// Adds the given <paramref name="player"/> to the lobby of the game matching the
		/// <paramref name="gameChoice"/> protocol.
		/// </summary>
		/// <param name="player">The player to add to the lobby.</param>
		/// <param name="gameChoice">The protocol of the player's selected game.</param>
		public static void RegisterPlayerForLobby(Player player, BoredWithFriendsProtocol gameChoice)
		{
			if (!lobbies.ContainsKey(gameChoice))
			{
				lobbies.Add(gameChoice, new LobbyGameState(gameChoice, new NoGuiGame()));
			}

			lobbies.TryGetValue(gameChoice, out LobbyGameState? lobby);

			lobby!.QueuePlayer(player);
			if (gameChoice.TryCreateNewGame(lobby!, out GameState? newGame))
			{
				RegisterGameState(newGame!);
				PacketSendUtility.BroadcastPacket(player, new ServerStartGame(gameChoice, newGame!.Players));
			}
			else if (isLocal)
			{
				PacketSendUtility.SendPacket(player, new ServerStartGame(gameChoice, new List<Player>() { player }));
			}
			else
			{
				PacketSendUtility.SendPacket(player, new ServerEnterGameLobby(lobby));
			}
		}

		/// <summary>
		/// Adds the given <paramref name="gameState"/> to <see cref="playerGames"/>.
		/// </summary>
		/// <param name="gameState">The game to register with this server.</param>
		public static void RegisterGameState(GameState gameState)
		{
			foreach (Player p in gameState.Players)
			{
				if (playerGames.ContainsKey(p))
				{
					_ = playerGames.Remove(p);
				}
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
			if (playerGames.ContainsKey(player))
			{
				_ = playerGames.Remove(player);
			}
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

			if (isLocal)
			{
				LocalConnection lcon = new(player);
				AddPlayerConnection(lcon);
				return lcon;
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
