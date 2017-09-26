namespace ComponentConsolePong
{
	public partial class Board : GameObject, Drawable, Resetable
	{
		/// <summary>
		/// Represents a <see cref="Cell"/> that is on the border of the game
		/// </summary>
		public class BorderCell : Component, Resetable
		{
			public virtual void Reset()
			{
				GetComponent<LetterSetter>().letter = '$';
			}
		}
	}
}
