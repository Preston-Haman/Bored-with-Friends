using BoredWithFriends.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Data
{
	internal class DatabaseContext: DbContext
	{
		public DbSet<PlayerLogin> PlayerLogins { get; set; } = null!;

		public DbSet<PlayerStatistics> PlayerStatistics { get; set; } = null!;

		public DatabaseContext()
		{
			
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			// Database = The desired name for the database
			// Server = The server we are connecting to. LocalDB is included with VS
			// Trusted_Connection - indicates that our windows account should be used
			options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BoredWithFriends;Trusted_Connection=True;");
		}
	}
}
