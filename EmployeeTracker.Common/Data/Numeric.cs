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
	/// <summary>
	/// Class which contains a set of numeric functions for data values.
	/// </summary>
	public static class Numeric
	{
		#region Numeric Members

		public static IType Abs(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, Math.Abs(oIType.ToDecimal(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, Math.Abs(oIType.ToDouble(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int16 :
					oIType = DataType.Create(oIType.DataType, Math.Abs(oIType.ToInt16(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int32 :
					oIType = DataType.Create(oIType.DataType, Math.Abs(oIType.ToInt32(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int64 :
					oIType = DataType.Create(oIType.DataType, Math.Abs(oIType.ToInt64(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, Math.Abs(oIType.ToSingle(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value absolute value", oIType.DataType));
			}
			return oIType;
		}

		public static IType Add(IType oIType1, IType oIType2)
		{
			IType oIType;
			switch (oIType1.TypeCode)
			{
				case TypeCode.Decimal :
					if (IsNumeric(oIType2))
						oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) + oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType2.DataType));
					break;
				case TypeCode.Double :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) + oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) + oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType2.DataType));
					break;
				case TypeCode.Int16 :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) + oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) + oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int32 :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) + oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int64 :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) + oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Single :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) + oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) + oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType2.DataType));
					break;
				case TypeCode.Int32 :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) + oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) + oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int64 :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) + oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Single :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) + oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) + oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType2.DataType));
					break;
				case TypeCode.Int64 :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) + oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) + oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Single :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) + oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) + oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType2.DataType));
					break;
				case TypeCode.Single :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) + oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) + oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) + oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType2.DataType));
					break;
				case TypeCode.String :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) + oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) + oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int16 :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) + oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int32 :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) + oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int64 :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) + oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Single :
								oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) + oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType2.DataType));
						}
					}
					else
					{
						throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType2.DataType));
					}
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value add", oIType1.DataType));
			}
			return oIType;
		}

		public static IType Ceiling(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, Math.Ceiling(oIType.ToDecimal(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, Math.Ceiling(oIType.ToDouble(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int16 :
				case TypeCode.Int32 :
				case TypeCode.Int64 :
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, Convert.ToSingle(Math.Ceiling(oIType.ToDouble(CultureInfo.CurrentCulture))), CultureInfo.CurrentCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for ceiling", oIType.DataType));
			}
			return oIType;
		}

		private static IType Check(IType oIType)
		{
			if (!Numeric.IsNumeric(oIType))
				throw new InvalidCastException(String.Format("Type {0} does not represent a numeric data type", oIType.DataType));
			return oIType;
		}

		public static IType Count(IType oIType)
		{
			return oIType.IsEmpty ? DataType.Create(DataType.Int32, 0, CultureInfo.InvariantCulture) : DataType.Create(DataType.Int32, 1, CultureInfo.InvariantCulture);
		}

		public static IType Count(IEnumerable<IType> oIEnumerable)
		{
			return oIEnumerable.Aggregate(0, (s, v) => v.IsEmpty ? s : ++s, s => DataType.Create(DataType.Int32, s, CultureInfo.InvariantCulture));
		}

		public static IType Decrement(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, oIType.ToDecimal(CultureInfo.InvariantCulture) - 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, oIType.ToDouble(CultureInfo.InvariantCulture) - 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int16 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt16(CultureInfo.InvariantCulture) - 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int32 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt32(CultureInfo.InvariantCulture) - 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int64 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt64(CultureInfo.InvariantCulture) - 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, oIType.ToSingle(CultureInfo.InvariantCulture) - 1, CultureInfo.InvariantCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for decrement", oIType.DataType));
			}
			return oIType;
		}

		public static IType Divide(IType oIType1, IType oIType2)
		{
			IType oIType;
			switch (oIType1.TypeCode)
			{
				case TypeCode.Decimal :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) / oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) / oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) / oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) / oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) / oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) / oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value divide", oIType2.DataType));
					}
					break;
				case TypeCode.Double :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) / oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) / oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) / oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) / oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) / oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) / oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value divide", oIType2.DataType));
					}
					break;
				case TypeCode.Int16 :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) / oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) / oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) / oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) / oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) / oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) / oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value divide", oIType2.DataType));
					}
					break;
				case TypeCode.Int32 :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) / oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) / oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) / oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) / oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) / oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) / oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value divide", oIType2.DataType));
					}
					break;
				case TypeCode.Int64 :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) / oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) / oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) / oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) / oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) / oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) / oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value divide", oIType2.DataType));
					}
					break;
				case TypeCode.Single :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) / oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) / oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) / oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) / oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) / oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) / oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value divide", oIType2.DataType));
					}
					break;
				case TypeCode.String :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) / oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) / oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) / oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) / oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) / oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) / oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value divide", oIType2.DataType));
					}
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value divide", oIType1.DataType));
			}
			return oIType;
		}

		public static bool Equal(IType oIType1, IType oIType2)
		{
			return oIType1.CompareTo(oIType2) == 0;
		}

		public static IType Exp(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DBNull :
					break;
				default :
					oIType = DataType.Create(oIType.DataType, Math.Exp(oIType.ToDouble(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
			}
			return oIType;
		}

		public static IType First(IEnumerable<IType> oIEnumerable)
		{
			return oIEnumerable.FirstOrDefault(e => !e.IsEmpty) ?? DataType.Empty(DataType.None);
		}

		public static IType Floor(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, Math.Floor(oIType.ToDecimal(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, Math.Floor(oIType.ToDouble(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int16 :
				case TypeCode.Int32 :
				case TypeCode.Int64 :
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, Convert.ToSingle(Math.Floor(oIType.ToDouble(CultureInfo.CurrentCulture))), CultureInfo.CurrentCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for floor", oIType.DataType));
			}
			return oIType;
		}

		public static bool GreaterThan(IType oIType1, IType oIType2)
		{
			return oIType1.CompareTo(oIType2) > 0;
		}

		public static bool GreaterThanEqual(IType oIType1, IType oIType2)
		{
			return oIType1.CompareTo(oIType2) >= 0;
		}

		public static IType Increment(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, oIType.ToDecimal(CultureInfo.InvariantCulture) + 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, oIType.ToDouble(CultureInfo.InvariantCulture) + 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int16 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt16(CultureInfo.InvariantCulture) + 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int32 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt32(CultureInfo.InvariantCulture) + 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int64 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt64(CultureInfo.InvariantCulture) + 1, CultureInfo.InvariantCulture);
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, oIType.ToSingle(CultureInfo.InvariantCulture) + 1, CultureInfo.InvariantCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for increment", oIType.DataType));
			}
			return oIType;
		}

		public static IType Iqr(IEnumerable<IType> oIEnumerable)
		{
			// get an ordered list of items
			var oList = oIEnumerable.Where(v => !v.IsEmpty).OrderBy(v => v).ToList();
			// get the count of items in the sequence
			var iCount = oList.Count;
			// 25th percentile round down
			var iIndex1 = iCount > 1 ? (iCount - 1) / 4 : 0;
			// 75th percentile round up
			var iIndex2 = iCount > 1 ? iCount * 3 / 4 : 0;
			// calculate the iqr
			return iCount > 0 ? Numeric.Subtract(oList[iIndex2], oList[iIndex1]) : DataType.Empty(DataType.None);
		}

		public static bool IsEmpty(IType oIType)
		{
			return oIType.IsEmpty;
		}

        public static bool IsInteger(IType oIType)
        {
            return DataType.IsInteger(oIType.DataType) && !oIType.IsArray && !oIType.IsEmpty;
        }

        public static bool IsNegative(IType oIType)
        {
			bool	result = false;
			switch (oIType.TypeCode)
			{
                case TypeCode.Decimal :
                    result = oIType.ToDecimal(CultureInfo.InvariantCulture) < Decimal.Zero;
                    break;
				case TypeCode.Double :
                    result = oIType.ToDouble(CultureInfo.InvariantCulture) < 0;
					break;
                case TypeCode.Int16 :
                    result = oIType.ToInt16(CultureInfo.InvariantCulture) < 0;
                    break;
                case TypeCode.Int32 :
                    result = oIType.ToInt32(CultureInfo.InvariantCulture) < 0;
                    break;
                case TypeCode.Int64 :
                    result = oIType.ToInt64(CultureInfo.InvariantCulture) < 0;
                    break;
				case TypeCode.Single :
					result = oIType.ToSingle(CultureInfo.InvariantCulture) < 0;
					break;
			}
			return result;
        }

        /// <summary>
		/// Determine if a value is the special NaN value.
		/// </summary>
		/// <param name="oValue">
		/// value to check for NaN
		/// </param>
		/// <returns>
		/// true if the value is NaN otherwise false
		/// </returns>
		public static bool IsNaN(IType oIType)
		{
			bool	result = false;
			switch (oIType.TypeCode)
			{
				case TypeCode.Double :
					result = Double.NaN.Equals(oIType.ToDouble(CultureInfo.InvariantCulture));
					break;
				case TypeCode.Single :
					result = Single.IsNaN(oIType.ToSingle(CultureInfo.InvariantCulture));
					break;
			}
			return result;
		}

		/// <summary>
		/// Determine if a string is the special NaN value.
		/// </summary>
		/// <param name="sValue">
		/// string containing the value
		/// </param>
		/// <returns>
		/// true if the string is NaN otherwise false
		/// </returns>
		public static bool IsNaN(String sValue, IFormatProvider oIFormatProvider)
		{
			return sValue.Equals(Double.NaN.ToString(oIFormatProvider));
		}

		public static bool IsNumeric(IType oIType)
		{
			return DataType.IsNumeric(oIType.DataType) && !oIType.IsArray && !oIType.IsEmpty;
		}

		public static IType Last(IEnumerable<IType> oIEnumerable)
		{
			return oIEnumerable.LastOrDefault(e => !e.IsEmpty) ?? DataType.Empty(DataType.None);
		}

		public static bool LessThan(IType oIType1, IType oIType2)
		{
			return oIType1.CompareTo(oIType2) < 0;
		}

		public static bool LessThanEqual(IType oIType1, IType oIType2)
		{
			return oIType1.CompareTo(oIType2) <= 0;
		}

		public static IType Log(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DBNull :
					break;
				default :
					oIType = DataType.Create(oIType.DataType, Math.Log(oIType.ToDouble(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
			}
			return oIType;
		}

		public static IType Log10(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DBNull :
					break;
				default :
					oIType = DataType.Create(oIType.DataType, Math.Log10(oIType.ToDouble(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
			}
			return oIType;
		}

		public static IType Max(IType oIType1, IType oIType2)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType2;
			else if (oIType2.IsEmpty)
				oIType = oIType1;
			else
				oIType = oIType1.CompareTo(oIType2) >= 0 ? oIType1 : oIType2;
			return oIType;
		}

		public static IType Max(IEnumerable<IType> oIEnumerable)
		{
			return oIEnumerable.Aggregate(DataType.Empty(DataType.None), (s, v) => Numeric.Max(s, v));
		}

		public static IType Mean(IEnumerable<IType> oIEnumerable)
		{
			return oIEnumerable.Where(v => !v.IsEmpty).Aggregate(DataType.Empty(DataType.None), (s, v) => s.IsEmpty ? Numeric.Check(v) : Numeric.Add(s, v), s => s.IsEmpty ? DataType.Empty(DataType.None) : Numeric.Divide(s, Numeric.Count(oIEnumerable)));
		}

		public static IType Median(IEnumerable<IType> oIEnumerable)
		{
			var oList = oIEnumerable.Where(v => !v.IsEmpty).OrderBy(v => v).ToList();
			return oList.Count == 0 ? DataType.Empty(DataType.None) : oList[(oList.Count - 1) / 2];
		}

		public static IType Min(IType oIType1, IType oIType2)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType2;
			else if (oIType2.IsEmpty)
				oIType = oIType1;
			else
				oIType = oIType1.CompareTo(oIType2) <= 0 ? oIType1 : oIType2;
			return oIType;
		}

		public static IType Min(IEnumerable<IType> oIEnumerable)
		{
			return oIEnumerable.Aggregate(DataType.Empty(DataType.None), (s, v) => Numeric.Min(s, v));
		}

		public static IType Modulus(IType oIType1, IType oIType2)
		{
			IType oIType;
			switch (oIType1.TypeCode)
			{
				case TypeCode.Decimal :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) % oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) % oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) % oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) % oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) % oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) % oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default:
							throw new InvalidCastException(String.Format("Invalid type {0} for value modulus", oIType2.DataType));
					}
					break;
				case TypeCode.Double :
				case TypeCode.Single :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) % oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) % oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) % oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) % oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) % oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) % oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default:
							throw new InvalidCastException(String.Format("Invalid type {0} for value modulus", oIType2.DataType));
					}
					break;
				case TypeCode.Int16 :
				case TypeCode.Int32 :
				case TypeCode.Int64 :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) % oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) % oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) % oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) % oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) % oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) % oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default:
							throw new InvalidCastException(String.Format("Invalid type {0} for value modulus", oIType2.DataType));
					}
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value modulus", oIType1.DataType));
			}
			return oIType;
		}

		public static IType Multiply(IType oIType1, IType oIType2)
		{
			IType oIType;
			switch (oIType1.TypeCode)
			{
				case TypeCode.Decimal :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
						case TypeCode.Double :
						case TypeCode.Int16 :
						case TypeCode.Int32 :
						case TypeCode.Int64 :
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) * oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value multiply", oIType2.DataType));
					}
					break;
				case TypeCode.Double :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
						case TypeCode.Double :
						case TypeCode.Int16 :
						case TypeCode.Int32 :
						case TypeCode.Int64 :
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) * oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value multiply", oIType2.DataType));
					}
					break;
				case TypeCode.Int16 :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) * oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) * oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) * oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) * oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) * oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) * oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value multiply", oIType2.DataType));
					}
					break;
				case TypeCode.Int32 :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) * oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) * oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) * oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) * oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) * oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) * oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value multiply", oIType2.DataType));
					}
					break;
				case TypeCode.Int64 :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) * oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) * oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) * oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) * oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) * oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) * oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value multiply", oIType2.DataType));
					}
					break;
				case TypeCode.Single :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) * oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) * oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) * oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) * oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) * oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) * oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value multiply", oIType2.DataType));
					}
					break;
				case TypeCode.String :
					switch (oIType2.TypeCode)
					{
						case TypeCode.Decimal :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) * oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Double :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) * oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int16 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) * oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int32 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) * oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Int64 :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) * oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						case TypeCode.Single :
							oIType = DataType.Create(oIType2.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) * oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
							break;
						default :
							throw new InvalidCastException(String.Format("Invalid type {0} for value multiply", oIType2.DataType));
					}
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value multiply", oIType1.DataType));
			}
			return oIType;
		}

		public static IType Negate(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, -oIType.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, -oIType.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int16 :
					oIType = DataType.Create(oIType.DataType, -oIType.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int32 :
					oIType = DataType.Create(oIType.DataType, -oIType.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int64 :
					oIType = DataType.Create(oIType.DataType, -oIType.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, -oIType.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for negate", oIType.DataType));
			}
			return oIType;
		}

		public static bool NotEqual(IType oIType1, IType oIType2)
		{
			return oIType1.CompareTo(oIType2) != 0;
		}

		public static IType Percentile(IEnumerable<IType> oIEnumerable, int iPercent)
		{
			IType oIType;
			// get an ordered list of items
			var oList = oIEnumerable.Where(v => !v.IsEmpty).OrderBy(v => v).ToList();
			if (oList.Count == 0)
				oIType = DataType.Empty(DataType.None);
			else if (iPercent < 0)
				throw new ArgumentException("Invalid percent for percentile function " + iPercent);
			else if (iPercent == 0)
				oIType = oList[0];
			else if (iPercent <= 50)
				oIType = oList[((oList.Count - 1) * iPercent) / 100];
			else if (iPercent < 100)
				oIType = oList[oList.Count * iPercent / 100];
			else if (iPercent == 100)
				oIType = oList[oList.Count - 1];
			else
				throw new ArgumentException("Invalid percent for percentile function " + iPercent);
			return oIType;
		}

		public static IType Pi()
		{
			return DataType.Create(DataType.Double, Math.PI, CultureInfo.InvariantCulture);
		}

		public static IType Power(IType oIType1, IType oIType2)
		{
			return oIType1.IsEmpty ? oIType1 : DataType.Create(oIType1.DataType, Math.Pow(oIType1.ToDouble(CultureInfo.InvariantCulture), oIType2.ToDouble(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
		}

		public static IType Round(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DBNull :
					break;
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, Math.Round(oIType.ToDecimal(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, Math.Round(oIType.ToDouble(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int16 :
				case TypeCode.Int32 :
				case TypeCode.Int64 :
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, Convert.ToSingle(Math.Round(oIType.ToDouble(CultureInfo.CurrentCulture))), CultureInfo.CurrentCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for round", oIType.DataType));
			}
			return oIType;
		}

		public static IType Round(IType oIType, int iScale)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.DBNull :
					break;
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, Math.Round(oIType.ToDecimal(CultureInfo.CurrentCulture), iScale), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, Math.Round(oIType.ToDouble(CultureInfo.CurrentCulture), iScale), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int16 :
				case TypeCode.Int32 :
				case TypeCode.Int64 :
					break;
				case TypeCode.Single :
					oIType = DataType.Create(DataType.Single, Math.Round(oIType.ToDouble(CultureInfo.CurrentCulture), iScale), CultureInfo.CurrentCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for round", oIType.DataType));
			}
			return oIType;
		}

		public static IType Round(IType oIType1, IType oIType2)
		{
			switch (oIType1.TypeCode)
			{
				case TypeCode.DBNull :
					break;
				case TypeCode.Decimal :
					oIType1 = DataType.Create(oIType1.DataType, Math.Round(oIType1.ToDecimal(CultureInfo.CurrentCulture), oIType2.ToInt32(CultureInfo.InvariantCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Double :
					oIType1 = DataType.Create(oIType1.DataType, Math.Round(oIType1.ToDouble(CultureInfo.CurrentCulture), oIType2.ToInt32(CultureInfo.InvariantCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int16 :
				case TypeCode.Int32 :
				case TypeCode.Int64 :
					break;
				case TypeCode.Single :
					oIType1 = DataType.Create(DataType.Single, Math.Round(oIType1.ToDouble(CultureInfo.CurrentCulture), oIType2.ToInt32(CultureInfo.InvariantCulture)), CultureInfo.CurrentCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for round", oIType1.DataType));
			}
			return oIType1;
		}

		public static IType Sqrt(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, Convert.ToDecimal(Math.Sqrt(oIType.ToDouble(CultureInfo.CurrentCulture))), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, Math.Sqrt(oIType.ToDouble(CultureInfo.CurrentCulture)), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int16 :
					oIType = DataType.Create(oIType.DataType, Convert.ToInt16(Math.Sqrt(oIType.ToDouble(CultureInfo.CurrentCulture))), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int32 :
					oIType = DataType.Create(oIType.DataType, Convert.ToInt32(Math.Sqrt(oIType.ToDouble(CultureInfo.CurrentCulture))), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Int64 :
					oIType = DataType.Create(oIType.DataType, Convert.ToInt64(Math.Sqrt(oIType.ToDouble(CultureInfo.CurrentCulture))), CultureInfo.CurrentCulture);
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, Convert.ToSingle(Math.Sqrt(oIType.ToDouble(CultureInfo.CurrentCulture))), CultureInfo.CurrentCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value square root", oIType.DataType));
			}
			return oIType;
		}

		public static IType Sign(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(DataType.Int32, Math.Sign(oIType.ToDecimal(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(DataType.Int32, Math.Sign(oIType.ToDouble(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int16 :
					oIType = DataType.Create(DataType.Int32, Math.Sign(oIType.ToInt16(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int32 :
					oIType = DataType.Create(DataType.Int32, Math.Sign(oIType.ToInt32(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int64 :
					oIType = DataType.Create(DataType.Int32, Math.Sign(oIType.ToInt64(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Single :
					oIType = DataType.Create(DataType.Int32, Math.Sign(oIType.ToSingle(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value sign", oIType.DataType));
			}
			return oIType;
		}

		public static IType Square(IType oIType)
		{
			switch (oIType.TypeCode)
			{
				case TypeCode.Decimal :
					oIType = DataType.Create(oIType.DataType, oIType.ToDecimal(CultureInfo.InvariantCulture) * oIType.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Double :
					oIType = DataType.Create(oIType.DataType, oIType.ToDouble(CultureInfo.InvariantCulture) * oIType.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int16 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt16(CultureInfo.InvariantCulture) * oIType.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int32 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt32(CultureInfo.InvariantCulture) * oIType.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Int64 :
					oIType = DataType.Create(oIType.DataType, oIType.ToInt64(CultureInfo.InvariantCulture) * oIType.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				case TypeCode.Single :
					oIType = DataType.Create(oIType.DataType, oIType.ToSingle(CultureInfo.InvariantCulture) * oIType.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value square", oIType.DataType));
			}
			return oIType;
		}

		public static IType Stdev(IEnumerable<IType> oIEnumerable)
		{
			// get the list of items
			var oList = oIEnumerable.Where(v => !v.IsEmpty).ToList();
			// get the mean
			var oIType = Mean(oList);
			// calculate the stdev
			return oList.Count > 1 ? oList.Aggregate(DataType.Empty(DataType.None), (s, v) => s.IsEmpty ? Numeric.Square(Numeric.Subtract(v, oIType)) : Numeric.Add(s, Numeric.Square(Numeric.Subtract(v, oIType))), v => v.IsEmpty ? v : Numeric.Sqrt(Numeric.Divide(v, DataType.Create(DataType.Int32, oList.Count - 1, CultureInfo.InvariantCulture)))) : DataType.Empty(DataType.None);
		}

		public static IType Stdevp(IEnumerable<IType> oIEnumerable)
		{
			// get the list of items
			var oList = oIEnumerable.Where(v => !v.IsEmpty).ToList();
			// get the mean
			var oIType = Mean(oList);
			// calculate the stdev
			return oList.Aggregate(DataType.Empty(DataType.None), (s, v) => s.IsEmpty ? Numeric.Square(Numeric.Subtract(v, oIType)) : Numeric.Add(s, Numeric.Square(Numeric.Subtract(v, oIType))), v => v.IsEmpty ? v : Numeric.Sqrt(Numeric.Divide(v, DataType.Create(DataType.Int32, oList.Count, CultureInfo.InvariantCulture))));
		}

		public static IType Subtract(IType oIType1, IType oIType2)
		{
			IType oIType;
			switch (oIType1.TypeCode)
			{
				case TypeCode.Decimal :
					if (IsNumeric(oIType2))
						oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) - oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType2.DataType));
					break;
				case TypeCode.Double :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) - oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) - oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType2.DataType));
					break;
				case TypeCode.Int16 :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) - oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) - oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int32 :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) - oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int64 :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) - oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Single :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) - oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) - oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType2.DataType));
					break;
				case TypeCode.Int32 :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) - oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) - oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int64 :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) - oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Single :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) - oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) - oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType2.DataType));
					break;
				case TypeCode.Int64 :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) - oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) - oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Single :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) - oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) - oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType2.DataType));
					break;
				case TypeCode.Single :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) - oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) - oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) - oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
						}
					}
					else
						throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType2.DataType));
					break;
				case TypeCode.String :
					if (IsNumeric(oIType2))
					{
						switch (oIType2.TypeCode)
						{
							case TypeCode.Decimal :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDecimal(CultureInfo.InvariantCulture) - oIType2.ToDecimal(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Double :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToDouble(CultureInfo.InvariantCulture) - oIType2.ToDouble(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int16 :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt16(CultureInfo.InvariantCulture) - oIType2.ToInt16(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int32 :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt32(CultureInfo.InvariantCulture) - oIType2.ToInt32(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Int64 :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToInt64(CultureInfo.InvariantCulture) - oIType2.ToInt64(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							case TypeCode.Single :
								oIType = DataType.Create(oIType1.DataType, oIType1.ToSingle(CultureInfo.InvariantCulture) - oIType2.ToSingle(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
								break;
							default :
								throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType2.DataType));
						}
					}
					else
					{
						throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType2.DataType));
					}
					break;
				default :
					throw new InvalidCastException(String.Format("Invalid type {0} for value subtract", oIType1.DataType));
			}
			return oIType;
		}

		public static IType Sum(IType oIType1, IType oIType2)
		{
			IType oIType;
			if (oIType1.IsEmpty)
				oIType = oIType2;
			else if (oIType2.IsEmpty)
				oIType = oIType1;
			else
				oIType = Add(oIType1, oIType2);
			return oIType;
		}

		public static IType Sum(IEnumerable<IType> oIEnumerable)
		{
			return oIEnumerable.Where(v => !v.IsEmpty).Aggregate(DataType.Empty(DataType.None), (s, v) => s.IsEmpty ? Numeric.Check(v) : Numeric.Add(s, v));
		}

		#endregion
	}
}
