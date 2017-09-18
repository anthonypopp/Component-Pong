using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ComponentConsolePong
{
	/// <summary>
	/// An action that is associated with input
	/// </summary>
	public enum Process
	{
		MOVE_POSITIVE,
		MOVE_NEGATIVE,
		NO_MOVE_POSITIVE,
		NO_MOVE_NEGATIVE,
		RESET_GAME,
		COUNT
	}

	public static class InputHelper
	{

		/// <summary>
		/// The key assocated with a given input
		/// </summary>
		public enum KeyRelationShip
		{
			UP_KEY = Key.Up,
			DOWN_KEY = Key.Down, 
			R_KEY = Key.R,
		}
		
		/// <summary>
		/// The means of which input is occuring
		/// </summary>
		public enum KeyAction
		{
			HELD_DOWN,
			JUST_PRESSED,
			JUST_RELEASED,
			NOT_PRESSED
		}

		/// <summary>
		/// A process that connects input to actions performed in game
		/// </summary>
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
