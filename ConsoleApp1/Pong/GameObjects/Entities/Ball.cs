namespace ComponentConsolePong
{
	/// <summary>
	/// Represents a ball tha moves and bouces on the screen
	/// </summary>
	public class Ball : Entity
	{
		public Ball(float xSpeed, float ySpeed) : base()
		{
			AddComponent(new WallBouncer(this, xSpeed, ySpeed, ROOT.rect));
			AddComponent(new Attractor(this));
			AddComponent(new PaddleBouncer(this, 1, 1.01f));
			AddComponent(new Scoreable(this));
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
