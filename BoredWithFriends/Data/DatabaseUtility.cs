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
		/// Validates a player login attempt. If true is returned, the <paramref name="playerID"/>
		/// will be populated properly; else it will be set to -1. True will be returned if either
		/// a new account with the given credentials was created, or an existing account with the
		/// given credentials exists.
		/// <br></br><br></br>
		/// If <paramref name="createNew"/> is true:<br></br>
		/// An attempt will be made to create a new Player in the database, if it succeeds the new
		/// player's playerID will be output, and true will be returned.
		/// <br></br><br></br>
		/// if <paramref name="createNew"/> is false:<br></br>
		/// An attempt to identify the given credentials as an existing user will be made. If the
		/// user exists, and the passwords match, then true will be returned.
		/// </summary>
		/// <param name="username">The name of the player.</param>
		/// <param name="password">The password of the player.</param>
		/// <param name="createNew">Whether or not a new account should be made with the given credentials.</param>
		/// <param name="playerID">The PlayerID of the valid player, or -1 if the credentials are invalid.</param>
		/// <returns>True if the given credentials are valid, false otherwise.</returns>
		public static bool AreValidCredentials(string username, string password, bool createNew, out int playerID)
		{
			playerID = -1;
			if (createNew)
			{
				if (NameExists(username))
				{
					return false;
				}

				playerID = CreateNewPlayer(username, password);
				return true;
			}
			else
			{
				PlayerLogin user = GetPlayerLogin(username);
				if (user.Password == password)
				{
					playerID = user.PlayerID;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Creates a new player account, and a new set of statistics
		/// in the database and returns the new account's PlayerID.
		/// </summary>
		/// <param name="userName">The username to add to this account</param>
		/// <param name="password">The password used for this account</param>
		/// <returns>The PlayerID of the new account.</returns>
		public static int CreateNewPlayer(string userName, string password)
		{
			PlayerLogin newUser = new()
			{
				UserName = userName,
				Password = password,
				//set player statistics ID and create related object with defaults
				PlayerStatisticsID = new()
			};

			using DatabaseContext database = new();
			database.Add(newUser);
			database.SaveChanges();
			return newUser.PlayerID;
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
				database.SaveChanges();
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
			database.SaveChanges();
		}

		/// <summary>
		/// Updates the given <paramref name="player"/> password with the <paramref name="newPassword"/>
		/// if <paramref name="currentPassword"/> matches the password in the database. If the passwords
		/// matched, and an update was attempted, true is returned; false otherwise.
		/// </summary>
		/// <param name="player">The player who wants to update their password.</param>
		/// <param name="currentPassword">The current password of the <paramref name="player"/>.</param>
		/// <param name="newPassword">The new password to set.</param>
		/// <returns>True if an attempt to update the password in the database was made.</returns>
		public static bool UpdatePassword(Player player, string currentPassword, string newPassword)
		{
			using DatabaseContext database = new();
			PlayerLogin? userLogin = (from logins in database.PlayerLogins
									  where logins.UserName == player.Name
									  select logins).SingleOrDefault();

			if (userLogin is not null && userLogin.Password == currentPassword)
			{
				userLogin.Password = newPassword;
				database.Update(userLogin);
				database.SaveChanges();
				return true;
			}
			return false;
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
			database.SaveChanges();
		}

		/// <summary>
		/// Checks if the given <paramref name="password"/> matches the password on record for
		/// the given <paramref name="player"/>, and deletes the player's account if so. If the
		/// account has been deleted, true is returned; false otherwise.
		/// </summary>
		/// <param name="player">The <see cref="Player"/> assigned to the account to delete.</param>
		/// <param name="password">The current password for the account.</param>
		/// <returns>True if the player's account was deleted; false otherwise.</returns>
		public static bool DeleteUser(Player player, string password)
		{
			PlayerLogin user = GetPlayerLogin(player.Name);
			if (user.Password == password)
			{
				DeleteUser(user);
				return true;
			}
			return false;
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
			database.SaveChanges();
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
			database.SaveChanges();
			
		}

		/// <summary>
		/// Adds up total time logged in during a session in milliseconds
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
			database.SaveChanges();

		}
	}
}
