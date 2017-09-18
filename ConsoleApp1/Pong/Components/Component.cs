using System;

namespace ComponentConsolePong
{
	public class Component
	{
		public bool enabled = true;
		public Component(GameObject owner)
		{
			this.owner = owner ?? throw new Exception("Cannot have a null owner");
			rect = owner.rect;
		}
		public GameObject owner;
		public Rectangle rect;
	}
}
