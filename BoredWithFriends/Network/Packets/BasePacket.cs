using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets
{
	/// <summary>
	/// An exception indicating that something has violated either the protocol definition,
	/// or some implementation specific detail.
	/// </summary>
	internal class ProtocolException : Exception
	{
		/// <summary>
		/// Creates a new <see cref="ProtocolException"/> with the given message.
		/// </summary>
		/// <param name="message">The message explaining why this exception was thrown.</param>
		public ProtocolException(string? message) : base(message)
		{
			//Nothing to do.
		}
	}

	/// <summary>
	/// A simple header for all <see cref="BasePacket"/> implementations.
	/// </summary>
	internal struct PacketHeader
	{
		/// <summary>
		/// The size of the header in bytes.
		/// </summary>
		public const byte HEADER_SIZE = 9;

		/// <summary>
		/// The size of the packet this header is for.
		/// </summary>
		public readonly short size;

		/// <summary>
		/// The protocol the packet this header is for belongs to.
		/// </summary>
		public readonly short protocol;

		/// <summary>
		/// The opcode that identifies the packet this header is for.
		/// </summary>
		public readonly short opcode;

		/// <summary>
		/// The version of the packet. This has been included for the future,
		/// but will likely not see any real use.
		/// </summary>
		public readonly byte version;

		/// <summary>
		/// A simple check that will allow verification that this packet's opcode is intact.
		/// It shouldn't be necessary to use this over a TCP connection, but the implementation
		/// details of what gets written here may eventually change. At this time, the value
		/// represented here is just a bit-flipped version of the opcode.
		/// </summary>
		public readonly short checksum;

		/// <summary>
		/// Creates a new <see cref="PacketHeader"/> with the given information.
		/// </summary>
		/// <param name="size">The size of this packet.</param>
		/// <param name="protocol">The protocol this packet belongs to.</param>
		/// <param name="opcode">The opcode of this packet.</param>
		/// <param name="version">The version of this packet.</param>
		public PacketHeader(short size, short protocol, short opcode, byte version)
		{
			this.size = size;
			this.protocol = protocol;
			this.opcode = opcode;
			this.version = version;
			checksum = (short) ~opcode;
		}

		/// <summary>
		/// Creates a new <see cref="PacketHeader"/> with the given information.
		/// </summary>
		/// <param name="raw">An array of bytes of length <see cref="HEADER_SIZE"/>
		/// which contains the raw bytes of a packet header.</param>
		public PacketHeader(byte[] raw)
		{
			//Not really necessary to make this stream; but it's certainly convenient!
			ByteArrayStream buffer = new(raw);
			size = buffer.ReadShort();
			protocol = buffer.ReadShort();
			opcode = buffer.ReadShort();
			version = buffer.ReadByte();
			checksum = buffer.ReadShort();
		}

		/// <summary>
		/// Writes this <see cref="PacketHeader"/> to the given <see cref="ByteArrayStream"/>.
		/// </summary>
		/// <param name="buffer">The stream to write this header into.</param>
		public void Write(ByteArrayStream buffer)
		{
			buffer.WriteShort(size);
			buffer.WriteShort(protocol);
			buffer.WriteShort(opcode);
			buffer.WriteByte(version);
			buffer.WriteShort(checksum);
		}

		/// <summary>
		/// Checks if this <see cref="PacketHeader"/> matches the expected protocol and opcode.
		/// </summary>
		/// <param name="protocol"></param>
		/// <param name="opcode"></param>
		/// <returns></returns>
		public bool IsValid(short protocol, short opcode)
		{
			return this.protocol == protocol && this.opcode == opcode && ~opcode == checksum;
		}
	}

	/// <summary>
	/// This is a packet that was sent from a client.
	/// </summary>
	internal abstract class ClientPacket : BasePacket
	{
		//Server probably needs to know which connection is sending the packet...!
		protected override abstract void RunImpl(Connection con);

		protected override void RunImpl()
		{
			//Do nothing.
		}

		/// <summary>
		/// Server-side convenience method.
		/// <br></br><br></br>
		/// Casts the given <paramref name="con"/> as a <see cref="PlayerConnection"/> and returns it,
		/// if possible.
		/// </summary>
		/// <param name="con">The <see cref="Connection"/> to convert.</param>
		/// <returns><paramref name="con"/> as a <see cref="PlayerConnection"/>.</returns>
		/// <exception cref="InvalidOperationException">If the connection is not a <see cref="PlayerConnection"/>.</exception>
		protected PlayerConnection GetPlayerConnection(Connection con)
		{
			if (con is PlayerConnection pcon)
			{
				return pcon;
			}
			throw new InvalidOperationException($"Cannot execute {this.GetType().Name} packet " +
					$"while the connection is not of type {nameof(PlayerConnection)}");
		}

		/// <summary>
		/// Server-side convenience method.
		/// <br></br><br></br>
		/// Outputs <paramref name="pcon"/> and <paramref name="game"/> if <paramref name="con"/> is a
		/// <see cref="PlayerConnection"/> playing a game of type <typeparamref name="Game"/>.
		/// </summary>
		/// <typeparam name="Game">A <see cref="GameState"/> class that player is expected to be playing.</typeparam>
		/// <param name="con">The connection this packet was received on.</param>
		/// <param name="pcon">The given <paramref name="con"/> cast as a <see cref="PlayerConnection"/>.</param>
		/// <param name="game">The <see cref="GameState"/> of type <typeparamref name="Game"/> that
		/// <paramref name="pcon"/> is playing.</param>
		/// <exception cref="InvalidOperationException">If <paramref name="con"/> is not a <see cref="PlayerConnection"/>
		/// or if the current game is not of type <typeparamref name="Game"/>.</exception>
		protected void GetPlayerConnectionAndGameState<Game>(Connection con, out PlayerConnection pcon, out Game game)
			where Game : GameState
		{
			if (con is not PlayerConnection playerConnection || Server.GetGameState(playerConnection.Player) is not Game gamestate)
			{
				throw new InvalidOperationException($"Cannot execute {this.GetType().Name} packet " +
					$"while the connection is not of type {nameof(PlayerConnection)} " +
					$"or the registered {nameof(GameState)} is not of type {typeof(Game).Name}.");
			}
			pcon = playerConnection;
			game = gamestate;
		}
	}

	/// <summary>
	/// This is a packet that was sent from the server.
	/// </summary>
	internal abstract class ServerPacket : BasePacket
	{
		/// <summary>
		/// Client-side convenience method.
		/// <br></br><br></br>
		/// Outputs <paramref name="game"/> if the <see cref="Client.LocalGameState"/> is of type <typeparamref name="Game"/>.
		/// </summary>
		/// <typeparam name="Game">A <see cref="GameState"/> class that player is expected to be playing.</typeparam>
		/// <param name="game">The <see cref="GameState"/> of type <typeparamref name="Game"/> that
		/// the player is playing.</param>
		/// <exception cref="InvalidOperationException">If the <see cref="Client.LocalGameState"/> is not
		/// of type <typeparamref name="Game"/></exception>
		protected void GetClientGameState<Game>(out Game game)
			where Game : GameState
		{
			if (Client.LocalGameState is not Game localGame)
			{
				throw new InvalidOperationException($"Cannot execute {this.GetType().Name} packet " +
					$"while the client is not playing in a {typeof(Game).Name}.");
			}

			game = localGame;
		}
	}

	/// <summary>
	/// A packet that can be sent across the network over a <see cref="Connection"/>.
	/// <br></br><br></br>
	/// Concrete subclasses must mark themselves with the following Attribute:<br></br>
	/// <list type="bullet"><see cref="PacketAttribute"/></list>
	/// </summary>
	internal abstract class BasePacket
	{
		private const byte VERSION = 0; //This will likely never be used in any real sense.

		/// <summary>
		/// A buffer of bytes this packet can write to or read from. This is used as a middle-man
		/// between the network and the server/client so the socket can read or write all
		/// packet information at once.
		/// </summary>
		private ByteArrayStream buffer = null!;

		/// <summary>
		/// Peeks at the raw header of the incoming packet, and returns a <see cref="PacketHeader"/>
		/// that represents it. The <paramref name="buffer"/> will be filled with all bytes of the
		/// identified packet.
		/// </summary>
		/// <param name="con">The <see cref="Connection"/> with a pending packet.</param>
		/// <param name="buffer">A <see cref="ByteArrayStream"/> that will be filled with the packet data.</param>
		/// <returns>A <see cref="PacketHeader"/> representing the packet that has been filled into the
		/// <paramref name="buffer"/>.</returns>
		private static PacketHeader ExamineHeader(Connection con, out ByteArrayStream buffer)
		{
			//TODO: This implementation is likely to cause problems later...
			PacketHeader header = new(con.Peek(PacketHeader.HEADER_SIZE));

			buffer = new(con.Read(header.size));
			return header;
		}

		/// <summary>
		/// Writes the given <paramref name="packet"/> out to the given <see cref="Connection"/>. If something
		/// goes wrong, this method will return false.
		/// </summary>
		/// <param name="con">The <see cref="Connection"/> to write the <paramref name="packet"/> out to.</param>
		/// <param name="packet">The <see cref="BasePacket"/> implementation that represents the packet to write.</param>
		/// <returns>True if successful, false otherwise.</returns>
		public static bool Send(Connection con, BasePacket packet)
		{
			//TODO: Should probably handle this better...
			try
			{
				packet.Write(con);
				return true;
			}
			catch (Exception e)
			{
				Debug.Fail($"{e.GetType().Name}: {e.Message}", e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Reads the next packet from the given <see cref="Connection"/>, constructs the associated packet class,
		/// and calls its <see cref="RunImpl"/>. If something goes wrong, this method will return false.
		/// <br></br><br></br>
		/// It's possible that this method will reject the given connection and dispose of it; false will be
		/// returned in this case. Callers should check if <paramref name="con"/> is still valid and active after
		/// this method returns.
		/// </summary>
		/// <param name="con">The <see cref="Connection"/> with an incoming packet.</param>
		/// <returns>True if successful, false otherwise.</returns>
		public static bool Receive(Connection con)
		{
			//TODO: Should probably handle this better...
			try
			{
				PacketHeader header = ExamineHeader(con, out ByteArrayStream packetBuffer);
				BasePacket packet;
				try
				{
					packet = PacketAttribute.CreatePacket(header);

					//This attribute must exist for the above method to return normally.
					PacketAttribute? packetInfo = Attribute.GetCustomAttribute(packet.GetType(), typeof(PacketAttribute)) as PacketAttribute;
					if (!con.MatchesAnyConnectionState(packetInfo!.ValidState))
					{
						throw new ArgumentException($"Connection has invalid connection state for packet: {con.GetType().Name}");
					}
				}
				catch (ArgumentException)
				{
					//For clarity: this also catches the exception from the CreatePacket method call.
					System.Diagnostics.Debug.WriteLine("A connection sent an invalid packet; discarding connection.");
					con.Close();
					return false;
				}
				
				packet.Read(header, packetBuffer);

				packet.RunImpl(con);

				return true;
			}
			catch (Exception e)
			{
				Debug.Fail($"{e.GetType().Name}: {e.Message}", e.StackTrace);
				return false;
			}
		}

		public static void RunLocally(BasePacket packet, LocalConnection con)
		{
			packet.RunImpl(con);
		}

		/// <summary>
		/// Validates that the packet header matches this packet implementation, and then reads all
		/// data from the <paramref name="buffer"/>.
		/// </summary>
		/// <param name="head">The <see cref="PacketHeader"/> of this packet.</param>
		/// <param name="buffer">A buffer containing all bytes of this packet.</param>
		/// <exception cref="ProtocolException">If this packet is not implemented properly,
		/// or the <paramref name="head"/> does not match the <paramref name="buffer"/>.</exception>
		private void Read(PacketHeader head, ByteArrayStream buffer)
		{
			if (Attribute.GetCustomAttribute(this.GetType(), typeof(PacketAttribute)) is not PacketAttribute packetInfo)
			{
				//Shouldn't even be possible
				throw new ProtocolException($"The packet {this.GetType().Name} does not declare either its protocol, its opcode, or both.");
			}

			if (!head.IsValid(packetInfo.Protocol, packetInfo.Opcode))
			{
				throw new ProtocolException("The given packet header does not match this packet implementation!");
			}

			if (head.size != buffer.Size)
			{
				throw new ProtocolException("The given packet header does not match this packet's buffer!");
			}
			this.buffer = buffer;

			//Skip header
			this.buffer.Position = PacketHeader.HEADER_SIZE;

			//Let subclass read information
			ReadImpl();
		}

		/// <summary>
		/// Writes this packet out to the given <see cref="Connection"/>.
		/// </summary>
		/// <param name="con">The <see cref="Connection"/> to write this packet out to.</param>
		/// <exception cref="ProtocolException">If this packet is not implemented properly, or violates the protocol.</exception>
		private void Write(Connection con)
		{
			if (Attribute.GetCustomAttribute(this.GetType(), typeof(PacketAttribute)) is not PacketAttribute packetInfo)
			{
				throw new ProtocolException($"The packet {this.GetType().Name} does not declare either its protocol, its opcode, or both.");
			}
			buffer = new ByteArrayStream();

			PacketHeader header = new(PacketHeader.HEADER_SIZE, packetInfo.Protocol, packetInfo.Opcode, VERSION);
			header.Write(buffer);

			//Let subclass write information
			WriteImpl();

			if (buffer.Size > short.MaxValue)
			{
				throw new ProtocolException($"The packet {this.GetType().Name} writes more data than is allowable by the Bored with Friends Protocol.");
			}

			//Write size as first two bytes
			buffer.Position = 0;
			buffer.WriteShort((short) buffer.Size);

			con.Write(buffer.Size, buffer.GetBackingArray());
		}

		/// <summary>
		/// Reads information that is specific to this packet's implementation.
		/// 
		/// Implementations may use the Read methods provided by <see cref="BasePacket"/>.
		/// </summary>
		protected abstract void ReadImpl();

		/// <summary>
		/// Writes information that is specific to this packet's implementation.
		/// 
		/// Implementations may use the Write methods provided by <see cref="BasePacket"/>.
		/// </summary>
		protected abstract void WriteImpl();

		/// <summary>
		/// Acts on the information contained within this packet.
		/// <br></br><br></br>
		/// This method is for subclasses that might need access to the Connection
		/// for this packet while acting upon the data. Packets that run on the
		/// server side may wish to have access to the connection in order to
		/// identify the source user. Packets that run on the client side
		/// are unlikely to need the same access, but this method will be here
		/// for that case, too.
		/// <br></br><br></br>
		/// The default implementation merely calls <see cref="RunImpl()"/>, which
		/// should be sufficient for the majority of subclass implementations.
		/// </summary>
		/// <param name="con"></param>
		protected virtual void RunImpl(Connection con)
		{
			RunImpl();
		}

		/// <summary>
		/// Acts on the information contained within this packet.
		/// </summary>
		protected abstract void RunImpl();


		//Read and Write methods for subclasses since they don't have access to the ByteArrayStream

		/// <summary>
		/// Reads the specified number of bytes from <see cref="buffer"/>.
		/// </summary>
		/// <param name="byteCount">The number of bytes to read.</param>
		/// <returns>A byte array containing the information that was read from this packet.</returns>
		protected byte[] Read(int byteCount)
		{
			return buffer.Read(byteCount);
		}

		/// <summary>
		/// Reads the specified number of bytes from the <see cref="buffer"/>, and outputs them in <paramref name="bytes"/>.
		/// </summary>
		/// <param name="byteCount">The number of bytes to read.</param>
		/// <param name="bytes">An array of bytes that will contain the information that was read from this packet.</param>
		protected void Read(int byteCount, out byte[] bytes)
		{
			buffer.Read(byteCount, out byte[] temp);
			bytes = temp;
		}

		/// <summary>
		/// Reads the next byte from this packet's <see cref="buffer"/> and returns it.
		/// </summary>
		/// <returns>The next byte from this packet.</returns>
		protected byte ReadByte()
		{
			return buffer.ReadByte();
		}

		/// <summary>
		/// Reads the next short from this packet's <see cref="buffer"/> and returns it.
		/// </summary>
		/// <returns>The next short from this packet.</returns>
		protected short ReadShort()
		{
			return buffer.ReadShort();
		}

		/// <summary>
		/// Reads the next int from this packet's <see cref="buffer"/> and returns it.
		/// </summary>
		/// <returns>The next int from this packet.</returns>
		protected int ReadInt()
		{
			return buffer.ReadInt();
		}

		/// <summary>
		/// Reads the next long from this packet's <see cref="buffer"/> and returns it.
		/// </summary>
		/// <returns>The next long from this packet.</returns>
		protected long ReadLong()
		{
			return buffer.ReadLong();
		}

		/// <summary>
		/// Reads the next bool from this packet's <see cref="buffer"/> and returns it.
		/// </summary>
		/// <returns>The next bool from this packet.</returns>
		protected bool ReadBool()
		{
			return buffer.ReadBool();
		}

		/// <summary>
		/// Reads the next char from this packet's <see cref="buffer"/> and returns it.
		/// </summary>
		/// <returns>The next char from this packet.</returns>
		protected char ReadChar()
		{
			return buffer.ReadChar();
		}

		/// <summary>
		/// Reads the next string from this packet's <see cref="buffer"/> and returns it.
		/// </summary>
		/// <returns>The next string from this packet.</returns>
		protected string ReadString()
		{
			return buffer.ReadString();
		}

		/// <summary>
		/// Writes the given <paramref name="bytes"/> to this packet's <see cref="buffer"/>.
		/// </summary>
		/// <param name="bytes">An array of bytes to write to this packet.</param>
		protected void Write(byte[] bytes)
		{
			buffer.Write(bytes);
		}

		/// <summary>
		/// Writes the given <paramref name="bytes"/> to this packet's <see cref="buffer"/> up to
		/// the <paramref name="length"/> index.
		/// </summary>
		/// <param name="bytes">An array of bytes to write to this packet.</param>
		/// <param name="length">The number of bytes to write from the array.</param>
		protected void Write(byte[] bytes, int length)
		{
			buffer.Write(bytes, length);
		}

		/// <summary>
		/// Writes the next byte to this packet's <see cref="buffer"/>.
		/// </summary>
		/// <param name="value">The byte to write.</param>
		protected void WriteByte(byte value)
		{
			buffer.WriteByte(value);
		}

		/// <summary>
		/// Writes the next short to this packet's <see cref="buffer"/>.
		/// </summary>
		/// <param name="value">The short to write.</param>
		protected void WriteShort(short value)
		{
			buffer.WriteShort(value);
		}

		/// <summary>
		/// Writes the next int to this packet's <see cref="buffer"/>.
		/// </summary>
		/// <param name="value">The int to write.</param>
		protected void WriteInt(int value)
		{
			buffer.WriteInt(value);
		}

		/// <summary>
		/// Writes the next long to this packet's <see cref="buffer"/>.
		/// </summary>
		/// <param name="value">The long to write.</param>
		protected void WriteLong(long value)
		{
			buffer.WriteLong(value);
		}

		/// <summary>
		/// Writes the next bool to this packet's <see cref="buffer"/>.
		/// </summary>
		/// <param name="value">The bool to write.</param>
		protected void WriteBool(bool value)
		{
			buffer.WriteBool(value);
		}

		/// <summary>
		/// Writes the next char to this packet's <see cref="buffer"/>.
		/// </summary>
		/// <param name="value">The char to write.</param>
		protected void WriteChar(char value)
		{
			buffer.WriteChar(value);
		}

		/// <summary>
		/// Writes the next string to this packet's <see cref="buffer"/>.
		/// </summary>
		/// <param name="value">The string to write.</param>
		protected void WriteString(string value)
		{
			buffer.WriteString(value);
		}
	}
}
