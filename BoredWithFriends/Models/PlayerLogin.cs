using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Models
{
	[Index("UserName", IsUnique = true)]
	internal class PlayerLogin
	{
		[Key]
		public int PlayerID { get; private set; }

		[ForeignKey(nameof(PlayerID))]
		public PlayerStatistics PlayerStatisticsID { get; set; } = null!;

		
		public string UserName { get; set; } = null!;

		public string Password { get; set; } = null!;

		public DateTime LastLoginTime { get; set; }

	}
}
