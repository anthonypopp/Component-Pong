using System;
using System.Collections.Generic;

namespace ComponentConsolePong.Input
{
	public static class InputManager
	{

		/// <summary>
		/// A list of Actions in the game that relate to a given <see cref="Process"/>
		/// </summary>
		private static Dictionary<Process, List<Action>> register = new Dictionary<Process, List<Action>>();

		/// <summary>
		/// A map of <see cref="Processe"/> to a process
		/// </summary>
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

		/// <summary>
		/// Registers a Actions to a process. When the process is activated, this actions will be invoked
		/// </summary>
		public static void Register(Action action, Process process)
		{
			register[process].Add(action);
		}

		//todo, add unregister

		public static void Update()
		{
			for (int i = 0; i < (int)Process.COUNT; i++)
			{
				List<Action> inputs = register[(Process)i];

				inputBook[(Process)i].Update();
				bool active = inputBook[(Process)i].Activated();

				//If a Process is active, call all functions that have been registered to it
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
