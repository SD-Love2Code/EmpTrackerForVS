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
	[XmlRoot(ElementName = "date")]
	public sealed class DateType : BaseType, ISerializable
	{
		#region Private Member Variables

		private DateTime _date;

		#endregion

		#region Internal Constrctors

		internal DateType() :
			base(DataType.Date)
		{
		}

		internal DateType(IConvertible value, IFormatProvider provider) :
			base(DataType.Date)
		{
			_date = Convert.ToDateTime(value, provider).Date;
		}

        internal DateType(string value, IFormatProvider provider) :
            base(DataType.Date)
        {
            DateTimeOffset offset;
            if (!DateTimeOffset.TryParse(value, provider, DateTimeStyles.None, out offset))
                throw new FormatException(string.Format("Invalid date {0}", value));
            _date = offset.Date;
        }

        internal DateType(string value, string format, IFormatProvider provider) :
			base(DataType.Date)
		{
			if (!DateTime.TryParseExact(value, format, provider, DateTimeStyles.None, out _date))
				throw new FormatException(string.Format("Invalid date {0} for format {1}", value, format));
		}

		internal DateType(SerializationInfo info, StreamingContext context) :
			base(DataType.Date)
		{
			_date = info.GetDateTime("data");
		}

		#endregion

		#region IConvertible Members

		public override object ToType(Type type, IFormatProvider provider)
		{
			object result;
			if (type.Equals(typeof(DateTime)))
				result = _date;
			else if (type.Equals(typeof(String)))
				result = ToString(provider);
			else
				result = Convert.ChangeType(_date, type, provider);
			return result;
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("data", _date);
		}

		#endregion

		#region IType Members

		public override int CompareTo(IType type, CultureInfo culture)
		{
			int result;
			try
			{
				if (type.IsArray)
				{
					switch (type.Item.Count())
					{
						case 0 :
							result = -1;
							break;
						case 1 :
							result = CompareTo(type.Item.First(), culture);
							break;
						default :
							result = CompareTo(type.Item.First(), culture);
							if (result == 0)
								result = -1;
							break;
					}
				}
				else
				{
					result = type.IsEmpty ? 1 : _date.CompareTo(type.ToDateTime(culture));
				}
			}
			catch (InvalidCastException oInvalidCastException)
			{
				throw new ArgumentException(String.Format("Invalid type {0} for {1} comparison", DataType, type.DataType), oInvalidCastException);
			}
			return result;
		}

		public override DateTime ToDateTime(IFormatProvider provider)
		{
			return _date;
		}

        public override string ToString(IFormatProvider provider)
		{
			string			result;
			CultureInfo		culture;
			// check for the invariant culture
            if ((culture = provider as CultureInfo) != null && culture.LCID == 0x007F)
			{
				// use specific formatting for the invariant culture
                result = _date.ToString("yyyy-MM-dd", provider);
			}
			else
			{
				// use culture specific short date format
                result = _date.ToString("d", provider);
			}
			return result;
		}

		public override IConvertible ToType(TypeCode code, IFormatProvider provider)
		{
			IConvertible value;
			if (code == TypeCode.DateTime)
				value = _date;
			else if (code == TypeCode.String)
				value = ToString(provider);
			else
				value = (IConvertible) Convert.ChangeType(_date, code, provider);
			return value;
		}

		public override IType ToType(DataType type, IFormatProvider provider)
		{
			IType value;
			if (type == DataType.Date)
				value = this;
			else if (type.TypeCode == TypeCode.String)
				value = DataType.Create(type, ToString(provider), provider);
			else if (DataType.IsInteger(type))
				value = DataType.Create(type, _date.Subtract(new DateTime(1970, 1, 1)).Days, provider);
			else
				value = DataType.Create(type, _date, provider);
			return value;
		}

		public override IType ToType(DataType type, string format, IFormatProvider provider)
		{
			IType value;
			if (type == DataType.Date)
				value = this;
			else if (type == DataType.String)
				value = DataType.Create(type, _date.ToString(format, provider), provider);
			else if (type == DataType.DateTime)
            {
                // Get a timestamp for midnight of the specified timezone
                var tz = TimeZoneInfo.FindSystemTimeZoneById(format);

                var utcTime = _date.Add(-tz.GetUtcOffset(_date));

                value = DataType.Create(type, DateTime.SpecifyKind(utcTime, DateTimeKind.Utc), provider);
            }
            else
				value = DataType.Create(type, _date, provider);
			return value;
		}

		#endregion

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader reader)
		{
			_date = XmlConvert.ToDateTime(reader.ReadElementString(), XmlDateTimeSerializationMode.RoundtripKind);
		}

		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteValue(XmlConvert.ToString(_date, XmlDateTimeSerializationMode.RoundtripKind));
		}

		#endregion

		#region Object Members

		public override int GetHashCode()
		{
			return _date.GetHashCode();
		}

		#endregion
	}
}
