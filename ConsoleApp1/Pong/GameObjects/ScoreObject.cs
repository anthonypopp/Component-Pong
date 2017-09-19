namespace ComponentConsolePong
{
	/// <summary>
	/// Represents an area where a score can be made in the game
	/// </summary>
	public class ScoreObject : GameObject
	{
		public ScoreObject(Rectangle scoreRect, int player) : base()
		{
			rect.Set(scoreRect.position, scoreRect.dimention);
			AddComponent(new Score(player));
		}
	}
}
