using System;
using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
	public class Component : Quitable
	{
		public bool enabled = true;
		public Component()
		{
		}
		private GameObject _owner;
		private Rectangle _rect;

		public GameObject owner { get => _owner;
			set
			{
				_owner = value;
				_rect = _owner.rect;
			}
		}

		public Rectangle rect { get => _rect; }

		public virtual void Quit()
		{
			InputManager.Unregister(this);
		}
	}
}
