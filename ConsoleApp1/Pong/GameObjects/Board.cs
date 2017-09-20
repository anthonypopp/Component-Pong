using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="GameObject"/> that allows text to be rendered in Raster Format to the console
	/// </summary>
	public class Board : GameObject, Drawable, Resetable
	{
		public Board() : base()
		{
		}

		/// <summary>
		/// A Component Responsible for updating the "pixel" or character being rendered in a <see cref="Cell"/>
		/// </summary>
		private class LetterSetter : Component, Updateable
		{
			char letter;
			public LetterSetter(Cell owner, char letter)
				:base()
			{
				this.letter = letter;
			}

			public void Update(float deltaTime)
			{
				((Cell)owner).Letter = letter;
			}
		}

		/// <summary>
		/// Represents a single "pixel" or character on the screen
		/// </summary>
		public class Cell : GameObject
		{
			private char letter = '.';

			public char Letter { get => letter; set => letter = value; }

			public Cell(): base()
			{
				AddComponent(new LetterSetter(this, '.'));
			}
		}

		List<List<Cell>> paint;
		StringBuilder text;
		const int WIDTH = 20;
		const int HEIGHT = 80;

		public void Init(Action<GameObject> AddGameObject)
		{

			text = new StringBuilder();
			paint = new List<List<Cell>>();
			for (int w = 0; w < WIDTH; w++)
			{
				paint.Add(new List<Cell>());
				for (int h = 0; h < HEIGHT; h++)
				{
					paint[w].Add(new Cell());
					AddGameObject(paint[w][h]);
				}
			}
			AddGameObject(this);
		}
		
		public Cell GetCell(float x, float y)
		{
			int xIndex = (int)((x * WIDTH) / 100.0f);
			int yIndex = (int)((y * HEIGHT) / 100.0f);
			xIndex = MathHelper.Clamp(xIndex, 0, WIDTH - 1);
			yIndex = MathHelper.Clamp(yIndex, 0, HEIGHT - 1);
			return paint[xIndex][yIndex];
		}
		
		public void Draw(Board board)
		{
			Console.Clear();
			for (int w = 0; w < WIDTH; w++)
			{
				text.Clear();
				for (int h = 0; h < HEIGHT; h++)
				{
					text.Append(paint[w][h].Letter.ToString());
				}

				Console.WriteLine(text.ToString());
			}
		}

		public char GetChar()
		{
			return ' ';
		}

		public void Reset()
		{
			rect.Set(0, 0, 100, 100);
		}
	}
}
