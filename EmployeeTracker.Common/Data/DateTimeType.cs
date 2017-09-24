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
	[XmlRoot(ElementName = "datetime")]
	public sealed class DateTimeType : BaseType, ISerializable
	{
		#region Private Member Variables

		private DateTime m_oDateTime;

		#endregion

		#region Internal Constructors

		internal DateTimeType() :
			base(DataType.DateTime)
		{
		}

		internal DateTimeType(IConvertible oIConvertible, IFormatProvider oIFormatProvider) :
			base(DataType.DateTime)
		{
			m_oDateTime = Convert.ToDateTime(oIConvertible, oIFormatProvider);
		}

		internal DateTimeType(string sValue, string sFormat, IFormatProvider oIFormatProvider) :
			base(DataType.DateTime)
		{
			m_oDateTime = DateTime.ParseExact(sValue, sFormat, oIFormatProvider);
		}

		internal DateTimeType(SerializationInfo info, StreamingContext context) :
			base(DataType.DateTime)
		{
			m_oDateTime = info.GetDateTime("data");
		}

		#endregion

		#region IConvertible Members

		public override object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			object result;
			if (oType.Equals(typeof(DateTime)))
				result = m_oDateTime;
			else if (oType.Equals(typeof(String)))
				result = ToString(oIFormatProvider);
			else
				result = Convert.ChangeType(m_oDateTime, oType, oIFormatProvider);
			return result;
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", m_oDateTime);
		}

		#endregion

		#region IType Members

		public override int CompareTo(IType oIType, CultureInfo oCultureInfo)
		{
			int result;
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
					result = oIType.IsEmpty ? 1 : m_oDateTime.CompareTo(oIType.ToDateTime(oCultureInfo));
				}
			}
			catch (InvalidCastException oInvalidCastException)
			{
				throw new ArgumentException(String.Format("Invalid type {0} for {1} comparison", DataType, oIType.DataType), oInvalidCastException);
			}
			return result;
		}

		public override DateTime ToDateTime(IFormatProvider oIFormatProvider)
		{
			return m_oDateTime;
		}

		public override string ToString(IFormatProvider oIFormatProvider)
		{
			string			result;
			CultureInfo		oCultureInfo;
			// check for the invariant culture
			if ((oCultureInfo = oIFormatProvider as CultureInfo) != null && oCultureInfo.LCID == 0x007F)
			{
				// use specific formatting for the invariant culture
                result = m_oDateTime.ToString("o", oIFormatProvider);
			}
			else
			{
				// use culture specific default format
				result = m_oDateTime.ToString(oIFormatProvider);
			}
			return result;
		}

		public override IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			IConvertible oIConvertible;
			if (oTypeCode == TypeCode.DateTime)
				oIConvertible = m_oDateTime;
			else if (oTypeCode == TypeCode.String)
				oIConvertible = ToString(oIFormatProvider);
			else
				oIConvertible = (IConvertible) Convert.ChangeType(m_oDateTime, oTypeCode, oIFormatProvider);
			return oIConvertible;
		}

		public override IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			IType oIType;
			if (oDataType == DataType.DateTime)
				oIType = this;
			else if (oDataType.TypeCode == TypeCode.String)
				oIType = DataType.Create(oDataType, ToString(oIFormatProvider), oIFormatProvider);
			else if (DataType.IsInteger(oDataType))
				oIType = DataType.Create(oDataType, m_oDateTime.Subtract(new DateTime(1970, 1, 1)).Days, oIFormatProvider);
			else
				oIType = DataType.Create(oDataType, m_oDateTime, oIFormatProvider);
			return oIType;
		}

		public override IType ToType(DataType oDataType, string sFormat, IFormatProvider oIFormatProvider)
		{
			IType oIType;
            if (oDataType == DataType.DateTime)
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(sFormat);

                if (m_oDateTime.Kind == DateTimeKind.Utc && tz == TimeZoneInfo.Utc)
                {
                    oIType = this;
                }
                else
                {
                    DateTime value;

                    if (m_oDateTime.Kind == DateTimeKind.Utc)
                        value = TimeZoneInfo.ConvertTimeFromUtc(m_oDateTime, tz);
                    else
                        value = TimeZoneInfo.ConvertTime(m_oDateTime, TimeZoneInfo.Local, tz);

                    oIType = DataType.Create(oDataType, value, oIFormatProvider);
                }
            }
            else if (oDataType == DataType.String)
                oIType = DataType.Create(oDataType, m_oDateTime.ToString(sFormat, oIFormatProvider), oIFormatProvider);
            else
                oIType = DataType.Create(oDataType, m_oDateTime, oIFormatProvider);

			return oIType;
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader oXmlReader)
		{
			m_oDateTime = XmlConvert.ToDateTime(oXmlReader.ReadElementString(), XmlDateTimeSerializationMode.RoundtripKind);
		}

		public override void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteValue(XmlConvert.ToString(m_oDateTime, XmlDateTimeSerializationMode.RoundtripKind));
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oDateTime.GetHashCode();
		}

		#endregion
	}
}
