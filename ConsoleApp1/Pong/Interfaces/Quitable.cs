namespace ComponentConsolePong
{
	/// <summary>
	///  Allows a <see cref="GameObject"/> or <see cref="Component"/> to clean up itself at the end of the applications lifecycle
	/// </summary>
	public interface Quitable
	{
		void Quit();
	}
}
