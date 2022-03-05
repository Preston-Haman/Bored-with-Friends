using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets
{
	[AttributeUsage(AttributeTargets.Class)]
	internal class OpcodeAttribute : Attribute
	{
		public short Opcode { get; }

		public OpcodeAttribute(short opCode)
		{
			Opcode = opCode;
		}
	}
}
