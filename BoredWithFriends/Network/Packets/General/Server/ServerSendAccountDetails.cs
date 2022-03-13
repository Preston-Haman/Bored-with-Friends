using BoredWithFriends.Data;
using BoredWithFriends.Games;
using BoredWithFriends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network.Packets.General.Server
{
	/// <summary>
	/// Sent by the server in response to <see cref="Client.ClientAccountManagement"/>.
	/// </summary>
	[Packet(typeof(ServerSendAccountDetails), BoredWithFriendsProtocol.General, (short) GeneralOps.ServerSendAccountDetails)]
	internal class ServerSendAccountDetails : ServerPacket
	{
		private PlayerStatistics stats;

		public ServerSendAccountDetails(Player player)
		{
			stats = DatabaseContext.GetPlayerStatistics(player);
		}

		protected override void ReadImpl()
		{
			stats = new()
			{
				PlayerID = ReadInt(),
				LastPlayedTime = new(ReadInt(), ReadInt(), ReadInt(), ReadInt(), ReadInt(), ReadInt(), ReadInt()),
				TotalPlayTime = ReadLong(),
				RoundsPlayed = ReadInt(),
				Wins = ReadInt(),
				Losses = ReadInt()
			};
		}

		protected override void RunImpl()
		{
			Network.Client.RaiseEvent(GeneralEvent.AccountManagementReady, stats);
		}

		protected override void WriteImpl()
		{
			WriteInt(stats.PlayerID);
			WriteInt(stats.LastPlayedTime.Year);
			WriteInt(stats.LastPlayedTime.Month);
			WriteInt(stats.LastPlayedTime.Day);
			WriteInt(stats.LastPlayedTime.Hour);
			WriteInt(stats.LastPlayedTime.Minute);
			WriteInt(stats.LastPlayedTime.Second);
			WriteInt(stats.LastPlayedTime.Millisecond);
			WriteLong(stats.TotalPlayTime);
			WriteInt(stats.RoundsPlayed);
			WriteInt(stats.Wins);
			WriteInt(stats.Losses);
		}
	}
}
