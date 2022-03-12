using BoredWithFriends.Network.Packets.General.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Client
{
	/// <summary>
	/// Sent by the client when the user wants to update their password from account management.
	/// </summary>
	[Packet(typeof(ClientUpdatePassword), BoredWithFriendsProtocol.General, (short) GeneralOps.ClientUpdatePassword)]
	internal class ClientUpdatePassword : ClientPacket
	{
		private byte[] encryptedCurrentPassword;

		private int key1, key2, key3;

		private byte[] encryptedNewPassword;

		private int key4, key5, key6;

		public ClientUpdatePassword(string currentPassword, string newPassword)
		{
			encryptedCurrentPassword = PoorMansEncryption.Encrypt(currentPassword, out key1, out key2, out key3);
			encryptedNewPassword = PoorMansEncryption.Encrypt(newPassword, out key4, out key5, out key6);
		}

		protected override void ReadImpl()
		{
			encryptedCurrentPassword = Read(ReadShort());
			key1 = ReadInt();
			key2 = ReadInt();
			key3 = ReadInt();
			encryptedNewPassword = Read(ReadShort());
			key4 = ReadInt();
			key5 = ReadInt();
			key6 = ReadInt();
		}

		protected override void RunImpl(Connection con)
		{
			throw new NotImplementedException();
			PlayerConnection pcon = GetPlayerConnection(con);
			string currentPassword = PoorMansEncryption.Decrypt(encryptedCurrentPassword, key1, key2, key3);
			string newPassword = PoorMansEncryption.Decrypt(encryptedNewPassword, key4, key5, key6);

			//TODO: Call database password update code
			bool updateResult = false;

			PacketSendUtility.SendPacket(pcon.Player, new ServerApprovePasswordUpdate(updateResult));
		}

		protected override void WriteImpl()
		{
			WriteShort((short) encryptedCurrentPassword.Length);
			WriteInt(key1);
			WriteInt(key2);
			WriteInt(key3);
			WriteShort((short) encryptedNewPassword.Length);
			WriteInt(key4);
			WriteInt(key5);
			WriteInt(key6);
		}
	}
}
