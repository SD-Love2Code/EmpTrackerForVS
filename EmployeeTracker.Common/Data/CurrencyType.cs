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
	[XmlRoot(ElementName = "currency")]
	public sealed class CurrencyType : BaseType, ISerializable
	{
		#region Private Member Variables

		private static readonly NumberFormatInfo m_oNumberFormatInfo;

		private decimal m_oDecimal;

		#endregion

		#region Static Constructor

		static CurrencyType()
		{
			// create an invariant culture
			m_oNumberFormatInfo = new NumberFormatInfo();
			m_oNumberFormatInfo.NumberGroupSeparator = string.Empty;
		}

		#endregion

		#region Internal Constructors

		internal CurrencyType() :
			base(DataType.Currency)
		{
		}

		internal CurrencyType(decimal oDecimal) :
			base(DataType.Currency)
		{
			m_oDecimal = oDecimal;
		}

		internal CurrencyType(IConvertible oIConvertible, IFormatProvider oIFormatProvider) :
			base(DataType.Currency)
		{
			switch (oIConvertible.GetTypeCode())
			{
				case TypeCode.String :
					m_oDecimal = Decimal.Parse(oIConvertible.ToString(oIFormatProvider), NumberStyles.Currency, oIFormatProvider);
					break;
				default :
					m_oDecimal = oIConvertible.ToDecimal(oIFormatProvider);
					break;
			}
		}

		internal CurrencyType(SerializationInfo info, StreamingContext context) :
			base(DataType.Currency)
		{
			m_oDecimal = info.GetDecimal("data");
		}

		#endregion

		#region IConvertible Members

		public override object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			return Convert.ChangeType(m_oDecimal, oType, oIFormatProvider);
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", m_oDecimal);
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
					result = oIType.IsEmpty ? 1 : m_oDecimal.CompareTo(oIType.ToDecimal(oCultureInfo));
				}
			}
			catch (InvalidCastException oInvalidCastException)
			{
				throw new ArgumentException(String.Format("Invalid type {0} for {1} comparison", DataType, oIType.DataType), oInvalidCastException);
			}
			return result;
		}

		public override decimal ToDecimal(IFormatProvider oIFormatProvider)
		{
			return m_oDecimal;
		}

		public override string ToString(IFormatProvider oIFormatProvider)
		{
			string			result;
			CultureInfo		oCultureInfo;
			// check for the invariant culture
			if ((oCultureInfo = (CultureInfo) oIFormatProvider as CultureInfo) != null && oCultureInfo.LCID == 0x007F)
			{
				// use the special invariant format
				result = m_oDecimal.ToString("N", m_oNumberFormatInfo);
			}
			else
			{
				// use culture specific currency format
				result = m_oDecimal.ToString("C", oIFormatProvider);
			}
			return result;
		}

		public override IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			return (IConvertible) Convert.ChangeType(m_oDecimal, oTypeCode, oIFormatProvider);
		}

		public override IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return oDataType == DataType.Currency ? this : DataType.Create(oDataType, m_oDecimal, oIFormatProvider);
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader oXmlReader)
		{
			m_oDecimal = XmlConvert.ToDecimal(oXmlReader.ReadElementString());
		}

		public override void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteValue(XmlConvert.ToString(m_oDecimal));
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oDecimal.GetHashCode();
		}

		#endregion
	}
}
