using BoredWithFriends.Games;
using BoredWithFriends.Network.Packets.General.Server;
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
		/// An event that is raised when the client has connected to the server and is ready
		/// to send credentials.
		/// <br></br><br></br>
		/// Subscribers are responsible for calling <see cref="PacketSendUtility.SendPacket(Packets.ClientPacket)"/>
		/// with an instance of <see cref="Packets.General.Client.ClientLogin"/>.
		/// </summary>
		public static event EventHandler? LoginReadyEvent;

		/// <summary>
		/// An event that is raised when the client connection to the server has been terminated.
		/// </summary>
		public static event EventHandler? ClientStoppedEvent;

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
				if (clientNetworkHandler is not null)
				{
					clientPlayer = value;
				}
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

		/// <summary>
		/// Raises the <see cref="LoginReadyEvent"/> so subscribers may send the
		/// login packet with credentials.
		/// </summary>
		/// <param name="serverConnectedPacket"></param>
		public static void RaiseLoginReadyEvent(ServerConnected serverConnectedPacket)
		{
			EventHandler? loginReady = LoginReadyEvent;

			if (loginReady is not null)
			{
				loginReady(serverConnectedPacket, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Raises the <see cref="ClientStoppedEvent"/> so subscribers may
		/// do something about it.
		/// </summary>
		/// <param name="sender">The optional object that is raising this event.</param>
		public static void RaiseClientStoppedEvent(object? sender = null)
		{
			EventHandler? clientStopped = ClientStoppedEvent;

			if (clientStopped is not null)
			{
				clientStopped(sender, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Stops the client network handling, and raises the <see cref="ClientStoppedEvent"/> so subscribers know.
		/// </summary>
		public static void StopClient()
		{
			if (clientNetworkHandler is not null)
			{
				clientNetworkHandler.Stop();
			}

			RaiseClientStoppedEvent();
		}
	}
}
