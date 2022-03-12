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
		/// A publisher of <see cref="GeneralEvent"/> triggers. When a general event
		/// occurs, a subscriber can decide to do something about it.
		/// </summary>
		public static event EventHandler<GeneralEvent>? GeneralEvents;

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
		/// Raises an event of the given <paramref name="eventType"/>.
		/// </summary>
		/// <param name="eventType">The type of event being raised.</param>
		/// <param name="sender">The triggering object of this event.</param>
		public static void RaiseEvent(GeneralEvent eventType, object? sender = null)
		{
			EventHandler<GeneralEvent>? eventHandler = GeneralEvents;

			if (eventHandler is not null)
			{
				eventHandler(sender, eventType);
			}
		}

		/// <summary>
		/// Stops the client network handling, and raises the <see cref="GeneralEvent.ClientStopped"/>
		/// event so subscribers know.
		/// </summary>
		public static void StopClient(object? sender = null)
		{
			if (clientNetworkHandler is not null)
			{
				clientNetworkHandler.Stop();
			}

			RaiseEvent(GeneralEvent.ClientStopped, sender);
		}
	}

	/// <summary>
	/// A named general event that can be raised by <see cref="Client.RaiseEvent"/>.
	/// </summary>
	internal enum GeneralEvent : byte
	{
		/// <summary>
		/// An event that is raised when the client has connected to the server and is ready
		/// to send credentials.
		/// <br></br><br></br>
		/// Subscribers are responsible for calling <see cref="PacketSendUtility.SendPacket(Packets.ClientPacket)"/>
		/// with an instance of <see cref="Packets.General.Client.ClientLogin"/>.
		/// </summary>
		LoginReady,

		/// <summary>
		/// An event that is raised when the client has failed to login due to invalid credentials.
		/// <br></br><br></br>
		/// The sending object will be a <see cref="LoginResult"/> built from the packet
		/// the server sent.
		/// </summary>
		LoginFailedInvalidCredentials,

		/// <summary>
		/// An event that is raised when the client has failed to login due to the username for a new
		/// account being unavailable.
		/// <br></br><br></br>
		/// The sending object will be a <see cref="LoginResult"/> built from the packet
		/// the server sent.
		/// </summary>
		LoginFailedUsernameNotAvailable,

		/// <summary>
		/// An event that is raised when the client has logged in successfully. The sending
		/// object will be a <see cref="LoginResult"/> built from the packet
		/// the server sent.
		/// </summary>
		LoggedIn,

		/// <summary>
		/// An event that is raised when the client has attempted to open Account Management
		/// and has received their account information from the server.
		/// <br></br><br></br>
		/// The sending object will be the <see cref="Models.PlayerStatistics"/> built from the
		/// packet the server sent.
		/// </summary>
		AccountManagementReady,

		/// <summary>
		/// An event that is raised when the client has attempted to update their password,
		/// and the server has responded with the result.
		/// <br></br><br></br>
		/// The sending object will be a bool where a true value indicates that the password
		/// has updated successfully, and a false value indicates that the update was not
		/// accepted.
		/// </summary>
		PasswordUpdateResultReceived,

		/// <summary>
		/// An event that is raised when the client has attempted to delete their account, and
		/// the server has responded with the result.
		/// <br></br><br></br>
		/// The sending object will be a bool where a true value indicates that the account has
		/// been deleted, and a false value indicates that the account was not deleted.
		/// </summary>
		AccountDeletionResult,

		/// <summary>
		/// An event that is raised when the client has attempted to join an online game, and
		/// the server did not have any other players to match them with. This indicates
		/// that the client has to wait for another player to join in order to begin playing.
		/// <br></br><br></br>
		/// The sending object will be a <see cref="Packets.BoredWithFriendsProtocol"/> value
		/// indicating the selected game.
		/// </summary>
		EnteredGameLobby,

		/// <summary>
		/// An event that is raised when the client connection to the server has been terminated.
		/// </summary>
		ClientStopped
	}
}
