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
		ClientConnect,
		ClientLogin,
		ClientAccountManagement,
		ClientUpdatePassword,
		ClientDeleteAccount,
		ClientSelectGame,
		ClientDisconnect,

		//Server Ops
		ServerConnected,
		ServerApproveLogin,
		ServerSendAccountDetails,
		ServerApprovePasswordUpdate,
		ServerAccountDeleted,
		ServerEnterGameLobby,
		ServerAddPlayer,
		ServerAddSpectator,
		ServerStartGame,
		ServerCloseConnection
	}
}
