using System;

namespace ComponentConsolePong
{
	public struct Vector2
	{
		/// <summary>
		/// A Vector2 with a value of (0, 0)
		/// </summary>
		public static Vector2 Zero = new Vector2(0, 0);
		
		/// <summary>
		/// A Vector2 with a value of (1, 1)
		/// </summary>
		public static Vector2 One = new Vector2(1, 1);

		public float x;
		public float y;

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// Moves this vector by a given x and y amount (+=)
		/// </summary>
		/// <param name="amountX">the amount to move on the x-axis</param>
		/// <param name="amountY">the amount ot move on the y-axis</param>
		public void MoveBy(float amountX, float amountY)
		{
			x += amountX;
			y += amountY;
		}

		/// <summary>
		/// Moves this vector by a given x and y amount (+=)
		/// </summary>
		/// <param name="amountX">the amount to move on the x-axis</param>
		/// <param name="amountY">the amount ot move on the y-axis</param>
		public void MoveBy(Vector2 amount)
		{
			x += amount.x;
			y += amount.y;
		}

		/// <summary>
		/// Multiplies the x value by -1
		/// </summary>
		public void FlipX()
		{
			x *= -1.0f;
		}

		/// <summary>
		/// Multiplies the y value by -1
		/// </summary>
		public void FlipY()
		{
			y *= -1.0f;
		}

		#region Operators
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
#endregion
	}
}
