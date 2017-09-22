using System.Collections.Generic;

namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="Component"/> that responsds to collisions on any paddle in the game
	/// Requires <see cref="WallBouncer"/>
	/// </summary>
	public class PaddleBouncer : Component, Resetable, Updateable
	{
		float framesToWait;
		float framesLeft;
		float speedMultiplier;
		List<Paddle> paddles = new List<Paddle>();
		WallBouncer mover;

		public PaddleBouncer(float framesToWait, float speedMultiplier)
		{
			framesLeft = 0;
			this.framesToWait = framesToWait;
			this.speedMultiplier = speedMultiplier;
		}

		public void Reset()
		{
			paddles = GameObject.GetGameObjectsInGame<Paddle>();
			mover = owner.GetComponent<WallBouncer>();
			framesLeft = 0;
		}

		public void Update(float deltaTime)
		{
			if (framesLeft > 0)
			{
				framesLeft--;
			}
			if (IsColliding())
			{
				framesLeft = framesToWait;
				mover.speed.y *= -speedMultiplier;
			}
		}

		private bool IsColliding()
		{
			foreach (Paddle paddle in paddles)
			{
				if (rect.Intersects(paddle.rect))
				{
					return true;
				}
			}
			return false;
		}
	}
}
