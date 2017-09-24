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

namespace EmployeeTracker.Common.Data
{
	public static class Temporal
	{
		#region Interval Enumeration

		public enum Interval
		{
			DAY,
			HOUR,
            MILLISECOND,
			MINUTE,
			MONTH,
			SECOND,
			WEEK,
			YEAR,
		}

		#endregion

		#region Temporal Members

		public static IType Add(IType oIType1, IType oIType2, Interval oInterval)
		{
			IType oIType;
			if (oIType1.IsEmpty || oIType2.IsEmpty)
			{
				oIType = oIType1;
			}
			else
			{
				var iValue = oIType2.ToInt32(CultureInfo.CurrentCulture);
				// determine the output data type
				var oDataType = oIType1.DataType == DataType.Date || oIType1.DataType == DataType.DateTime ? oIType1.DataType : DataType.DateTime;
				switch (oInterval)
				{
					case Interval.DAY:
						oIType = DataType.Create(oDataType, oIType1.ToDateTime(CultureInfo.CurrentCulture).AddDays(iValue), CultureInfo.InvariantCulture);
						break;
					case Interval.HOUR:
						oIType = DataType.Create(oDataType, oIType1.ToDateTime(CultureInfo.CurrentCulture).AddHours(iValue), CultureInfo.InvariantCulture);
						break;
                    case Interval.MILLISECOND:
                        oIType = DataType.Create(oDataType, oIType1.ToDateTime(CultureInfo.CurrentCulture).AddMilliseconds(iValue), CultureInfo.InvariantCulture);
                        break;
                    case Interval.MINUTE:
						oIType = DataType.Create(oDataType, oIType1.ToDateTime(CultureInfo.CurrentCulture).AddMinutes(iValue), CultureInfo.InvariantCulture);
						break;
					case Interval.MONTH:
						oIType = DataType.Create(oDataType, oIType1.ToDateTime(CultureInfo.CurrentCulture).AddMonths(iValue), CultureInfo.InvariantCulture);
						break;
					case Interval.SECOND:
						oIType = DataType.Create(oDataType, oIType1.ToDateTime(CultureInfo.CurrentCulture).AddSeconds(iValue), CultureInfo.InvariantCulture);
						break;
					case Interval.WEEK:
						oIType = DataType.Create(oDataType, oIType1.ToDateTime(CultureInfo.CurrentCulture).AddDays(iValue * 7), CultureInfo.InvariantCulture);
						break;
					case Interval.YEAR:
						oIType = DataType.Create(oDataType, oIType1.ToDateTime(CultureInfo.CurrentCulture).AddYears(iValue), CultureInfo.InvariantCulture);
						break;
					default:
						throw new InvalidCastException(string.Format("Invalid interval type {0}", oInterval.ToString()));
				}
			}
			return oIType;
		}

		public static IType Add(IType oIType1, IType oIType2, IType oIType3)
		{
			Interval oInterval;
			if (Enum.TryParse<Interval>(oIType3.ToString(CultureInfo.InvariantCulture), true, out oInterval))
				return Add(oIType1, oIType2, oInterval);
			else
				throw new InvalidCastException(string.Format("Invalid interval type {0}", oIType3.ToString(CultureInfo.InvariantCulture)));
		}

		/// <summary>
		/// Get the current date.
		/// </summary>
		/// <returns>
		/// current date
		/// </returns>
		public static IType CurrentDate()
		{
			return DataType.Create(DataType.Date, DateTime.Today, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Get the day of month (1 - 31) for a date or timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a date
		/// </param>
		/// <returns>
		/// day of month
		/// </returns>
		public static IType DayOfMonth(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime :
					oIType = DataType.Create(DataType.Int32, oIType.ToDateTime(CultureInfo.InvariantCulture).Day, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull :
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for day of month", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Get the day of week (1 - 7) for a date or timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a date
		/// </param>
		/// <returns>
		/// day of week
		/// </returns>
		public static IType DayOfWeek(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime :
					oIType = DataType.Create(DataType.Int32, oIType.ToDateTime(CultureInfo.InvariantCulture).DayOfWeek + 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull :
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for day of week", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Get the day of year (1 - 366) for a date or timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a date
		/// </param>
		/// <returns>
		/// day of year
		/// </returns>
		public static IType DayOfYear(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime :
					oIType = DataType.Create(DataType.Int32, oIType.ToDateTime(CultureInfo.InvariantCulture).DayOfYear, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull :
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for day of year", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Get the hour of day (0 - 23) for a timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a timestamp
		/// </param>
		/// <returns>
		/// hour of day
		/// </returns>
		public static IType Hour(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime :
					oIType = DataType.Create(DataType.Int32, oIType.ToDateTime(CultureInfo.InvariantCulture).Hour, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull :
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for hour", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Get the minute of the hour (0 - 59) for a timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a timestamp
		/// </param>
		/// <returns>
		/// minute of hour
		/// </returns>
		public static IType Minute(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime:
					oIType = DataType.Create(DataType.Int32, oIType.ToDateTime(CultureInfo.InvariantCulture).Minute, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull:
					break;
				default:
					throw new InvalidCastException(String.Format("Invalid type {0} for minute", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Get the month of year (1 - 12) for a date or timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a date
		/// </param>
		/// <returns>
		/// month of year
		/// </returns>
		public static IType Month(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime :
					oIType = DataType.Create(DataType.Int32, oIType.ToDateTime(CultureInfo.InvariantCulture).Month, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull :
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for month", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Get the current date and time.
		/// </summary>
		/// <returns>
		/// current date and time
		/// </returns>
		public static IType Now()
		{
			return DataType.Create(DataType.DateTime, DateTime.Now, CultureInfo.InvariantCulture);
		}

        /// <summary>
        /// Get the end of month day date and time.
        /// </summary>
        /// <returns>
        ///  end of month day date and time
        /// </returns>
        public static IType EOMonth()
        {
            TimeZoneInfo.ClearCachedData();
            TimeZoneInfo tz = TimeZoneInfo.Local;
            tz = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            DateTime endOfMonth = new DateTime(today.Year, today.Month, 1).AddMonths(2).AddDays(-1);
            var bankNow = TimeZoneInfo.ConvertTimeFromUtc(endOfMonth, tz);
            var processingDate = bankNow.Date;
            var timeToRun = TimeZoneInfo.ConvertTimeToUtc(processingDate.AddDays(1), tz);
            return DataType.Create(DataType.DateTime, timeToRun, CultureInfo.InvariantCulture);
        }

		/// <summary>
		/// Get the quarter of year (1 - 4) for a date or timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a date
		/// </param>
		/// <returns>
		/// quarter of year
		/// </returns>
		public static IType Quarter(IType oIType)
		{
			int iRemainder;
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime :
					oIType = DataType.Create(DataType.Int32, Math.DivRem(oIType.ToDateTime(CultureInfo.InvariantCulture).Month - 1, 3, out iRemainder) + 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull :
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for quarter", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Get the second of the minute (0 - 59) for a timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a timestamp
		/// </param>
		/// <returns>
		/// second of minute
		/// </returns>
		public static IType Second(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime:
					oIType = DataType.Create(DataType.Int32, oIType.ToDateTime(CultureInfo.InvariantCulture).Second, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull:
					break;
				default:
					throw new InvalidCastException(String.Format("Invalid type {0} for second", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Subtract two values using an interval.
		/// </summary>
		/// <param name="oIType1">
		/// first value
		/// </param>
		/// <param name="oIType2">
		/// second value
		/// </param>
		/// <param name="oInterval">
		/// type of interval
		/// </param>
		/// <returns>
		/// difference in interval units
		/// </returns>
		public static IType Subtract(IType oIType1, IType oIType2, Interval oInterval)
		{
			IType oIType;
			if (oIType1.IsEmpty || oIType2.IsEmpty)
			{
				oIType = oIType1;
			}
			else
			{
				var oDateTime1 = oIType1.ToDateTime(CultureInfo.CurrentCulture);
				var oDateTime2 = oIType2.ToDateTime(CultureInfo.CurrentCulture);
				switch (oInterval)
				{
					case Interval.DAY :
						oIType = DataType.Create(DataType.Int32, oDateTime1.Subtract(oDateTime2).Days, CultureInfo.InvariantCulture);
						break;
					case Interval.HOUR :
						oIType = DataType.Create(DataType.Int32, Math.Floor(oDateTime1.Subtract(oDateTime2).TotalHours), CultureInfo.InvariantCulture);
						break;
					case Interval.MILLISECOND :
						oIType = DataType.Create(DataType.Int64, Math.Floor(oDateTime1.Subtract(oDateTime2).TotalMilliseconds), CultureInfo.InvariantCulture);
						break;
					case Interval.MINUTE :
						oIType = DataType.Create(DataType.Int32, Math.Floor(oDateTime1.Subtract(oDateTime2).TotalMinutes), CultureInfo.InvariantCulture);
						break;
					case Interval.MONTH :
						oIType = DataType.Create(DataType.Int32, (oDateTime1.Year - oDateTime2.Year) * 12 + oDateTime1.Month - oDateTime2.Month, CultureInfo.InvariantCulture);
						break;
					case Interval.SECOND :
						oIType = DataType.Create(DataType.Int32, Math.Floor(oDateTime1.Subtract(oDateTime2).TotalSeconds), CultureInfo.InvariantCulture);
						break;
					case Interval.WEEK :
						int iRemainder;
						oIType = DataType.Create(DataType.Int32, Math.DivRem(oDateTime1.Subtract(oDateTime2).Days - 1, 7, out iRemainder), CultureInfo.InvariantCulture);
						break;
					case Interval.YEAR :
						oIType = DataType.Create(DataType.Int32, oDateTime1.Year - oDateTime2.Year, CultureInfo.InvariantCulture);
						break;
					default :
						throw new InvalidCastException(string.Format("Invalid interval type {0}", oInterval.ToString()));
				}
			}
			return oIType;
		}

		public static IType Subtract(IType oIType1, IType oIType2, IType oIType3)
		{
			Interval oInterval;
			if (Enum.TryParse<Interval>(oIType3.ToString(CultureInfo.InvariantCulture), true, out oInterval))
				return Subtract(oIType1, oIType2, oInterval);
			else
				throw new InvalidCastException(string.Format("Invalid interval type {0}", oIType3.ToString(CultureInfo.InvariantCulture)));
		}

		/// <summary>
		/// Get the week of year (1 - 53) for a date or timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a date
		/// </param>
		/// <returns>
		/// week of year
		/// </returns>
		public static IType Week(IType oIType)
		{
			int iRemainder;
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime :
					oIType = DataType.Create(DataType.Int32, Math.DivRem(oIType.ToDateTime(CultureInfo.InvariantCulture).DayOfYear - 1, 7, out iRemainder) + 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull :
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for week", oIType.DataType));
			}
			return oIType;
		}

		/// <summary>
		/// Get the year for a date or timestamp.
		/// </summary>
		/// <param name="oIType">
		/// value that is a date
		/// </param>
		/// <returns>
		/// year
		/// </returns>
		public static IType Year(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DateTime :
					oIType = DataType.Create(DataType.Int32, oIType.ToDateTime(CultureInfo.InvariantCulture).Year, CultureInfo.InvariantCulture);
					break;
				case TypeCode.DBNull :
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for year", oIType.DataType));
			}
			return oIType;
		}

		#endregion
	}
}
