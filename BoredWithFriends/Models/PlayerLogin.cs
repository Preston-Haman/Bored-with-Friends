using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Models
{
	internal class PlayerLogin
	{
		[Key]
		public int PlayerID { get; private set; }

		public PlayerStatistics PlayerStatistics { get; set; } = null!;

		public string UserName { get; set; } = null!;

		public string Password { get; set; } = null!;

		public DateTime LastLoginTime { get; private set; }

	}
}
