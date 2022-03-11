using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	/// <summary>
	/// This will attempt to encrypt strings and return the result. It is by no means secure;
	/// it's more of an easter egg written as a bit of fun just to avoid sending passwords
	/// as plaintext.
	/// </summary>
	internal static class PoorMansEncryption
	{
		private static readonly Random RND = new();

		/// <summary>
		/// The string that the <see cref="PAD"/> is made from.
		/// </summary>
		private const string STRING_PAD = "Because it is so clear, it takes longer to realize;" +
			"if you recognize this pad as shoddy encryption, then the packet was sent long ago.";

		/// <summary>
		/// A pad to use for encryption.
		/// </summary>
		private static readonly byte[] PAD = Encoding.Unicode.GetBytes(STRING_PAD);

		public static byte[] Encrypt(string input, out int key1, out int key2, out int key3, int modWith = 253)
		{
			byte[] inputBytes = Encoding.Unicode.GetBytes(input);

			int random = RND.Next(1, int.MaxValue);

			key1 = RND.Next(1, int.MaxValue);

			key2 = random % modWith;

			key3 = random / modWith;

			int startingIndex = ((key1 % modWith) + (random * modWith)) % PAD.Length;

			for (int i = 0; i < inputBytes.Length; i++)
			{
				inputBytes[i] ^= PAD[startingIndex++ % PAD.Length];
			}

			return inputBytes;
		}

		public static string Decrypt(byte[] inputBytes, int key1, int key2, int key3, int modWith = 253)
		{
			int random = key2 + (key3 * modWith);

			int startingIndex = ((key1 % modWith) + (random * modWith)) % PAD.Length;

			for (int i = 0; i < inputBytes.Length; i++)
			{
				inputBytes[i] ^= PAD[startingIndex++ % PAD.Length];
			}

			return Encoding.Unicode.GetString(inputBytes);
		}

	}
}
