using System;
using System.Collections.Generic;
using System.Text;

using Iesi.Collections;

namespace MyNHibernateContrib.GenericCollections
{
	/// <summary>
	/// Wraps a <see cref="System.Collections.IList"/> instance
	/// in the .NET 2.0 <see cref="IList"/> generic interface.
	/// </summary>
	/// <remarks>
	/// This is especially useful for NHibernate applications, as
	/// NHibernate currently does not support generic collections.
	/// NHibernate can manage the non-generic collection, which in your
	/// class can remain private.  Then expose your collection 
	/// using generics by wrapping your non-generic collection using this
	/// class.
	/// </remarks>
	public class IListWrapper<T> : IList<T>
	{
		public IListWrapper(System.Collections.IList list)
		{
			if (list == null) throw new ArgumentNullException("list");
			this.list = list;
		}
		protected System.Collections.IList list;

		#region Events
		public class GenericListEventArgs<T> : EventArgs
		{
			public GenericListEventArgs(T item)
			{
				Item = item;
			}
			public T Item;
		}
		public event EventHandler<GenericListEventArgs<T>> Adding;
		public event EventHandler<GenericListEventArgs<T>> Removing;
		protected virtual void OnAdding(T item)
		{
			EventHandler<GenericListEventArgs<T>> adding = Adding;
			if (adding != null)
				adding(this, new GenericListEventArgs<T>(item));
		}
		protected virtual void OnRemoving(T item)
		{
			EventHandler<GenericListEventArgs<T>> removing = Removing;
			if (removing != null)
				removing(this, new GenericListEventArgs<T>(item));
		}
		#endregion

		#region IList<T> Members

		public int IndexOf(T item)
		{
			return list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			OnAdding(item);
			list.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			OnRemoving(this[index]);
			list.RemoveAt(index);
		}

		public T this[int index]
		{
			get { return (T)list[index]; }
			set
			{
				OnRemoving(this[index]);
				OnAdding(value);
				list[index] = value;
			}
		}

		#endregion

		#region ICollection<T> Members

		public void Add(T item)
		{
			OnAdding(item);
			list.Add(item);
		}

		public void Clear()
		{
			foreach (T item in this)
				OnRemoving(item);
			list.Clear();
		}

		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return list.Count; }
		}

		public bool IsReadOnly
		{
			get { return list.IsReadOnly; }
		}

		public bool Remove(T item)
		{
			if (!list.Contains(item)) return false;
			OnRemoving(item);
			list.Remove(item);
			return true;
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return (new IEnumerableWrapper<T>(list)).GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion
	}
}
