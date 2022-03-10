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
				Password = password,
				//set playerstatistics ID and create related object with defaults
				PlayerStatisticsID = new()
			};

			DatabaseContext database = new();
			database.AddAsync(newUser);
			database.SaveChangesAsync();
		}

		/// <summary>
		/// Searches for a specific user name in the PlayerLogin database
		/// </summary>
		/// <param name="userName">The userName to search for</param>
		/// <returns>The PlayerLogin object containing that user name</returns>
		/// /// <exception cref="ArgumentNullException">If a userName is not found in the PlayerLogin table</exception>
		public static PlayerLogin GetPlayerLogin(string userName)
		{
			DatabaseContext database = new();

			PlayerLogin? user = (from logins in database.PlayerLogins
								 where logins.UserName == userName
								 select logins).SingleOrDefault();

			if (user != null)
			{
				return user;
			}
			else
			{
				throw new ArgumentNullException($"User name {userName} does not exist");
			}
		}

		/// <summary>
		/// Searches for a specific user in the PlayerStatistics table
		/// </summary>
		/// <param name="userName">The userName to search for</param>
		/// <returns>The PlayerStatistics Object related to the userName from PlayerLogin table</returns>
		/// <exception cref="ArgumentNullException">If a PlayerStatistics PlayerID is not found in the table</exception>
		public static PlayerStatistics GetPlayerStatistics(string userName)
		{
			int playerID = GetPlayerLogin(userName).PlayerID;

			DatabaseContext database = new();

			PlayerStatistics? userStats = database.PlayerStatistics.Find(playerID);

			if (userStats != null)
			{
				return userStats;
			}
			else
			{
				throw new ArgumentNullException($"No PlayerStatistics PlayerID under {userName}");
			}
		}

		/// <summary>
		/// Updates a user's password by searching for 
		/// their username in the database
		/// </summary>
		/// <param name="userName">The user to change passwords</param>
		/// <param name="password">The password to change to</param>
		public static void UpdatePassword(string userName, string password)
		{
			PlayerLogin user = GetPlayerLogin(userName);

			user.Password = password;

			DatabaseContext database = new();
			database.Update(user);
			database.SaveChangesAsync();
		}

		/// <summary>
		/// Deletes all data in database under given userName
		/// </summary>
		/// <param name="userName">The user name of the data to delete</param>
		public static void DeleteUser(string userName)
		{
			PlayerLogin user = GetPlayerLogin(userName);
			PlayerStatistics userStatistics = GetPlayerStatistics(userName);

			DatabaseContext database = new();
			database.Remove(user);
			database.Remove(userStatistics);
			database.SaveChangesAsync();
		}
	}
}
