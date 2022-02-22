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

			bool nameTaken = NameExists(userName);
			bool nameBlank = string.IsNullOrEmpty(userName);
			bool passwordsMatch = password.Equals(confirmPassword);

			if (nameTaken)
			{
				MessageBox.Show("This username has been taken");
			}
			if (nameBlank)
				{
					MessageBox.Show("Username cannot be blank");
				}
			if (!passwordsMatch)
			{
				MessageBox.Show("Passwords do not match");
			}
			if (!nameTaken && !nameBlank && passwordsMatch)
			{
				PlayerLogin newUser = new()
				{
					UserName = userName,	
					Password = password,
				};

				DatabaseContext database = new();
				database.Add(newUser);
				database.SaveChanges();
			}
		}
		/// <summary>
		/// Checks if a user name exists in the database
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		private static bool NameExists(string userName)
		{
			DatabaseContext database = new();

			PlayerLogin ? nameSearch = (from logins in database.PlayerLogins
								   where logins.UserName == userName
								   select logins).SingleOrDefault();
			//object? login = null;
			if (nameSearch is null)
			{
				return false;
			}
			return true;
		}

	}
}
