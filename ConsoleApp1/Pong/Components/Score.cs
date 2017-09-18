using System.Collections.Generic;

namespace ComponentConsolePong
{
	/// <summary>
	/// A component that checks for <see cref="Scoreable"/> for when to score points to a player
	/// </summary>
	public class Score : Component, Resetable, Updateable
	{

		List<Scoreable> scoreable;
		Dictionary<Scoreable, bool> collidingBook;
		private int player;
		private int score = 0;

		public Score(GameObject owner, int player) : base(owner)
		{
			this.player = player;
			scoreable = new List<Scoreable>();
			collidingBook = new Dictionary<Scoreable, bool>();
		}


		/// <summary> Returns the player number </summary>
		public int GetPlayer()
		{
			return player;
		}

		/// <summary> Returns the score </summary>
		public int GetScore()
		{
			return score;
		}

		public void Reset()
		{
			score = 0;
			scoreable.Clear();
			scoreable = GameObject.GetComponentsInGame<Scoreable>();
			collidingBook = new Dictionary<Scoreable, bool>();
			foreach (Scoreable s in scoreable)
			{
				collidingBook.Add(s, false);
			}
		}

		public void Update(float deltaTime)
		{
			foreach (Scoreable s in scoreable)
			{
				if (rect.Intersects(s.rect))
				{
					if (!collidingBook[s])
					{
						IncrementScore();
						collidingBook[s] = true;
					}
				}
				else
				{
					collidingBook[s] = false;
				}
			}
		}
		private void IncrementScore()
		{
			score++;
		}
	}
}
