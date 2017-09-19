namespace ComponentConsolePong
{
	/// <summary>
	/// A component that is able to move its owner
	/// </summary>
	public class Mover : Component, Updateable
	{
		public Mover(float speedX, float speedY)
		{
			this.speedX = speedX;
			this.speedY = speedY;
		}
		public float speedX;
		public float speedY;
		public virtual void Update(float deltaTime)
		{
			rect.position.MoveBy(deltaTime * speedX, deltaTime * speedY);
		}
	}
}
