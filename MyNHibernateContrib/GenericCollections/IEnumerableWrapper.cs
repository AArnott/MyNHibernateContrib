using System;
using System.Collections.Generic;
using System.Text;

namespace MyNHibernateContrib.GenericCollections
{
	/// <summary>
	/// Wraps a <see cref="System.Collections.IEnumerable"/> instance
	/// in the .NET 2.0 <see cref="IEnumerable"/> generic interface.
	/// </summary>
	/// <remarks>
	/// This is especially useful for NHibernate applications, as
	/// NHibernate currently does not support generic collections.
	/// NHibernate can manage the non-generic collection, which in your
	/// class can remain private.  Then expose your collection 
	/// using generics by wrapping your non-generic collection using this
	/// class.
	/// </remarks>
	public class IEnumerableWrapper<T> : IEnumerable<T>
	{
		public IEnumerableWrapper(System.Collections.IEnumerable e)
		{
			if (e == null) throw new ArgumentNullException("e");
			this.e = e;
		}
		private System.Collections.IEnumerable e;

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return new GenericEnumeratorWrapper<T>(e);
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		public class GenericEnumeratorWrapper<T> : IEnumerator<T>
		{
			public GenericEnumeratorWrapper(System.Collections.IEnumerable e)
			{
				enumerator = e.GetEnumerator();
			}
			private System.Collections.IEnumerator enumerator;

			#region IEnumerator<T> Members

			public T Current
			{
				get { return (T)enumerator.Current; }
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{
			}

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			public bool MoveNext()
			{
				return enumerator.MoveNext();
			}

			public void Reset()
			{
				enumerator.Reset();
			}

			#endregion

			#region IEnumerator Members

			bool System.Collections.IEnumerator.MoveNext()
			{
				return this.MoveNext();
			}

			void System.Collections.IEnumerator.Reset()
			{
				this.Reset();
			}

			#endregion
		}

	}
}
