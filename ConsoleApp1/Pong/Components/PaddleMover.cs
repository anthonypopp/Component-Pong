using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="Mover"/> GameObject that is controlled by player Input
	/// </summary>
	public class PaddleMover : Mover
	{
		Rectangle bounds;
		public PaddleMover(GameObject owner, Rectangle bounds, float speedX, Process onMoveProcess, Process onStopProcess) : base(owner, speedX, 0)
		{
			enabled = false;
			this.bounds = bounds;
			InputManager.Register(this, OnMovePaddle, onMoveProcess);
			InputManager.Register(this, OnStopPaddle, onStopProcess);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
		}
		
		private void OnMovePaddle()
		{
			enabled = true;
		}

		private void OnStopPaddle()
		{
			enabled = false;
		}
	}
}
