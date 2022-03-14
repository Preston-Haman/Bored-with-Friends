using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Games
{
	internal class GeneralGameState : GameState
	{
		public GeneralGameState(IGeneralGameGui generalGameGui) : base(generalGameGui)
		{
			//Do nothing.
		}

		public void AddPlayer(Player player)
		{
			AddCompetingPlayers(player);
		}

		public override Player GetCurrentPlayer()
		{
			return Players[0];
		}

		public override int GetMaxPlayers()
		{
			return short.MaxValue; //Arbitrary.
		}

		public override void ResetGame()
		{
			//Nothing to reset.
		}
	}
}
