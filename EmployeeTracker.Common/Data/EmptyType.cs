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
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EmployeeTracker.Common.Data
{
	[Serializable]
	[XmlRoot(ElementName = "empty")]
	public class EmptyType : IType, IXmlSerializable
	{
		#region Private Member Variables

		private DataType m_oDataType;

		#endregion

		#region Internal Constructors

		internal EmptyType()
		{
		}

		internal EmptyType(DataType oDataType)
		{
			m_oDataType = oDataType;
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			int result;
			IType oIType;
			if ((oIType = obj as IType) != null)
				result = CompareTo(oIType);
			else
				throw new ArgumentException("The object is not the same type as this instance for comparison");
			return result;
		}

		#endregion

		#region IComparable<IType> Members

		public int CompareTo(IType oIType)
		{
			return CompareTo(oIType, CultureInfo.CurrentCulture);
		}

		#endregion

		#region IConvertible Members

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		public bool ToBoolean(IFormatProvider oIFormatProvider)
		{
			return (bool) ToType(TypeCode.Boolean, oIFormatProvider);
		}

		public byte ToByte(IFormatProvider oIFormatProvider)
		{
			return (byte) ToType(TypeCode.Byte, oIFormatProvider);
		}

		public char ToChar(IFormatProvider oIFormatProvider)
		{
			return (char) ToType(TypeCode.Char, oIFormatProvider);
		}

		public DateTime ToDateTime(IFormatProvider oIFormatProvider)
		{
			return (DateTime) ToType(TypeCode.DateTime, oIFormatProvider);
		}

		public decimal ToDecimal(IFormatProvider oIFormatProvider)
		{
			return (decimal) ToType(TypeCode.Decimal, oIFormatProvider);
		}

		public double ToDouble(IFormatProvider oIFormatProvider)
		{
			return (double) ToType(TypeCode.Double, oIFormatProvider);
		}

		public short ToInt16(IFormatProvider oIFormatProvider)
		{
			return (short) ToType(TypeCode.Int16, oIFormatProvider);
		}

		public int ToInt32(IFormatProvider oIFormatProvider)
		{
			return (int) ToType(TypeCode.Int32, oIFormatProvider);
		}

		public long ToInt64(IFormatProvider oIFormatProvider)
		{
			return (long) ToType(TypeCode.Int64, oIFormatProvider);
		}

		public sbyte ToSByte(IFormatProvider oIFormatProvider)
		{
			return (sbyte) ToType(TypeCode.SByte, oIFormatProvider);
		}

		public float ToSingle(IFormatProvider oIFormatProvider)
		{
			return (float) ToType(TypeCode.Single, oIFormatProvider);
		}

		public string ToString(IFormatProvider oIFormatProvider)
		{
			return string.Empty;
		}

		public object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			return Convert.ChangeType(DBNull.Value, oType, oIFormatProvider);
		}

		public ushort ToUInt16(IFormatProvider oIFormatProvider)
		{
			return (ushort) ToType(TypeCode.UInt32, oIFormatProvider);
		}

		public uint ToUInt32(IFormatProvider oIFormatProvider)
		{
			return (uint) ToType(TypeCode.UInt32, oIFormatProvider);
		}

		public ulong ToUInt64(IFormatProvider oIFormatProvider)
		{
			return (ulong) ToType(TypeCode.UInt64, oIFormatProvider);
		}

		#endregion

		#region IEquatable<IType> Members

		public bool Equals(IType oIType)
		{
			return oIType.IsEmpty;
		}

		#endregion

		#region IType Members

		public DataType DataType
		{
			get
			{
				return m_oDataType;
			}
		}

		public bool IsArray
		{
			get
			{
				return false;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return true;
			}
		}

		public IEnumerable<IType> Item
		{
			get
			{
				return Enumerable.Empty<IType>();
			}
		}

		public TypeCode TypeCode
		{
			get
			{
				return TypeCode.DBNull;
			}
		}

		public int CompareTo(IType oIType, CultureInfo oCultureInfo)
		{
			return oIType.IsEmpty ? 0 : -1;
		}

		public T ToType<T>(IFormatProvider oIFormatProvider) where T : IConvertible
		{
			return (T) ToType(Type.GetTypeCode(typeof(T)), oIFormatProvider);
		}

		public IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			IConvertible oIConvertible;
			switch (oTypeCode)
			{
				case TypeCode.DBNull :
					oIConvertible = DBNull.Value;
					break;
				case TypeCode.String :
					oIConvertible = string.Empty;
					break;
				default :
					throw new InvalidCastException(string.Format("Cannot convert an empty value to type {0}", oTypeCode));
			}
			return oIConvertible;
		}

		public IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return DataType.Empty(oDataType);
		}

		public IType ToType(DataType oDataType, string sFormat, IFormatProvider oIFormatProvider)
		{
			return DataType.Empty(oDataType);
		}

		public bool TryToType(DataType oDataType, IFormatProvider oIFormatProvider, out IType oIType)
		{
			oIType = oDataType == DataType.None ? this : DataType.Empty(oDataType);
			return true;
		}

		#endregion

		#region Object Members

		public override bool Equals(object oObject)
		{
			bool result;
			IType oIType;
			if ((oIType = oObject as IType) != null)
				result = Equals(oIType);
			else
				result = DBNull.Value.Equals(oObject);
			return result;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override string ToString()
		{
			return string.Empty;
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader oXmlReader)
		{
			m_oDataType = DataType.Parse(oXmlReader.GetAttribute("type"));
			oXmlReader.Skip();
		}

		public void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteAttributeString("type", m_oDataType.ToString());
		}

		#endregion
	}
}
