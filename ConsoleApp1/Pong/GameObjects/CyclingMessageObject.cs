using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentConsolePong
{
	class CyclingMessageObject : GameObject
	{
		public CyclingMessageObject(string title, float cycleTime)
		{
			AddComponent(new CyclingMessage(title, cycleTime));
		}
	}
}
