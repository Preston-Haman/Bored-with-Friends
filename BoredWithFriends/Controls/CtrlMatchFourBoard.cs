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
	internal partial class CtrlMatchFourBoard : UserControl, IMatchFourGui
	{
		/// <summary>
		/// The amount of pixels to use as padding around the holes of the gameboard.
		/// </summary>
		public readonly int inset;

		private MatchFourGameState gameState = null!;

		public CtrlMatchFourBoard() : this(3)
		{
			//Nothing to do; this is for the designer mostly.
		}

		public CtrlMatchFourBoard(int inset = 3)
		{
			InitializeComponent();
			this.inset = inset;
		}

		public void UpdateBoardDisplay(MatchFourGameState game)
		{
			gameState = game;
			Refresh();
		}

		private void CtrlMatchFour_Paint(object sender, PaintEventArgs e)
		{
			// Determine size of window
			int width = Size.Width;
			int height = Size.Height;

			int columns = gameState is null ? 7 : gameState.Columns;
			int rows = gameState is null ? 6 : gameState.Rows;

			//Split size into 7x6 sections
			int sectionWidth = (width / columns);
			int sectionHeight = (height / rows);

			//Accommodate inset
			int circleWidth = sectionWidth - (inset * 2);
			int circleHeight = sectionHeight - (inset * 2);

			//Get the excess pixels from the above division, and prepare to use it as a padding offset.
			//x and y here will be the top left origin for each section.
			int x = (width % columns) / 2;
			int y = (height % rows) / 2;

			//Setup colours
			SolidBrush emptyBrush = new(ParentForm.BackColor);
			SolidBrush blueBrush = new(Color.Blue);
			SolidBrush redBrush = new(Color.Red);
			Pen outlinePen = new(Color.Black, 2F);

			//Get the graphics context
			Graphics g = e.Graphics;

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					//Fill sections with circles of blue, red, or empty colour based on GameState
					switch (gameState is null ? MatchFourGameState.BoardToken.Empty : gameState.GetTokenAt(i, j))
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
