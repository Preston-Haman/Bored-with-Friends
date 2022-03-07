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
			database.AddAsync(newUser);
			database.AddAsync(statistics);
			database.SaveChangesAsync();
		}

		/// <summary>
		/// Searches for a specific user name in the database
		/// </summary>
		/// <param name="userName">The UserName to search for</param>
		/// <returns>The PlayerLogin object containing that user name</returns>
		public static PlayerLogin? RetrieveByUser(string userName)
		{
			DatabaseContext database = new();

			PlayerLogin? user = (from logins in database.PlayerLogins
								 where logins.UserName == userName
								 select logins).SingleOrDefault();

			CheckNull(user);

			return user;
		}

		/// <summary>
		/// Checks if a user name exists in the database
		/// </summary>
		/// <param name="userName">The user name to search for</param>
		/// <returns>True if found</returns>
		public static bool NameExists(string userName)
		{
			DatabaseContext database = new();
			PlayerLogin? user = (from logins in database.PlayerLogins
								 where logins.UserName == userName
								 select logins).SingleOrDefault();

			if (user is null)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Updates a user's password by searching for 
		/// their username in the database
		/// </summary>
		/// <param name="userName">The user to change passwords</param>
		/// <param name="password">The password to change to</param>
		public static void UpdatePassword(string userName, string password)
		{
			PlayerLogin? user = RetrieveByUser(userName);

			user.Password = password;

			DatabaseContext database = new();
			database.Update(user);
			database.SaveChangesAsync();
		}

		public static void DeleteUser(string userName)
		{
			PlayerLogin? user = RetrieveByUser(userName);

			DatabaseContext? database = new();
			database.Remove(user);
			database.SaveChangesAsync();
		}

		public static PlayerStatistics RetreiveStats(string userName)
		{
			//TO DO: Create private retrieve by ID method
			PlayerLogin? user = RetrieveByUser(userName);
			int id = user.PlayerID;

			CheckNull(user);

			DatabaseContext database = new();
			PlayerStatistics? userStats = (from statistics in database.PlayerStatistics
										  where statistics.PlayerID == id
										  select statistics).SingleOrDefault();
			//MessageBox.Show(userStats.ToString());
			return userStats;
		}

		/// <summary>
		/// Checks if a username is null, and thows an ArgumentNulLexception if null.
		/// </summary>
		/// <param name="userName">The object to check if null</param>
		/// <exception cref="ArgumentNullException">Notifies user no such user name exists</exception>
		private static void CheckNull(Object userName)
		{
			if (userName == null)
			{
				throw new ArgumentNullException("No such user name exists");
			}
		}
	}
}
