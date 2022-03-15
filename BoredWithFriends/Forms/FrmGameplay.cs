using BoredWithFriends.Games;
using BoredWithFriends.Network;
using BoredWithFriends.Network.Packets;
using BoredWithFriends.Network.Packets.General.Server;
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
	internal partial class FrmGameplay : Form, IGeneralGameGui
	{
		public bool LocalMode
		{
			get
			{
				return ctrlPlayerList1.LocalMode;
			}

			set
			{
				ctrlPlayerList1.LocalMode = value;
			}
		}

		public FrmGameplay()
		{
			InitializeComponent();
			this.ApplyGeneralTheme();
			StartPosition = FormStartPosition.CenterScreen;

			Client.GeneralEvents += GeneralEventHandler;
		}

		public void AddPlayer(GameState game, Player player, bool spectator = false)
		{
			ctrlPlayerList1.AddPlayer(spectator, player);
		}

		public void DeclareGameEnd(GameState game)
		{
			string msg;
			if (game.GameHasWinner)
			{
				StringBuilder str = new();
				str.Append("The game has ended. ");

				for (int i = 0; i < game.Winners.Count - 1; i++)
				{
					str.Append(game.Winners[i]);
					str.Append(", ");
				}

				if (game.Winners.Count > 1)
				{
					str.Append("and ");
				}

				str.Append(game.Winners[^1]);
				str.Append(" Won!");
				msg = str.ToString();
			}
			else
			{
				msg = "The game has ended in a stalemate.";
			}
			MessageBox.Show(this, msg, "The Game Ended!", MessageBoxButtons.OK);
		}

		public void GameStateChanged(GameState newGameState)
		{
			foreach (Form mdi in MdiChildren)
			{
				mdi.Close();
			}
			
			ctrlPlayerList1.Clear();
			ctrlPlayerList1.AddPlayer(players: newGameState.Players.ToArray());
			ctrlPlayerList1.AddPlayer(true, newGameState.Spectators.ToArray());

			Form? frm = null;

			if (newGameState is GeneralGameState)
			{
				frm = new FrmGameSelection();
			}

			if (newGameState is MatchFourGameState matchFour)
			{
				frm = new FrmMatchFour(matchFour);
			}

			if (frm is not null)
			{
				frm.FormBorderStyle = FormBorderStyle.None;
				frm.StartPosition = FormStartPosition.Manual;
				frm.Location = new Point(212, 0);
				frm.MdiParent = this;
				frm.Visible = true;
			}
		}

		public void RemovePlayer(GameState game, Player player, bool spectator = false)
		{
			ctrlPlayerList1.RemovePlayer(player, spectator);
		}

		public void UpdateActivePlayer(GameState game, Player player)
		{
			ctrlPlayerList1.SetActivePlayer(player);
		}

		public void GeneralEventHandler(object? sender, GeneralEvent genEvent)
		{
			switch (genEvent)
			{
				case GeneralEvent.EnteredGameLobby:
					if (sender is LobbyResult lobbyResult)
					{
						LobbyGameState lobby = new(lobbyResult.GameChoice, this);
						foreach (Player p in lobbyResult.Players)
						{
							lobby.QueuePlayer(p);
						}
						Client.LocalGameState = lobby;
					}
					break;
				case GeneralEvent.GameStart:
					if (sender is GameStartResult gameStartResult)
					{
						List<Player> players = gameStartResult.Players;
						int pCount = players.Count;

						switch (gameStartResult.Protocol)
						{
							case BoredWithFriendsProtocol.General:
								throw new InvalidOperationException();
							case BoredWithFriendsProtocol.MatchFour:
								MatchFourGameState newGame = new(6, 7, pCount > 0 ? players[0] : null!, pCount > 1 ? players[1] : null!, this);
								Client.LocalGameState = newGame;
								break;
							default:
								throw new NotImplementedException();
						}
					}
					break;
				default:
					//Unsupported event; do nothing.
					break;
			}
		}
	}
}
