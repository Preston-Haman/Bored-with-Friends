using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	/// <summary>
	/// Holds state for when this application is running as a remote client.
	/// </summary>
	internal static class Client
	{
		/// <summary>
		/// The network handler responsible for sending/receiving packet information on the network.
		/// </summary>
		private static ClientNetworkHandler clientNetworkHandler = null!;

		/// <summary>
		/// The <see cref="Player"/> value for <see cref="ClientPlayer"/>.
		/// </summary>
		private static Player? clientPlayer = null;

		/// <summary>
		/// The current player that is logged in for this remote client.
		/// </summary>
		public static Player ClientPlayer //Remember to set from login packet
		{
			get
			{
				if (clientPlayer is null)
				{
					return LocalGameState!.GetCurrentPlayer();
				}
				return clientPlayer;
			}

			set
			{
				clientPlayer = value;
			}
		}

		/// <summary>
		/// The local game state for this client; it should be in an identical state
		/// to the server side game state.
		/// </summary>
		public static GameState? LocalGameState
		{
			//Our local state represents the state on the server side
			//The code is written like this for the sake of local gameplay
			get
			{
				return Server.LocalGameState;
			}

			set
			{
				Server.LocalGameState = value;
			}
		}

		/// <summary>
		/// The entry point for running this application as a remote client.
		/// </summary>
		/// <param name="serverIP">The IP address of the remote server.</param>
		/// <param name="port">The port of the remote server to connect to.</param>
		public static void StartClient(string serverIP = "127.0.0.1", int port = 7777)
		{
			if (clientNetworkHandler is null)
			{
				clientNetworkHandler = new();
				PacketSendUtility.NetHandler = clientNetworkHandler;
			}

			clientNetworkHandler.ConnectToServer(serverIP, port);
		}

		public static void StopClient()
		{
			if (clientNetworkHandler is not null)
			{
				clientNetworkHandler.Stop();
			}
		}
	}
}
