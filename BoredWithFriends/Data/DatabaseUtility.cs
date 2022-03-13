using BoredWithFriends.Games;
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

			using DatabaseContext database = new();
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
			using DatabaseContext database = new();

			PlayerLogin? userLogin = (from logins in database.PlayerLogins
								 where logins.UserName == userName
								 select logins).SingleOrDefault();

			if (userLogin != null)
			{
				Player user = new(userLogin.PlayerID, userLogin.UserName);

				userLogin.SetLastLoginToNow();
				database.SaveChangesAsync();
				return userLogin;

			}
			else
			{
				throw new ArgumentNullException($"User name {userName} does not exist");
			}
		}

		/// <summary>
		/// Searches for a specific user in the PlayerStatistics table
		/// </summary>
		/// <param name="user">The user to search for</param>
		/// <returns>The PlayerStatistics Object related to the userName from PlayerLogin table</returns>
		/// <exception cref="ArgumentNullException">If a PlayerStatistics PlayerID is not found in the table</exception>
		public static PlayerStatistics GetPlayerStatistics(Player user)

		{
			using DatabaseContext database = new();

			PlayerStatistics? userStats = database.PlayerStatistics.Find(user.PlayerID);

			if (userStats != null)
			{
				return userStats;
			}
			else
			{
				throw new ArgumentNullException($"No PlayerStatistics PlayerID under {user.Name}");
			}
		}

		/// <summary>
		/// Searches for a specific user in the PlayerStatistics table
		/// </summary>
		/// <param name="userLogin">The userName to search for</param>
		/// <returns>The PlayerStatistics Object related to the userName from PlayerLogin table</returns>
		/// <exception cref="ArgumentNullException">If a PlayerStatistics PlayerID is not found in the table</exception>
		public static PlayerStatistics GetPlayerStatistics(PlayerLogin userLogin)
		{
			using DatabaseContext database = new();

			PlayerStatistics? userStats = database.PlayerStatistics.Find(userLogin.PlayerID);

			if (userStats != null)
			{
				return userStats;
			}
			else
			{
				throw new ArgumentNullException($"No PlayerStatistics PlayerID under {userLogin.UserName}");
			}
		}


		/// <summary>
		/// Checks if a user name exists in the database
		/// </summary>
		/// <param name="userName">The user name to search for</param>
		/// <returns>True if found</returns>
		public static bool NameExists(string userName)
		{
			using DatabaseContext database = new();
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
		/// Updates a user's password by using their PlayerLogin and given string
		/// </summary>
		/// <param name="user">The user to change passwords</param>
		/// <param name="newPassword">The password to change to</param>
		public static void UpdatePassword(PlayerLogin user, string newPassword)
		{
			user.Password = newPassword;

			using DatabaseContext database = new();
			database.Update(user);
			database.SaveChangesAsync();
		}

		/// <summary>
		/// Deletes all data in database linked to a PlayerLogin
		/// </summary>
		/// <param name="userName">The user name of the data to delete</param>
		public static void DeleteUser(PlayerLogin user)
		{
			PlayerStatistics userStatistics = GetPlayerStatistics(user);
			
			using DatabaseContext database = new();			

			database.Remove(user);
			database.Remove(userStatistics);
			database.SaveChangesAsync();
		}

		/// <summary>
		/// Adds a win when a player wins a game
		/// </summary>
		/// <param name="user">User to add a win to</param>
		public static void AddWin(Player user)
		{
			PlayerStatistics userStats = GetPlayerStatistics(user);
			userStats.Wins++;
			userStats.RoundsPlayed++;

			using DatabaseContext database = new();
			database.Update(userStats);
			database.SaveChangesAsync();
		}

		/// <summary>
		/// Adds a loss when a player loses a game 
		/// </summary>
		/// <param name="user">User to give one loss to</param>
		public static void AddLoss(Player user)
		{			
			PlayerStatistics userStats = GetPlayerStatistics(user);
			userStats.Losses++;
			userStats.RoundsPlayed++;

			using DatabaseContext database = new();
			database.Update(userStats);
			database.SaveChangesAsync();
			
		}

		/// <summary>
		/// Adds up total time logged in during a session in milisceonds
		/// to reflect how long a user has been logged in since creation.
		/// </summary>
		/// <param name="user">Player to add time to</param>
		public static void AddPlayTime(Player user)
		{
			PlayerStatistics userStats = GetPlayerStatistics(user);
			PlayerLogin userLogin = GetPlayerLogin(user.Name);

			TimeSpan currentSessionTime = DateTime.Now.Subtract(userLogin.LastLoginTime);
			long timeInMiliseconds = (long)currentSessionTime.TotalMilliseconds;

			userStats.TotalPlayTime += timeInMiliseconds;

			using DatabaseContext database = new();
			database.Update(userStats);
			database.SaveChangesAsync();

		}
	}
}