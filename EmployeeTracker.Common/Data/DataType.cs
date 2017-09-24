// Copyright Notice! 
// This document is protected under the trade secret and copyright 
// laws as the property of Fidelity National Information Services, Inc. 
// Copying, reproduction or distribution should be limited and only to
// employees with a “need to know” to do their job. 
// Any disclosure of this document to third parties is strictly prohibited.

// © 2015 Fidelity National Information Services.
// All rights reserved worldwide.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace EmployeeTracker.Common.Data
{
    [Serializable]
	public class DataType : IEquatable<DataType>
	{
		#region Enumeration Kind

		private enum Kind
		{
			None,

			Boolean,
			Currency,
			Date,
			DateTime,
			Decimal,
			Double,
			Int16,
			Int32,
			Int64,
			Single,
			String,
			Text
		}

		#endregion

		#region Private Member Variables

		/// <summary>
		/// xml serializer for data
		/// </summary>
		//private static readonly ISerializer<IType> m_oISerializer = Serializer<IType>.Create();

		/// <summary>
		/// dictionary of data types by name
		/// </summary>
		private static readonly Dictionary<string, DataType> m_hDataTypeByName = new Dictionary<string, DataType>(Enum.GetValues(typeof(Kind)).Cast<Kind>().Select(k => new DataType(k)).ToDictionary(d => d.ToString()), StringComparer.InvariantCultureIgnoreCase);

		/// <summary>
		/// instance of each data type
		/// </summary>
		private static readonly DataType m_oDataTypeBoolean = m_hDataTypeByName[Kind.Boolean.ToString()];
		private static readonly DataType m_oDataTypeCurrency = m_hDataTypeByName[Kind.Currency.ToString()];
		private static readonly DataType m_oDataTypeDate = m_hDataTypeByName[Kind.Date.ToString()];
		private static readonly DataType m_oDataTypeDateTime = m_hDataTypeByName[Kind.DateTime.ToString()];
		private static readonly DataType m_oDataTypeDecimal = m_hDataTypeByName[Kind.Decimal.ToString()];
		private static readonly DataType m_oDataTypeDouble = m_hDataTypeByName[Kind.Double.ToString()];
		private static readonly DataType m_oDataTypeInt16 = m_hDataTypeByName[Kind.Int16.ToString()];
		private static readonly DataType m_oDataTypeInt32 = m_hDataTypeByName[Kind.Int32.ToString()];
		private static readonly DataType m_oDataTypeInt64 = m_hDataTypeByName[Kind.Int64.ToString()];
		private static readonly DataType m_oDataTypeSingle = m_hDataTypeByName[Kind.Single.ToString()];
		private static readonly DataType m_oDataTypeString = m_hDataTypeByName[Kind.String.ToString()];
		private static readonly DataType m_oDataTypeText = m_hDataTypeByName[Kind.Text.ToString()];
		private static readonly DataType m_oDataTypeNone = m_hDataTypeByName[Kind.None.ToString()];

		/// <summary>
		/// kind of data type for efficiency
		/// </summary>
		private readonly Kind m_oKind;

		/// <summary>
		/// type code for the implementation type
		/// </summary>
		private readonly TypeCode m_oTypeCode;

		/// <summary>
		/// empty value for the type
		/// </summary>
		private readonly IType m_oIType;

		/// <summary>
		/// indicates if the type is an integer
		/// </summary>
		private readonly bool m_bInteger;

		/// <summary>
		/// indicates if the type is a numeric
		/// </summary>
		private readonly bool m_bNumeric;

		#endregion

		#region Private Constructors

		private DataType(Kind oKind)
		{
			m_oKind = oKind;
			switch (oKind)
			{
				case Kind.Boolean :
					m_oTypeCode = TypeCode.Boolean;
					break;
				case Kind.Currency :
					m_oTypeCode = TypeCode.Decimal;
					m_bNumeric = true;
					break;
				case Kind.Date :
					m_oTypeCode = TypeCode.DateTime;
					break;
				case Kind.DateTime :
					m_oTypeCode = TypeCode.DateTime;
					break;
				case Kind.Decimal :
					m_oTypeCode = TypeCode.Decimal;
					m_bNumeric = true;
					break;
				case Kind.Double :
					m_oTypeCode = TypeCode.Double;
					m_bNumeric = true;
					break;
				case Kind.Int16 :
					m_oTypeCode = TypeCode.Int16;
					m_bInteger = true;
					m_bNumeric = true;
					break;
				case Kind.Int32 :
					m_oTypeCode = TypeCode.Int32;
					m_bInteger = true;
					m_bNumeric = true;
					break;
				case Kind.Int64 :
					m_oTypeCode = TypeCode.Int64;
					m_bInteger = true;
					m_bNumeric = true;
					break;
				case Kind.None :
					m_oTypeCode = TypeCode.DBNull;
					break;
				case Kind.Single :
					m_oTypeCode = TypeCode.Single;
					m_bNumeric = true;
					break;
				case Kind.String :
					m_oTypeCode = TypeCode.String;
					break;
				case Kind.Text :
					m_oTypeCode = TypeCode.String;
					break;
				default :
					m_oTypeCode = TypeCode.String;
					break;
			}
			m_oIType = new EmptyType(this);
		}

		#endregion

		#region DataType Members

		public static DataType Boolean
		{
			get
			{
				return m_oDataTypeBoolean;
			}
		}

		public static DataType Currency
		{
			get
			{
				return m_oDataTypeCurrency;
			}
		}

		public static DataType Date
		{
			get
			{
				return m_oDataTypeDate;
			}
		}

		public static DataType DateTime
		{
			get
			{
				return m_oDataTypeDateTime;
			}
		}

		public static DataType Decimal
		{
			get
			{
				return m_oDataTypeDecimal;
			}
		}

		public static DataType Double
		{
			get
			{
				return m_oDataTypeDouble;
			}
		}

		public static DataType Int16
		{
			get
			{
				return m_oDataTypeInt16;
			}
		}

		public static DataType Int32
		{
			get
			{
				return m_oDataTypeInt32;
			}
		}

		public static DataType Int64
		{
			get
			{
				return m_oDataTypeInt64;
			}
		}

		public static DataType None
		{
			get
			{
				return m_oDataTypeNone;
			}
		}

		public static DataType Single
		{
			get
			{
				return m_oDataTypeSingle;
			}
		}

		public static DataType String
		{
			get
			{
				return m_oDataTypeString;
			}
		}

		public static DataType Text
		{
			get
			{
				return m_oDataTypeText;
			}
		}

		public TypeCode TypeCode
		{
			get
			{
				return m_oTypeCode;
			}
		}

		public static IEnumerable<DataType> Values
		{
			get
			{
				return m_hDataTypeByName.Values.Where(d => d.m_oKind != Kind.None);
			}
		}

		public static IType Create(IConvertible oIConvertible, IFormatProvider oIFormatProvider)
		{
			IType		oIType;
			switch (oIConvertible.GetTypeCode())
			{
				case TypeCode.Boolean :
					oIType = new BooleanType(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.Byte :
					oIType = new Int16Type(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.Char :
					oIType = new StringType(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.DateTime :
					oIType = new DateTimeType(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.DBNull :
					oIType = DataType.Empty(DataType.None);
					break;
				case TypeCode.Decimal :
					oIType = new DecimalType(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.Double :
					oIType = new DoubleType(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.Int16 :
					oIType = new Int16Type(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.Int32 :
					oIType = new Int32Type(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.Int64 :
					oIType = new Int64Type(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.Object :
					oIType = oIConvertible as IType ?? new StringType(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.SByte :
					oIType = new Int16Type(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.Single :
					oIType = new SingleType(oIConvertible, oIFormatProvider);
					break;
				case TypeCode.String :
					oIType = new StringType(oIConvertible, oIFormatProvider);
					break;
				default :
					throw new ArgumentException(string.Format("Invalid value type {0} specified", oIConvertible.GetType()));
			}
			return oIType;
		}

		public static IType Create(DataType oDataType, IConvertible oIConvertible, IFormatProvider oIFormatProvider)
		{
			IType		oIType;
			switch (oDataType.m_oKind)
			{
				case Kind.Boolean :
					oIType = new BooleanType(oIConvertible, oIFormatProvider);
					break;
				case Kind.Currency :
					oIType = new CurrencyType(oIConvertible, oIFormatProvider);
					break;
				case Kind.Date :
					oIType = new DateType(oIConvertible, oIFormatProvider);
					break;
				case Kind.DateTime :
					oIType = new DateTimeType(oIConvertible, oIFormatProvider);
					break;
				case Kind.Decimal :
					oIType = new DecimalType(oIConvertible, oIFormatProvider);
					break;
				case Kind.Double :
					oIType = new DoubleType(oIConvertible, oIFormatProvider);
					break;
				case Kind.Int16 :
					oIType = new Int16Type(oIConvertible, oIFormatProvider);
					break;
				case Kind.Int32 :
					oIType = new Int32Type(oIConvertible, oIFormatProvider);
					break;
				case Kind.Int64 :
					oIType = new Int64Type(oIConvertible, oIFormatProvider);
					break;
				case Kind.Single :
					oIType = new SingleType(oIConvertible, oIFormatProvider);
					break;
				case Kind.String :
					oIType = new StringType(oIConvertible, oIFormatProvider);
					break;
				case Kind.Text :
					oIType = new TextType(oIConvertible, oIFormatProvider);
					break;
				default :
					throw new ArgumentException(string.Format("Invalid type {0} specified", oDataType));
			}
			return oIType;
		}

		public static IType Create(string sValue)
		{
			return string.IsNullOrEmpty(sValue) ? DataType.Empty(DataType.String) : new StringType(sValue);
		}

		public static IType Create(DataType oDataType, string sValue, IFormatProvider oIFormatProvider)
		{
			IType		oIType;
			if (string.IsNullOrEmpty(sValue))
			{
				oIType = Empty(oDataType);
			}
			else
			{
				switch (oDataType.m_oKind)
				{
					case Kind.Boolean :
						oIType = new BooleanType(sValue, oIFormatProvider);
						break;
					case Kind.Currency :
						oIType = new CurrencyType(sValue, oIFormatProvider);
						break;
					case Kind.Date :
						oIType = new DateType(sValue, oIFormatProvider);
						break;
					case Kind.DateTime :
						oIType = new DateTimeType(sValue, oIFormatProvider);
						break;
					case Kind.Decimal :
						oIType = new DecimalType(sValue, oIFormatProvider);
						break;
					case Kind.Double :
						oIType = new DoubleType(sValue, oIFormatProvider);
						break;
					case Kind.Int16 :
						oIType = new Int16Type(sValue, oIFormatProvider);
						break;
					case Kind.Int32 :
						oIType = new Int32Type(sValue, oIFormatProvider);
						break;
					case Kind.Int64 :
						oIType = new Int64Type(sValue, oIFormatProvider);
						break;
					case Kind.None :
						oIType = new StringType(sValue);
						break;
					case Kind.Single :
						oIType = new SingleType(sValue, oIFormatProvider);
						break;
					case Kind.String :
						oIType = new StringType(sValue);
						break;
					case Kind.Text :
						oIType = new TextType(sValue);
						break;
					default :
						throw new ArgumentException(string.Format("Invalid type {0} specified", oDataType));
				}
			}
			return oIType;
		}

		public static IType Create(DataType oDataType, string sValue, string sFormat, IFormatProvider oIFormatProvider)
		{
			IType		oIType;
			if (string.IsNullOrEmpty(sValue))
			{
				oIType = Empty(oDataType);
			}
			else
			{
				switch (oDataType.m_oKind)
				{
					case Kind.Boolean :
						oIType = new BooleanType(sValue, oIFormatProvider);
						break;
					case Kind.Currency :
						oIType = new CurrencyType(sValue, oIFormatProvider);
						break;
					case Kind.Date :
						oIType = new DateType(sValue, sFormat, oIFormatProvider);
						break;
					case Kind.DateTime :
						oIType = new DateTimeType(sValue, sFormat, oIFormatProvider);
						break;
					case Kind.Decimal :
						oIType = new DecimalType(sValue, oIFormatProvider);
						break;
					case Kind.Double :
						oIType = new DoubleType(sValue, oIFormatProvider);
						break;
					case Kind.Int16 :
						oIType = new Int16Type(sValue, oIFormatProvider);
						break;
					case Kind.Int32 :
						oIType = new Int32Type(sValue, oIFormatProvider);
						break;
					case Kind.Int64 :
						oIType = new Int64Type(sValue, oIFormatProvider);
						break;
					case Kind.Single :
						oIType = new SingleType(sValue, oIFormatProvider);
						break;
					case Kind.String :
						oIType = new StringType(sValue);
						break;
					case Kind.Text :
						oIType = new TextType(sValue);
						break;
					default :
						throw new ArgumentException(string.Format("Invalid type {0} specified", oDataType));
				}
			}
			return oIType;
		}

		public static IType Create(DataType oDataType, IEnumerable<IConvertible> aIEnumerable, IFormatProvider oIFormatProvider)
		{
			return new ArrayType(oDataType, aIEnumerable.Select(i => DataType.Create(oDataType, i, oIFormatProvider)));
		}

		//public static IType Deserialize(XmlReader oXmlReader)
		//{
		//	return m_oISerializer.Deserialize(oXmlReader);
		//}

		public static IType Empty(DataType oDataType)
		{
			return oDataType.m_oIType;
		}

		public static Type GetType(TypeCode oTypeCode)
		{
			Type	oType;
			switch (oTypeCode)
			{
				case TypeCode.Boolean :
					oType = typeof(Boolean);
					break;
				case TypeCode.Byte :
					oType = typeof(Byte);
					break;
				case TypeCode.Char :
					oType = typeof(Char);
					break;
				case TypeCode.DateTime :
					oType = typeof(DateTime);
					break;
				case TypeCode.DBNull :
					oType = typeof(DBNull);
					break;
				case TypeCode.Decimal :
					oType = typeof(Decimal);
					break;
				case TypeCode.Double :
					oType = typeof(Double);
					break;
				case TypeCode.Empty :
					oType = null;
					break;
				case TypeCode.Int16 :
					oType = typeof(Int16);
					break;
				case TypeCode.Int32 :
					oType = typeof(Int32);
					break;
				case TypeCode.Int64 :
					oType = typeof(Int64);
					break;
				case TypeCode.Object :
					oType = typeof(Object);
					break;
				case TypeCode.SByte :
					oType = typeof(SByte);
					break;
				case TypeCode.Single :
					oType = typeof(Single);
					break;
				case TypeCode.String :
					oType = typeof(String);
					break;
				case TypeCode.UInt16 :
					oType = typeof(UInt16);
					break;
				case TypeCode.UInt32 :
					oType = typeof(UInt32);
					break;
				case TypeCode.UInt64 :
					oType = typeof(UInt64);
					break;
				default :
					throw new ArgumentException("Invalid type code specified", oTypeCode.ToString());
			}
			return oType;
		}

		public static bool IsInteger(DataType oDataType)
		{
			return oDataType.m_bInteger;
		}

		public static bool IsNumeric(DataType oDataType)
		{
			return oDataType.m_bNumeric;
		}

		public static DataType Parse(string sDataType)
		{
			DataType oDataType;
			if (m_hDataTypeByName.TryGetValue(sDataType, out oDataType))
				return oDataType;
			else
				throw new ArgumentException("Invalid data type name " + sDataType);
		}

		//public static void Serialize(XmlWriter oXmlWriter, IType oIType)
		//{
		//	m_oISerializer.Serialize(oXmlWriter, oIType);
		//}

		public static IType ToType(IType oIType, DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return oDataType == DataType.None ? oIType : oIType.ToType(oDataType, oIFormatProvider);
		}

		public static IEnumerable<IType> ToType(IEnumerable<IType> oIEnumerable, DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return oIEnumerable.Select(t => oDataType == DataType.None ? t : t.ToType(oDataType, oIFormatProvider));
		}

		public static bool TryParse(DataType oDataType, string sValue, IFormatProvider oIFormatProvider, out IType oIType)
		{
			bool result = true;
			try
			{
				oIType = Create(oDataType, sValue, oIFormatProvider);
			}
			catch (ArithmeticException)
			{
				result = false;
				oIType = null;
			}
			catch (FormatException)
			{
				result = false;
				oIType = null;
			}
			catch (InvalidCastException)
			{
				result = false;
				oIType = null;
			}
			return result;
		}

        public static bool TryParse(DataType oDataType, string sValue, string sFormat, IFormatProvider oIFormatProvider, out IType oIType)
        {
            bool result = true;
            try
            {
                oIType = Create(oDataType, sValue, sFormat, oIFormatProvider);
            }
            catch (ArithmeticException)
            {
                result = false;
                oIType = null;
            }
            catch (FormatException)
            {
                result = false;
                oIType = null;
            }
            catch (InvalidCastException)
            {
                result = false;
                oIType = null;
            }
            return result;
        }

        public static bool TryParse(string sDataType, out DataType oDataType)
		{
			bool	result;
			if (!(result = m_hDataTypeByName.TryGetValue(sDataType, out oDataType)))
				oDataType = DataType.String;
			return result;
		}

		#endregion

		#region IEquatable<DataType> Members

		public bool Equals(DataType other)
		{
			return m_oKind.Equals(other.m_oKind);
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oKind.GetHashCode();
		}

		public override string ToString()
		{
			return Enum.GetName(typeof(Kind), m_oKind);
		}

		#endregion
	}
}