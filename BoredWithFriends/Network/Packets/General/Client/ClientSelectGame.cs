﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Client
{
	[Packet(typeof(ClientSelectGame), BoredWithFriendsProtocol.General, (short) GeneralOps.ClientSelectGame)]
	internal class ClientSelectGame : ClientPacket
	{
		protected override void ReadImpl()
		{
			throw new NotImplementedException();
		}

		protected override void RunImpl(Connection con)
		{
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			throw new NotImplementedException();
		}
	}
}