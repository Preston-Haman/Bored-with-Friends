using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	/// <summary>
	/// Represents a client's connection to this application while it's running as a server.
	/// <br></br><br></br>
	/// It's important to note that this connection may not be a legitimate user of this
	/// application. There is a chance that some service is scanning for open ports and
	/// they made a connection to us. These connection objects are short lived; they are
	/// either replaced with a <see cref="PlayerConnection"/>, or rejected.
	/// </summary>
	internal class ClientConnection : Connection
	{
		/// <summary>
		/// Tracks whether or not this connection's underlying TcpClient
		/// has been adopted by something else.
		/// <br></br><br></br>
		/// If something else has adopted the TcpClient, then this
		/// connection will act as if it has been closed.
		/// </summary>
		protected bool isAdopted = false;

		public ClientConnection(TcpClient client) : base(client)
		{
			//Nothing to do.
		}

		/// <inheritdoc/>
		public override bool IsOpen()
		{
			return !isAdopted && base.IsOpen();
		}

		/// <summary>
		/// Returns the underlying TcpClient this Connection uses,
		/// and considers this connection to be closed.
		/// <br></br><br></br>
		/// <seealso cref="isAdopted"/>
		/// </summary>
		/// <returns>The underlying TcpClient this connection uses.</returns>
		public virtual TcpClient AdoptClient()
		{
			isAdopted = true;
			return client;
		}
	}

	/// <summary>
	/// Represents a Player's connection to this application while it's running as a server.
	/// </summary>
	internal class PlayerConnection : Connection
	{
		/// <summary>
		/// The <see cref="BoredWithFriends.Games.Player"/> this connection is for.
		/// <br></br><br></br>
		/// This property is only assigned during construction of this connection.
		/// </summary>
		public Player Player { get; private set; }

		/// <summary>
		/// The PlayerID property of <see cref="Player"/>.
		/// <br></br><br></br>
		/// This property is just for convenient access.
		/// </summary>
		public int PlayerID
		{
			get
			{
				return Player.PlayerID;
			}
		}

		/// <summary>
		/// Creates a new <see cref="PlayerConnection"/> using the given <see cref="TcpClient"/>.
		/// <br></br><br></br>
		/// The <paramref name="player"/> is stored in the <see cref="Player"/> property.
		/// </summary>
		/// <param name="client">The TcpClient this connection will use.</param>
		/// <param name="player">The <see cref="BoredWithFriends.Games.Player"/> that this connection is for.</param>
		public PlayerConnection(TcpClient client, Player player) : base(client)
		{
			Player = player;
		}

		public override void Close()
		{
			base.Close();
			Server.PlayerDisconnected(this);
		}
	}

	/// <summary>
	/// Represents a connection to a Server when this application is running as a client.
	/// </summary>
	internal class ServerConnection : Connection
	{
		/// <summary>
		/// The remote IP and port of the server this connection is for.
		/// </summary>
		public IPEndPoint ServerInfo { get; }
		
		/// <summary>
		/// Creates a new <see cref="ServerConnection"/>. This will create a new TcpClient and attempt
		/// to connect to the given <paramref name="serverIP"/> on the given <paramref name="serverPort"/>
		/// in blocking mode.
		/// </summary>
		/// <param name="serverIP">The remote IP of the server to connect to as a string.</param>
		/// <param name="serverPort">The remote port of the server to connect to.</param>
		public ServerConnection(string serverIP, int serverPort) : this(new IPEndPoint(IPAddress.Parse(serverIP), serverPort))
		{
			//Nothing else to do.
		}

		/// <summary>
		/// Creates a new <see cref="ServerConnection"/>. This will create a new TcpClient and attempt
		/// to connect to the given <paramref name="serverIP"/> in blocking mode.
		/// </summary>
		/// <param name="serverIP">The remote IP and Port of the server to connect to.</param>
		public ServerConnection(IPEndPoint serverIP) : base(new TcpClient())
		{
			ServerInfo = serverIP;
			client.Connect(serverIP);
		}
	}

	/// <summary>
	/// Represents a local player. This implementation passes null to the base
	/// classes for the TcpClient; the consequences of this are that reading
	/// and writing from this connection will throw an exception.
	/// </summary>
	internal class LocalConnection : PlayerConnection
	{
		/// <summary>
		/// Creates a new <see cref="LocalConnection"/> for the given <paramref name="player"/>.
		/// </summary>
		/// <param name="player">The player this connection is for.</param>
		public LocalConnection(Player player) : base(null!, player)
		{
			//Nothing to do.
		}
	}

	/// <summary>
	/// Represents the state of a <see cref="Connection"/>. State in this case does not
	/// refer to the status of the network connection; rather, it refers to the state
	/// of the source of the connection.
	/// <br></br><br></br>
	/// The source of any given connection is unknown until communication with it occurs.
	/// After establishing communication, the source can be identified as a Bored with Friends
	/// application (or at least an application that mimics the network protocol), and in the
	/// case of a player, may login to an account. These three states are represented by
	/// the named values of this enum:
	/// <list type="bullet"><see cref="Unknown"/></list>
	/// <list type="bullet"><see cref="Handshook"/></list>
	/// <list type="bullet"><see cref="Authed"/></list>
	/// </summary>
	internal enum ConnectionState : byte
	{
		/// <summary>
		/// The <see cref="Connection"/> is to an Unknown source.
		/// </summary>
		Unknown = 1,

		/// <summary>
		/// The <see cref="Connection"/> is likely to a Bored with Friends Application.
		/// </summary>
		Handshook = 1 << 1,
		
		/// <summary>
		/// The <see cref="Connection"/> is to an Authorized player/server.
		/// </summary>
		Authed = 1 << 2
	}

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
		/// The state of what's known about this connection's source. This can be used to filter packets
		/// from entities that should not be allowed to send them on this connection.
		/// </summary>
		protected ConnectionState connectionState = ConnectionState.Unknown;

		/// <summary>
		/// Tracks if <see cref="client"/> has had its resources closed and disposed of.<br></br>
		/// This is set to true by a call to <see cref="Close"/>.
		/// </summary>
		private bool disposed = false;

		/// <summary>
		/// Creates a <see cref="Connection"/> with the given <see cref="TcpClient"/>.
		/// </summary>
		/// <param name="client">A <see cref="TcpClient"/> for this <see cref="Connection"/></param>
		public Connection(TcpClient client)
		{
			this.client = client;
		}

		/// <summary>
		/// Adjust this connection's <see cref="connectionState"/> to match the given
		/// <paramref name="state"/>.
		/// </summary>
		/// <param name="state">The new <see cref="ConnectionState"/> to set this
		/// connection to.</param>
		public void SetConnectionState(ConnectionState state)
		{
			connectionState = state;
		}

		/// <summary>
		/// Checks if <see cref="connectionState"/> matches any of the given <paramref name="states"/>.
		/// If so, returns true; otherwise, false.
		/// </summary>
		/// <param name="states">A list of states to check for.</param>
		/// <returns>True if any of the given <paramref name="states"/> match the current <see cref="connectionState"/>.</returns>
		public bool MatchesAnyConnectionState(params ConnectionState[] states)
		{
			//ConnectionState is not meant to be a flag enum, but the values map like one.
			//That lets callers specify a list of states to match.
			int flags = 0;
			foreach (ConnectionState state in states)
			{
				flags |= (int) state;
			}

			return ((int) connectionState & flags) == 1;
		}

		/// <summary>
		/// Closes and disposes of <see cref="client"/>.
		/// </summary>
		public virtual void Close()
		{
			client.Close();
			client.Dispose();
			disposed = true;
		}

		/// <summary>
		/// Checks if this connection is open and returns the result.
		/// </summary>
		/// <returns>True if this connection is active, false otherwise.</returns>
		public virtual bool IsOpen()
		{
			if (!client.Connected && !disposed)
			{
				Close();
			}
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
