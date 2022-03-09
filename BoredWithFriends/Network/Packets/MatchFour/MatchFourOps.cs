using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour
{
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
