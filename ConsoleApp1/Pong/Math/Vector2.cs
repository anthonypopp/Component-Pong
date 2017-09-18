using System;

namespace ComponentConsolePong
{
	public struct Vector2
	{
		public static Vector2 Zero = new Vector2(0, 0);
		public static Vector2 One = new Vector2(1, 1);
		public float x;
		public float y;
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		public void MoveBy(float amountX, float amountY)
		{
			x += amountX;
			y += amountY;
		}

		public static Vector2 operator *(Vector2 l, float r)
		{
			return new Vector2(l.x * r, l.y * r);
		}
		public static Vector2 operator *(Vector2 l, Vector2 r)
		{
			return new Vector2(l.x * r.x, l.y * r.y);
		}
		public static Vector2 operator -(Vector2 l, Vector2 r)
		{
			return new Vector2(l.x - r.x, l.y - r.y);
		}
		public static Vector2 operator +(Vector2 l, Vector2 r)
		{
			return new Vector2(l.x + r.x, l.y + r.y);
		}

		public static Vector2 operator /(Vector2 l, float div)
		{
			if (div == 0)
			{
				throw new Exception("Cannot div by 0");
			}
			return new Vector2(l.x / div, l.y / div);
		}
	}
}
