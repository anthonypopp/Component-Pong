namespace ComponentConsolePong
{

	/// <summary>
	/// Contains an X,Y, Width, and Height as <see cref="Vector2"/>
	/// </summary>
	public class Rectangle
	{
		public Rectangle()
		{
		}
		public Rectangle(float x, float y, float width, float height)
		{
			Set(x, y, width, height);
		}

		public Vector2 position;
		public Vector2 dimention;

		public Rectangle Set(float x, float y, float width, float height)
		{
			position.x = x;
			position.y = y;
			dimention.x = width;
			dimention.y = height;
			return this;
		}
		public Rectangle Set(Vector2 position, Vector2 dimention)
		{
			this.dimention = dimention;
			this.position = position;
			return this;
		}

		public Rectangle Centerfy()
		{
			CenterX();
			CenterY();
			return this;
		}

		/// <summary>
		/// Returns the center position of this rectangle
		/// </summary>
		public Vector2 Center()
		{
			return new Vector2(Left() + HalfWidth(), Bottom() + HalfHeight());
		}

		/// <summary>
		/// Centers this <see cref="Rectangle"/>'s x-axis on itself, returns a references for chaining
		/// </summary>
		public Rectangle CenterX()
		{
			position.x -= HalfWidth();
			return this;
		}

		/// <summary>
		/// Centers this <see cref="Rectangle"/>'s y-axis on itself, returns a reference for chaining
		/// </summary>
		public Rectangle CenterY()
		{
			position.y -= HalfHeight();
			return this;
		}

		/// <summary>
		/// The right of the <see cref="Rectangle"/> (x + width)
		/// </summary>
		public float Right()
		{
			return Left() + Width();
		}

		/// <summary>
		/// The top of the <see cref="Rectangle"/> (y + height)
		/// </summary>
		public float Top()
		{
			return Bottom() + Height();
		}

		/// <summary>
		/// The bottom of the <see cref="Rectangle"/> (y)
		/// </summary>
		public float Bottom()
		{
			return position.y;
		}

		/// <summary>
		/// The left of the <see cref="Rectangle"/> (x)
		/// </summary>
		public float Left()
		{
			return position.x;
		}
		
		/// <summary>
		/// The width of this <see cref="Rectangle"/>
		/// </summary>
		/// <returns></returns>
		public float Width()
		{
			return dimention.x;
		}

		/// <summary>
		/// The height of this  <see cref="Rectangle"/>
		/// </summary>
		public float Height()
		{
			return dimention.y;
		}

		public float HalfWidth()
		{
			return Width() / 2.0f;
		}
		public float HalfHeight()
		{
			return Height() / 2.0f;
		}

		/// <summary>
		/// Returns true if this <see cref="Rectangle"/> intersects with another one
		/// </summary>
		public bool Intersects(Rectangle other)
		{
			if (other == null)
			{
				return false;
			}

			if (Left() < other.Right() &&
				Right() >= other.Left() &&
				 Bottom() < other.Top() &&
				Top() >= other.Bottom())
			{
				return true;
			}
			return false;
		}
	}
}
