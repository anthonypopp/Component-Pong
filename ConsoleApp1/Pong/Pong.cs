using System.Collections.Generic;

namespace ComponentConsolePong
{
	/// <summary>
	/// Base for all things game's of Pong
	/// </summary>
	class Pong
	{
		List<GameObject> gameObjects;
		Board board;

		public Pong()
		{
			board = new Board();
			board.MakeRoot();

			gameObjects = new List<GameObject>();
			Init(gameObjects);

			board.Init(gameObjects);
			
			Reset();
		}

		public void Init(List<GameObject> gameObjects)
		{
			gameObjects.Add(new TimeManager(10, Reset));
			gameObjects.Add(new Ball(4, 2));
			gameObjects.Add(new Ball(3, 4));
			gameObjects.Add(new PlayerPaddle());
			gameObjects.Add(new AIPaddle(0.1f));
			gameObjects.Add(new AIPaddle(0.5f));
			gameObjects.Add(new TitleMessenger("Let's get ready to Rubleeee!!!!!!"));
			gameObjects.Add(new ScoreMessenger());
			gameObjects.Add(new ScoreObject(new Rectangle(0, 0, 100, 2), 1));
			gameObjects.Add(new ScoreObject(new Rectangle(0, 98, 100,2), 2));
		}

		
		public void Reset()
		{
			foreach (GameObject thing in gameObjects)
			{
				thing.ResetComponents();
				if (typeof(Resetable).IsAssignableFrom(thing.GetType()))
				{
					((Resetable)thing).Reset();
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
	}
}
