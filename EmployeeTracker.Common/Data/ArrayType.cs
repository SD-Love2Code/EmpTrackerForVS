// Copyright Notice! 
// This document is protected under the trade secret and copyright 
// laws as the property of Fidelity National Information Services, Inc. 
// Copying, reproduction or distribution should be limited and only to
// employees with a “need to know” to do their job. 
// Any disclosure of this document to third parties is strictly prohibited.

// © 2015 Fidelity National Information Services.
// All rights reserved worldwide.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace EmployeeTracker.Common.Data
{
	public class ArrayType : IType
	{
		#region Private Member Variables

		//private static readonly ISerializer<IType> m_oISerializer = Serializer<IType>.Create();

		private DataType m_oDataType;

		private readonly List<IType> m_aIType;

		#endregion

		#region Internal Constructors

		internal ArrayType()
		{
			m_aIType = new List<IType>();
		}

		internal ArrayType(DataType oDataType, IEnumerable<IType> oIEnumerable)
		{
			m_oDataType = oDataType;
			m_aIType = oIEnumerable.ToList();
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
			if (m_aIType.Count == 1)
				return m_aIType[0].ToBoolean(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public byte ToByte(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToByte(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public char ToChar(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToChar(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public DateTime ToDateTime(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToDateTime(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public decimal ToDecimal(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToDecimal(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public double ToDouble(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToDouble(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public short ToInt16(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToInt16(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public int ToInt32(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToInt32(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public long ToInt64(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToInt64(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public float ToSingle(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToSingle(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public sbyte ToSByte(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToSByte(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public string ToString(IFormatProvider oIFormatProvider)
		{
			StringBuilder	oStringBuilder = new StringBuilder();
			for (int i = 0; i < m_aIType.Count; i++)
			{
				if (i > 0)
					oStringBuilder.Append(',');
				oStringBuilder.Append(m_aIType[i].ToString(oIFormatProvider));
			}
			return oStringBuilder.ToString();
		}

		public object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			object result;
			if (oType.Equals(typeof(String)))
				result = ToString(oIFormatProvider);
			else if (m_aIType.Count == 1)
				result = m_aIType[0].ToType(oType, oIFormatProvider);
			else
				throw new InvalidCastException(String.Format("Cannot convert an array to {0}", oType.Name));
			return result;
		}

		public ushort ToUInt16(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToUInt16(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public uint ToUInt32(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToUInt32(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		public ulong ToUInt64(IFormatProvider oIFormatProvider)
		{
			if (m_aIType.Count == 1)
				return m_aIType[0].ToUInt64(oIFormatProvider);
			else
				throw new FormatException("Cannot convert a multi-value value");
		}

		#endregion

		#region IEquatable<IType> Members

		public bool Equals(IType oIType)
		{
			return CompareTo(oIType) == 0;
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
				return true;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Child values for an array value.
		/// </summary>
		public IEnumerable<IType> Item
		{
			get
			{
				return m_aIType;
			}
		}

		public TypeCode TypeCode
		{
			get
			{
				return TypeCode.String;
			}
		}

		public int CompareTo(IType oIType, CultureInfo oCultureInfo)
		{
			int		result;
			if (oIType.IsArray)
			{
				result = 0;
				for (int i = 0; result == 0 && i < Math.Min(m_aIType.Count, oIType.Item.Count()); ++i)
					result = m_aIType[i].CompareTo(oIType.Item.ElementAt(i));
				if (result == 0)
					result = m_aIType.Count - oIType.Item.Count();
			}
			else
			{
				switch (m_aIType.Count)
				{
					case 0 :
						result = DataType.Empty(m_oDataType).CompareTo(oIType);
						break;
					case 1 :
						result = m_aIType[0].CompareTo(oIType);
						break;
					default :
						result = m_aIType[0].CompareTo(oIType);
						if (result == 0)
							result = 1;
						break;
				}
			}
			return result;
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
				case TypeCode.String :
					oIConvertible = ToString(oIFormatProvider);
					break;
				default :
					if (m_aIType.Count == 1)
						oIConvertible = m_aIType[0].ToType(oTypeCode, oIFormatProvider);
					else
						throw new InvalidCastException(String.Format("Cannot convert an array to {0}", oTypeCode));
					break;
			}
			return oIConvertible;
		}

		public IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return m_oDataType == oDataType ? this : new ArrayType(oDataType, m_aIType.Select(oIType => oIType.ToType(oDataType, oIFormatProvider)));
		}

		public IType ToType(DataType oDataType, string sFormat, IFormatProvider oIFormatProvider)
		{
			return m_oDataType == oDataType ? this : new ArrayType(oDataType, m_aIType.Select(oIType => oIType.ToType(oDataType, sFormat, oIFormatProvider)));
		}

		public bool TryToType(DataType oDataType, IFormatProvider oIFormatProvider, out IType oIType)
		{
			bool result;
			try
			{
				oIType = oDataType == DataType.None ? this : ToType(oDataType, oIFormatProvider);
				result = true;
			}
			catch (ArithmeticException)
			{
				oIType = null;
				result = false;
			}
			catch (FormatException)
			{
				oIType = null;
				result = false;
			}
			catch (InvalidCastException)
			{
				oIType = null;
				result = false;
			}
			return result;
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		//public void ReadXml(XmlReader oXmlReader)
		//{
		//	m_oDataType = DataType.Parse(oXmlReader.GetAttribute("type"));
		//	if (oXmlReader.IsEmptyElement)
		//	{
		//		oXmlReader.Skip();
		//	}
		//	else
		//	{
		//		oXmlReader.ReadStartElement();
		//		while (oXmlReader.IsStartElement())
		//			m_aIType.Add(m_oISerializer.Deserialize(oXmlReader));
		//		oXmlReader.ReadEndElement();
		//	}
		//}

		//public void WriteXml(XmlWriter oXmlWriter)
		//{
		//	oXmlWriter.WriteAttributeString("type", m_oDataType.ToString());
		//	foreach (var oIType in m_aIType)
		//		m_oISerializer.Serialize(oXmlWriter, oIType);
		//}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return ToString(CultureInfo.CurrentCulture).GetHashCode();
		}

		public override string ToString()
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		#endregion
	}
}
