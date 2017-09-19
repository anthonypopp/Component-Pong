using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentConsolePong
{
	// marks this as making things want to move towards it
	public class Attractor : Component
	{
		public Attractor()
		{
		}
	}

	// marks this as moving towards attractors
	public class Attractee : Component, Resetable, Updateable
	{
		List<Attractor> attractors;
		public float attractSpeed;
		public Attractee(float attractSpeed)
		{
			this.attractSpeed = attractSpeed;
		}

		public void Reset()
		{
			attractors = GameObject.GetComponentsInGame<Attractor>();
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
}
