using BoredWithFriends.Data;
using BoredWithFriends.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoredWithFriends.Forms
{
	public partial class FrmAccountCreation : Form
	{

		public FrmAccountCreation()
		{
			InitializeComponent();
			this.ApplyGeneralTheme();
		}

		private void btnBackToLogin_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnCreateAccount_Click(object sender, EventArgs e)
		{
			string userName = txtUserName.Text;
			string password = txtPassword.Text;
			string confirmPassword = txtConfirmPassword.Text;

			if (IsValidLogin(userName, password, confirmPassword))
			{
				DatabaseContext.CreateNewPlayer(userName, password);

				Close();
				FrmAccountConfirmation ConfirmNotice = new();
				ConfirmNotice.ShowDialog();
			}
		}
		/// <summary>
		/// Checks if a user name and password is valid to be entered into datbase
		/// </summary>
		/// <param name="userName">Username for login to insert to database</param>
		/// <param name="password">Password for login to insert to database</param>
		/// <param name="confirmPassword">Confirmation of password to ensure it is the one desired</param>
		/// <returns></returns>
		private bool IsValidLogin(string userName, string password, string confirmPassword)
		{
			//clear warnings to adjust to warnings to needed input adjustments
			ClearWarnings();

			bool nameBlank = string.IsNullOrEmpty(userName);
			bool nameTaken = DatabaseContext.NameExists(userName);
			bool passwordBlank = string.IsNullOrEmpty(password);
			bool passwordsMatch = password.Equals(confirmPassword);

			if (nameBlank)
			{
				lblWarningInvalidName.Text = "Username cannot be blank";				
			}
			else if (nameTaken)
			{
				lblWarningInvalidName.Text = "This username has been taken";
			}
			if (passwordBlank)
			{
				lblWarningInvalidPassword.Text = "Password cannot be blank";
			}
			else if (!passwordsMatch)
			{
				lblWarningInvalidPassword.Text = "Passwords do not match";
			}

			return (!nameBlank && !nameTaken && !passwordBlank && passwordsMatch);
		}

		/// <summary>
		/// Clears all warnings from FrmAccountCreation
		/// </summary>
		private void ClearWarnings()
		{
			lblWarningInvalidName.Text = string.Empty;
			lblWarningInvalidPassword.Text = string.Empty;
		}


		//private static bool NameExists(string userName)
		//{
		//	DatabaseContext database = new();

		//	PlayerLogin? nameSearch = (from logins in database.PlayerLogins
		//						   where logins.UserName == userName
		//						   select logins).SingleOrDefault();

		//	if (nameSearch is null)
		//	{
		//		return false;
		//	}
		//	return true;
		//}

	}
}
