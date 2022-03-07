using BoredWithFriends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Data
{
	internal partial class DatabaseContext
	{
		/// <summary>
		/// Creates a new player account, and a new set of statistics
		/// in the database
		/// </summary>
		/// <param name="userName">The username to add to this account</param>
		/// <param name="password">The password used for this acount</param>
		public static void CreateNewPlayer(string userName, string password)
		{
			PlayerLogin newUser = new()
			{
				UserName = userName,
				Password = password
			};

			PlayerStatistics statistics = new()
			{
				LastPlayedTime = DateTime.Now,
				TotalPlayTime = 0,
				RoundsPlayed = 0,
				Wins = 0,
				Losses = 0
			};

			DatabaseContext database = new();
			database.Add(newUser);
			database.Add(statistics);
			database.SaveChanges();
		}

		/// <summary>
		/// Checks if a user name exists in the database
		/// </summary>
		/// <param name="userName">The user name to search for</param>
		/// <returns>True if found</returns>
		public static bool NameExists(string userName)
		{			
			PlayerLogin? nameSearch = NameSearch(userName);

			if (nameSearch is null)
			{
				return false;
			}
			return true;
		}
		public static void UpdatePassword(string userName, string password)
		{
			//look for username
			throw new NotImplementedException();

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		private static PlayerLogin? NameSearch(string userName)
		{
			DatabaseContext database = new();

			PlayerLogin? nameSearch = (from logins in database.PlayerLogins
									   where logins.UserName == userName
									   select logins).SingleOrDefault();

			return nameSearch;
		}
	}
}
