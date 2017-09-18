namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="DrawableObject"/> that is also <see cref="Resetable"/>
	/// </summary>
	public class Entity : DrawableObject, Resetable
	{
		public Entity() : base()
		{
		}
		public virtual void Reset()
		{
		}
	}
}
