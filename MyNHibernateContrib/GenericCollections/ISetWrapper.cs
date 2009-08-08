using System;
using System.Collections.Generic;
using System.Text;

using Iesi.Collections;

namespace MyNHibernateContrib.GenericCollections
{
	/// <summary>
	/// Wraps a <see cref="ISet"/> instance
	/// in the .NET 2.0 <see cref="ICollection"/> generic interface.
	/// </summary>
	/// <remarks>
	/// This is especially useful for NHibernate applications, as
	/// NHibernate currently does not support generic collections.
	/// NHibernate can manage the non-generic collection, which in your
	/// class can remain private.  Then expose your collection 
	/// using generics by wrapping your non-generic collection using this
	/// class.
	/// </remarks>
	public class ISetWrapper<T> : ICollection<T>
	{
		public ISetWrapper(ISet set)
		{
			if (set == null) throw new ArgumentNullException("set");
			this.set = set;
		}
		protected ISet set;

		#region Events
		public class GenericSetEventArgs<T> : EventArgs
		{
			public GenericSetEventArgs(T item)
			{
				Item = item;
			}
			public T Item;
		}
		public event EventHandler<GenericSetEventArgs<T>> Adding;
		public event EventHandler<GenericSetEventArgs<T>> Added;
		public event EventHandler<GenericSetEventArgs<T>> Removing;
		public event EventHandler<GenericSetEventArgs<T>> Removed;
		protected virtual void OnAdding(T item)
		{
			EventHandler<GenericSetEventArgs<T>> adding = Adding;
			if (adding != null)
				adding(this, new GenericSetEventArgs<T>(item));
		}
		protected virtual void OnAdded(T item)
		{
			EventHandler<GenericSetEventArgs<T>> added = Added;
			if (added != null)
				added(this, new GenericSetEventArgs<T>(item));
		}
		protected virtual void OnRemoving(T item)
		{
			EventHandler<GenericSetEventArgs<T>> removing = Removing;
			if (removing != null)
				removing(this, new GenericSetEventArgs<T>(item));
		}
		protected virtual void OnRemoved(T item)
		{
			EventHandler<GenericSetEventArgs<T>> removed = Removed;
			if (removed != null)
				removed(this, new GenericSetEventArgs<T>(item));
		}
		#endregion

		#region ICollection<T> Members

		public bool IsReadOnly
		{
			get { return false; }
		}

		public int Count
		{
			get { return set.Count; }
		}

		public void Add(T item)
		{
			OnAdding(item);
			set.Add(item);
			OnAdded(item);
		}

		public bool Contains(T item)
		{
			return set.Contains(item);
		}

		public bool Remove(T item)
		{
			if (!Contains(item)) return false;
			OnRemoving(item);
			bool result = set.Remove(item);
			OnRemoved(item);
			return result;
		}

		public void Clear()
		{
			T[] list = new T[Count];
			int i = 0;
			foreach (T item in this)
			{
				OnRemoving(item);
				list[i++] = item;
			}
			set.Clear();
			foreach(T item in list)
				OnRemoved(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			set.CopyTo(array, arrayIndex);
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return (new IEnumerableWrapper<T>(set)).GetEnumerator();
		}
		
		#endregion
		
		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return set.GetEnumerator();
		}

		#endregion
	}
}
