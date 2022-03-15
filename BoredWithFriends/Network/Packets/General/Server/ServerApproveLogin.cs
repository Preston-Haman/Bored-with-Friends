using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Sent by the server in response to <see cref="Client.ClientLogin"/>.
	/// </summary>
	[Packet(typeof(ServerApproveLogin), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerApproveLogin)]
	internal class ServerApproveLogin : ServerPacket
	{
		private bool createNew;

		private bool approved;

		private int playerID;

		private string username;

		public ServerApproveLogin(bool approved, int playerID = -1, string username = "", bool createNew = false)
		{
			this.createNew = createNew;
			this.approved = approved;
			this.playerID = playerID;
			this.username = username;
		}

		protected override void ReadImpl()
		{
			createNew = ReadBool();
			approved = ReadBool();
			playerID = ReadInt();
			username = ReadString();
		}

		protected override void RunImpl()
		{
			if (approved)
			{
				Network.Client.ClientPlayer = new Player(playerID, username);
				Network.Client.RaiseEvent(GeneralEvent.LoggedIn, new LoginResult(approved, playerID, username));
			}
			else
			{
				Network.Client.RaiseEvent(
					createNew ? GeneralEvent.LoginFailedUsernameNotAvailable
							  : GeneralEvent.LoginFailedInvalidCredentials, new LoginResult(approved, playerID, username));
			}
		}

		protected override void WriteImpl()
		{
			WriteBool(createNew);
			WriteBool(approved);
			WriteInt(playerID);
			WriteString(username);
		}
	}

	/// <summary>
	/// The result of attempting to login to the server.
	/// </summary>
	internal class LoginResult
	{
		public bool Approved { get; }

		private readonly int playerID;

		private readonly string username;

		public LoginResult(bool approved, int playerID, string username)
		{
			Approved = approved;
			this.playerID = playerID;
			this.username = username;
		}

		public string Username
		{
			get
			{
				if (Approved)
				{
					return username;
				}

				throw new InvalidOperationException("This Login was not Approved.");
			}
		}

		public int PlayerID
		{
			get
			{
				if (Approved)
				{
					return playerID;
				}

				throw new InvalidOperationException("This Login was not Approved.");
			}
		}
	}
}
