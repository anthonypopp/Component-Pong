using System;

namespace ComponentConsolePong
{
	/// <summary>
	/// Contains a variety of math related functions
	/// </summary>
	public static class MathHelper
	{
		public static float TO_RADIANS = 0.0174533f;
		public static float TO_DEGREES = 57.2858f;

		public static float Sin(float radians)
		{
			return (float)Math.Sin(radians);
		}
		public static float Cos(float radians)
		{
			return (float)Math.Cos(radians);
		}
		public static int Clamp(int i, int min, int max)
		{
			return Math.Min(Math.Max(i, min), max);
		}
	}
}
