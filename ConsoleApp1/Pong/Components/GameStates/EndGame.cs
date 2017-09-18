using System.Collections.Generic;

namespace ComponentConsolePong
{
	/// <summary>
	/// Determines if the game has ended
	/// </summary>
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
					if (winner.GetScore() < score.GetScore())
					{
						winner = score;
					}

					//Check to see if their is a winner
					if (winner.GetScore() >= scoreToWin)
					{
						TimeManager.TIME_SCALE = 0;
						break;
					}
				}
				title = "Player " + winner.GetPlayer() + " is in the lead";
			}
			else
			{
				title = "Player " + winner.GetPlayer() + " Won!";
			}
		}

		public void Write()
		{
			Log.Print(title);
		}
	}
}
