using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour
{
	/// <summary>
	/// The named opcodes for the <see cref="BoredWithFriendsProtocol.MatchFour"/> protocol.
	/// </summary>
	internal enum MatchFourOps : short
	{
		//Client Ops
		ClientRequestBoardState,
		ClientPlayToken,
		ClientClearBoard,

		//Server Ops
		ServerSendBoardState,
		ServerTokenPlayed,
		ServerBoardCleared,
	}
}
