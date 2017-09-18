namespace ComponentConsolePong
{
	/// <summary>
	/// Allows a <code>GameObject</code> or <code>Component</code> to be updated each frame
	/// </summary>
	public interface Updateable
	{
		void Update(float deltaTime);
	}
}
