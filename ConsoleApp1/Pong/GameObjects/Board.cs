using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentConsolePong
{
	/// <summary>
	/// A <see cref="GameObject"/> that allows text to be rendered in Raster Format to the console
	/// </summary>
	public partial class Board : GameObject, Drawable, Resetable
	{
		public Board() : base()
		{
		}

		/// <summary>
		/// Represents a single "pixel" or character on the screen
		/// </summary>
		public class Cell : GameObject, CoponentDrawable
		{
			private char letter = '.';
			public Vector2 position = new Vector2();

			public char Letter { get => letter; set => letter = value; }

			public Cell(int x, int y): base()
			{
				position.x = x;
				position.y = y;
				AddComponent(new LetterSetter('.'));
			}
		}

		List<List<Cell>> paint;

		/// <summary>
		/// paint cells that compose of the boarder
		/// </summary>
		List<Cell> border;

		StringBuilder text;
		const int WIDTH = 20;
		const int HEIGHT = 80;
		const int BORDER = 2;

		public void Init(Action<GameObject> AddGameObject)
		{

			text = new StringBuilder();
			paint = new List<List<Cell>>();

			for (int w = 0; w < WIDTH; w++)
			{
				paint.Add(new List<Cell>());
				for (int h = 0; h < HEIGHT; h++)
				{
					paint[w].Add(new Cell(w,h));
					AddGameObject(paint[w][h]);
				}
			}

			AddGameObject(this);

			InitBoarder();
		}

		private void InitBoarder()
		{
			border = new List<Cell>();

			//Add boarder in circular order
			int index = 0;
			int thickness = 0;
			for (thickness = 0; thickness < BORDER; thickness++)
			{
				//Top
				for (index = thickness; index < WIDTH - 1 - thickness; index++)
				{
					border.Add(paint[index][thickness]);
				}
				//Right
				for (index = thickness; index < HEIGHT - 1 - thickness; index++)
				{
					border.Add(paint[WIDTH - 1 - thickness][index]);
				}
				//Bottom
				for (index = WIDTH - 1 - thickness; index > thickness; index--)
				{
					border.Add(paint[index][HEIGHT - 1 - thickness]);
				}
				//Left
				for (index = HEIGHT - 1 - thickness; index > thickness; index--)
				{
					border.Add(paint[thickness][index]);
				}
			}
			
			foreach(Cell cell in border)
			{
				cell.AddComponent(new FlashingBorderCell());
			}
		}

		public Cell GetCell(float x, float y, bool useBorder = true)
		{
			int width = WIDTH;
			int height = HEIGHT;
			if (useBorder)
			{
				width -= BORDER * 2;
				height -= BORDER * 2;
			}
			int x0 = useBorder ? BORDER : 0;
			int y0 = useBorder ? BORDER : 0;
			int xIndex = x0 + (int)(((x * width) / 100.0f) + 0.5f);
			int yIndex = y0 + (int)(((y * height) / 100.0f) + 0.5f);
			xIndex = MathHelper.Clamp(xIndex, x0, Math.Min(width, WIDTH - 1));
			yIndex = MathHelper.Clamp(yIndex, y0, Math.Min(height, HEIGHT - 1));
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
