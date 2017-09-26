namespace ComponentConsolePong
{
	public partial class Board : GameObject, Drawable, Resetable
	{
		/// <summary>
		/// A <see cref="BorderCell"/> that flashes like a cool 90's resturant
		/// </summary>
		public class FlashingBorderCell : BorderCell, Updateable, Drawable
		{
			float totalTime;
			float flashAround; //Time which this should change characters
			Cell myCell;

			public override void Reset()
			{
				if (!owner.Is<Cell>())
				{
					throw new System.Exception("Owner should be a Cell");
				}
				myCell = (Cell)owner;
				totalTime = 0;
				flashAround = myCell.position.Manhattan() % 8;
				base.Reset();
			}
			public void Update(float deltaTime)
			{
				totalTime += deltaTime;
			}

			public void Draw(Board board)
			{
				if (((int)(totalTime)) % 9 == flashAround)
				{
					myCell.Letter = GetChar();
				}
			}

			public char GetChar()
			{
				return '^';
			}
		}
	}
}
