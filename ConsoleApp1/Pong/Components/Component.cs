using System;
using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
	/// <summary>
	/// The base class for all <see cref="Component"/> objects, which are attached to <see cref="GameObject"/>
	/// </summary>
	public class Component : Quitable
	{
		/// <summary>
		/// Whether or not this component will perform: <see cref="Updateable"/>
		/// </summary>
		public bool enabled = true;
		public Component()
		{
		}
		private GameObject _owner;
		private Rectangle _rect;

		/// <summary>
		/// The <see cref="GameObject"/> this component is attached to
		/// </summary>
		public GameObject owner { get => _owner;
			set
			{
				_owner = value;
				_rect = _owner.rect;
			}
		}

		/// <summary>
		/// The <see cref="owner"/>'s <see cref="Rectangle"/>
		/// </summary>
		public Rectangle rect { get => _rect; }

		/// <summary>
		/// Called when the applications closes
		/// </summary>
		public virtual void Quit()
		{
			InputManager.Unregister(this);
		}

		/// <summary>
		/// Checks if this is of a certain type
		/// </summary>
		/// <typeparam name="T">the <see cref="System.Type"/></typeparam>
		public bool IsAssignableFrom<T>()
		{
			return ((typeof(T).IsAssignableFrom(GetType())));
		}

		/// <summary>
		/// Returns a component attached to the owner
		/// </summary>
		/// <typeparam name="T">A <see cref="Component>"/></typeparam>
		public T GetComponent<T>() where T : Component
		{
			return owner.GetComponent<T>();
		}
	}
}
