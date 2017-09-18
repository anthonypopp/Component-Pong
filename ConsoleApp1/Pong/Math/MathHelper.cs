using System;

namespace ComponentConsolePong
{
	public static class MathHelper
	{
		public static float Radians(float angle)
		{
			return angle * 0.0174533f;
		}
		public static float Degrees(float radians)
		{
			return radians * 57.2958f;
		}
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
