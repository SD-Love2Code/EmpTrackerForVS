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
	[XmlRoot(ElementName = "int64")]
	public sealed class Int64Type : BaseType, ISerializable
	{
		#region Private Member Variables

		private long m_oInt64;

		#endregion

		#region Internal Constructors

		internal Int64Type() :
			base(DataType.Int64)
		{
		}

		internal Int64Type(long oInt64) :
			base(DataType.Int64)
		{
			m_oInt64 = oInt64;
		}

		internal Int64Type(IConvertible oIConvertible, IFormatProvider oIFormatProvider) :
			base(DataType.Int64)
		{
			m_oInt64 = Convert.ToInt64(oIConvertible, oIFormatProvider);
		}

		internal Int64Type(SerializationInfo info, StreamingContext context) :
			base(DataType.Int64)
		{
			m_oInt64 = info.GetInt64("data");
		}

		#endregion

		#region IConvertible Members

		public override object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			return Convert.ChangeType(m_oInt64, oType, oIFormatProvider);
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", m_oInt64);
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
						case 0:
							result = -1;
							break;
						case 1:
							result = CompareTo(oIType.Item.First(), oCultureInfo);
							break;
						default:
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
						case TypeCode.Single :
							result = ToSingle(oCultureInfo).CompareTo(oIType.ToSingle(oCultureInfo));
							break;
						default :
							result = oIType.IsEmpty ? 1 : m_oInt64.CompareTo(oIType.ToInt64(oCultureInfo));
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

		public override long ToInt64(IFormatProvider oIFormatProvider)
		{
			return m_oInt64;
		}

		public override IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			return (IConvertible) Convert.ChangeType(m_oInt64, oTypeCode, oIFormatProvider);
		}

		public override IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			IType oIType;
			if (oDataType == DataType.Int64)
				oIType = this;
			else if (oDataType == DataType.Date)
				oIType = DataType.Create(oDataType, new DateTime(1970, 1, 1).AddDays(m_oInt64), oIFormatProvider);
			else
				oIType = DataType.Create(oDataType, m_oInt64, oIFormatProvider);
			return oIType;
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader oXmlReader)
		{
			m_oInt64 = XmlConvert.ToInt64(oXmlReader.ReadElementString());
		}

		public override void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteValue(XmlConvert.ToString(m_oInt64));
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oInt64.GetHashCode();
		}

		#endregion
	}
}
