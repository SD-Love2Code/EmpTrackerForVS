// Copyright Notice! 
// This document is protected under the trade secret and copyright 
// laws as the property of Fidelity National Information Services, Inc. 
// Copying, reproduction or distribution should be limited and only to
// employees with a “need to know” to do their job. 
// Any disclosure of this document to third parties is strictly prohibited.

// © 2015 Fidelity National Information Services.
// All rights reserved worldwide.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EmployeeTracker.Common.Data
{
	public static class Text
	{
		#region Text Members

        public static IType Char(IType oIType)
        {
            return DataType.Create(char.ToString(Convert.ToChar(oIType.ToInt16(CultureInfo.InvariantCulture))));
        }

		public static IType Concat(IType oIType1, IType oIType2)
		{
			return oIType1.IsEmpty ? oIType2 : (oIType2.IsEmpty ? oIType1 : DataType.Create(oIType1.DataType.TypeCode == TypeCode.String ? oIType1.DataType : DataType.String, string.Concat(oIType1.ToString(CultureInfo.InvariantCulture), oIType2.ToString(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture));
		}

		public static IType Concat(IEnumerable<IType> oIEnumerable)
		{
			return oIEnumerable.Aggregate(DataType.Empty(DataType.String), (s, v) => Concat(s, v));
		}

		public static IType Concat(params IType[] aoIType)
		{
			return aoIType.Aggregate(DataType.Empty(DataType.String), (s, v) => Concat(s, v));
		}

		public static IType Concatenate(IEnumerable<IType> oIEnumerable, string sSeparator)
		{
			return oIEnumerable.Aggregate(new StringBuilder(), (s, v) => v.IsEmpty ? s : s.Append(v.ToString(CultureInfo.InvariantCulture)).Append(sSeparator), s => s.Length == 0 ? DataType.Empty(DataType.String) : DataType.Create(s.ToString(0, s.Length - sSeparator.Length)));
		}

		public static bool EndsWith(IType oIType1, IType oIType2)
		{
			bool result;
			if (oIType1.IsEmpty)
				result = oIType2.IsEmpty;
			else if (oIType2.IsEmpty)
				result = true;
			else
				result = oIType1.ToString(CultureInfo.InvariantCulture).EndsWith(oIType2.ToString(CultureInfo.InvariantCulture));
			return result;
		}

		public static bool Includes(IType oIType1, IType oIType2)
		{
			bool result;
			if (oIType1.IsEmpty)
				result = oIType2.IsEmpty;
			else if (oIType2.IsEmpty)
				result = true;
			else
				result = oIType1.ToString(CultureInfo.InvariantCulture).IndexOf(oIType2.ToString(CultureInfo.InvariantCulture)) >= 0;
			return result;
		}

		public static IType Insert(IType oIType1, IType oIType2, IType oIType3, IType oIType4)
		{
			// get the original value
			var sValue1 = oIType1.IsEmpty ? string.Empty : oIType1.ToString(CultureInfo.InvariantCulture);
			// get the offset
			var iOffset = oIType2.ToInt32(CultureInfo.InvariantCulture);
			// get the length
			var iLength = oIType3.ToInt32(CultureInfo.InvariantCulture);
			// get the insert value
			var sValue2 = oIType4.IsEmpty ? string.Empty : oIType4.ToString(CultureInfo.InvariantCulture);
			// remove the substring if needed
			if (iLength > 0 && iOffset <= sValue1.Length)
			{
				if (iOffset + iLength <= sValue1.Length)
					sValue1 = sValue1.Remove(iOffset - 1, iLength);
				else
					sValue1 = sValue1.Remove(iOffset - 1);
			}
			// insert the string at the specified offset
			sValue1 = sValue1.Insert(iOffset - 1, sValue2);
			return sValue1.Length == 0 ? DataType.Empty(oIType1.DataType) : DataType.Create(oIType1.DataType.TypeCode == TypeCode.String ? oIType1.DataType : DataType.String, sValue1, CultureInfo.InvariantCulture);
		}

		public static IType Left(IType oIType1, IType oIType2)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType1;
			else
			{
				// get the string value
				var sValue = oIType1.ToString(CultureInfo.InvariantCulture);
				// get the character count
				var iCount = oIType2.ToInt32(CultureInfo.InvariantCulture);
				// get the portion of the string
				if (iCount <= 0)
					oIType = DataType.Empty(oIType1.DataType);
				else if (iCount < sValue.Length)
					oIType = DataType.Create(oIType1.DataType.TypeCode == TypeCode.String ? oIType1.DataType : DataType.String, sValue.Substring(0, iCount), CultureInfo.InvariantCulture);
				else
					oIType = oIType1;
			}
			return oIType;
		}

		public static IType LeftTrim(IType oIType)
		{
			return oIType.IsEmpty ? oIType : DataType.Create(oIType.DataType, oIType.ToString(CultureInfo.InvariantCulture).TrimStart(' '), CultureInfo.InvariantCulture);
		}

		public static IType Length(IType oIType)
		{
			return oIType.IsEmpty ? DataType.Create(DataType.Int32, 0, CultureInfo.InvariantCulture) : DataType.Create(DataType.Int32, oIType.ToString(CultureInfo.InvariantCulture).Length, CultureInfo.InvariantCulture);
		}

		public static IType Locate(IType oIType1, IType oIType2)
		{
			IType oIType;
			if (oIType1.IsEmpty || oIType2.IsEmpty)
				oIType = DataType.Create(DataType.Int32, 0, CultureInfo.InvariantCulture);
			else
				oIType = DataType.Create(DataType.Int32, oIType2.ToString(CultureInfo.InvariantCulture).IndexOf(oIType1.ToString(CultureInfo.InvariantCulture)) + 1, CultureInfo.InvariantCulture);
			return oIType;
		}

		public static IType Locate(IType oIType1, IType oIType2, IType oIType3)
		{
			IType oIType;
			if (oIType1.IsEmpty || oIType2.IsEmpty)
				oIType = DataType.Create(DataType.Int32, 0, CultureInfo.InvariantCulture);
			else
				oIType = DataType.Create(DataType.Int32, oIType2.ToString(CultureInfo.InvariantCulture).IndexOf(oIType1.ToString(CultureInfo.InvariantCulture), oIType3.ToInt32(CultureInfo.InvariantCulture) - 1) + 1, CultureInfo.InvariantCulture);
			return oIType;
		}

		public static IType LowerCase(IType oIType)
		{
			return oIType.IsEmpty ? oIType : DataType.Create(oIType.DataType, oIType.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
		}

		public static IType Repeat(IType oIType1, IType oIType2)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType1;
			else
				oIType = Concat(Enumerable.Repeat<IType>(oIType1, oIType2.ToInt32(CultureInfo.InvariantCulture)));
			return oIType;
		}

		public static IType Replace(IType oIType1, IType oIType2, IType oIType3)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType1;
			else if (oIType2.IsEmpty)
				oIType = oIType1;
			else
			{
				var sValue1 = oIType1.ToString(CultureInfo.InvariantCulture);
				var sValue2 = oIType2.ToString(CultureInfo.InvariantCulture);
				oIType = DataType.Create(oIType1.DataType.TypeCode == TypeCode.String ? oIType1.DataType : DataType.String, sValue1.Replace(sValue2, oIType3.IsEmpty ? string.Empty : oIType3.ToString(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
			}
			return oIType;
		}

		public static IType Right(IType oIType1, IType oIType2)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType1;
			else
			{
				// get the string value
				var sValue = oIType1.ToString(CultureInfo.InvariantCulture);
				// get the character count
				var iCount = oIType2.ToInt32(CultureInfo.InvariantCulture);
				// get the portion of the string
				if (iCount <= 0)
					oIType = DataType.Empty(oIType1.DataType);
				else if (iCount < sValue.Length)
					oIType = DataType.Create(oIType1.DataType.TypeCode == TypeCode.String ? oIType1.DataType : DataType.String, sValue.Substring(sValue.Length - iCount, iCount), CultureInfo.InvariantCulture);
				else
					oIType = oIType1;
			}
			return oIType;
		}

		public static IType RightTrim(IType oIType)
		{
			return oIType.IsEmpty ? oIType : DataType.Create(oIType.DataType, oIType.ToString(CultureInfo.InvariantCulture).TrimEnd(' '), CultureInfo.InvariantCulture);
		}

		public static bool StartsWith(IType oIType1, IType oIType2)
		{
			bool result;
			if (oIType1.IsEmpty)
				result = oIType2.IsEmpty;
			else if (oIType2.IsEmpty)
				result = true;
			else
				result = oIType1.ToString(CultureInfo.InvariantCulture).StartsWith(oIType2.ToString(CultureInfo.InvariantCulture));
			return result;
		}

		public static IType Substring(IType oIType1, IType oIType2)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType1;
			else
			{
				// get the string value
				var sValue = oIType1.ToString(CultureInfo.InvariantCulture);
				// get the offset
				var iOffset = oIType2.ToInt32(CultureInfo.InvariantCulture);
				try
				{
					// get the portion of the string
					oIType = DataType.Create(oIType1.DataType.TypeCode == TypeCode.String ? oIType1.DataType : DataType.String, sValue.Substring(iOffset - 1), CultureInfo.InvariantCulture);
				}
				catch (ArgumentOutOfRangeException oArgumentOutOfRangeException)
				{
					throw new ArithmeticException(String.Format("Invalid start {0} for substring of value {1}", iOffset, sValue), oArgumentOutOfRangeException);
				}
			}
			return oIType;
		}

		public static IType Substring(IType oIType1, IType oIType2, IType oIType3)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType1;
			else
			{
				// get the string value
				var sValue = oIType1.ToString(CultureInfo.InvariantCulture);
				// get the offset
				var iOffset = oIType2.ToInt32(CultureInfo.InvariantCulture);
				// get the offset
				var iLength = oIType3.ToInt32(CultureInfo.InvariantCulture);
				try
				{
					// get the portion of the string
					oIType = DataType.Create(oIType1.DataType.TypeCode == TypeCode.String ? oIType1.DataType : DataType.String, sValue.Substring(iOffset - 1, iLength), CultureInfo.InvariantCulture);
				}
				catch (ArgumentOutOfRangeException oArgumentOutOfRangeException)
				{
					throw new ArithmeticException(String.Format("Invalid start {0} or length {1} for substring of value {2}", iOffset, iLength, sValue), oArgumentOutOfRangeException);
				}
			}
			return oIType;
		}

		public static IType UpperCase(IType oIType)
		{
			return oIType.IsEmpty ? oIType : DataType.Create(oIType.DataType, oIType.ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
		}

		#endregion
	}
}
