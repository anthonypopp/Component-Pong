namespace ComponentConsolePong
{
	public class MessageableAdapter : GameObject, Messageable
	{
		public virtual void Write()
		{
			Write("");
		}

		protected virtual void Write(string message)
		{
			Program.WriteMessage(message);
		}
	}
}
