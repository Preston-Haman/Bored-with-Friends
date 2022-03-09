using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	internal static class Server
	{
		private static readonly Dictionary<Player, PlayerConnection> playerConnections = new();

		private static readonly Dictionary<Player, GameState> playerGames = new();

		private static readonly Dictionary<Player, GameState> spectatorGames = new();

		private static NetworkHandler serverNetworkHandler = null!;

		private static bool isStarted = false;

		private static bool isLocal = false;

		public static GameState? LocalGameState { get; set; }

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

		public static void PlayerDisconnected(PlayerConnection con)
		{
			playerConnections.Remove(con.Player);
			playerGames.Remove(con.Player);
			spectatorGames.Remove(con.Player);
		}

		public static void AddPlayerConnection(PlayerConnection con)
		{
			playerConnections.Add(con.Player, con);
		}

		public static void RegisterGameState(GameState gameState)
		{
			foreach (Player p in gameState.Players)
			{
				playerGames.Add(p, gameState);
			}
		}

		public static void RegisterGameState(Player player, GameState gameState)
		{
			playerGames.Add(player, gameState);
		}

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

		public static PlayerConnection GetPlayerConnection(Player player)
		{
			if (playerConnections.TryGetValue(player, out PlayerConnection? con))
			{
				return con;
			}
			throw new ArgumentException($"No such {nameof(player)} has been registered.");
		}

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
