using BoredWithFriends.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

			if (password.Equals(confirmPassword))
			{
				PlayerLogin newUser = new PlayerLogin()
				{
					UserName = userName;
			}

			Data.DatabaseContext.Add()
			}
	}
	}
}
