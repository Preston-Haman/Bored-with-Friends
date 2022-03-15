using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Sent by the server in response to <see cref="Client.ClientDeleteAccount"/>.
	/// </summary>
	[Packet(typeof(ServerAccountDeleted), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerAccountDeleted)]
	internal class ServerAccountDeleted : ServerPacket
	{
		private bool result;

		public ServerAccountDeleted(bool deletionResult)
		{
			result = deletionResult;
		}

		protected override void ReadImpl()
		{
			result = ReadBool();
		}

		protected override void RunImpl()
		{
			Network.Client.RaiseEvent(GeneralEvent.AccountDeletionResult, result);
		}

		protected override void WriteImpl()
		{
			WriteBool(result);
		}
	}
}
