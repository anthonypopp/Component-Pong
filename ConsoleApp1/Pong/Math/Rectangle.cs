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
		public Vector2 Center()
		{
			return new Vector2(Left() + HalfWidth(), Bottom() + HalfHeight());
		}
		public Rectangle CenterX()
		{
			position.x -= HalfWidth();
			return this;
		}
		public Rectangle CenterY()
		{
			position.y -= HalfHeight();
			return this;
		}
		public float Right()
		{
			return Left() + Width();
		}

		public float Top()
		{
			return Bottom() + Height();
		}

		public float Bottom()
		{
			return position.y;
		}
		public float Left()
		{
			return position.x;
		}

		public float Width()
		{
			return dimention.x;
		}
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

		public bool Intersects(Rectangle other)
		{
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
