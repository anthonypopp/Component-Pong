namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="MessageableAdapter"/> that messages a title that does not change during the course of the game
	/// </summary>
	public class TitleMessenger : MessageableAdapter
	{
		protected string title = "Title";
		public TitleMessenger(string title)
			: base()
		{
			this.title = title;
		}

		public override void Write()
		{
			Write(title);
		}
	}
}
