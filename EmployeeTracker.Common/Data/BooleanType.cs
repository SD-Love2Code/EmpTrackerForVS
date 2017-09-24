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
	[XmlRoot(ElementName = "boolean")]
	public sealed class BooleanType : BaseType, ISerializable
	{
		#region Private Member Variables

		private bool m_oBoolean;

		#endregion

		#region Internal Constructors

		internal BooleanType() :
			base(DataType.Boolean)
		{
		}

		internal BooleanType(bool oBoolean) :
			base(DataType.Boolean)
		{
			m_oBoolean = oBoolean;
		}

		internal BooleanType(IConvertible oIConvertible, IFormatProvider oIFormatProvider) :
			base(DataType.Boolean)
		{
			try
			{
				m_oBoolean = Convert.ToBoolean(oIConvertible, oIFormatProvider);
			}
			catch (FormatException)
			{
				if ("0".Equals(oIConvertible))
					m_oBoolean = false;
				else if ("1".Equals(oIConvertible))
					m_oBoolean = true;
				else
					throw;
			}
		}

		internal BooleanType(SerializationInfo info, StreamingContext context) :
			base(DataType.Boolean)
		{
			m_oBoolean = info.GetBoolean("data");
		}

		#endregion

		#region IConvertible Members

		public override object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			return Convert.ChangeType(m_oBoolean, oType, oIFormatProvider);
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", m_oBoolean);
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
					result = m_oBoolean.CompareTo(oIType.ToBoolean(oCultureInfo));
				}
			}
			catch (InvalidCastException oInvalidCastException)
			{
				throw new ArgumentException(String.Format("Invalid type {0} for {1} comparison", DataType, oIType.DataType), oInvalidCastException);
			}
			return result;
		}

		public override bool ToBoolean(IFormatProvider oIFormatProvider)
		{
			return m_oBoolean;
		}

		public override IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			return (IConvertible) Convert.ChangeType(m_oBoolean, oTypeCode, oIFormatProvider);
		}

		public override IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return oDataType == DataType.Boolean ? this : DataType.Create(oDataType, m_oBoolean, oIFormatProvider);
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader oXmlReader)
		{
			m_oBoolean = XmlConvert.ToBoolean(oXmlReader.ReadElementString());
		}

		public override void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteValue(XmlConvert.ToString(m_oBoolean));
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oBoolean.GetHashCode();
		}

		#endregion
	}
}
