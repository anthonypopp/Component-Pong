using System;
using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
	public class Component : Quitable
	{
		public bool enabled = true;
		public Component(GameObject owner)
		{
			this.owner = owner ?? throw new Exception("Cannot have a null owner");
			rect = owner.rect;
		}
		public GameObject owner;
		public Rectangle rect;

		public virtual void Quit()
		{
			InputManager.Unregister(this);
		}
	}
}
