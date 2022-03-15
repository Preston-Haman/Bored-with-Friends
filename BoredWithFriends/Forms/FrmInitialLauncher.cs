using BoredWithFriends.Games;
using BoredWithFriends.Network;
using BoredWithFriends.Network.Packets;
using BoredWithFriends.Network.Packets.General.Client;
using BoredWithFriends.Network.Packets.General.Server;
using System.Net;

namespace BoredWithFriends.Forms
{
	public partial class FrmInitialLauncher : Form
	{
		private bool accountCreationIsOpen = false;

		public FrmInitialLauncher()
		{
			InitializeComponent();
			this.ApplyGeneralTheme();
			StartPosition = FormStartPosition.CenterScreen;

			Client.GeneralEvents += GeneralEventHandler;

			//TODO: Remove for online testing (requires server version of app running)
			cbxPlayOnline.Enabled = false;
		}

		private void cbxPlayOnline_CheckedChanged(object sender, EventArgs e)
		{
			txtServerIP.Enabled = txtPort.Enabled = cbxPlayOnline.Checked;
		}

		private void btnCreateAccount_Click(object sender, EventArgs e)
		{
			string username = txtUsername.Text;
			string password = txtPassword.Text;
			bool local = !cbxPlayOnline.Checked;

			FrmAccountCreation? frmAccountCreation;

			if (local)
			{
				frmAccountCreation = new(username, password, local);
			}
			else if (IPAddress.TryParse(txtServerIP.Text, out IPAddress? _) && int.TryParse(txtPort.Text, out int port))
			{
				frmAccountCreation = new(username, password, local, txtServerIP.Text, port);
			}
			else
			{
				MessageBox.Show(this, "Please double check your server IP and port input.", "Internal Error:", MessageBoxButtons.OK);
				return;
			}

			accountCreationIsOpen = true;
			frmAccountCreation.Closed += (s, args) => { accountCreationIsOpen = false; };
			frmAccountCreation.ShowDialog();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			if (!cbxPlayOnline.Checked)
			{
				//Local server
				Server.StartServer();
				PacketSendUtility.SendLocalPacket(new LocalClientConnection(), new ClientLogin(txtUsername.Text, txtPassword.Text));
			}
			else if (IPAddress.TryParse(txtServerIP.Text, out IPAddress? _) && int.TryParse(txtPort.Text, out int port))
			{
				Client.StartClient(txtServerIP.Text, int.Parse(txtPort.Text));
			}
			else
			{
				MessageBox.Show(this, "Please double check your server IP and port input.", "Internal Error:", MessageBoxButtons.OK);
				return;
			}
		}

		private void btnGuest_Click(object sender, EventArgs e)
		{
			Player player = new("Guest");
			OpenGameplayForm(player, true);
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void FrmInitialLauncher_Click(object sender, EventArgs e)
		{
			this.ActiveControl = null;
		}

		private void OpenGameplayForm(Player player, bool local)
		{
			FrmGameplay frmGameplay = new();
			frmGameplay.LocalMode = local;

			GeneralGameState generalGameState = new(frmGameplay);
			generalGameState.AddPlayer(player);

			if (local)
			{
				Server.StartServer(); //Local server
				Server.LocalGameState = generalGameState;
				if (player.IsGuest)
				{
					Server.AddPlayerConnection(new LocalConnection(player));
				}
			}
			else
			{
				Client.LocalGameState = generalGameState;
			}

			frmGameplay.Closed += (s, args) => { this.Close(); };
			frmGameplay.Show();
			Visible = false;
		}

		private void GeneralEventHandler(object? sender, GeneralEvent genEvent)
		{
			switch (genEvent)
			{
				case GeneralEvent.ConnectionFailed:
					string msg = "The server did not respond. Please verify that the IP and port information" +
						"is correct, and that the server is online, then try again.";
					MessageBox.Show(msg, "Connection Error:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				case GeneralEvent.LoginReady:
					PacketSendUtility.SendPacket(new ClientLogin(txtUsername.Text, txtPassword.Text, accountCreationIsOpen));
					break;
				case GeneralEvent.LoginFailedInvalidCredentials:
					string msg2 = "Your login credentials were not recognized; please double check that you" +
						"have entered the correct information and try again.";
					MessageBox.Show(this, msg2, "Connection Error:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				case GeneralEvent.LoggedIn:
					if (sender is LoginResult login)
					{
						Player player = new(login.PlayerID, login.Username);
						OpenGameplayForm(player, !cbxPlayOnline.Checked);
					}
					break;
				case GeneralEvent.ClientStopped:
					Visible = true;
					string msg3 = "You are no longer connected to the server; please try again later.";
					MessageBox.Show(this, msg3, "Network Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				default:
					//Unsupported event; do nothing.
					break;
			}
		}
	}
}
