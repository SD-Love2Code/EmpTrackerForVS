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
	[XmlRoot(ElementName = "single")]
	public sealed class SingleType : BaseType, ISerializable
	{
		#region Private Member Variables

		private float m_oSingle;

		#endregion

		#region internal Constructors

		internal SingleType() :
			base(DataType.Single)
		{
		}

		internal SingleType(float oSingle) :
			base(DataType.Single)
		{
			m_oSingle = oSingle;
		}

		internal SingleType(IConvertible oIConvertible, IFormatProvider oIFormatProvider) :
			base(DataType.Single)
		{
			m_oSingle = Convert.ToSingle(oIConvertible, oIFormatProvider);
		}

		internal SingleType(SerializationInfo info, StreamingContext context) :
			base(DataType.Single)
		{
			m_oSingle = info.GetSingle("data");
		}

		#endregion

		#region IConvertible Members

		public override object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			return Convert.ChangeType(m_oSingle, oType, oIFormatProvider);
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", m_oSingle);
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
						case TypeCode.Double :
							result = ToDouble(oCultureInfo).CompareTo(oIType.ToDouble(oCultureInfo));
							break;
						default :
							result = oIType.IsEmpty ? 1 : m_oSingle.CompareTo(oIType.ToSingle(oCultureInfo));
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

		public override float ToSingle(IFormatProvider oIFormatProvider)
		{
			return m_oSingle;
		}

		public override IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			return (IConvertible) Convert.ChangeType(m_oSingle, oTypeCode, oIFormatProvider);
		}

		public override IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return oDataType == DataType.Single ? this : DataType.Create(oDataType, m_oSingle, oIFormatProvider);
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader oXmlReader)
		{
			m_oSingle = XmlConvert.ToSingle(oXmlReader.ReadElementString());
		}

		public override void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteValue(XmlConvert.ToString(m_oSingle));
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oSingle.GetHashCode();
		}

		#endregion
	}
}
