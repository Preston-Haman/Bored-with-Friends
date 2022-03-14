using BoredWithFriends.Data;
using BoredWithFriends.Models;
using BoredWithFriends.Network;
using BoredWithFriends.Network.Packets;
using BoredWithFriends.Network.Packets.General.Client;
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
		private readonly bool local;

		private readonly string serverIP;

		private readonly int port;

		public FrmAccountCreation(string username = "", string password = "", bool local = false, string serverIP = "127.0.0.1", int port = 7777)
		{
			InitializeComponent();
			this.ApplyGeneralTheme();

			Client.GeneralEvents += GeneralEventHandler;

			txtUserName.Text = username;
			txtPassword.Text = password;
			this.local = local;
			this.serverIP = serverIP;
			this.port = port;
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
				if (local)
				{
					//Local server
					Server.StartServer();
					BasePacket.RunLocally(new ClientLogin(userName, password, true), new LocalClientConnection());
				}
				else
				{
					Client.StartClient(serverIP, port);
				}
			}
		}

		/// <summary>
		/// Checks that <paramref name="userName"/> and <paramref name="password"/> are not
		/// empty, and that <paramref name="confirmPassword"/> matches <paramref name="password"/>.
		/// If all checks are okay, then true is returned; otherwise false is returned and error
		/// messages are populated on the GUI.
		/// </summary>
		/// <param name="userName">Username for login</param>
		/// <param name="password">Password for login</param>
		/// <param name="confirmPassword">Confirmation of password to ensure it is the one desired</param>
		/// <returns>True if all checks pass; false otherwise.</returns>
		private bool IsValidLogin(string userName, string password, string confirmPassword)
		{
			//clear warnings to adjust to warnings to needed input adjustments
			ClearWarnings();

			bool nameBlank = string.IsNullOrEmpty(userName);
			bool passwordBlank = string.IsNullOrEmpty(password);
			bool passwordsMatch = password == confirmPassword;

			if (nameBlank)
			{
				lblWarningInvalidName.Text = "Username cannot be blank";				
			}

			if (passwordBlank)
			{
				lblWarningInvalidPassword.Text = "Password cannot be blank";
			}
			else if (!passwordsMatch)
			{
				lblWarningInvalidPassword.Text = "Passwords do not match";
			}

			return (!nameBlank && !passwordBlank && passwordsMatch);
		}

		/// <summary>
		/// Clears all warnings from FrmAccountCreation
		/// </summary>
		private void ClearWarnings()
		{
			lblWarningInvalidName.Text = string.Empty;
			lblWarningInvalidPassword.Text = string.Empty;
		}

		private void GeneralEventHandler(object? sender, GeneralEvent genEvent)
		{
			switch (genEvent)
			{
				case GeneralEvent.ConnectionFailed:
					string msg = "The server did not respond. Please verify that the IP and port information" +
						"is correct, and that the server is online, then try again.";
					MessageBox.Show(this, msg, "Connection Error:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				case GeneralEvent.LoginReady:
					PacketSendUtility.SendPacket(new ClientLogin(txtUserName.Text, txtPassword.Text, true));
					break;
				case GeneralEvent.LoginFailedUsernameNotAvailable:
					lblWarningInvalidName.Text = "This username has been taken";
					break;
				case GeneralEvent.LoggedIn:
					Close();
					Client.GeneralEvents -= GeneralEventHandler;
					new FrmAccountConfirmation().ShowDialog();
					break;
				default:
					//Do nothing; unsupported event.
					break;
			}
		}
	}
}
