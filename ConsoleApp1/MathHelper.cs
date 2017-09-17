using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public static class MathHelper
	{
		public static float Radians(this float angle)
		{
			return angle * 0.0174533f;
		}
		public static float Degrees(this float radians)
		{
			return radians * 57.2958f;
		}
		public static float Sin(this float radians)
		{
			return (float)Math.Sin(radians);
		}
		public static float Cos(this float radians)
		{
			return (float)Math.Cos(radians);
		}
		public static int Clamp(this int i, int min, int max)
		{
			return Math.Min(Math.Max(i, min), max);
		}
	}
}
