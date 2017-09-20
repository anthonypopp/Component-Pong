using System.Collections.Generic;

namespace ComponentConsolePong
{
	/// <summary>
	/// Base for all things game's of Pong
	/// </summary>
	class Pong
	{

		public List<GameObject> gameObjects = new List<GameObject>();
		Board board = new Board();
		public bool running = true;

		public Pong()
		{
			board.MakeRoot();
			Init();
			Reset();
		}

		public void Init()
		{
			Add(new TimeManager(10, Reset, StopGame));
			Add(new Ball(4, 2));
			Add(new Ball(3, 4));
			Add(new PlayerPaddle());
			Add(new AIPaddle(0.1f));
			Add(new AIPaddle(0.5f));
			Add(new TitleMessenger("Let's get ready to Rubleeee!!!!!!"));
			Add(new CyclingMessageObject("The Croud Goes Wild. ", 30.0f));
			Add(new ScoreMessenger());
			Add(new ScoreObject(new Rectangle(0, 0, 100, 2), 1));
			Add(new ScoreObject(new Rectangle(0, 98, 100,2), 2));
			board.Init(Add);
		}


		/// <summary>
		/// Adds a game object to the list of <see cref="gameObjects"/>
		/// </summary>
		/// <param name="gameObject">the object to add</param>
		private void Add(GameObject gameObject)
		{
			gameObjects.Add(gameObject);
		}
		
		private void Reset()
		{
			foreach (GameObject gameObject in gameObjects)
			{
				gameObject.ResetComponents();
				if (typeof(Resetable).IsAssignableFrom(gameObject.GetType()))
				{
					((Resetable)gameObject).Reset();
				}
			}
		}

		public void Update(float deltaTime)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				gameObject.UpdateComponents(deltaTime);
			}
		}

		public void Write()
		{
			foreach (GameObject gameObject in gameObjects)
			{
				gameObject.WriteComponents();
				if (typeof(Messageable).IsAssignableFrom(gameObject.GetType()))
				{
					((Messageable)gameObject).Write();
				}
			}
		}

		public void Draw()
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (typeof(Drawable).IsAssignableFrom(gameObject.GetType()))
				{
					((Drawable)gameObject).Draw(board);
				}
			}
		}

		public void Quit()
		{
			foreach (GameObject gameObject in gameObjects)
			{
				gameObject.Quit();
			}
		}

		/// <summary>
		/// Called when the game should no longer execute. Game will then quit after next draw cycle
		/// </summary>
		private void StopGame()
		{
			running = false;
		}

	}
}
