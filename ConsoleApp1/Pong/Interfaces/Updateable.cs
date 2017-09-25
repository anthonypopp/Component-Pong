namespace ComponentConsolePong
{
	/// <summary>
	/// Allows a <see cref="GameObject"/> or <see cref="Component"/> to be updated each frame
	/// </summary>
	public interface Updateable
	{
		void Update(float deltaTime);
	}
}
