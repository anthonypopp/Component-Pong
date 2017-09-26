using System;
using System.Collections.Generic;
using ComponentConsolePong.Input;

namespace ComponentConsolePong
{
	/// <summary>
	/// Similar to Game Objects in Unity
	/// </summary>
	public class GameObject : Quitable
	{
		/// <summary>Top topmost GameObject in the game. All GameObjets are children of this</summary>
		public static GameObject ROOT;

		GameObject parent;
		List<GameObject> children = new List<GameObject>(); //Children of the GameObject
		List<Component> components = new List<Component>(); //Components attached to this GameObject
		Dictionary<Type, Component> lookup = new Dictionary<Type, Component>(); //A map used for quick access for attached Components
		public Rectangle rect = new Rectangle();

		protected GameObject()
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
		
		/// <summary>
		/// Sets the parent of this <see cref="GameObject"/>
		/// </summary>
		/// <param name="parent">The parent to set, null to set to <see cref="ROOT"/>"/></param>
		public void SetParent(GameObject parent)
		{
			if (this == parent)
			{
				return;
			}
			if (parent == null)
			{
				parent = ROOT;
			}
			this.parent?.children.Remove(this);
			parent.children.Add(this);
			this.parent = parent;
		}
		
		/// <summary>
		/// Returns a list of all <see cref="GameObject"/> on the <see cref="ROOT"/>
		/// </summary>
		/// <typeparam name="T">A <see cref="GameObject"/></typeparam>
		public static List<T> GetGameObjectsInGame<T>(bool allowSubclasses = true) where T : GameObject
		{
			return ROOT.GetGameObjectsInChildren<T>(allowSubclasses);
		}

		/// <summary>
		/// Returns a list of all <see cref="GameObject"/> attached to this GameObject and children
		/// </summary>
		public List<T> GetGameObjectsInChildren<T>(bool allowSubclasses) where T : GameObject
		{
			return GetGameObjectsInChildren(new List<T>(), allowSubclasses);
		}

		/// <summary>
		/// Returns a list of all <see cref="GameObject"/> that are children of this
		/// </summary>
		/// <typeparam name="T">A GameObject</typeparam>
		/// <param name="result">the list of T</param>
		/// <param name="allowSubclasses">if true, subclasses of T will be included in the list/></param>
		/// <returns>the list of T</returns>
		private List<T> GetGameObjectsInChildren<T>(List<T> result, bool allowSubclasses) where T : GameObject
		{
			if (Is<T>())
			{
				result.Add((T)this);
			}
			foreach (GameObject child in children)
			{
				child.GetGameObjectsInChildren(result, allowSubclasses);
			}
			return result;
		}

		/// <summary>
		/// Returns a list of all <see cref="Component"/> on the <see cref="ROOT"/>
		/// </summary>
		/// <typeparam name="T">A <see cref="Component"/></typeparam>
		public static List<T> GetComponentsInGame<T>() where T : Component
		{
			return ROOT.GetComponentsInChildren<T>();
		}

		/// <summary>
		/// Returns a list of all <see cref="Component"/> attached to this GameObject and children
		/// </summary>
		public List<T> GetComponentsInChildren<T>() where T : Component
		{
			return GetComponentsInChildren(new List<T>());
		}

		/// <summary>
		/// Returns a list of all <see cref="Component"/> attached to this GameObject and children
		/// </summary>
		/// <typeparam name="T">The type of component being seeked</typeparam>
		/// <param name="result">a reference to the to the list we are adding components to</param>
		/// <returns>the updated list of components</returns>
		private List<T> GetComponentsInChildren<T>(List<T> result) where T : Component
		{
			T t = GetComponent<T>();
			if (t != null)
			{
				result.Add(t);
			}
			foreach (GameObject child in children)
			{
				child.GetComponentsInChildren(result);
			}
			return result;
		}

		/// <summary>
		/// Returns the first occurance of a component attached to this GameObject
		/// </summary>
		/// <typeparam name="T">A Component</typeparam>
		/// <returns>The <see cref="Component"/>, null if none exists</returns>
		public T GetComponent<T>() where T : Component
		{

			Type t = typeof(T);
			if (lookup.ContainsKey(t))
			{
				return (T)lookup[t];
			}
			return null;
		}

		/// <summary>
		/// Adds a <see cref="Component"/> to this <see cref="GameObject"/>
		/// </summary>
		/// <typeparam name="T">A <see cref="GameObject"/></typeparam>
		/// <param name="component"></param>
		/// <returns>returns a reference of this object</returns>
		public GameObject AddComponent<T>(T component) where T : Component
		{
			component.owner = this;
			//Allow multiple components
			components.Add(component);

			if (!lookup.ContainsKey(typeof(T)))
			{
				lookup.Add(typeof(T), component);  //But only return the first occurance of it :/
			}
			return this;
			//Todo, lookup list of same component instead
		}

		/// <summary>
		/// Returns whether or not a given GameObject if of a certain type
		/// </summary>
		/// <typeparam name="T">A GameObject to check against</typeparam>
		/// <param name="includeSubclass">if true, will also check for classes which inherent from T</param>
		/// <returns></returns>
		public bool Is<T>(bool includeSubclass = true) where T : GameObject
		{
			return ((!includeSubclass || typeof(T).IsSubclassOf(GetType())) || this is T);
		}

		/// <summary>
		/// Updates all <see cref="Updateable"/> components
		/// </summary>
		/// <param name="deltaTime">time between fames</param>
		public void UpdateComponents(float deltaTime)
		{
			foreach (Component component in components)
			{
				if (component.enabled)
				{
					if (component.IsAssignableFrom<Updateable>())
					{
						((Updateable)component).Update(deltaTime);
					}
				}
			}
		}

		/// <summary>
		/// Draws all <see cref="Drawable"/> components
		/// </summary>
		/// <param name="board">the board to draw on</param>
		public void DrawComponents(Board board)
		{
			foreach (Component component in components)
			{
				if (component.IsAssignableFrom<Drawable>())
				{
					((Drawable)component).Draw(board);
				}
			}
		}

		/// <summary>
		/// Resets all <see cref="Resetable"/> components
		/// </summary>
		public void ResetComponents()
		{
			foreach (Component component in components)
			{
				if (component.IsAssignableFrom<Resetable>())
				{
					((Resetable)component).Reset();
				}
			}
		}

		/// <summary>
		/// Writes messages from <see cref="Messageable"/> components
		/// </summary>
		public void WriteComponents()
		{
			foreach (Component component in components)
			{
				if (component.IsAssignableFrom<Messageable>())
				{
					((Messageable)component).Write();
				}
			}
		}

		/// <summary>
		/// Makes a GameObject a child of this, and sets its owner to this too
		/// </summary>
		/// <param name="child">the GameObject being set as the child</param>
		public void AddChild(GameObject child)
		{
			if (child.parent != null)
			{
				if (!children.Contains(child))
				{
					children.Add(child);
					child.parent = this;
				}
			}
			else
			{
				throw new Exception("Cannot Add ROOT as child");
			}
		}

		public virtual void Quit()
		{
			//Unregister inputs
			InputManager.Unregister(this);
			foreach (Component component in components)
			{
				component.Quit();
			}
		}

		/// <summary>
		/// Checks if this is of a certain type
		/// </summary>
		/// <typeparam name="T">the <see cref="System.Type"/></typeparam>
		/// <returns></returns>
		public bool IsAssignableFrom<T>()
		{
			return ((typeof(T).IsAssignableFrom(GetType())));
		}
	}
}
