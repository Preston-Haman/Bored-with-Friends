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
	public partial class FrmGameSelection : Form
	{
		public FrmGameSelection()
		{
			InitializeComponent();
			this.ApplyGeneralTheme();
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btMatchFourStart_Click(object sender, EventArgs e)
		{
			FrmMatchFour MatchFourForm = new();
			MatchFourForm.ShowDialog();
		}
	}
}
