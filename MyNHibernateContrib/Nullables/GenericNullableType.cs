using System;

using NHibernate.Type;
using NHibernate.SqlTypes;

namespace MyNHibernateContrib.Nullables
{
	public abstract class GenericNullableType<T, SqlT> : NullableTypesType
		where T : struct
		where SqlT : SqlType, new()
	{
		internal static readonly T? Default = new T?();

		public GenericNullableType()
			: base(new SqlT())
		{
		}

		protected GenericNullableType(SqlType type)
			: base(type)
		{
		}

		public override bool Equals(object x, object y)
		{
			//get boxed values.
			T? xTyped = (T?)x;
			return xTyped.Equals(y);
		}

		public override object NullValue
		{
			get { return Default; }
		}
		
		public override bool HasNiceEquals
		{
			get { return true; }
		}
		
		public override bool IsMutable
		{
			get { return true; }
		}

		public override string Name
		{
			get { return typeof(T).Name + "?"; }
		}

		public override System.Type ReturnedClass
		{
			get { return typeof(T?); }
		}

		public override object DeepCopyNotNull(object val)
		{
			return val;
		}

		public override object Get(System.Data.IDataReader rs, int index)
		{
			//TODO: perhaps Int32? has a method/operator/contructor that will take an object.
			object value = rs[index];

			if( value==DBNull.Value )
			{
				return Default;
			}
			else 
			{
				return new T?((T)Convert.ChangeType(value, typeof(T)));
			}
		}

		public override void Set(System.Data.IDbCommand cmd, object value, int index)
		{
			System.Data.IDataParameter parameter = (System.Data.IDataParameter)cmd.Parameters[index];
			T? nullableValue = (T?)value;

			if( nullableValue.HasValue ) 
			{
				parameter.Value = nullableValue.Value;
			}
			else 
			{
				parameter.Value = DBNull.Value;
			}
		}

		public override string ToString(object val)
		{
			return val.ToString();
		}

		public override object FromStringValue(string xml)
		{
			return Convert.ChangeType(xml, typeof(T));
		}
	}
}
