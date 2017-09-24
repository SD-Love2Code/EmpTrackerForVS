// Copyright Notice! 
// This document is protected under the trade secret and copyright 
// laws as the property of Fidelity National Information Services, Inc. 
// Copying, reproduction or distribution should be limited and only to
// employees with a “need to know” to do their job. 
// Any disclosure of this document to third parties is strictly prohibited.

// © 2015 Fidelity National Information Services.
// All rights reserved worldwide.

using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace EmployeeTracker.Common.Data
{
	[Serializable]
	[XmlRoot(ElementName = "int32")]
	public sealed class Int32Type : BaseType, ISerializable
	{
		#region Private Member Variables

		private int m_oInt32;

		#endregion

		#region Internal Constructors

		internal Int32Type() :
			base(DataType.Int32)
		{
		}

		internal Int32Type(int oInt32) :
			base(DataType.Int32)
		{
			m_oInt32 = oInt32;
		}

		internal Int32Type(IConvertible oIConvertible, IFormatProvider oIFormatProvider) :
			base(DataType.Int32)
		{
			m_oInt32 = Convert.ToInt32(oIConvertible, oIFormatProvider);
		}

		internal Int32Type(SerializationInfo info, StreamingContext context) :
			base(DataType.Int32)
		{
			m_oInt32 = info.GetInt32("data");
		}

		#endregion

		#region IConvertible Members

		public override object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			return Convert.ChangeType(m_oInt32, oType, oIFormatProvider);
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", m_oInt32);
		}

		#endregion

		#region IType Members

		public override int CompareTo(IType oIType, CultureInfo oCultureInfo)
		{
			int		result;
			try
			{
				if (oIType.IsArray)
				{
					switch (oIType.Item.Count())
					{
						case 0 :
							result = -1;
							break;
						case 1 :
							result = CompareTo(oIType.Item.First(), oCultureInfo);
							break;
						default :
							result = CompareTo(oIType.Item.First(), oCultureInfo);
							if (result == 0)
								result = -1;
							break;
					}
				}
				else
				{
					switch (oIType.TypeCode)
					{
						case TypeCode.Decimal :
							result = ToDecimal(oCultureInfo).CompareTo(oIType.ToDecimal(oCultureInfo));
							break;
						case TypeCode.Double :
							result = ToDouble(oCultureInfo).CompareTo(oIType.ToDouble(oCultureInfo));
							break;
						case TypeCode.Int64 :
							result = ToInt64(oCultureInfo).CompareTo(oIType.ToInt64(oCultureInfo));
							break;
						case TypeCode.Single :
							result = ToSingle(oCultureInfo).CompareTo(oIType.ToSingle(oCultureInfo));
							break;
						default:
							result = oIType.IsEmpty ? 1 : m_oInt32.CompareTo(oIType.ToInt32(CultureInfo.CurrentCulture));
							break;
					}
				}
			}
			catch (InvalidCastException oInvalidCastException)
			{
				throw new ArgumentException(String.Format("Invalid type {0} for {1} comparison", DataType, oIType.DataType), oInvalidCastException);
			}
			return result;
		}

		public override int ToInt32(IFormatProvider oIFormatProvider)
		{
			return m_oInt32;
		}

		public override IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			return (IConvertible) Convert.ChangeType(m_oInt32, oTypeCode, oIFormatProvider);
		}

		public override IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			IType oIType;
			if (oDataType == DataType.Int32)
				oIType = this;
			else if (oDataType == DataType.Date)
				oIType = DataType.Create(oDataType, new DateTime(1970, 1, 1).AddDays(m_oInt32), oIFormatProvider);
			else
				oIType = DataType.Create(oDataType, m_oInt32, oIFormatProvider);
			return oIType;
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader oXmlReader)
		{
			m_oInt32 = XmlConvert.ToInt32(oXmlReader.ReadElementString());
		}

		public override void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteValue(XmlConvert.ToString(m_oInt32));
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oInt32.GetHashCode();
		}

		#endregion
	}
}
