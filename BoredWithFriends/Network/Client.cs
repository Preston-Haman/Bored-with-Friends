using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	internal static class Client
	{
		private static ClientNetworkHandler clientNetworkHandler = null!;

		public static Player ClientPlayer { get; set; } = null!; //Remember to set from login packet

		public static GameState? LocalGameState { get; set; }

		public static void StartClient(string serverIP = "127.0.0.1", int port = 7777)
		{
			if (clientNetworkHandler is null)
			{
				clientNetworkHandler = new();
				PacketSendUtility.NetHandler = clientNetworkHandler;
			}

			clientNetworkHandler.ConnectToServer(serverIP, port);
		}
	}
}
