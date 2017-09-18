namespace ComponentConsolePong
{
	/// <summary>
	/// Allows a <code>GameObject</code> or <code>Component</code> to Reset itself when the game resets. Can also be used for initializeation after all game objects on the board have been added
	/// </summary>
	public interface Resetable
	{
		void Reset();
	}
}
