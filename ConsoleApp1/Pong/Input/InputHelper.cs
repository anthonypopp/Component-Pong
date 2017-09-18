using System;
using System.Collections.Generic;

namespace ComponentConsolePong.Input
{
	public static class InputManager
	{

		/// <summary>
		/// A list of Actions in the game that relate to a given <see cref="Process"/>
		/// </summary>
		private static Dictionary<Process, Dictionary<Object, List<Action>>> register = new Dictionary<Process, Dictionary<object, List<Action>>>();

		/// <summary>
		/// A map of <see cref="Processe"/> to a process
		/// </summary>
		private static Dictionary<Process, Processe> inputBook = new Dictionary<Process, Processe>();

		public static void Init()
		{
			inputBook.Add(Process.NO_MOVE_POSITIVE, new Processe(KeyAction.JUST_RELEASED, KeyRelationShip.UP_KEY));
			inputBook.Add(Process.NO_MOVE_NEGATIVE, new Processe(KeyAction.JUST_RELEASED, KeyRelationShip.DOWN_KEY));
			inputBook.Add(Process.MOVE_NEGATIVE, new Processe(KeyAction.JUST_PRESSED, KeyRelationShip.DOWN_KEY));
			inputBook.Add(Process.MOVE_POSITIVE, new Processe(KeyAction.JUST_PRESSED, KeyRelationShip.UP_KEY));
			inputBook.Add(Process.RESET_GAME, new Processe(KeyAction.HELD_DOWN, KeyRelationShip.R_KEY));
			
			for (int i = 0; i < (int)Process.COUNT; i++)
			{
				register.Add((Process)i, new Dictionary<object, List<Action>>());
			}
		}

		/// <summary>
		/// Registers a Actions to a process. When the process is activated, this actions will be invoked
		/// </summary>
		public static void Register(Object owner, Action action, Process process)
		{
			if (!register[process].ContainsKey(owner))
			{
				register[process].Add(owner, new List<Action>());
			}
			register[process][owner].Add(action);
		}

		/// <summary>
		/// Unregisters all Actions attached to an owner
		/// </summary>
		public static void Unregister(Object owner)
		{
			foreach (Dictionary<Object, List<Action>> book in register.Values)
			{
				if (book.ContainsKey(owner))
				{
					book.Remove(owner);
				}
			}
		}

		//todo, add unregister

		public static void Update()
		{
			for (int i = 0; i < (int)Process.COUNT; i++)
			{

				Dictionary<Object, List<Action>> book = register[(Process)i];

				foreach (List<Action> inputs in book.Values)
				{
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
}
