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
	[XmlRoot(ElementName = "text")]
	public sealed class TextType : BaseType, ISerializable
	{
		#region Private Member Variables

		private string m_oString;

		#endregion

		#region Internal Constructors

		internal TextType() :
			base(DataType.Text)
		{
		}

		internal TextType(string oString) :
			base(DataType.Text)
		{
			m_oString = oString;
		}

		internal TextType(IConvertible oIConvertible, IFormatProvider oIFormatProvider) :
			base(DataType.Text)
		{
			m_oString = oIConvertible.ToString(oIFormatProvider);
		}

		internal TextType(SerializationInfo info, StreamingContext context) :
			base(DataType.Text)
		{
			m_oString = info.GetString("data");
		}

		#endregion

		#region IComparable<IDataType> Members

		#endregion

		#region IConvertible Members

		public override object ToType(Type oType, IFormatProvider oIFormatProvider)
		{
			return Convert.ChangeType(m_oString, oType, oIFormatProvider);
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", m_oString);
		}

		#endregion

		#region IType Members

		public override bool IsEmpty
		{
			get
			{
				return m_oString.Length == 0;
			}
		}

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
						case TypeCode.Int16 :
							result = ToInt16(oCultureInfo).CompareTo(oIType.ToInt16(oCultureInfo));
							break;
						case TypeCode.Int32 :
							result = ToInt32(oCultureInfo).CompareTo(oIType.ToInt32(oCultureInfo));
							break;
						case TypeCode.Int64 :
							result = ToInt64(oCultureInfo).CompareTo(oIType.ToInt64(oCultureInfo));
							break;
						case TypeCode.Single :
							result = ToSingle(oCultureInfo).CompareTo(oIType.ToSingle(oCultureInfo));
							break;
						default :
							result = oCultureInfo.CompareInfo.Compare(m_oString, oIType.ToString(oCultureInfo), CompareOptions.OrdinalIgnoreCase);
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

		public override string ToString(IFormatProvider oIFormatProvider)
		{
			return m_oString;
		}

		public override IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider)
		{
			return (IConvertible) Convert.ChangeType(m_oString, oTypeCode, oIFormatProvider);
		}

		public override IType ToType(DataType oDataType, IFormatProvider oIFormatProvider)
		{
			return oDataType == DataType.Text ? this : DataType.Create(oDataType, m_oString, oIFormatProvider);
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader oXmlReader)
		{
			m_oString = oXmlReader.ReadElementString();
		}

		public override void WriteXml(XmlWriter oXmlWriter)
		{
			oXmlWriter.WriteValue(m_oString);
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return m_oString.GetHashCode();
		}

		#endregion
	}
}
