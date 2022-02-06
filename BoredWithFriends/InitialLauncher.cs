namespace BoredWithFriends
{
	public partial class FrmInitialLauncher : Form
	{
		public FrmInitialLauncher()
		{
			InitializeComponent();
		}

        private void btnLocalGameLaunch_Click(object sender, EventArgs e)
        {
			//Prompt user yes or no to login
			DialogResult dialogResult = MessageBox.Show("   Would you like to login to track \n " + 
														"            your game stats?", "Login Prompt", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				//create and open a login form
				FrmLogin loginForm = new();
				loginForm.ShowDialog();
			}
			else if (dialogResult == DialogResult.No)
			{
				//go to game form without logging in
			}
		}
    }
}