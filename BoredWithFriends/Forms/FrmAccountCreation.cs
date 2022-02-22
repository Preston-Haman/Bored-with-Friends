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

			if (!password.Equals(confirmPassword) || !isValidName(userName)){
				if (!password.Equals(confirmPassword))
				{
					MessageBox.Show("password mismatch");
				}
				if (!isValidName(userName))
				{
					MessageBox.Show("not valid username");
				}
			}
			else
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

		private static bool isValidName(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
			{
				return false;
			}
			if (NameExists(userName))
			{
				return false;
			}
			return true;
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
