using System.Windows.Input;


namespace ComponentConsolePong.Input
{
	/// <summary>
	/// Connects a <see cref="Process"/> to a <see cref="KeyAction"/> with a given <see cref="KeyRelationShip"/>
	/// </summary>
	public class Processe
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
}
