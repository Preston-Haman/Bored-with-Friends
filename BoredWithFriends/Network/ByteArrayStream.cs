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
		/// <summary>
		/// Returns the byte at the given <paramref name="position"/> within <paramref name="value"/>.
		/// <br></br><br></br>
		/// The position of a byte is defined as a zero based indexing of the multiple bytes
		/// that make up <paramref name="value"/>, where the zero-index is the least significant
		/// byte.
		/// </summary>
		/// <param name="value">The multi-byte numeric value to retrieve a byte from.</param>
		/// <param name="position">The position of the byte to retrieve.</param>
		/// <returns>The byte at the given <paramref name="position"/> within <paramref name="value"/>.</returns>
		public static byte GetByteAtPosition(this short value, int position)
		{
			return (byte) ((value >> position * 8) & 0xFF);
		}

		/// <summary>
		/// Returns the byte at the given <paramref name="position"/> within <paramref name="value"/>.
		/// <br></br><br></br>
		/// The position of a byte is defined as a zero based indexing of the multiple bytes
		/// that make up <paramref name="value"/>, where the zero-index is the least significant
		/// byte.
		/// </summary>
		/// <param name="value">The multi-byte numeric value to retrieve a byte from.</param>
		/// <param name="position">The position of the byte to retrieve.</param>
		/// <returns>The byte at the given <paramref name="position"/> within <paramref name="value"/>.</returns>
		public static byte GetByteAtPosition(this int value, int position)
		{
			return (byte) ((value >> position * 8) & 0xFF);
		}

		/// <summary>
		/// Returns the byte at the given <paramref name="position"/> within <paramref name="value"/>.
		/// <br></br><br></br>
		/// The position of a byte is defined as a zero based indexing of the multiple bytes
		/// that make up <paramref name="value"/>, where the zero-index is the least significant
		/// byte.
		/// </summary>
		/// <param name="value">The multi-byte numeric value to retrieve a byte from.</param>
		/// <param name="position">The position of the byte to retrieve.</param>
		/// <returns>The byte at the given <paramref name="position"/> within <paramref name="value"/>.</returns>
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
		/// <summary>
		/// An internal buffer of bytes stored in this stream.
		/// </summary>
		private byte[] buffer;

		/// <summary>
		/// The current position to read or write at.
		/// </summary>
		private int position = 0;

		/// <summary>
		/// The position of this stream.
		/// <br></br><br></br>
		/// The position is the current location within the internal buffer of data where calls
		/// to read or write will read or write from. As data is written from this stream, the
		/// position is moved forward; the same is true when data is read from this stream.
		/// <br></br><br></br>
		/// It's worth noting that if a caller plan to both write to and read from this stream,
		/// they will have to reposition the stream as necessary for their use. To read the
		/// entirety of the data that was previously written to this stream, set the position
		/// to zero.
		/// </summary>
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

		/// <summary>
		/// The amount of data (in bytes) that has been written to this stream.
		/// </summary>
		public int Size { get; private set; } = 0;

		/// <summary>
		/// Creates a new <see cref="ByteArrayStream"/> with the default initial
		/// capacity of 1024.
		/// </summary>
		public ByteArrayStream() : this(1024)
		{
			//Nothing to do
		}

		/// <summary>
		/// Creates a new <see cref="ByteArrayStream"/> with the specified <paramref name="initialCapacity"/>.
		/// </summary>
		/// <param name="initialCapacity">The initial capacity to use for the internal buffer of this stream.</param>
		public ByteArrayStream(int initialCapacity)
		{
			buffer = new byte[initialCapacity];
		}

		/// <summary>
		/// Creates a new <see cref="ByteArrayStream"/>, and uses the entirety of <paramref name="data"/>
		/// as the data of the internal buffer. The internal buffer is sized to match the given
		/// <paramref name="data"/>, and then the <paramref name="data"/> is copied into it.
		/// The <see cref="Position"/> of this stream is set to zero, and the <see cref="Size"/> is
		/// set to the length of the <paramref name="data"/>.
		/// </summary>
		/// <param name="data">The data to use for the initial state of this stream.</param>
		public ByteArrayStream(byte[] data)
		{
			buffer = new byte[data.Length];
			Size = buffer.Length;
			Array.Copy(data, buffer, data.Length);
		}

		/// <summary>
		/// Returns the byte array that backs this stream as its buffer. The returned array is not a copy.
		/// </summary>
		/// <returns>The internal <see cref="buffer"/> directly.</returns>
		public byte[] GetBackingArray()
		{
			return buffer;
		}

		/// <summary>
		/// Checks that the stream has at least the specified <paramref name="amount"/> of data left available
		/// to be read from the current <see cref="Position"/>.
		/// </summary>
		/// <param name="amount">The amount of data to check for.</param>
		/// <exception cref="ArgumentException">If the specified amount is not available.</exception>
		private void EnsureRemaining(int amount)
		{
			if (Size - position < amount)
			{
				throw new ArgumentException("This stream does not have enough data to read the specified number of bytes.");
			}
		}

		/// <summary>
		/// Reads the specified number of bytes from this stream into a byte array and returns it.
		/// </summary>
		/// <param name="byteCount">The number of bytes to read from this stream.</param>
		/// <returns>An array containing the bytes read from this stream.</returns>
		/// <exception cref="ArgumentException">If the specified amount of bytes are not available.</exception>
		public byte[] Read(int byteCount)
		{
			Read(byteCount, out byte[] bytes);
			return bytes;
		}

		/// <summary>
		/// Reads the specified number of bytes from this stream into <paramref name="bytes"/>.
		/// </summary>
		/// <param name="byteCount">The number of bytes to read from this stream.</param>
		/// <param name="bytes">An array that will contain the bytes read from this stream.</param>
		/// <exception cref="ArgumentException">If the specified amount of bytes are not available.</exception>
		public void Read(int byteCount, out byte[] bytes)
		{
			EnsureRemaining(byteCount);
			bytes = new byte[byteCount];
			Array.Copy(buffer, position, bytes, 0, byteCount);
			position += byteCount;
		}

		/// <summary>
		/// Reads the next byte from this stream and returns it.
		/// </summary>
		/// <returns>The next available byte from this stream.</returns>
		/// <exception cref="ArgumentException">If the stream does not have a byte of data available.</exception>
		public byte ReadByte()
		{
			EnsureRemaining(1);
			return buffer[position++];
		}

		/// <summary>
		/// Reads the next short from this stream in Little Endian byte order and returns it.
		/// </summary>
		/// <returns>The next short available from this stream.</returns>
		/// <exception cref="ArgumentException">If the stream does not have a short available to read.</exception>
		public short ReadShort()
		{
			Read(2, out byte[] bytes);
			return (short) ((bytes[1] << 8) | bytes[0]);
		}

		/// <summary>
		/// Reads the next int from this stream in Little Endian byte order and returns it.
		/// </summary>
		/// <returns>The next int available from this stream.</returns>
		/// <exception cref="ArgumentException">If the stream does not have an int available to read.</exception>
		public int ReadInt()
		{
			Read(4, out byte[] bytes);
			return (bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0];
		}

		/// <summary>
		/// Reads the next long from this stream in Little Endian byte order and returns it.
		/// </summary>
		/// <returns>The next long available from this stream</returns>
		/// <exception cref="ArgumentException">If the stream does not have a long available to read.</exception>
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

		/// <summary>
		/// Reads the next byte from this stream and returns true if it's non-zero.
		/// </summary>
		/// <returns>A bool representing the next byte available in this stream.</returns>
		/// <exception cref="ArgumentException">If the stream does not have a byte available to read.</exception>
		public bool ReadBool()
		{
			return ReadByte() != 0;
		}

		/// <summary>
		/// Reads the next char from this stream and returns it.
		/// </summary>
		/// <returns>The next char available from this stream.</returns>
		/// <exception cref="ArgumentException">If the stream does not have a char available to read.</exception>
		public char ReadChar()
		{
			return (char) ReadByte();
		}

		/// <summary>
		/// Reads the next string from this stream and returns it.
		/// </summary>
		/// <returns>The next string available from this stream.</returns>
		/// <exception cref="ArgumentException">If the stream does not have a string available to read.</exception>
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

		/// <summary>
		/// Ensures that the underlying buffer has at least enough capacity to store the
		/// specified <paramref name="capacity"/>. If it does not, then it will be resized.
		/// <br></br><br></br>
		/// If the capacity is grown, it will either grow to the exact <paramref name="capacity"/>
		/// specified, or be increased in size by 1024. The decision is made based on the current
		/// capacity; if the given <paramref name="capacity"/> cannot be achieved by growing by
		/// 1024, then the capacity will be increased to match the exact specified <paramref name="capacity"/>.
		/// </summary>
		/// <param name="capacity">The amount of bytes to ensure <see cref="buffer"/> is capable of storing.</param>
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

		/// <summary>
		/// Writes the given <paramref name="bytes"/> to this stream's buffer and advances the
		/// <see cref="Position"/> of this stream by the length of the data.
		/// </summary>
		/// <param name="bytes">The bytes to write to this stream's buffer.</param>
		public void Write(byte[] bytes)
		{
			Write(bytes, bytes.Length);
		}

		/// <summary>
		/// Writes the given <paramref name="length"/> of data from <paramref name="bytes"/> to
		/// this stream's buffer and advances the <see cref="Position"/> of this stream by
		/// <paramref name="length"/>.
		/// </summary>
		/// <param name="bytes">The source of the data to write to this stream.</param>
		/// <param name="length">The number of bytes to write from <paramref name="bytes"/> to this stream.</param>
		public void Write(byte[] bytes, int length)
		{
			EnsureCapacity(Size + length);
			Array.Copy(bytes, 0, buffer, position, length);
			position += length;
			Size += length;
		}

		/// <summary>
		/// Writes the given <paramref name="value"/> as a byte to this stream and advances the
		/// <see cref="Position"/> accordingly.
		/// </summary>
		/// <param name="value">The value to write to this stream.</param>
		public void WriteByte(byte value)
		{
			EnsureCapacity(Size + 1);
			buffer[position++] = value;
			Size++;
		}

		/// <summary>
		/// Writes the given <paramref name="value"/> as a short to this stream and advances the
		/// <see cref="Position"/> accordingly. The value is represented in Little Endian byte order.
		/// </summary>
		/// <param name="value">The value to write to this stream.</param>
		public void WriteShort(short value)
		{
			EnsureCapacity(Size + 2);
			for (int i = 0; i < 2; i++)
			{
				buffer[position++] = value.GetByteAtPosition(i);
			}
			Size += 2;
		}

		/// <summary>
		/// Writes the given <paramref name="value"/> as an int to this stream and advances the
		/// <see cref="Position"/> accordingly. The value is represented in Little Endian byte order.
		/// </summary>
		/// <param name="value">The value to write to this stream.</param>
		public void WriteInt(int value)
		{
			EnsureCapacity(Size + 4);
			for (int i = 0; i < 4; i++)
			{
				buffer[position++] = value.GetByteAtPosition(i);
			}
			Size += 4;
		}

		/// <summary>
		/// Writes the given <paramref name="value"/> as a long to this stream and advances the
		/// <see cref="Position"/> accordingly. The value is represented in Little Endian byte order.
		/// </summary>
		/// <param name="value">The value to write to this stream.</param>
		public void WriteLong(long value)
		{
			EnsureCapacity(Size + 8);
			for (int i = 0; i < 8; i++)
			{
				buffer[position++] = value.GetByteAtPosition(i);
			}
			Size += 8;
		}

		/// <summary>
		/// Writes the given <paramref name="value"/> as a bool to this stream and advances the
		/// <see cref="Position"/> accordingly. The value is represented as a single byte;
		/// zero for false, and any non-zero value for true (one is written).
		/// </summary>
		/// <param name="value">The value to write to this stream.</param>
		public void WriteBool(bool value)
		{
			WriteByte((byte) (value ? 1 : 0));
		}

		/// <summary>
		/// Writes the given <paramref name="value"/> as a char to this stream and advances the
		/// <see cref="Position"/> accordingly. The value is represented as a single byte.
		/// </summary>
		/// <param name="value">The value to write to this stream.</param>
		public void WriteChar(char value)
		{
			WriteByte((byte) value);
		}

		/// <summary>
		/// Writes the given <paramref name="value"/> as a string to this stream and advances the
		/// <see cref="Position"/> accordingly. The value is written as a single byte for
		/// each character, with a preceding short to specify the length.
		/// </summary>
		/// <param name="value">The value to write to this stream.</param>
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
