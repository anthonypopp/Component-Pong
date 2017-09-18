namespace ComponentConsolePong
{
	/// <summary>
	///  Allows a <see cref="GameObject"/> or <see cref="Component"/> to render a character on the board each frame. Called after Update
	/// </summary>
	public interface Drawable
	{
		void Draw(Board board);
		char GetChar();
	}

	/// <summary>
	///  Allows a <see cref="GameObject"/> or <see cref="Component"/> to clean up itself at the end of the applications lifecycle
	/// </summary>
	public interface Quitable
	{
		void Quit();
	}
}
