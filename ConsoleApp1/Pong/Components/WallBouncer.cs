namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="Mover"/> that moves reflects when colliding with a given  <see cref="Rectangle"/>
	/// </summary>
	public class WallBouncer : Mover
	{
		public WallBouncer(float speedX, float speedY, Rectangle bounds)
			: base(speedX, speedY)
		{
			this.bounds = bounds;
		}

		Rectangle bounds;
		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			if (rect.Right() > bounds.Right() || rect.Left() <= bounds.Left())
			{
				speed.x *= -1.0f;
			}
			if (rect.Top() > bounds.Top() || rect.Bottom() <= bounds.Bottom())
			{
				speed.y *= -1.0f;
			}
		}
	}
}
