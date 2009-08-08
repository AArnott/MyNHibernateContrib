using System;
using System.Diagnostics;

using NHibernate.Type;
using NHibernate.SqlTypes;

namespace MyNHibernateContrib.Nullables
{
	public abstract class GenericNullableEnumType<T, NHT, SqlT> : GenericNullableType<NHT, SqlT>
		where T : struct // System.Enum
		where NHT : struct
		where SqlT : SqlType, new()
	{
		public override bool Equals(object x, object y)
		{
			//get boxed values.
			if (x != null)
			{
				System.Enum xTyped = (System.Enum)x;
				return xTyped.Equals(y);
			}
			else
				return base.Equals(x, y);
		}
		public override void Set(System.Data.IDbCommand cmd, object value, int index)
		{
			if (value != null)
			{
				System.Data.IDataParameter parameter = (System.Data.IDataParameter)cmd.Parameters[index];
				parameter.Value = value;
			}
			else
				base.Set(cmd, value, index);
		}
		public override object Get(System.Data.IDataReader rs, int index)
		{
			object value = rs[index];
			if (value == DBNull.Value)
				return base.Get(rs, index);
			else
			{
				Debug.Assert(value != null);
				return Enum.Parse(typeof(T), value.ToString());
			}
		}
	}
}
