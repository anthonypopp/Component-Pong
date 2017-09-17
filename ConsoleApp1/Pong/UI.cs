using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public struct Vector2
	{
		public static Vector2 Zero = new Vector2(0, 0);
		public static Vector2 One = new Vector2(1, 1);
		public float x;
		public float y;
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		public void MoveBy(float amountX, float amountY)
		{
			x += amountX;
			y += amountY;
		}

		public static Vector2 operator *(Vector2 l, float r)
		{
			return new Vector2(l.x * r, l.y * r);
		}
		public static Vector2 operator *(Vector2 l, Vector2 r)
		{
			return new Vector2(l.x * r.x, l.y * r.y);
		}
		public static Vector2 operator -(Vector2 l, Vector2 r)
		{
			return new Vector2(l.x - r.x, l.y - r.y);
		}
		public static Vector2 operator +(Vector2 l, Vector2 r)
		{
			return new Vector2(l.x + r.x, l.y + r.y);
		}

		public static Vector2 operator /(Vector2 l, float div)
		{
			if (div == 0)
			{
				throw new Exception("Cannot div by 0");
			}
			return new Vector2(l.x / div, l.y / div);
		}
	}
	public interface Updateable
	{
		void Update(float deltaTime);
	}

	//Called after all initilization has been done and at the start of a new game
	public interface Resetable
	{
		void Reset();
	}
	public interface Drawable
	{
		void Draw(Board board);
		char GetChar();
	}
	public interface Inputable
	{
	}

	public class Component
	{
		public bool enabled = true;
		public Component(Thing owner)
		{
			this.owner = owner ?? throw new Exception("Cannot have a null owner");
			rect = owner.rect;
		}
		public Thing owner;
		public Rectangle rect;
	}

	public class PaddleMover : Mover, Inputable
	{
		Rectangle bounds;
		bool actiated = false;
		public PaddleMover(Thing owner, Rectangle bounds, float speedX, Process process, Process noDo) : base(owner, speedX, 0)
		{
			enabled = false;
			this.bounds = bounds;
			InputHelper.Register(Do, process);
			InputHelper.Register(Dont, noDo);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
		}

		public void Do()
		{
			enabled = true;
		}

		public void Dont()
		{
			enabled = false;
		}
	}

	public class PaddleCollision : Component, Resetable, Updateable
	{
		float framesToWait;
		float framesLeft;
		float speedMultiplier;
		List<Paddle> paddles = new List<Paddle>();
		WallReflector mover;

		public PaddleCollision(Thing owner, float framesToWait, float speedMultiplier) : base(owner)
		{
			framesLeft = 0;
			this.framesToWait = framesToWait;
			this.speedMultiplier = speedMultiplier;
		}

		public void Reset()
		{
			paddles = Thing.GetThingsInGame<Paddle>();
			mover = owner.GetComponent<WallReflector>();
			framesLeft = 0;
		}

		public void Update(float deltaTime)
		{
			if (framesLeft > 0)
			{
				framesLeft--;
			}
			if (IsColliding())
			{
				framesLeft = framesToWait;
				mover.speedY *= -speedMultiplier;
			}
		}

		private bool IsColliding()
		{
			foreach (Paddle paddle in paddles)
			{
				if (rect.Intersects(paddle.rect))
				{
					return true;
				}
			}
			return false;
		}
	}

	public class Mover : Component, Updateable
	{
		public Mover(Thing owner, float speedX, float speedY)
			: base(owner)
		{
			this.speedX = speedX;
			this.speedY = speedY;
		}
		public float speedX;
		public float speedY;
		public virtual void Update(float deltaTime)
		{
			rect.position.MoveBy(deltaTime * speedX, deltaTime * speedY);
		}
	}
	public class WallReflector : Mover
	{
		public WallReflector(Thing owner, float speedX, float speedY, Rectangle bounds)
			: base(owner, speedX, speedY)
		{
			this.bounds = bounds;
		}

		Rectangle bounds;
		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			if (rect.Top() > bounds.Top() || rect.Bottom() <= bounds.Bottom())
			{
				speedY *= -1.0f;
			}
			if (rect.Right() > bounds.Right() || rect.Left() <= bounds.Left())
			{
				speedX *= -1.0f;
			}
		}
	}
	// marks this as making things want to move towards it
	public class Attractor : Component
	{
		public Attractor(Thing owner) : base(owner)
		{
		}
	}

	// marks this as moving towards attractors
	public class Attractee : Component, Resetable, Updateable
	{
		List<Attractor> attractors;
		public float attractSpeed;
		public Attractee(Thing owner, float attractSpeed) : base(owner)
		{
			this.attractSpeed = attractSpeed;
		}

		public void Reset()
		{
			attractors = Thing.GetComponentsInGame<Attractor>();
		}

		public virtual void Update(float deltaTime)
		{
			if (attractors.Count == 0)
			{
				return;
			}
			Vector2 total = Vector2.Zero;
			//every frame will create a lot of Vector2 garbage
			foreach (Attractor attractor in attractors)
			{
				total += attractor.rect.position;
			}
			Vector2 ave = total / attractors.Count;

			float oldY = rect.position.y;
			rect.position += ((ave - rect.position) * attractSpeed);
			rect.position.y = oldY;
		}
	}

	public class Rectangle
	{
		public Rectangle()
		{
		}
		public Rectangle(float x, float y, float width, float height)
		{
			Set(x, y, width, height);
		}

		public Vector2 position;
		public Vector2 dimention;

		public Rectangle Set(float x, float y, float width, float height)
		{
			position.x = x;
			position.y = y;
			dimention.x = width;
			dimention.y = height;
			return this;
		}
		public Rectangle Set(Vector2 position, Vector2 dimention)
		{
			this.dimention = dimention;
			this.position = position;
			return this;
		}

		public Rectangle Centerfy()
		{
			CenterX();
			CenterY();
			return this;
		}
		public Vector2 Center()
		{
			return new Vector2(Left() + HalfWidth(), Bottom() + HalfHeight());
		}
		public Rectangle CenterX()
		{
			position.x -= HalfWidth();
			return this;
		}
		public Rectangle CenterY()
		{
			position.y -= HalfHeight();
			return this;
		}
		public float Right()
		{
			return Left() + Width();
		}

		public float Top()
		{
			return Bottom() + Height();
		}

		public float Bottom()
		{
			return position.y;
		}
		public float Left()
		{
			return position.x;
		}

		public float Width()
		{
			return dimention.x;
		}
		public float Height()
		{
			return dimention.y;
		}
		public float HalfWidth()
		{
			return Width() / 2.0f;
		}
		public float HalfHeight()
		{
			return Height() / 2.0f;
		}

		public bool Intersects(Rectangle other)
		{
			if (Left() < other.Right() &&
				Right() >= other.Left() &&
				 Bottom() < other.Top() &&
				Top() >= other.Bottom())
			{
				return true;
			}
			return false;
		}
	}

	public class Thing
	{
		public static Thing ROOT;

		protected Thing()
		{
			if (ROOT == null)
			{
				MakeRoot();
			}
			else
			{
				SetParent(ROOT);
			}
		}
		public void MakeRoot()
		{
			ROOT = this;
		}

		public void SetParent(Thing thing)
		{
			if (this == thing)
			{
				return;
			}
			if (thing == null)
			{
				thing = ROOT;
			}
			parent?.children.Remove(this);
			thing.children.Add(this);
			parent = thing;
		}

		Thing parent;
		List<Thing> children = new List<Thing>();
		List<Component> components = new List<Component>();
		Dictionary<Type, Component> lookup = new Dictionary<Type, Component>();

		//In percent
		public Rectangle rect = new Rectangle();


		//Thing Getting
		public static List<T> GetThingsInGame<T>(bool allowSubclasses = true) where T : Thing
		{
			return ROOT.GetThingsInChildren<T>(allowSubclasses);
		}
		public List<T> GetThingsInChildren<T>(bool allowSubclasses) where T : Thing
		{
			return GetThingsInChildren(new List<T>(), allowSubclasses);
		}
		private List<T> GetThingsInChildren<T>(List<T> result, bool allowSubclasses) where T : Thing
		{
			if (Is<T>())
			{
				result.Add((T)this);
			}
			foreach (Thing child in children)
			{
				child.GetThingsInChildren(result, allowSubclasses);
			}
			return result;
		}
		//Component Getting
		public static List<T> GetComponentsInGame<T>() where T : Component
		{
			return ROOT.GetComponentsInChildren<T>();
		}
		public List<T> GetComponentsInChildren<T>() where T : Component
		{
			return GetComponentsInChildren(new List<T>());
		}
		private List<T> GetComponentsInChildren<T>(List<T> result) where T : Component
		{
			T t = GetComponent<T>();
			if (t != null)
			{
				result.Add(t);
			}
			foreach (Thing child in children)
			{
				child.GetComponentsInChildren(result);
			}
			return result;
		}
		public T GetComponent<T>() where T : Component
		{

			Type t = typeof(T);
			if (lookup.ContainsKey(t))
			{
				return (T)lookup[t];
			}
			return null;
		}
		public void AddComponent<T>(T component) where T : Component
		{
			//Allow multiple components
			components.Add(component);

			if (!lookup.ContainsKey(typeof(T)))
			{
				lookup.Add(typeof(T), component);  //But only return the first occurance of it :/
			}
			//Todo, lookup list of same component instead
		}

		public bool Is<T>(bool includeSubclass = true) where T : Thing
		{
			return ((!includeSubclass || typeof(T).IsSubclassOf(GetType())) || this is T);
		}

		public void UpdateComponents(float deltaTime)
		{
			foreach (Component component in components)
			{
				if (component.enabled)
				{
					if (typeof(Updateable).IsAssignableFrom(component.GetType()))
					{
						((Updateable)component).Update(deltaTime);
					}
				}
			}
		}
		public void ResetComponents()
		{
			foreach (Component component in components)
			{
				if (typeof(Resetable).IsAssignableFrom(component.GetType()))
				{
					((Resetable)component).Reset();
				}
			}
		}
		public void WriteComponents()
		{
			foreach (Component component in components)
			{
				if (typeof(Messageable).IsAssignableFrom(component.GetType()))
				{
					((Messageable)component).Write();
				}
			}
		}

		public void AddChild(Thing thing)
		{
			if (thing.parent != null)
			{
				if (!children.Contains(thing))
				{
					children.Add(thing);
					thing.parent = this;
				}
			}
			else
			{
				throw new Exception("Cannot Add ROOT as child");
			}
		}
	}

	public class ScoreThing : Thing
	{
		public ScoreThing(Rectangle scoreRect, int player) : base()
		{
			rect.Set(scoreRect.position, scoreRect.dimention);
			AddComponent(new Score(this, player));
		}
	}
	public class DrawableThing : Thing, Drawable
	{
		public DrawableThing() : base()
		{
		}
		public virtual void Draw(Board board)
		{
			float graularity = 0.1f;
			for (float w = 0; w < rect.dimention.x; w += graularity)
			{
				for (float h = 0; h < rect.dimention.y; h += graularity)
				{
					board.GetCell(rect.position.x + w, rect.position.y + h).Letter = GetChar();
				}
			}
		}

		public virtual char GetChar()
		{
			return ' ';
		}

	}

	public class Entity : DrawableThing, Resetable
	{
		public Entity() : base()
		{
		}
		public virtual void Reset()
		{
		}
	}
	public class Ball : Entity
	{
		public Ball(float xSpeed, float ySpeed) : base()
		{
			AddComponent(new WallReflector(this, xSpeed, ySpeed, ROOT.rect));
			AddComponent(new Attractor(this));
			AddComponent(new PaddleCollision(this, 1, 1.01f));
			AddComponent(new Scoreable(this));
		}

		public override void Reset()
		{
			base.Reset();
			rect.Set(50, 50, 5, 5).Centerfy();
		}
		public override char GetChar()
		{
			return 'O';
		}
	}
	//Marks a component as being able to make scores
	public class Scoreable : Component
	{
		public Scoreable(Thing owner) : base(owner)
		{
		}
	}
	public class Score : Component, Resetable, Updateable
	{
		public int score = 0;
		public int playerNum;
		List<Scoreable> scoreable;
		Dictionary<Scoreable, bool> collidingBook;
		public Score(Thing owner, int playerNum) : base(owner)
		{
			this.playerNum = playerNum;
			scoreable = new List<Scoreable>();
			collidingBook = new Dictionary<Scoreable, bool>();
		}

		public void Reset()
		{
			score = 0;
			scoreable.Clear();
			scoreable = Thing.GetComponentsInGame<Scoreable>();
			collidingBook = new Dictionary<Scoreable, bool>();
			foreach (Scoreable s in scoreable)
			{
				collidingBook.Add(s, false);
			}
		}

		public void Update(float deltaTime)
		{
			foreach (Scoreable s in scoreable)
			{
				if (rect.Intersects(s.rect))
				{
					if (!collidingBook[s])
					{
						IncrementScore();
						collidingBook[s] = true;
					}
				}
				else
				{
					collidingBook[s] = false;
				}
			}
		}
		private void IncrementScore()
		{
			score++;
		}
	}

	public class Paddle : Entity
	{
		public override char GetChar()
		{
			return 'X';
		}
	}
	public class PlayerPaddle : Paddle
	{
		//Controlled by player input
		public override void Reset()
		{
			base.Reset();
			rect.Set(50, 90, 20, 5).Centerfy();
			AddComponent(new PaddleMover(this, ROOT.rect, -3, Process.MOVE_POSITIVE));
			AddComponent(new PaddleMover(this, ROOT.rect, 3, Process.MOVE_NEGATIVE));
		}
	}
	public class AIPaddle : Paddle
	{
		public AIPaddle(float attractSpeed) : base()
		{
			AddComponent(new Attractee(this, attractSpeed));
		}

		//Moves towards the ball
		public override void Reset()
		{
			base.Reset();
			rect.Set(50, 10, 20, 5).Centerfy();
		}

		
	}

	public interface Messageable
	{
		void Write();
	}
	public class Messenger : Thing, Messageable
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
	//Write the title of the game
	public class TitleMessenger : Messenger
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

	public class ScoreMessenger : Messenger, Resetable
	{
		List<Score> scores = new List<Score>();
		public ScoreMessenger()
		{
		}

		public void Reset()
		{
			scores = GetComponentsInGame<Score>();
		}

		public override void Write()
		{
			string message = "Scores - ";
			foreach(Score score in scores)
			{
				message += " Player " + score.playerNum + ": " + score.score + " ";
			}
			Write(message);
		}

	}
}
