using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.GenericCollections
{
	public class IEnumerableDictionaryWrapper<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
	{
		public IEnumerableDictionaryWrapper(System.Collections.IEnumerable dictionary)
		{
			this.dictionary = dictionary;
		}
		private System.Collections.IEnumerable dictionary;


		#region IEnumerable<KeyValuePair<TKey,TValue>> Members

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new GenericDictionaryEnumeratorWrapper<TKey, TValue>(dictionary.GetEnumerator());
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		public class GenericDictionaryEnumeratorWrapper<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
		{
			public GenericDictionaryEnumeratorWrapper(System.Collections.IEnumerator dictionary)
			{
				this.dictionary = dictionary;
			}

			System.Collections.IEnumerator dictionary;
			KeyValuePair<TKey, TValue> cachePair;
			bool cacheSet = false;

			#region IEnumerator<KeyValuePair<TKey,TValue>> Members

			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (!cacheSet)
					{
						System.Collections.DictionaryEntry entry = (System.Collections.DictionaryEntry)dictionary.Current;
						cachePair = new KeyValuePair<TKey, TValue>((TKey)entry.Key, (TValue)entry.Value);
						cacheSet = true;
					}
					return cachePair;
				}
			}

			#endregion

			#region IDisposable Members

			public void Dispose() { }

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			public bool MoveNext()
			{
				cacheSet = false;
				return dictionary.MoveNext();
			}

			public void Reset()
			{
				dictionary.Reset();
				cacheSet = false;
			}

			#endregion
		}
	}
}
