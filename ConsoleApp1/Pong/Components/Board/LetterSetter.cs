namespace ComponentConsolePong
{
	public partial class Board : GameObject, Drawable, Resetable
	{
		/// <summary>
		/// A <see cref="Component"/> Responsible for updating the "pixel" or character being rendered in a <see cref="Cell"/>
		/// </summary>
		private class LetterSetter : Component, Updateable
		{
			public char letter;
			public LetterSetter(char letter) :base()
			{
				this.letter = letter;
			}

			public void Update(float deltaTime)
			{
				((Cell)owner).Letter = letter;
			}
		}
	}
}
