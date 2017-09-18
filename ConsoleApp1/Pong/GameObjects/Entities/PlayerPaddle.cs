using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="Paddle"/> that is controlled by input
	/// </summary>
	public class PlayerPaddle : Paddle
	{
		public override void Reset()
		{
			base.Reset();
			rect.Set(50, 90, 20, 5).Centerfy();
			AddComponent(new PaddleMover(this, ROOT.rect, -3, Process.MOVE_POSITIVE, Process.NO_MOVE_POSITIVE));
			AddComponent(new PaddleMover(this, ROOT.rect, 3, Process.MOVE_NEGATIVE, Process.NO_MOVE_NEGATIVE));
		}
	}
}
