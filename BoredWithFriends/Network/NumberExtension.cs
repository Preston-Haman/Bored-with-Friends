using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	internal static class NumberExtension
	{
		public static byte GetByteAtPosition(this short value, int position)
		{
			return (byte) ((value >> position * 8) & 0xFF);
		}

		public static byte GetByteAtPosition(this int value, int position)
		{
			return (byte) ((value >> position * 8) & 0xFF);
		}

		public static byte GetByteAtPosition(this long value, int position)
		{
			return (byte) ((value >> position * 8) & ((long) 0xFF));
		}
	}
}
