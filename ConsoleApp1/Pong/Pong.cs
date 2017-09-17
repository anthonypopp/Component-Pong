using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Pong
	{
		List<Thing> things;
		Board board;

		public Pong()
		{
			board = new Board();
			board.MakeRoot();

			things = new List<Thing>();
			Init(things);

			board.Init(things);
			
			Reset();
		}

		public void Init(List<Thing> things)
		{

			things.Add(new TimeHelper(10, Reset));
			things.Add(new Ball(4, 2));
			things.Add(new Ball(3, 4));
			things.Add(new Ball(6, 3));
			things.Add(new PlayerPaddle());
			things.Add(new AIPaddle(0.1f));
			things.Add(new AIPaddle(0.5f));
			things.Add(new TitleMessenger("Let's get ready to Rubleeee!!!!!!"));
			things.Add(new ScoreMessenger());
			things.Add(new ScoreThing(new Rectangle(0, 0, 100, 2), 1));
			things.Add(new ScoreThing(new Rectangle(0, 98, 100,2), 2));
		}

		
		public void Reset()
		{
			foreach (Thing thing in things)
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
			foreach (Thing thing in things)
			{
				thing.UpdateComponents(deltaTime);
			}
		}

		public void Write()
		{
			foreach (Thing thing in things)
			{
				thing.WriteComponents();
				if (typeof(Messageable).IsAssignableFrom(thing.GetType()))
				{
					((Messageable)thing).Write();
				}
			}
		}

		public void Draw()
		{
			foreach (Thing thing in things)
			{
				if (typeof(Drawable).IsAssignableFrom(thing.GetType()))
				{
					((Drawable)thing).Draw(board);
				}
			}
		}
	}
}
