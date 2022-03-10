using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.MatchFour.Client
{
	/// <summary>
	/// Sent by the client when the player plays a token.
	/// </summary>
	[Packet(typeof(ClientPlayToken), BoredWithFriendsProtocol.MatchFour, (short) MatchFourOps.ClientPlayToken)]
	internal class ClientPlayToken : ClientPacket
	{
		private int row = -1;

		private int column = -1; //Invalid default value just in case.

		public ClientPlayToken(int column, int row)
		{
			this.row = row;
			this.column = column;
 		}

		protected override void ReadImpl()
		{
			row = ReadInt();
			column = ReadInt();
		}

		protected override void RunImpl(Connection con)
		{
			//Get GameState for this packet, and play the token at the specified column if possible
			//Reply with ServerTokenPlayed
			//Reply with packet to set the player turn
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			WriteInt(row);
			WriteInt(column);
		}
	}
}
