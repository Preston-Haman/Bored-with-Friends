using BoredWithFriends.Games;
using BoredWithFriends.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	internal static class PacketSendUtility
	{

		public static NetworkHandler NetHandler { get; set; } = null!;

		public static void SendPacket(ClientPacket packet)
		{
			if (NetHandler is LocalNetworkHandler)
			{
				SendLocalPacket(packet);
				return;
			}

			if (NetHandler is ClientNetworkHandler client)
			{
				client.SendPacket(client.Connection!, packet);
				return;
			}

			throw new InvalidOperationException("Cannot send packets to a server while operating as a server.");
		}

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

		private static void SendLocalPacket(BasePacket packet)
		{
			NetHandler.SendPacket(Server.GetLocalConnection(Server.LocalGameState!.getCurrentPlayer()), packet);
		}
	}
}
