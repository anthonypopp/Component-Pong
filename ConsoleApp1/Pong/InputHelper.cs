using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ConsoleApp1
{

	public enum Process
	{
		MOVE_POSITIVE,
		MOVE_NEGATIVE,
		NO_MOVE_NEGATIVE,
		NO
		RESET_GAME,
		COUNT
	}

	public static class InputHelper
	{

		public enum KeyRelationShip
		{
			UP_KEY = Key.Up,
			DOWN_KEY = Key.Down, 
			R_KEY = Key.R,
		}
		

		public enum KeyAction
		{
			HELD_DOWN,
			JUST_PRESSED,
			JUST_RELEASED,
			NOT_PRESSED
		}

		private class Processe
		{
			KeyAction action;
			KeyRelationShip key;

			private bool curActivated;
			private bool prevActivated;
			public Processe(KeyAction action, KeyRelationShip key)
			{
				this.action = action;
				this.key = key;
			}
			public void Update()
			{
				prevActivated = curActivated;
				curActivated = Keyboard.IsKeyDown((Key)key);
			}
			public bool Activated()
			{
				switch (action)
				{
					case KeyAction.HELD_DOWN:
						return curActivated;
					case KeyAction.NOT_PRESSED:
						return !curActivated;
					case KeyAction.JUST_PRESSED:
						return curActivated && !prevActivated;
					case KeyAction.JUST_RELEASED:
						return !curActivated && prevActivated;

				}
				return false;
			}
		}
		static Dictionary<Process, List<Action>> register = new Dictionary<Process, List<Action>>();
		static Dictionary<Process, Processe> inputBook = new Dictionary<Process, Processe>();

		public static void Init()
		{
			inputBook.Add(Process.MOVE_NEGATIVE, new Processe(KeyAction.HELD_DOWN, KeyRelationShip.DOWN_KEY));
			inputBook.Add(Process.MOVE_POSITIVE, new Processe(KeyAction.HELD_DOWN, KeyRelationShip.UP_KEY));
			inputBook.Add(Process.RESET_GAME, new Processe(KeyAction.HELD_DOWN, KeyRelationShip.R_KEY));
			for (int i = 0; i < (int)Process.COUNT; i++)
			{
				register.Add((Process)i, new List<Action>());
			}
		}

		public static void Register(Action action, Process process)
		{
			register[process].Add(action);
		}
		//todo, add unregister when destoryint of objects happens

		public static void Update()
		{
			for (int i = 0; i < (int)Process.COUNT; i++)
			{
				List<Action> inputs = register[(Process)i];

				inputBook[(Process)i].Update();
				bool active = inputBook[(Process)i].Activated();

				foreach (Action input in inputs)
				{
					if (active)
					{
						input();
					}
				}
			}
		}
	}
}
