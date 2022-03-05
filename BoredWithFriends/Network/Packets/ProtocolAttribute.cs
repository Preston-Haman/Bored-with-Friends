using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets
{
	[AttributeUsage(AttributeTargets.Class)]
	internal class ProtocolAttribute : Attribute
	{
		public short Protocol { get; }

		public ProtocolAttribute(short protocol)
		{
			Protocol = protocol;
		}
	}
}
