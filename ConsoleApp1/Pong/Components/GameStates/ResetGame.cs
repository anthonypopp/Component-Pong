using System;
using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
	/// <summary>
	/// Determines when the game should be reset
	/// </summary>
	public class ResetGame : Component, Updateable, Resetable, Messageable
	{
		bool canReset;
		string message;
		private Action resetActionOnEndGame;
		public ResetGame(GameObject owner, Action resetActionOnEndGame) : base(owner)
		{
			this.resetActionOnEndGame = resetActionOnEndGame;
			InputManager.Register(OnReset, Process.RESET_GAME);
		}
		
		public void OnReset()
		{
			if (canReset)
			{
				resetActionOnEndGame();
			}
		}

		public void Reset()
		{
			canReset = false;
			message = "";
		}

		public void Update(float deltaTime)
		{
			if (deltaTime == 0)
			{
				message = "Game is done, please press R to restart";
				canReset = true;
			}
			else
			{
				message = "The Game is currently running";
			}
		}

		public void Write()
		{
			Program.WriteMessage(message);
		}
	}
}
