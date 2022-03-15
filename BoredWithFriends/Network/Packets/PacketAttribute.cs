using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets
{
	/// <summary>
	/// An Attribute that registers the marked class as an active extension of <see cref="BasePacket"/>.
	/// This Attribute controls packets that are available to be read by this application. If a packet
	/// implementation is not marked by this Attribute, then it will be considered invalid when
	/// information is read from the network connection.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class PacketAttribute : Attribute
	{
		/// <summary>
		/// The protocol associated with the marked packet implementation.
		/// <br></br><br></br>
		/// <seealso cref="BoredWithFriendsProtocol"/>
		/// </summary>
		public short Protocol { get; }

		/// <summary>
		/// The opcode that uniquely identifies this packet. Each protocol has its own
		/// enum set for opcodes stored in the BoredWithFriends.Network.Packets.[Protocol Name]
		/// namespace. Protocol names can be identified in <see cref="BoredWithFriendsProtocol"/>.
		/// </summary>
		public short Opcode { get; }

		/// <summary>
		/// The <see cref="ConnectionState"/> in which this packet is considered valid.
		/// By default, this would be <see cref="ConnectionState.Authed"/>; if a packet
		/// is valid for a different state, it must be specified. Only one state is
		/// supported per packet.
		/// </summary>
		public ConnectionState ValidState { get; }

		/// <summary>
		/// Marks the using class as an implementation of <see cref="BasePacket"/>.
		/// </summary>
		/// <param name="classType">The <see cref="Type"/> of the class using this attribute.</param>
		/// <param name="protocol">The <see cref="BoredWithFriendsProtocol"/> the packet is for.</param>
		/// <param name="opcode">The opcode for this packet; see <seealso cref="Opcode"/> for more information.</param>
		/// <param name="validState">The valid connection state for this packet; see <seealso cref="ValidState"/>
		/// for more information.</param>
		/// <exception cref="ArgumentException">If <paramref name="classType"/> is not a subclass of <see cref="BasePacket"/></exception>
		public PacketAttribute(Type classType, BoredWithFriendsProtocol protocol, short opcode, ConnectionState validState = ConnectionState.Authed)
		{
			if (!classType.IsSubclassOf(typeof(BasePacket)))
			{
				throw new ArgumentException($"The {nameof(PacketAttribute)} class is only allowed on subclasses of {nameof(BasePacket)}");
			}

			if (Enum.GetUnderlyingType(opcode.GetType()) != typeof(short))
			{
				throw new ArgumentException($"The {nameof(opcode)} Enum must use a short for the underlying type.");
			}

			Protocol = (short) protocol;

			Opcode = opcode;

			RegisterPacket(classType, Protocol, Opcode);

			ValidState = validState;
		}

		/// <summary>
		/// A Dictionary of packet types that have been marked with this Attribute.
		/// The Dictionary maps the numeric value of the protocol (as a short) to a second
		/// Dictionary that maps the numeric value of the opcode (as a short) to the
		/// <see cref="Type"/> of the packet implementation.
		/// </summary>
		private static readonly Dictionary<short, Dictionary<short, Type>> REGISTERED_PACKETS = new();

		/// <summary>
		/// Registers the given packet type with <see cref="REGISTERED_PACKETS"/>.
		/// </summary>
		/// <param name="packet">The type of the packet to register.</param>
		/// <param name="protocol">The numeric value of the protocol for this packet.</param>
		/// <param name="opcode">The numeric value of the opcode for this packet.</param>
		/// <exception cref="ArgumentException"></exception>
		private static void RegisterPacket(Type packet, short protocol, short opcode)
		{
			if (!REGISTERED_PACKETS.ContainsKey(protocol))
			{
				REGISTERED_PACKETS.Add(protocol, new Dictionary<short, Type>());
			}

			if (REGISTERED_PACKETS.TryGetValue(protocol, out Dictionary<short, Type>? packetsByOpcode))
			{
				if (packetsByOpcode.ContainsKey(opcode))
				{
					throw new ArgumentException($"The {nameof(opcode)}: {opcode:x} for this {nameof(protocol)}: {protocol:x} is already in use.");
				}
				packetsByOpcode.Add(opcode, packet);
			}
		}

		/// <summary>
		/// Creates an instance of a BasePacket implementation identified by the protocol and opcode
		/// stored within the given <paramref name="header"/> and returns it.
		/// </summary>
		/// <param name="header">A <see cref="PacketHeader"/> representing the type of packet to create.</param>
		/// <returns>The new instance of the packet implementation associated with the given <paramref name="header"/>.</returns>
		/// <exception cref="ArgumentException">If the <paramref name="header"/> does not correspond to any known
		/// packet implementations.</exception>
		public static BasePacket CreatePacket(PacketHeader header)
		{
			if (REGISTERED_PACKETS.TryGetValue(header.protocol, out Dictionary<short, Type>? packetsByOpcode))
			{
				if (packetsByOpcode.TryGetValue(header.opcode, out Type? packet))
				{
					//Activator.CreateInstance(packet) can throw an exception if there isn't a parameterless constructor.
					//Requiring one seems silly, so...
					if (FormatterServices.GetUninitializedObject(packet) is BasePacket ret)
					{
						return ret;
					}
				}
			}
			throw new ArgumentException("The requested packet cannot be identified.");
		}
	}
}
