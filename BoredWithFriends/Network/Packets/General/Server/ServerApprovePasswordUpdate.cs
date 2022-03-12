using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Sent by the server in response to <see cref="Client.ClientUpdatePassword"/>.
	/// </summary>
	[Packet(typeof(ServerApprovePasswordUpdate), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerApprovePasswordUpdate)]
	internal class ServerApprovePasswordUpdate : ServerPacket
	{
		private bool result;

		public ServerApprovePasswordUpdate(bool updateResult)
		{
			result = updateResult;
		}

		protected override void ReadImpl()
		{
			result = ReadBool();
		}

		protected override void RunImpl()
		{
			Network.Client.RaiseEvent(GeneralEvent.PasswordUpdateResultReceived, result);
		}

		protected override void WriteImpl()
		{
			WriteBool(result);
		}
	}
}
