namespace BoredWithFriends.Forms
{
	public partial class FrmInitialLauncher : Form
	{
		public FrmInitialLauncher()
		{
			InitializeComponent();
			this.ApplyGeneralTheme();
		}

		private void btnCreateAccount_Click(object sender, EventArgs e)
		{
			//create and open a login form
			FrmAccountCreation AccountCreationForm = new();
			AccountCreationForm.ShowDialog();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
