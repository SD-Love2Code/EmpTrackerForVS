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
	[XmlRoot(ElementName = "double")]
	public sealed class DoubleType : BaseType, ISerializable
	{
		#region Private Member Variables

		private double m_oDouble;

		#endregion

		#region Internal Constructors

		internal DoubleType() :
			base(DataType.Double)
		{
		}

		internal DoubleType(double oDouble) :
			base(DataType.Double)
		{
			m_oDouble = oDouble;
		}

		internal DoubleType(IConvertible oIConvertible, IFormatProvider oIFormatProvider) :
			base(DataType.Double)
		{
			m_oDouble = Convert.ToDouble(oIConvertible, oIFormatProvider);
		}

		internal DoubleType(SerializationInfo info, StreamingContext context) :
			base(DataType.Double)
		{
			m_oDouble = info.GetDouble("data");
		}

		#endregion

		#region IConvertible Members

		public override object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			return Convert.ChangeType(m_oDouble, oType, oIFormatProvider);
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", m_oDouble);
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
					result = oIType.IsEmpty ? 1 : m_oDouble.CompareTo(oIType.ToDouble(oCultureInfo));
				}
			}
			catch (InvalidCastException oInvalidCastException)
			{
				throw new ArgumentException(String.Format("Invalid type {0} for {1} comparison", DataType, oIType.DataType), oInvalidCastException);
			}
			return result;
		}

		public override double ToDouble(IFormatProvider oIFormatProvider)
		{
			return m_oDouble;
		}

		public override IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			return (IConvertible) Convert.ChangeType(m_oDouble, oTypeCode, oIFormatProvider);
		}

		public override IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return oDataType == DataType.Double ? this : DataType.Create(oDataType, m_oDouble, oIFormatProvider);
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader oXmlReader)
		{
			m_oDouble = XmlConvert.ToDouble(oXmlReader.ReadElementString());
		}

		public override void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteValue(XmlConvert.ToString(m_oDouble));
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oDouble.GetHashCode();
		}

		#endregion
	}
}
