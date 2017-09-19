namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="Paddle"/> that is controlled by the computer
	/// </summary>
	public class AIPaddle : Paddle
	{
		public AIPaddle(float attractSpeed) : base()
		{
			AddComponent(new Attractee(attractSpeed));
		}

		//Moves towards the ball
		public override void Reset()
		{
			base.Reset();
			rect.Set(50, 10, 20, 5).Centerfy();
		}
	}
}
