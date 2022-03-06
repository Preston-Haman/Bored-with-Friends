using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General
{
	internal enum GeneralOps
	{
		//Client Ops
		Connect,
		CreateAccount,
		Login,
		AccountManagement,
		UpdatePassword,
		DeleteAccount,
		SelectGame,
		Disconnect,

		//Server Ops
		Connected,
		ApproveAccount,
		ApproveLogin,
		SendAccountDetails,
		ApprovePasswordUpdate,
		AccountDeleted,
		EnterGameLobby,
		AddPlayer,
		AddSpectator,
		StartGame,
		CloseConnection
	}
}
