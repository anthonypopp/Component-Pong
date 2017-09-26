namespace ComponentConsolePong
{
	/// <summary>
	/// Represents a ball tha moves and bouces on the screen
	/// </summary>
	public class Ball : Entity
	{
		public Ball(float xSpeed, float ySpeed) : base()
		{
			AddComponent(new WallBouncer(xSpeed, ySpeed, ROOT.rect));
			AddComponent(new Attractor());
			AddComponent(new PaddleBouncer(1, 1.01f));
			AddComponent(new Scoreable());
		}

		public override void Reset()
		{
			base.Reset();
			rect.Set(50, 50, 5, 5).Centerfy();
		}

		public override char GetChar()
		{
			return 'O';
		}
	}
}
