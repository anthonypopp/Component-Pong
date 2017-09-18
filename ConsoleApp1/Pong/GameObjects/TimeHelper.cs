using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentConsolePong
{
	public class TimeManager : GameObject, Resetable
	{
		public static float TIME_SCALE = 1.0f;
		public TimeManager(int scoreToBeat, Action resetGameAction) : base()
		{
			AddComponent(new EndGame(this, scoreToBeat));
			AddComponent(new ResetGame(this, resetGameAction));
		}

		public void Reset()
		{
			TIME_SCALE = 1.0f;
		}
	}
}
