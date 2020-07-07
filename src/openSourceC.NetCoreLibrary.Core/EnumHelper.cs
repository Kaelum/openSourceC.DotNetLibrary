using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for EnumHelper.
	/// </summary>
	public static class EnumHelper
	{
		#region EnumMemberAttribute & XmlEnumAttribute

		/// <summary>
		///		Converts the serialized value of one or more enumerated constants to an equivalent
		///		enumerated object.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <i>serializedValue</i>.</typeparam>
		/// <param name="serializedValue">A string containing the name to convert.</param>
		/// <returns>
		/// </returns>
		public static TEnum ParseEnumMember<TEnum>(string serializedValue)
			where TEnum : struct, Enum
		{
			return ParseEnumMember<TEnum>(serializedValue, false);
		}

		/// <summary>
		///		Converts the serialized value of one or more enumerated constants to an equivalent
		///		enumerated object.  A parameter specifies whether the operation is case-sensitive.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <i>serializedValue</i>.</typeparam>
		/// <param name="serializedValue">A string containing the name to convert.</param>
		/// <param name="ignoreCase"><b>true</b> to ignore case; <b>false</b> to regard case.</param>
		/// <returns>
		/// </returns>
		public static TEnum ParseEnumMember<TEnum>(string serializedValue, bool ignoreCase)
			where TEnum : struct, Enum
		{
			if (TryParseEnumMember<TEnum>(serializedValue, ignoreCase, out TEnum result))
			{
				return result;
			}

			throw new ArgumentException("Value is not one of the defined constants for the enumeration.", "serializedValue");
		}

		/// <summary>
		///		Converts the serialized value of one or more enumerated constants to an equivalent
		///		enumerated object.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <i>serializedValue</i>.</typeparam>
		/// <param name="serializedValue">A string containing the name to convert.</param>
		/// <returns>
		/// </returns>
		public static TEnum? ParseNullableEnumMember<TEnum>(string serializedValue)
			where TEnum : struct, Enum
		{
			return ParseNullableEnumMember<TEnum>(serializedValue, false);
		}

		/// <summary>
		///		Converts the serialized value of one or more enumerated constants to an equivalent
		///		enumerated object.  A parameter specifies whether the operation is case-sensitive.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <i>serializedValue</i>.</typeparam>
		/// <param name="serializedValue">A string containing the name to convert.</param>
		/// <param name="ignoreCase"><b>true</b> to ignore case; <b>false</b> to regard case.</param>
		/// <returns>
		/// </returns>
		public static TEnum? ParseNullableEnumMember<TEnum>(string serializedValue, bool ignoreCase)
			where TEnum : struct, Enum
		{
			if (TryParseEnumMember<TEnum>(serializedValue, ignoreCase, out TEnum result))
			{
				return result;
			}

			return null;
		}

		/// <summary>
		///		Converts the serialized value of one or more enumerated constants to an equivalent
		///		enumerated object. The return value indicates whether the conversion succeeded.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <i>serializedValue</i>.</typeparam>
		/// <param name="serializedValue">A string containing the name to convert.</param>
		/// <param name="result">When this method returns, contains an object of type <i>TEnum</i>
		///		whose value is represented by <i>serializedValue</i>. This parameter is passed
		///		uninitialized.</param>
		/// <returns>
		///		<b>true</b> if the value parameter was converted successfully; otherwise, <b>false</b>.
		///	</returns>
		public static bool TryParseEnumMember<TEnum>(string serializedValue, out TEnum result)
			where TEnum : struct, Enum
		{
			return TryParseEnumMember<TEnum>(serializedValue, false, out result);
		}

		/// <summary>
		///		Converts the serialized value of one or more enumerated constants to an equivalent
		///		enumerated object.  A parameter specifies whether the operation is case-sensitive.
		///		The return value indicates whether the conversion succeeded.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <i>serializedValue</i>.</typeparam>
		/// <param name="serializedValue">A string containing the name to convert.</param>
		/// <param name="ignoreCase"><b>true</b> to ignore case; <b>false</b> to regard case.</param>
		/// <param name="result">When this method returns, contains an object of type <i>TEnum</i>
		///		whose value is represented by <i>serializedValue</i>. This parameter is passed
		///		uninitialized.</param>
		/// <returns>
		///		<b>true</b> if the value parameter was converted successfully; otherwise, <b>false</b>.
		///	</returns>
		public static bool TryParseEnumMember<TEnum>(string serializedValue, bool ignoreCase, out TEnum result)
			where TEnum : struct, Enum
		{
			StringComparison comparisonType = (ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
			FieldInfo[] fieldArray = typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.Public);

			foreach (FieldInfo field in fieldArray)
			{
				if (
					field.Name.Equals(serializedValue, comparisonType)
					|| ((EnumMemberAttribute[])field.GetCustomAttributes(typeof(EnumMemberAttribute), false)).Any(t => t.Value.Equals(serializedValue, comparisonType))
					|| ((XmlEnumAttribute[])field.GetCustomAttributes(typeof(XmlEnumAttribute), false)).Any(t => t.Name.Equals(serializedValue, comparisonType))
				)
				{
					object? rawValue = field.GetRawConstantValue();

					if (rawValue != null)
					{
						result = (TEnum)rawValue;
						return true;
					}
				}
			}

			result = default;
			return typeof(TEnum).IsSubclassOf(typeof(Nullable<>));
		}

		#endregion

		#region EnumMemberGuidAttribute

		/// <summary>
		///		Converts the <i>value</i> parameter to an equivalent enumerated object using the
		///		<see cref="T:EnumMemberGuidAttribute"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to convert <i>value</i> to.</typeparam>
		/// <param name="value">The value to convert.</param>
		/// <returns>
		///		An object of type <typeparamref name="TEnum"/> whose value is represented by
		///		<i>value</i>.
		/// </returns>
		public static TEnum ParseEnumMemberGuid<TEnum>(Guid value)
			where TEnum : struct, Enum
		{
			if (TryParseEnumMemberGuid<TEnum>(value, out TEnum result))
			{
				return result;
			}

			throw new ArgumentException("Type is not one of the defined constants for the enumeration.", "type");
		}

		/// <summary>
		///		Converts the <i>value</i> parameter to an equivalent nullable enumerated object
		///		using the <see cref="T:EnumMemberGuidAttribute"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to convert <i>value</i> to.</typeparam>
		/// <param name="value">The value to convert.</param>
		/// <returns>
		///		An object of type <typeparamref name="TEnum"/> whose value is represented by
		///		<i>value</i> if found; otherwise, <b>null</b>.
		/// </returns>
		public static TEnum? ParseNullableEnumMemberGuid<TEnum>(Guid value)
			where TEnum : struct, Enum
		{
			if (TryParseEnumMemberGuid<TEnum>(value, out TEnum result))
			{
				return result;
			}

			return null;
		}

		/// <summary>
		///		Converts the <i>value</i> parameter to an equivalent enumerated object using the
		///		<see cref="T:EnumMemberGuidAttribute"/>.  The return value indicates whether the
		///		conversion succeeded.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to convert <i>value</i> to.</typeparam>
		/// <param name="value">The value to convert.</param>
		/// <param name="result">When this method returns, contains an object of type
		///		<typeparamref name="TEnum"/> whose value is represented by <i>value</i>. This
		///		parameter is passed uninitialized.</param>
		/// <returns>
		///		<b>true</b> if <typeparamref name="TEnum"/> is a sub-class of
		///		<see cref="T:Nullable&lt;T&gt;>"/> or the value parameter was converted
		///		successfully; otherwise, <b>false</b>.
		///	</returns>
		public static bool TryParseEnumMemberGuid<TEnum>(Guid value, out TEnum result)
			where TEnum : struct, Enum
		{
			FieldInfo[] fieldArray = typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.Public);

			foreach (FieldInfo field in fieldArray)
			{
				if (((EnumMemberGuidAttribute[])field.GetCustomAttributes(typeof(EnumMemberGuidAttribute), false)).Any(t => t.Value.Equals(value)))
				{
					object? rawValue = field.GetRawConstantValue();

					if (rawValue != null)
					{
						result = (TEnum)rawValue;
						return true;
					}
				}
			}

			result = default;
			return typeof(TEnum).IsSubclassOf(typeof(Nullable<>));
		}

		#endregion

		#region RelatedTypeAttribute

		/// <summary>
		///		Converts type to an equivalent enumerated object using the
		///		<see cref="T:RelatedTypeAttribute"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to convert <i>type</i> to.</typeparam>
		/// <param name="type">The type to convert.</param>
		/// <returns>
		/// </returns>
		public static TEnum ParseRelatedType<TEnum>(Type type)
			where TEnum : struct, Enum
		{
			if (TryParseRelatedType<TEnum>(type, out TEnum result))
			{
				return result;
			}

			throw new ArgumentException("Type is not one of the defined constants for the enumeration.", "type");
		}

		/// <summary>
		///		Converts type to an equivalent enumerated object using the
		///		<see cref="T:RelatedTypeAttribute"/>.  A parameter specifies whether the operation
		///		is case-sensitive.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to convert <i>type</i> to.</typeparam>
		/// <param name="type">The type to convert.</param>
		/// <returns>
		/// </returns>
		public static TEnum? ParseNullableRelatedType<TEnum>(Type type)
			where TEnum : struct, Enum
		{
			if (TryParseRelatedType<TEnum>(type, out TEnum result))
			{
				return result;
			}

			return null;
		}

		/// <summary>
		///		Converts type to an equivalent enumerated object using the
		///		<see cref="T:RelatedTypeAttribute"/>.  A parameter specifies whether the operation
		///		is case-sensitive.  The return value indicates whether the conversion succeeded.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to convert <i>type</i> to.</typeparam>
		/// <param name="type">The type to convert.</param>
		/// <param name="result">When this method returns, contains an object of type <i>TEnum</i>
		///		whose value is represented by <i>serializedValue</i>. This parameter is passed
		///		uninitialized.</param>
		/// <returns>
		///		<b>true</b> if the value parameter was converted successfully; otherwise, <b>false</b>.
		///	</returns>
		public static bool TryParseRelatedType<TEnum>(Type type, out TEnum result)
			where TEnum : struct, Enum
		{
			FieldInfo[] fieldArray = typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.Public);

			foreach (FieldInfo field in fieldArray)
			{
				if (((RelatedTypeAttribute[])field.GetCustomAttributes(typeof(RelatedTypeAttribute), false)).Any(t => t.Type == null ? type == null : t.Type.Equals(type)))
				{
					object? rawValue = field.GetRawConstantValue();

					if (rawValue != null)
					{
						result = (TEnum)rawValue;
						return true;
					}
				}
			}

			result = default;
			return typeof(TEnum).IsSubclassOf(typeof(Nullable<>));
		}

		#endregion
	}
}
