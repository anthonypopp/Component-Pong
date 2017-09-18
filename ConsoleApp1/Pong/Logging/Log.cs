using System;

namespace ComponentConsolePong
{
	/// <summary>
	/// Used for logging messages
	/// </summary>
	public static class Log
	{
		/// <summary>
		/// Displayed a message to the console that will be seen by players
		/// </summary>
		public static void Print(string message)
		{
			Console.WriteLine('\n' + message);
		}
	}
}
