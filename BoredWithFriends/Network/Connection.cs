using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	/// <summary>
	/// A base class for connections; this class and subclasses are responsible for handling
	/// the network.
	/// 
	/// This class is being created as part of a student assignment with a get-it-to-work approach,
	/// and is not recommended for use outside of this project.
	/// </summary>
	internal abstract class Connection
	{
		/// <summary>
		/// A <see cref="TcpClient"/> that acts as the entry point to the network.
		/// </summary>
		protected TcpClient client;

		/// <summary>
		/// Creates a <see cref="Connection"/> with the given <see cref="TcpClient"/>.
		/// </summary>
		/// <param name="client">A <see cref="TcpClient"/> for this <see cref="Connection"/></param>
		public Connection(TcpClient client)
		{
			this.client = client;
		}

		/// <summary>
		/// Closes and disposes of <see cref="client"/>.
		/// </summary>
		public void Close()
		{
			client.Close();
			client.Dispose();
		}

		/// <summary>
		/// Checks if this connection is open and returns the result.
		/// </summary>
		/// <returns>True if this connection is active, false otherwise.</returns>
		public bool IsOpen()
		{
			return client.Connected;
		}

		/// <summary>
		/// Checks if this connection can read, and returns the result.
		/// </summary>
		/// <returns>True if this connection can read, false otherwise.</returns>
		protected bool CanRead()
		{
			return IsOpen() && client.GetStream().CanRead;
		}

		/// <summary>
		/// Checks if this connection can write, and returns the result.
		/// </summary>
		/// <returns>True if this connection can write, false otherwise.</returns>
		protected bool CanWrite()
		{
			return IsOpen() && client.GetStream().CanWrite;
		}

		/// <summary>
		/// Reads the given number of bytes from the connection if possible, and returns the data
		/// in a byte array. If the data is not available, an exception will be thrown.
		/// </summary>
		/// <param name="byteCount">The number of bytes to read.</param>
		/// <returns>A byte array containing the requested number of bytes from this connection.</returns>
		/// <exception cref="ConnectionException">If the read operation fails (likely due to the data not being present).</exception>
		public byte[] Read(int byteCount)
		{
			if (Read(byteCount, out byte[] bytes))
			{
				return bytes;
			}

			throw new ConnectionException();
		}

		/// <summary>
		/// Reads the given number of bytes from this connection into <paramref name="bytes"/>. If the data
		/// is available, this method will return true.
		/// </summary>
		/// <param name="byteCount">The number of bytes to read.</param>
		/// <param name="bytes">An array with the data that was read.</param>
		/// <returns>True if the data was read, false otherwise.</returns>
		public bool Read(int byteCount, out byte[] bytes)
		{
			bytes = new byte[byteCount];
			if (CanRead() && client.Available >= byteCount)
			{
				int offset = 0;
				do
				{
					offset += client.GetStream().Read(bytes, offset, byteCount);
				} while (offset < byteCount);
				
				return true;
			}
			return false;
		}

		/// <summary>
		/// Writes the given bytes out to this connection.
		/// </summary>
		/// <param name="bytes">The bytes to write.</param>
		/// <exception cref="ConnectionException">If the write operation fails.</exception>
		public void Write(byte[] bytes)
		{
			if (!Write(bytes.Length, bytes))
			{
				throw new ConnectionException();
			}
		}

		/// <summary>
		/// Writes the given bytes out to this connection up to the specified length. This method will
		/// return true if the operation succeeded, false otherwise.
		/// </summary>
		/// <param name="byteCount">The number of bytes to write from <paramref name="bytes"/></param>
		/// <param name="bytes">A byte array containing bytes to be written out to this connection.</param>
		/// <returns>True if the write completed, false otherwise.</returns>
		public bool Write(int byteCount, byte[] bytes)
		{
			if (CanWrite())
			{
				client.GetStream().Write(bytes, 0, byteCount);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Attempts to peek at the given number of bytes from the data incoming to this connection.
		/// If something goes wrong, an exception is thrown.
		/// </summary>
		/// <param name="byteCount">The number of bytes to peek at.</param>
		/// <returns>An array of the bytes that were peeked at.</returns>
		/// <exception cref="ConnectionException">If something goes wrong.</exception>
		public byte[] Peek(int byteCount)
		{
			if (Peek(byteCount, out byte[] bytes))
			{
				return bytes;
			}

			throw new ConnectionException();
		}

		/// <summary>
		/// Attempts to peek at the given number of bytes from the data incoming to this connection and store
		/// them in <paramref name="bytes"/>. If something goes wrong, an exception is thrown. If the data was
		/// not available, then this method will return false.
		/// </summary>
		/// <param name="byteCount">The number of bytes to peek at.</param>
		/// <param name="bytes">An array of bytes that were peeked at.</param>
		/// <returns>True if the data was available; false otherwise.</returns>
		/// <exception cref="ConnectionException"></exception>
		public bool Peek(int byteCount, out byte[] bytes)
		{
			//TODO: Consider a different approach; interacting with the Socket directly is prone to error.
			bytes = new byte[byteCount];
			if (CanRead() && client.Available >= byteCount)
			{
				client.Client.Receive(bytes, 0, byteCount, SocketFlags.Peek & SocketFlags.Partial, out SocketError errorCode);
				return errorCode switch
				{
					SocketError.Success => true,
					_ => throw new ConnectionException(),
				};
			}
			return false;
		}
	}

	/// <summary>
	/// An unspecified error with this Connection.
	/// </summary>
	internal class ConnectionException : Exception { }
}
