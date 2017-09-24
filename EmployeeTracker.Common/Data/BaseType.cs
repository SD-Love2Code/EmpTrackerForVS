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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EmployeeTracker.Common.Data
{
	public abstract class BaseType : IType, IXmlSerializable
	{
		#region Private Member Variables

		private readonly DataType m_oDataType;

		#endregion

		#region Protected Constructor

		protected BaseType(DataType oDataType)
		{
			m_oDataType = oDataType;
		}

		#endregion

		#region ICollection<IType> Members

		public void Add(IType item)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Clear()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool Contains(IType oIType)
		{
			return Equals(oIType);
		}

		public void CopyTo(IType[] array, int arrayIndex)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public int Count
		{
			get
			{
				return 1;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		public bool Remove(IType item)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			int result;
			IType oIType;
			if ((oIType = obj as IType) != null)
				result = CompareTo(oIType);
			else
				throw new ArgumentException("The object is not the same type as this instance for comparison");
			return result;
		}

		#endregion

		#region IComparable<IType> Members

		public int CompareTo(IType oIType)
		{
			return CompareTo(oIType, CultureInfo.CurrentCulture);
		}

		#endregion

		#region IConvertible Members

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		public virtual bool ToBoolean(IFormatProvider oIFormatProvider)
		{
			return (bool) ToType(TypeCode.Boolean, oIFormatProvider);
		}

		public byte ToByte(IFormatProvider oIFormatProvider)
		{
			return (byte) ToType(TypeCode.Byte, oIFormatProvider);
		}

		public char ToChar(IFormatProvider oIFormatProvider)
		{
			return (char) ToType(TypeCode.Char, oIFormatProvider);
		}

		public virtual DateTime ToDateTime(IFormatProvider oIFormatProvider)
		{
			return (DateTime) ToType(TypeCode.DateTime, oIFormatProvider);
		}

		public virtual decimal ToDecimal(IFormatProvider oIFormatProvider)
		{
			return (decimal) ToType(TypeCode.Decimal, oIFormatProvider);
		}

		public virtual double ToDouble(IFormatProvider oIFormatProvider)
		{
			return (double) ToType(TypeCode.Double, oIFormatProvider);
		}

		public virtual short ToInt16(IFormatProvider oIFormatProvider)
		{
			return (short) ToType(TypeCode.Int16, oIFormatProvider);
		}

		public virtual int ToInt32(IFormatProvider oIFormatProvider)
		{
			return (int) ToType(TypeCode.Int32, oIFormatProvider);
		}

		public virtual long ToInt64(IFormatProvider oIFormatProvider)
		{
			return (long) ToType(TypeCode.Int64, oIFormatProvider);
		}

		public sbyte ToSByte(IFormatProvider oIFormatProvider)
		{
			return (sbyte) ToType(TypeCode.SByte, oIFormatProvider);
		}

		public virtual float ToSingle(IFormatProvider oIFormatProvider)
		{
			return (float) ToType(TypeCode.Single, oIFormatProvider);
		}

		public virtual string ToString(IFormatProvider oIFormatProvider)
		{
			return (string) ToType(TypeCode.String, oIFormatProvider);
		}

		public abstract object ToType(Type oType, IFormatProvider oIFormatProvider);

		public ushort ToUInt16(IFormatProvider oIFormatProvider)
		{
			return (ushort) ToType(TypeCode.UInt16, oIFormatProvider);
		}

		public uint ToUInt32(IFormatProvider oIFormatProvider)
		{
			return (uint) ToType(TypeCode.UInt32, oIFormatProvider);
		}

		public ulong ToUInt64(IFormatProvider oIFormatProvider)
		{
			return (ulong) ToType(TypeCode.UInt64, oIFormatProvider);
		}

		#endregion

		#region IEquatable<IType> Members

		public bool Equals(IType oIType)
		{
			bool result;
			try
			{
				result = CompareTo(oIType) == 0;
			}
			catch (ArgumentException)
			{
				result = false;
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}

		#endregion

		#region IType Members

		public DataType DataType
		{
			get
			{
				return m_oDataType;
			}
		}

		public bool IsArray
		{
			get
			{
				return false;
			}
		}

		public virtual bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Child values for an array value.
		/// </summary>
		public IEnumerable<IType> Item
		{
			get
			{
				return Enumerable.Empty<IType>();
			}
		}

		public TypeCode TypeCode
		{
			get
			{
				return m_oDataType.TypeCode;
			}
		}

		public abstract int CompareTo(IType oIType, CultureInfo oCultureInfo);

		public T ToType<T>(IFormatProvider oIFormatProvider) where T : IConvertible
		{
			return (T) ToType(Type.GetTypeCode(typeof(T)), oIFormatProvider);
		}

		public abstract IConvertible ToType(TypeCode oTypeCode, IFormatProvider oIFormatProvider);

		public abstract IType ToType(DataType oDataType, IFormatProvider oIFormatProvider);

		public virtual IType ToType(DataType oDataType, string sFormat, IFormatProvider oIFormatProvider)
		{
			return ToType(oDataType, oIFormatProvider);
		}

		public bool TryToType(DataType oDataType, IFormatProvider oIFormatProvider, out IType oIType)
		{
			bool result;
			try
			{
				oIType = oDataType == DataType.None ? this : ToType(oDataType, oIFormatProvider);
				result = true;
			}
			catch (ArithmeticException)
			{
				oIType = null;
				result = false;
			}
			catch (FormatException)
			{
				oIType = null;
				result = false;
			}
			catch (InvalidCastException)
			{
				oIType = null;
				result = false;
			}
			return result;
		}

		#endregion

		#region Object Members

		public override bool Equals(object oObject)
		{
			bool result;
			IType oIType;
			if ((oIType = oObject as IType) != null)
				result = Equals(oIType);
			else
				result = false;
			return result;
		}

		public abstract override int GetHashCode();

		public override string ToString()
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public abstract void ReadXml(XmlReader oXmlReader);

		public abstract void WriteXml(XmlWriter oXmlWriter);

		#endregion
	}
}
