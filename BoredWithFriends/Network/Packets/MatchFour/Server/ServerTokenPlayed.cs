using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BoredWithFriends.Games.MatchFourGameState;

namespace BoredWithFriends.Network.Packets.MatchFour.Server
{
	internal class ServerTokenPlayed : ServerPacket
	{
		public ServerTokenPlayed()
		{
			//For reflection
		}

		private int row;

		private int column;

		private BoardToken token;

		public ServerTokenPlayed(int row, int column, BoardToken token)
		{
			this.row = row;
			this.column = column;
			this.token = token;
		}

		protected override void ReadImpl()
		{
			row = ReadInt();
			column = ReadInt();
			token = (BoardToken) ReadByte();
		}

		protected override void RunImpl()
		{
			/*TODO
			 * Check that the specified location on the board is available.
			 * If it's not, request the board state from the server and return.
			 * If it is, play the token in the specified column on the GameState.
			 */
			throw new NotImplementedException();
		}

		protected override void WriteImpl()
		{
			WriteInt(row);
			WriteInt(column);
			WriteByte((byte) token);
		}
	}
}
