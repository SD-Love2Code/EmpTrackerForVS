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
using System.Xml;

namespace EmployeeTracker.Common.Data
{
    /// <summary>
    /// Interface for a value which is represented by a data type.
    /// </summary>
	public interface IType : IComparable, IComparable<IType>, IConvertible, IEquatable<IType>
	{
		/// <summary>
		/// Data type of the value.
		/// </summary>
		DataType DataType
		{
			get;
		}

		/// <summary>
		/// Indicates that the value is an array type.
		/// </summary>
		bool IsArray
		{
			get;
		}

		/// <summary>
		/// Indicates that the value is empty
		/// </summary>
		bool IsEmpty
		{
			get;
		}

		/// <summary>
		/// Child values for an array value.
		/// </summary>
		IEnumerable<IType> Item
		{
			get;
		}

        /// <summary>
        /// The best native type for this value.
        /// </summary>
		TypeCode TypeCode
		{
			get;
		}

		/// <summary>
		/// Compare two values using a specified culture.
		/// </summary>
		/// <param name="oIType">
		/// other value to compare
		/// </param>
		/// <param name="oCultureInfo">
		/// culture for the comparison
		/// </param>
		/// <returns>
		/// order relative to zero (0 - equals, -1 less, 1 greater)
		/// </returns>
		int CompareTo(IType oIType, CultureInfo oCultureInfo);

		/// <summary>
		/// Convert to a native type using a generic
		/// </summary>
		/// <typeparam name="T">
		/// convertible type
		/// </typeparam>
		/// <param name="oIFormatProvider">
		/// format provider for the conversion
		/// </param>
		/// <returns>
		/// converted convertible value
		/// </returns>
		T ToType<T>(IFormatProvider oIFormatProvider) where T : IConvertible;

		/// <summary>
		/// Convert to a native type using a format provider.
		/// </summary>
		/// <param name="oTypeCode">
		/// type code of the native type
		/// </param>
		/// <param name="oIFormatProvider">
		/// format provider for the conversion
		/// </param>
		/// <returns>
		/// converted native value
		/// </returns>
		IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider);

		/// <summary>
		/// Convert a value to a data type using a format provider.
		/// </summary>
		/// <param name="oDataType">
		/// output data type
		/// </param>
		/// <param name="oIFormatProvider">
		/// format provider for conversion
		/// </param>
		/// <returns>
		/// converted value
		/// </returns>
		IType ToType(DataType oDataType, IFormatProvider oIFormatProvider);

		/// <summary>
		/// Convert a value to a data type using a string format and format provider.
		/// </summary>
		/// <param name="oDataType">
		/// output data type
		/// </param>
		/// <param name="sFormat">
		/// string format for conversion
		/// </param>
		/// <param name="oIFormatProvider">
		/// format provider for conversion
		/// </param>
		/// <returns>
		/// converted value
		/// </returns>
		IType ToType(DataType oDataType, string sFormat, IFormatProvider oIFormatProvider);

		/// <summary>
		/// Attempt to convert a value to a data type using a format provider.
		/// </summary>
		/// <param name="oDataType">
		/// output data type
		/// </param>
		/// <param name="oIFormatProvider">
		/// format provider for conversion
		/// </param>
		/// <param name="oIType">
		/// converted value on success
		/// </param>
		/// <returns>
		/// true for success else false
		/// </returns>
		bool TryToType(DataType oDataType, IFormatProvider oIFormatProvider, out IType oIType);
	}
}
