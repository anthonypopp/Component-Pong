namespace ComponentConsolePong
{
	/// <summary>
	///  Allows a <code>GameObject</code> or <code>Component</code> to render a character on the board each frame. Called after Update
	/// </summary>
	public interface Drawable
	{
		void Draw(Board board);
		char GetChar();
	}
}
