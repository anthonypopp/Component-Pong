using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentConsolePong
{
	public class TimeHelper : GameObject, Resetable
	{
		public static float TIME_SCALE = 1.0f;
		public TimeHelper(int scoreToBeat, Action resetGameAction) : base()
		{
			AddComponent(new EndGame(this, scoreToBeat));
			AddComponent(new ResetGame(this, resetGameAction));
		}

		public void Reset()
		{
			TIME_SCALE = 1.0f;
		}
	}

	public class ResetGame : Component, Updateable, Resetable, Messageable
	{
		bool canReset;
		string message;
		private Action resetActionOnEndGame;
		public ResetGame(GameObject owner, Action resetActionOnEndGame) : base(owner)
		{
			this.resetActionOnEndGame = resetActionOnEndGame;
			InputHelper.Register(Do, Process.RESET_GAME);
		}

		public void Do()
		{
			if (canReset)
			{
				resetActionOnEndGame();
			}
		}

		public void Reset()
		{
			canReset = false;
			message = "";
		}

		public void Update(float deltaTime)
		{
			if (deltaTime == 0)
			{
				message = "Game is done, please press R to restart";
				canReset = true;
			}
			else
			{
				message = "The Game is currently running";
			}
		}

		public void Write()
		{
			Program.WriteMessage(message);
		}
	}
	public class EndGame : Component, Updateable, Resetable, Messageable
	{
		List<Score> scores = new List<Score>();
		string title = "";
		Score winner = null;
		int scoreToWin;
		public EndGame(GameObject owner, int scoreToWin) : base(owner)
		{
			this.scoreToWin = scoreToWin;
		}

		public void Reset()
		{
			scores = GameObject.GetComponentsInGame<Score>();
			winner = scores[0];
			title = "";
		}

		public void Update(float deltaTime)
		{
			if (deltaTime > 0)
			{
				foreach (Score score in scores)
				{
					//Find who is in the lead
					if (winner.score < score.score)
					{
						winner = score;
					}

					//Check to see if their is a winner
					if (winner.score >= scoreToWin)
					{
						TimeHelper.TIME_SCALE = 0;
						break;
					}
				}
				title = "Player " + winner.playerNum + " is in the lead";
			}
			else
			{
				title = "Player " + winner.playerNum + " Won!";
			}
		}

		public void Write()
		{
			Program.WriteMessage(title);
		}
	}
}
