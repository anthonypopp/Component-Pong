namespace ComponentConsolePong
{
	/// <summary>
	/// A component that is able to move its owner
	/// </summary>
	public class Mover : Component, Updateable
	{
		public Vector2 speed;

		public Mover(float speedX, float speedY)
		{
			speed = new Vector2(speedX, speedY);
		}

		public virtual void Update(float deltaTime)
		{
			rect.position.MoveBy(deltaTime * speed.x, deltaTime * speed.y);
		}
	}
}
