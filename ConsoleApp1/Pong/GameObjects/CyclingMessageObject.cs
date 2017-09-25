namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="GameObject"/> that shows a <see cref="CyclingMessage"/> at a certain rate
	/// </summary>
	class CyclingMessageObject : GameObject
	{
		public CyclingMessageObject(string title, float cycleTime)
		{
			AddComponent(new CyclingMessage(title, cycleTime));
		}
	}
}
