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
		RequestBoardState,
		PlayToken,
		ClearBoard,

		//Server Ops
		SendBoardState,
		TokenPlayed,
		BoardCleared,
	}
}
