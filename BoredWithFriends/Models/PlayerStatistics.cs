using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Models
{
	internal class PlayerStatistics
	{
		[Key]
		public int PlayerID { get; set; }

		public DateTime LastPlayedTime { get; set; }

		public long TotalPlayTime { get; set; } //in ms.

		public int RoundsPlayed { get; set; }

		public int Wins { get; set; }

		public int Losses { get; set; }

	}
}
