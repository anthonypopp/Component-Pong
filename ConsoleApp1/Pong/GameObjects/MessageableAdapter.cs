namespace ComponentConsolePong
{
	/// <summary>
	/// GameObjects that acts as a wrapper for the <see cref="Messageable"/>
	/// </summary>
	public class MessageableAdapter : GameObject, Messageable
	{
		public virtual void Write()
		{
			Write("");
		}

		protected virtual void Write(string message)
		{
			Log.DisplayMessage('\n' + message);
		}
	}
}
