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
			if (rect.Top() > bounds.Top() || rect.Bottom() <= bounds.Bottom())
			{
				speedY *= -1.0f;
			}
			if (rect.Right() > bounds.Right() || rect.Left() <= bounds.Left())
			{
				speedX *= -1.0f;
			}
		}
	}
}
