using BoredWithFriends.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoredWithFriends.Controls
{
	internal partial class CtrlMatchFourBoard : UserControl
	{
		/// <summary>
		/// The amount of pixels to use as padding around the holes of the gameboard.
		/// </summary>
		public readonly int inset;

		public MatchFourGameState GameState { get; private set; }

		public CtrlMatchFourBoard() : this(null!)
		{
			//Nothing to do; this is for the designer mostly.
		}

		public CtrlMatchFourBoard(MatchFourGameState gameState, int inset = 3)
		{
			InitializeComponent();
			this.inset = inset;
			GameState = gameState;
		}

		public void NewGame(MatchFourGameState gameState)
		{
			GameState = gameState;
			Refresh();
		}

		private void CtrlMatchFour_Paint(object sender, PaintEventArgs e)
		{
			// Determine size of window
			int width = Size.Width;
			int height = Size.Height;

			//Split size into 7x6 sections
			int sectionWidth = (width / GameState.Columns);
			int sectionHeight = (height / GameState.Rows);

			//Accomodate inset
			int circleWidth = sectionWidth - (inset * 2);
			int circleHeight = sectionHeight - (inset * 2);

			//Get the excess pixels from the above division, and prepare to use it as a padding offset.
			//x and y here will be the top left origin for each section.
			int x = (width % GameState.Columns) / 2;
			int y = (height % GameState.Rows) / 2;

			//Setup colours
			SolidBrush emptyBrush = new(ParentForm.BackColor);
			SolidBrush redBrush = new(Color.Red);
			SolidBrush blueBrush = new(Color.Blue);
			Pen outlinePen = new(Color.Black, 2F);

			//Get the graphics context
			Graphics g = e.Graphics;

			for (int i = 0; i < GameState.Rows; i++)
			{
				for (int j = 0; j < GameState.Columns; j++)
				{
					//Fill sections with circles of blue, red, or empty colour based on GameState
					switch (GameState.GetTokenAt(j, i))
					{
						case MatchFourGameState.BoardToken.Empty:
							g.FillEllipse(emptyBrush, x + (j * sectionWidth) + inset, y + (i * sectionHeight) + inset, circleWidth, circleHeight);
							break;
						case MatchFourGameState.BoardToken.Blue:
							g.FillEllipse(blueBrush, x + (j * sectionWidth) + inset, y + (i * sectionHeight) + inset, circleWidth, circleHeight);
							break;
						case MatchFourGameState.BoardToken.Red:
							g.FillEllipse(redBrush, x + (j * sectionWidth) + inset, y + (i * sectionHeight) + inset, circleWidth, circleHeight);
							break;
						default:
							throw new NotImplementedException();
					}
					
					//Draw circles within the sections as an outline
					g.DrawEllipse(outlinePen, x + (j * sectionWidth) + inset, y + (i * sectionHeight) + inset, circleWidth, circleHeight);
				}
			}
		}
	}
}
