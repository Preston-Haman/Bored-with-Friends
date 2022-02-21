namespace BoredWithFriends.Forms
{
	public partial class FrmLogin : Form
	{
		public FrmLogin()
		{
			InitializeComponent();
			this.ApplyGeneralTheme();
		}

		private void btnBackToInitialLauncher_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnCreateAccount_Click(object sender, EventArgs e)
		{
			//create and open a login form
			FrmAccountCreation AccountCreationForm = new();
			AccountCreationForm.ShowDialog();
		}
	}
}
