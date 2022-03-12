using BoredWithFriends.Network.Packets.General.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Client
{
	[Packet(typeof(ClientLogin), BoredWithFriendsProtocol.General, (short) GeneralOps.ClientLogin, ConnectionState.Handshook)]
	internal class ClientLogin : ClientPacket
	{
		private bool createNew;

		private string username;

		private byte[] encryptedPassword;

		private int key1, key2, key3;

		public ClientLogin(string username, string password, bool createNew = false)
		{
			this.createNew = createNew;
			this.username = username;
			encryptedPassword = PoorMansEncryption.Encrypt(password, out key1, out key2, out key3);
		}

		protected override void ReadImpl()
		{
			createNew = ReadBool();
			username = ReadString();
			encryptedPassword = Read(ReadShort());
			key1 = ReadInt();
			key2 = ReadInt();
			key3 = ReadInt();
		}

		protected override void RunImpl(Connection con)
		{
			string password = PoorMansEncryption.Decrypt(encryptedPassword, key1, key2, key3);
			throw new NotImplementedException();

			//TODO: check database for user credentials/create them if necessary

			bool validUser = false;
			if (validUser)
			{
				//TODO: Get player, run these:
				//Network.Server.AuthConnectionAsPlayer(con, player);
				//PacketSendUtility.SendPacket(player, new ServerApproveLogin(true, player.PlayerID, player.Name, createNew));
			}
			else
			{
				PacketSendUtility.SendPacket(con, new ServerApproveLogin(false, createNew: createNew));
			}
		}

		protected override void WriteImpl()
		{
			WriteBool(createNew);
			WriteString(username);
			WriteShort((short) encryptedPassword.Length);
			Write(encryptedPassword);
			WriteInt(key1);
			WriteInt(key2);
			WriteInt(key3);
		}
	}
}
