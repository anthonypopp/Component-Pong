using System;
using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
	/// <summary>
	/// Determines when the game should be reset
	/// </summary>
	public class ResetGame : Component, Updateable, Resetable, Messageable
	{
		bool gameEnded;
		string message;
		private Action resetAction;
		private Action quitAction;
		public ResetGame(Action resetAction, Action quitAction)
		{
			this.resetAction = resetAction;
			this.quitAction = quitAction;
			InputManager.Register(this, OnReset, Process.RESET_GAME);
			InputManager.Register(this, OnQuit, Process.QUIT_GAME);
		}
		
		public void OnReset()
		{
			if (gameEnded)
			{
				resetAction();
			}
		}


		public void OnQuit()
		{
			quitAction();
		}

		public void Reset()
		{
			gameEnded = false;
			message = "";
		}

		public void Update(float deltaTime)
		{
			if (deltaTime == 0)
			{
				message = "Game is done, please press R to restart or Q to quit";
				gameEnded = true;
			}
			else
			{
				message = "The Game is currently running, Press Q to quit game";
			}
		}

		public void Write()
		{
			Log.Print(message);
		}
	}
}
