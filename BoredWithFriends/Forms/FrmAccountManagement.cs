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
	internal partial class FrmAccountManagement : Form
	{
		public FrmAccountManagement(PlayerLogin playerLogin, PlayerStatistics playerStats)
		{
			InitializeComponent();
			this.ApplyGeneralTheme();

			//Display user information
			//None of these need any preceeding text, it's already on the Form.
			lblUsername.Text = playerLogin.UserName;

			//Player stats may be refactored later, so I'll put the code for that in a block so it stands out
			{
				lblRoundsPlayed.Text = playerStats.RoundsPlayed.ToString();
				lblWins.Text = playerStats.Wins.ToString();
				lblLosses.Text = playerStats.Losses.ToString();

				//TODO: Convert this into a fancy Days, Hours, Minutes, Seconds display.
				lblPlayTime.Text = (playerStats.TotalPlayTime / 1000).ToString() + " seconds";
			}
		}

		private void btnChangePassword_Click(object sender, EventArgs e)
		{
			if (txtNewPassword.Text == txtConfirmNewPassword.Text)
			{
				string currentPassword = txtCurrentPassword.Text;

				//Ask database to update password, pass currentPassword for verification
				throw new NotImplementedException();
			}
			else
			{
				//TODO: Better message
				MessageBox.Show(this, "The new password does not match the confirmation field", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtConfirmNewPassword.Text = string.Empty;
			}
		}

		private void btnDeleteAccount_Click(object sender, EventArgs e)
		{
			//Confirm with user, ask database to remove account.
			throw new NotImplementedException();
		}
	}
}
