using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	/// <summary>
	/// A simple extension class that adds convenience methods to numeric types that allow
	/// retrieval of bytes by their index for multi-byte numeric value types.
	/// </summary>
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

	/// <summary>
	/// A simple buffer class that reads and writes in Little Endian byte order.
	/// This class is backed by an array that grows to accommodate the data written to it.
	/// The default initial size of the array is 1024 bytes, and it will grow in increments of
	/// 1024 bytes to support more data when necessary.
	/// </summary>
	internal class ByteArrayStream
	{
		private byte[] buffer;

		private int position = 0;

		public int Position
		{
			get
			{
				return position;
			}

			set
			{
				if (value > Size || value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(Position), "The new value is not within the bounds of this stream.");
				}
				position = value;
			}
		}

		public int Size { get; private set; } = 0;

		public ByteArrayStream() : this(1024)
		{
			//Nothing to do
		}

		public ByteArrayStream(int initialCapacity)
		{
			buffer = new byte[initialCapacity];
			Size = buffer.Length;
		}

		public ByteArrayStream(byte[] data)
		{
			buffer = new byte[data.Length];
			Array.Copy(data, buffer, data.Length);
		}

		public byte[] GetBackingArray()
		{
			return buffer;
		}

		private void EnsureRemaining(int amount)
		{
			if (Size - position < amount)
			{
				throw new ArgumentException("This stream does not have enough data to read the specified number of bytes.");
			}
		}

		public byte[] Read(int byteCount)
		{
			Read(byteCount, out byte[] bytes);
			return bytes;
		}

		public void Read(int byteCount, out byte[] bytes)
		{
			EnsureRemaining(byteCount);
			bytes = new byte[byteCount];
			Array.Copy(buffer, position, bytes, 0, byteCount);
			position += byteCount;
		}

		public byte ReadByte()
		{
			EnsureRemaining(1);
			return buffer[position++];
		}

		public short ReadShort()
		{
			Read(2, out byte[] bytes);
			return (short) ((bytes[1] << 8) | bytes[0]);
		}

		public int ReadInt()
		{
			Read(4, out byte[] bytes);
			return (bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0];
		}

		public long ReadLong()
		{
			Read(8, out byte[] bytes);

			//Not sure if this will work...
			long byte1 = (long) bytes[0];
			long byte2 = (long) bytes[1] << 8;
			long byte3 = (long) bytes[2] << 16;
			long byte4 = (long) bytes[3] << 24;
			long byte5 = (long) bytes[4] << 32;
			long byte6 = (long) bytes[5] << 40;
			long byte7 = (long) bytes[6] << 48;
			long byte8 = (long) bytes[7] << 64;

			return byte8 | byte7 | byte6 | byte5 | byte4 | byte3 | byte2 | byte1;
		}

		public bool ReadBool()
		{
			return ReadByte() != 0;
		}

		public char ReadChar()
		{
			return (char) ReadByte();
		}

		public string ReadString()
		{
			short length = ReadShort();
			Read(length, out byte[] bytes);

			StringBuilder str = new(length);
			for (int i = 0; i < bytes.Length; i++)
			{
				str.Append((char) bytes[i]);
			}

			return str.ToString();
		}

		private void EnsureCapacity(int capacity)
		{
			if (buffer.Length < capacity)
			{
				checked
				{
					Array.Resize<byte>(ref buffer, Size + 1024 < capacity ? capacity : Size + 1024);
				}
			}
		}

		public void Write(byte[] bytes)
		{
			Write(bytes, bytes.Length);
		}

		public void Write(byte[] bytes, int length)
		{
			EnsureCapacity(Size + length);
			Array.Copy(bytes, 0, buffer, position, length);
			position += length;
			Size += length;
		}

		public void WriteByte(byte value)
		{
			EnsureCapacity(Size + 1);
			buffer[position++] = value;
			Size++;
		}

		public void WriteShort(short value)
		{
			EnsureCapacity(Size + 2);
			for (int i = 0; i < 2; i++)
			{
				buffer[position++] = value.GetByteAtPosition(i);
			}
			Size += 2;
		}

		public void WriteInt(int value)
		{
			EnsureCapacity(Size + 4);
			for (int i = 0; i < 4; i++)
			{
				buffer[position++] = value.GetByteAtPosition(i);
			}
			Size += 4;
		}

		public void WriteLong(long value)
		{
			EnsureCapacity(Size + 8);
			for (int i = 0; i < 8; i++)
			{
				buffer[position++] = value.GetByteAtPosition(i);
			}
			Size += 8;
		}

		public void WriteBool(bool value)
		{
			WriteByte((byte) (value ? 1 : 0));
		}

		public void WriteChar(char value)
		{
			WriteByte((byte) value);
		}

		public void WriteString(string value)
		{
			if (value.Length > short.MaxValue)
			{
				throw new ArgumentException("This stream does not support writing strings longer than the max value of a signed 16-bit integer.");
			}

			WriteShort((short) value.Length);
			foreach (char c in value)
			{
				WriteChar(c);
			}
		}
	}
}
