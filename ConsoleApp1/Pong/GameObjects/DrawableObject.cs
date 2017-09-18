namespace ComponentConsolePong
{
	/// <summary>
	/// A GameObject that can be drawn on the screen.
	/// </summary>
	public class DrawableObject : GameObject, Drawable
	{
		public DrawableObject() : base()
		{
		}
		public virtual void Draw(Board board)
		{
			float graularity = 0.1f;
			for (float w = 0; w < rect.dimention.x; w += graularity)
			{
				for (float h = 0; h < rect.dimention.y; h += graularity)
				{
					board.GetCell(rect.position.x + w, rect.position.y + h).Letter = GetChar();
				}
			}
		}

		public virtual char GetChar()
		{
			return ' ';
		}

	}
}
