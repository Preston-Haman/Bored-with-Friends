using BoredWithFriends.Games;
using BoredWithFriends.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	/// <summary>
	/// A simple utility class for sending outbound packets.
	/// </summary>
	internal static class PacketSendUtility
	{
		/// <summary>
		/// The <see cref="NetworkHandler"/> that packets will be transmitted on.
		/// </summary>
		public static NetworkHandler NetHandler { get; set; } = null!;

		/// <summary>
		/// Attempts to send the given <paramref name="packet"/> to a remote server
		/// in charge of the client version of this application.
		/// </summary>
		/// <param name="packet">The packet to send.</param>
		/// <exception cref="InvalidOperationException">If this application is not running as a client.</exception>
		public static void SendPacket(ClientPacket packet)
		{
			if (NetHandler is LocalNetworkHandler)
			{
				SendLocalPacket(packet);
				return;
			}

			if (NetHandler is ClientNetworkHandler client)
			{
				client.SendPacket(client.ServerCon!, packet);
				return;
			}

			throw new InvalidOperationException("Cannot send packets to a server while operating as a server.");
		}

		/// <summary>
		/// Attempts to send the given <paramref name="packet"/> to the specified <paramref name="player"/>.
		/// </summary>
		/// <param name="player">The player to send the <paramref name="packet"/> to.</param>
		/// <param name="packet">The packet to send to the <paramref name="player"/>.</param>
		/// <exception cref="InvalidOperationException">If this application is not running as a server.</exception>
		public static void SendPacket(Player player, ServerPacket packet)
		{
			if (NetHandler is LocalNetworkHandler)
			{
				SendLocalPacket(packet);
				return;
			}

			if (NetHandler is ServerNetworkHandler)
			{
				NetHandler.SendPacket(Server.GetPlayerConnection(player), packet);
				return;
			}

			throw new InvalidOperationException("Cannot send packets to a client while operating as a client.");
		}

		/// <summary>
		/// Attempts to send the given <paramref name="packet"/> out on the specified <paramref name="con"/>.
		/// </summary>
		/// <param name="con">The <see cref="Connection"/> to send the <paramref name="packet"/> out on.</param>
		/// <param name="packet">The packet to send out on <paramref name="con"/>.</param>
		/// <exception cref="InvalidOperationException">If this application is not running as a server.</exception>
		public static void SendPacket(Connection con, ServerPacket packet)
		{
			if (NetHandler is LocalNetworkHandler)
			{
				SendLocalPacket(packet);
				return;
			}

			if (NetHandler is ServerNetworkHandler)
			{
				NetHandler.SendPacket(con, packet);
				return;
			}

			throw new InvalidOperationException("Cannot send packets to a client while operating as a client.");
		}

		/// <summary>
		/// Attempts to send the given <paramref name="packet"/> to all players associated
		/// with the game that the given <paramref name="player"/> is taking part in.
		/// </summary>
		/// <param name="player">The player taking part in the game of interest.</param>
		/// <param name="packet">The packet to send to all players of the game <paramref name="player"/> is in.</param>
		/// <exception cref="InvalidOperationException"></exception>
		public static void BroadcastPacket(Player player, ServerPacket packet)
		{
			if (NetHandler is LocalNetworkHandler)
			{
				SendLocalPacket(packet);
				return;
			}

			if (NetHandler is ServerNetworkHandler)
			{
				GameState gameState = Server.GetGameState(player);

				//Send to all players competing in the game.
				foreach (Player p in gameState.Players)
				{
					NetHandler.SendPacket(Server.GetPlayerConnection(p), packet);
				}

				//Send to all spectators.
				foreach (Player p in gameState.Spectators)
				{
					NetHandler.SendPacket(Server.GetPlayerConnection(p), packet);
				}
				
				return;
			}

			throw new InvalidOperationException("Cannot send packets to a client while operating as a client.");
		}

		/// <summary>
		/// Uses a <see cref="LocalConnection"/> to "send" the packet out to the <see cref="NetHandler"/>;
		/// this will result in problems if the <see cref="NetHandler"/> is not a <see cref="LocalNetworkHandler"/>.
		/// </summary>
		/// <param name="packet">The packet to send.</param>
		private static void SendLocalPacket(BasePacket packet)
		{
			NetHandler.SendPacket(Server.GetLocalConnection(Server.LocalGameState!.GetCurrentPlayer()), packet);
		}
	}
}
