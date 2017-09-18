using System.Collections.Generic;

namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="MessageableAdapter"/> that prints each player's score to the game
	/// </summary>
	public class ScoreMessenger : MessageableAdapter, Resetable
	{
		List<Score> scores = new List<Score>();
		public ScoreMessenger()
		{
		}

		public void Reset()
		{
			scores = GetComponentsInGame<Score>();
		}

		public override void Write()
		{
			string message = "Scores - ";
			foreach(Score score in scores)
			{
				message += " Player " + score.GetPlayer() + ": " + score.GetScore() + " ";
			}
			Write(message);
		}

	}
}
