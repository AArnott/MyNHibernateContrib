using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

using MyNHibernateContrib.GenericCollections;

namespace MyNHibernateContrib.GenericCollections
{
	/// <summary>
	/// Wraps a <see cref="System.Collections.IDictionary"/> instance
	/// in the .NET 2.0 <see cref="IDictionary"/> generic interface.
	/// </summary>
	/// <remarks>
	/// This is especially useful for NHibernate applications, as
	/// NHibernate currently does not support generic collections.
	/// NHibernate can manage the non-generic collection, which in your
	/// class can remain private.  Then expose your collection 
	/// using generics by wrapping your non-generic collection using this
	/// class.
	/// </remarks>
	public class IDictionaryWrapper<TKey, TValue> : IDictionary<TKey, TValue>
	{
		public IDictionaryWrapper(System.Collections.IDictionary dictionary)
		{
			if (dictionary == null) throw new ArgumentNullException("dictionary");
			this.dictionary = dictionary;
		}

		protected System.Collections.IDictionary dictionary;

		#region Events
		public class GenericDictionaryEventArgs<TKey, TValue> : EventArgs
		{
			public GenericDictionaryEventArgs(TKey key, TValue item)
			{
				Key = key;
				Item = item;
			}
			public readonly TKey Key;
			public readonly TValue Item;
		}
		public event EventHandler<GenericDictionaryEventArgs<TKey, TValue>> Adding;
		public event EventHandler<GenericDictionaryEventArgs<TKey, TValue>> Removing;
		protected virtual void OnAdding(TKey key, TValue item)
		{
			EventHandler<GenericDictionaryEventArgs<TKey, TValue>> adding = Adding;
			if (adding != null)
				adding(this, new GenericDictionaryEventArgs<TKey, TValue>(key, item));
		}
		protected virtual void OnRemoving(TKey key, TValue item)
		{
			EventHandler<GenericDictionaryEventArgs<TKey, TValue>> removing = Removing;
			if (removing != null)
				removing(this, new GenericDictionaryEventArgs<TKey, TValue>(key, item));
		}
		#endregion

		#region IDictionary<TKey,TValue> Members

		public void Add(TKey key, TValue value)
		{
			OnAdding(key, value);
			dictionary.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return dictionary.Contains(key);
		}

		public ICollection<TKey> Keys
		{
			get { return new List<TKey>(new IEnumerableWrapper<TKey>(dictionary.Keys)); }
		}

		public bool Remove(TKey key)
		{
			if (!ContainsKey(key)) return false;
			OnRemoving(key, this[key]);
			dictionary.Remove(key);
			return true;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (!dictionary.Contains(key))
			{
				value = default(TValue);
				return false;
			}
			value = this[key];
			return true;
		}

		public ICollection<TValue> Values
		{
			get { return new List<TValue>(new IEnumerableWrapper<TValue>(dictionary.Values)); }
		}

		public TValue this[TKey key]
		{
			get
			{
				return (TValue)dictionary[key];
			}
			set
			{
				if( dictionary.Contains(key) )
					OnRemoving(key, this[key]);
				OnAdding(key, value);
				dictionary[key] = value;
			}
		}

		#endregion

		#region ICollection<KeyValuePair<TKey,TValue>> Members

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			OnAdding(item.Key, item.Value);
			dictionary.Add(item.Key, item.Value);
		}

		public void Clear()
		{
			foreach (KeyValuePair<TKey, TValue> pair in this)
				OnRemoving(pair.Key, pair.Value);
			dictionary.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (!dictionary.Contains(item.Key)) return false;
			if ((item.Value == null) != (this[item.Key] == null)) return false;
			if (item.Value != null && !item.Value.Equals(this[item.Key])) return false;
			return true;
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			dictionary.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return dictionary.Count; }
		}

		public bool IsReadOnly
		{
			get { return dictionary.IsReadOnly; }
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (!Contains(item)) return false;
			OnRemoving(item.Key, item.Value);
			dictionary.Remove(item.Key);
			return true;
		}

		#endregion

		#region IEnumerable<KeyValuePair<TKey,TValue>> Members

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return (new IEnumerableDictionaryWrapper<TKey, TValue>(dictionary)).GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}
