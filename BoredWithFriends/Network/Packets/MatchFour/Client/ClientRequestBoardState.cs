using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour.Client
{
	/// <summary>
	/// Sent by the client to request the current state of the board.
	/// </summary>
	[Packet(typeof(ClientRequestBoardState), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ClientRequestBoardState)]
	internal class ClientRequestBoardState : ClientPacket
	{
		protected override void ReadImpl()
		{
			//Nothing to do.
		}

		protected override void RunImpl(Connection con)
		{
			//TODO: Reply with SendBoardState
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			//Nothing to do.
		}
	}
}
