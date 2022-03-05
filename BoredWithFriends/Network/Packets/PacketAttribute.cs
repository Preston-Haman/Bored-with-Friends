using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets
{
	internal static class PacketLookup
	{
		private static readonly Dictionary<short, Dictionary<short, Type>> REGISTERED_PACKETS = new();

		public static void RegisterPacket(Type packet, short protocol, short opcode)
		{
			if (!REGISTERED_PACKETS.ContainsKey(protocol))
			{
				REGISTERED_PACKETS.Add(protocol, new Dictionary<short, Type>());
			}

			if (REGISTERED_PACKETS.TryGetValue(protocol, out Dictionary<short, Type>? packetsByOpcode))
			{
				packetsByOpcode.Add(opcode, packet);
			}
		}

		public static BasePacket CreatePacket(PacketHeader header)
		{
			if (REGISTERED_PACKETS.TryGetValue(header.protocol, out Dictionary<short, Type>? packetsByOpcode))
			{
				if (packetsByOpcode.TryGetValue(header.opcode, out Type? packet))
				{
					if (Activator.CreateInstance(packet) is BasePacket ret)
					{
						return ret;
					}
				}
			}
			throw new ArgumentException("The requested packet cannot be identified.");
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	internal class PacketAttribute : Attribute
	{
		/// <summary>
		/// Marks the using class as an implementation of <see cref="BasePacket"/>. You must also
		/// declare <see cref="ProtocolAttribute"/> and <see cref="OpcodeAttribute"/>.
		/// </summary>
		/// <param name="classType">The <see cref="Type"/> of the class using this attribute.</param>
		/// <exception cref="ArgumentException">If <paramref name="classType"/> is not a subclass of <see cref="BasePacket"/></exception>
		public PacketAttribute(Type classType)
		{
			if (!classType.IsSubclassOf(typeof(BasePacket)))
			{
				throw new ArgumentException($"The {nameof(PacketAttribute)} class is only allowed on subclasses of {nameof(BasePacket)}");
			}

			if (Attribute.GetCustomAttribute(this.GetType(), typeof(ProtocolAttribute)) is not ProtocolAttribute protocol
				|| Attribute.GetCustomAttribute(this.GetType(), typeof(OpcodeAttribute)) is not OpcodeAttribute opcode)
			{
				throw new ArgumentException($"The packet {classType.Name} does not declare either its protocol, its opcode, or both.");
			}

			PacketLookup.RegisterPacket(classType, protocol.Protocol, opcode.Opcode);
		}
	}
}
