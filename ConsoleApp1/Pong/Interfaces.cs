namespace ComponentConsolePong
{
	/// <summary>
	/// Allows a <code>GameObject</code> or <code>Component</code> to display a message at the bottom of the screen
	/// </summary>
	public interface Messageable
	{
		void Write();
	}

	/// <summary>
	/// Allows a <code>GameObject</code> or <code>Component</code> to be updated each frame
	/// </summary>
	public interface Updateable
	{
		void Update(float deltaTime);
	}

	/// <summary>
	/// Allows a <code>GameObject</code> or <code>Component</code> to Reset itself when the game resets. Can also be used for initializeation after all game objects on the board have been added
	/// </summary>
	public interface Resetable
	{
		void Reset();
	}

	/// <summary>
	///  Allows a <code>GameObject</code> or <code>Component</code> to render a character on the board each frame. Called after Update
	/// </summary>
	public interface Drawable
	{
		void Draw(Board board);
		char GetChar();
	}
}
