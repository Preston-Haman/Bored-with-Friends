using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Forms
{
	internal static class Theme
	{
		/// <summary>
		/// Applies cosmetic changes to a Form and all of its controls.
		/// 
		/// This method iterates through the Controls collection property of the Form;
		/// callers must ensure that all Controls being themed have been added to the
		/// Controls collection. Visual Studio automatically adds controls to the
		/// collection in the InitializeComponent method.
		/// 
		/// Note that because this method alters cosmetic properties that can be set
		/// in Visual Studio's Designer, callers that wish to deviate from this theme
		/// must do so in code after calling this method.
		/// </summary>
		/// <param name="form">The Form this Theme will be applied to</param>
		public static void ApplyGeneralTheme(this Form form)
		{
			//Set Form colours
			form.BackColor = Color.DimGray;
			form.ForeColor = SystemColors.ActiveCaptionText; //Do we want this? 

			//Apply theme to controls
			foreach (Control control in form.Controls)
			{
				//The order of these types may need to change in the future (perhaps alphabetical order will work?).

				//Label
				if (control is Label lbl)
				{
					lbl.ForeColor = SystemColors.ActiveCaption;
					lbl.Font = new Font("Sigmar One", lbl.Font.Size, FontStyle.Regular, GraphicsUnit.Point);
				}

				/*
				//TextBox
				if (control is TextBox txt)
				{
					//Nothing to do, yet.
				}
				*/

				//Button
				if (control is Button btn)
				{
					btn.BackColor = SystemColors.ActiveCaption;
					btn.Cursor = Cursors.Hand;
				}

				//Checkbox
				if (control is CheckBox cbx)
				{
					cbx.ForeColor = SystemColors.ActiveCaption;
				}
			}
		}
	}
}
