using System;
using System.Collections.Generic;

namespace ComponentConsolePong.Input
{
	public static class InputManager
	{

		private static Dictionary<Process, List<Action>> register = new Dictionary<Process, List<Action>>();
		private static Dictionary<Process, Processe> inputBook = new Dictionary<Process, Processe>();

		public static void Init()
		{
			inputBook.Add(Process.NO_MOVE_POSITIVE, new Processe(KeyAction.NOT_PRESSED, KeyRelationShip.UP_KEY));
			inputBook.Add(Process.NO_MOVE_NEGATIVE, new Processe(KeyAction.NOT_PRESSED, KeyRelationShip.DOWN_KEY));
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

				//If a Process is active, call all functions taht have been Registered to it
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
