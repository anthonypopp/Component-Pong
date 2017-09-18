using System;
using System.Collections.Generic;
using System.Linq;
using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
    class Program
    {
		[STAThread] //required for input helper
		static void Main(string[] args)
        {
			InputManager.Init();
			Pong pong = new Pong();

			//Keep Playing Until the game has ended
			while (pong.running)
			{
				InputManager.Update();
				pong.Update(TimeManager.TIME_SCALE);

				System.Threading.Thread.Sleep(20);

				pong.Draw();
				pong.Write();
			}
			pong.Quit();
        }
    }
}
