using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentConsolePong
{
	public class TimeManager : GameObject, Resetable
	{
		public static float TIME_SCALE = 1.0f;
		public static bool PLAYING = true;
		public TimeManager(int scoreToBeat, Action resetAction, Action quitAction) : base()
		{
			AddComponent(new EndGame(this, scoreToBeat));
			AddComponent(new ResetGame(this, resetAction, quitAction));
		}

		public void Reset()
		{
			TIME_SCALE = 1.0f;
		}
	}
}
