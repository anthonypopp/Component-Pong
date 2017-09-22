using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentConsolePong
{
	/// <summary>
	/// Rotates a message in place, to make a string of text looking like a 90's movie theater
	/// </summary>
	class CyclingMessage : Component, Messageable, Resetable, Updateable
	{
		//How long it takes for the message to cycle
		float cycleTime = 0;
		float time;
		string message = "";
		StringBuilder title = new StringBuilder();

		public CyclingMessage(string message, float cycleTime)
		{
			this.message = message;
			this.cycleTime = cycleTime;
		}

		public void Reset()
		{
			time = 0;
		}

		public void Update(float deltaTime)
		{
			time += deltaTime;

		}

		public void Write()
		{
			title.Clear();
			int index = GetIndex();
			int i = index;
			for (; i < message.Length; i++)
			{
				title.Append(message[i]);
			}
			for (i = 0; i < index; i++)
			{
				title.Append(message[i]);
			}
			Log.Print(title.ToString());
		}

		private int GetIndex()
		{
			float percent = (time / cycleTime);
			percent = percent - (int)percent;
			return (int)((message.Length) * percent);
		}
	}
}
