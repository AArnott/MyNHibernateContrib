using System;

using NHibernate.Type;
using NHibernate.SqlTypes;

namespace MyNHibernateContrib.Nullables
{
	public class DateTime : GenericNullableType<System.DateTime, DateTimeSqlType> {}
	public class Boolean : GenericNullableType<System.Boolean, BooleanSqlType> {}
	public class Byte : GenericNullableType<System.Byte, ByteSqlType> { }
	public class Char : GenericNullableType<System.Char, StringFixedLengthSqlType> 
	{
		public Char() : base(new StringFixedLengthSqlType(1)) { }
	}
	public class Decimal : GenericNullableType<System.Decimal, DecimalSqlType> { }
	public class Double : GenericNullableType<System.Double, DoubleSqlType> { }
	public class Guid : GenericNullableType<System.Guid, GuidSqlType> { }
	public class Int16 : GenericNullableType<System.Int16, Int16SqlType> { }
	public class Int32 : GenericNullableType<System.Int32, Int32SqlType> { }
	public class Int64 : GenericNullableType<System.Int64, Int64SqlType> { }
	public class SByte : GenericNullableType<System.SByte, SByteSqlType> { }
	public class Single : GenericNullableType<System.Single, SingleSqlType> { }

	/// <summary>
	/// Summary description for NullablesTypes.
	/// </summary>
	public sealed class NullablesTypes
	{
		/// <summary>
		/// Nullables.NHibernate.NullableBoolean type
		/// </summary>
		public static readonly NullableType NullableBoolean = new Boolean();
	
		/// <summary>
		/// Nullables.NHibernate.NullableByte type
		/// </summary>
		public static readonly NullableType NullableByte = new Byte();
	
		/// <summary>
		/// Nullables.NHibernate.NullableDouble type
		/// </summary>
		public static readonly NullableType NullableDouble = new Double();
	
		/// <summary>
		/// Nullables.NHibernate.NullableInt16 type
		/// </summary>
		public static readonly NullableType NullableInt16 = new Int16();
	
		/// <summary>
		/// Nullables.NHibernate.NullableInt32 type
		/// </summary>
		public static readonly NullableType NullableInt32 = new Int32();
	
		/// <summary>
		/// Nullables.NHibernate.NullableInt64 type
		/// </summary>
		public static readonly NullableType NullableInt64 = new Int64();
	
		/// <summary>
		/// Nullables.NHibernate.NullableDecimal type
		/// </summary>
		public static readonly NullableType NullableDecimal = new Decimal();
	
		/// <summary>
		/// Nullables.NHibernate.NullableDateTime type
		/// </summary>
		public static readonly NullableType NullableDateTime = new DateTime();

		/// <summary>
		/// Nullables.NHibernate.NullableSByte type
		/// </summary>
		public static readonly NullableType NullableSByte = new SByte();
	
		/// <summary>
		/// Nullables.NHibernate.NullableSingle type
		/// </summary>
		public static readonly NullableType NullableSingle = new Single();
	
		/// <summary>
		/// Nullables.NHibernate.NullableGuid type
		/// </summary>
		public static readonly NullableType NullableGuid = new Guid();
	}
}
