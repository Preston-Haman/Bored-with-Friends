using BoredWithFriends.Data;
using BoredWithFriends.Games;
using BoredWithFriends.Network.Packets.General.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Client
{
	/// <summary>
	/// Sent by the client when the user wants to delete their account.
	/// </summary>
	[Packet(typeof(ClientDeleteAccount), BoredWithFriendsProtocol.General, (short) GeneralOps.ClientDeleteAccount)]
	internal class ClientDeleteAccount : ClientPacket
	{
		private byte[] encryptedPassword;

		private int key1, key2, key3;

		public ClientDeleteAccount(string password)
		{
			encryptedPassword = PoorMansEncryption.Encrypt(password, out key1, out key2, out key3);
		}

		protected override void ReadImpl()
		{
			encryptedPassword = Read(ReadShort());
			key1 = ReadInt();
			key2 = ReadInt();
			key3 = ReadInt();
		}

		protected override void RunImpl(Connection con)
		{
			Player player = GetPlayerConnection(con).Player;
			string password = PoorMansEncryption.Decrypt(encryptedPassword, key1, key2, key3);
			
			bool result = DatabaseContext.DeleteUser(player, password);
			PacketSendUtility.SendPacket(player, new ServerAccountDeleted(result));
		}

		protected override void WriteImpl()
		{
			WriteShort((short) encryptedPassword.Length);
			Write(encryptedPassword);
			WriteInt(key1);
			WriteInt(key2);
			WriteInt(key3);
		}
	}
}
