using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets
{
	[AttributeUsage(AttributeTargets.Class)]
	internal class PacketAttribute : Attribute
	{
		public short Protocol { get; }

		public short Opcode { get; }

		public ConnectionState ValidState { get; }

		/// <summary>
		/// Marks the using class as an implementation of <see cref="BasePacket"/>. You must also
		/// declare <see cref="ProtocolAttribute"/> and <see cref="OpcodeAttribute"/>.
		/// </summary>
		/// <param name="classType">The <see cref="Type"/> of the class using this attribute.</param>
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

		private static readonly Dictionary<short, Dictionary<short, Type>> REGISTERED_PACKETS = new();

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

		public static BasePacket CreatePacket(PacketHeader header)
		{
			if (REGISTERED_PACKETS.TryGetValue(header.protocol, out Dictionary<short, Type>? packetsByOpcode))
			{
				if (packetsByOpcode.TryGetValue(header.opcode, out Type? packet))
				{
					//Activator.CreateInstance(packet) can throw an exception if there isn't a paramless constructor.
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
