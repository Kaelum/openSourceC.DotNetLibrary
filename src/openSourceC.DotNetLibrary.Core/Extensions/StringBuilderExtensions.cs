using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace openSourceC.DotNetLibrary.Extensions
{
	/// <summary>
	///		Summary description for StringBuilderExtensions.
	/// </summary>
	public static class StringBuilderExtensions
	{
		#region AppendQueryStringPair

		/// <summary>
		///		Appends a Boolean value to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, bool? value, bool? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends an 8-bit unsigned integer to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, byte? value, byte? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a Unicode character to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, char? value, char? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a Unicode character array to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, char[]? value, char[]? defaultValue = null)
		{
			if ((value ??= defaultValue) is not null)
			{
				return stringBuilder.AppendQueryStringPair(key, new string(value!));
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a decimal number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, decimal? value, decimal? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a double-precision floating-point number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, double? value, double? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a Guid to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, Guid? value, Guid? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a single-precision floating-point number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, float? value, float? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends an <see cref="IEnumerable&lt;T&gt;"/> collection of type
		///		<see cref="string"/> to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, IEnumerable<string>? value, IEnumerable<string>? defaultValue = null)
		{
			if ((value ??= defaultValue) is not null)
			{
				StringBuilder sb = new StringBuilder();

				foreach (string item in value!)
				{
					if (sb.Length > 0)
					{
						sb.Append(",");
					}

					sb.Append(item);
				}

				return stringBuilder.AppendQueryStringPair(key, sb.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a string representation of an enumerator to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair<TEnum>(this StringBuilder stringBuilder, string key, TEnum? value, TEnum? defaultValue = null)
			where TEnum : struct, Enum
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString().ToLower());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a 32-bit signed integer number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, int? value, int? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a 64-bit signed integer number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, long? value, long? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends an 8-bit signed integer to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		[CLSCompliant(false)]
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, sbyte? value, sbyte? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a 16-bit signed integer number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, short? value, short? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a <see cref="DateTime"/> value to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="dateTimeFormat">The DateTime format to use.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, string dateTimeFormat, DateTime? value, DateTime? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString(dateTimeFormat));
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a <see cref="DateTimeOffset"/> value to the parameters collection after converting it to a <see cref="DateTime"/> value.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="dateTimeFormat">The DateTime format to use.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="kind">The <see cref="E:DateTimeKind"/> to use for the conversion.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, string dateTimeFormat, DateTimeOffset? value, DateTimeKind kind, DateTimeOffset? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				switch (kind)
				{
					case DateTimeKind.Local:
					{
						return stringBuilder.AppendQueryStringPair(key, value!.Value.LocalDateTime.ToString(dateTimeFormat));
					}

					case DateTimeKind.Utc:
					{
						return stringBuilder.AppendQueryStringPair(key, value!.Value.UtcDateTime.ToString(dateTimeFormat));
					}

					case DateTimeKind.Unspecified:
					default:
					{
						return stringBuilder.AppendQueryStringPair(key, value!.Value.DateTime.ToString(dateTimeFormat));
					}
				}
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a <see cref="DateTimeOffset"/> value to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="dateTimeFormat">The DateTime format to use.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, string dateTimeFormat, DateTimeOffset? value, DateTimeOffset? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString(dateTimeFormat));
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a <see cref="M:WebUtility.UrlEncode"/> encoded query-string key-value pair
		///		to the end of the <see cref="StringBuilder"/> object.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, string? value, string? defaultValue = null)
		{
			if (!string.IsNullOrEmpty(value = (string.IsNullOrEmpty(value) ? defaultValue : value)))
			{
				if (stringBuilder.Length != 0) { stringBuilder.Append('&'); }

				return stringBuilder.Append(WebUtility.UrlEncode(key)).Append('=').Append(WebUtility.UrlEncode(value));
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a <see cref="string"/> array to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, string[]? value, string[]? defaultValue = null)
		{
			if ((value ??= defaultValue) is not null)
			{
				StringBuilder sb = new StringBuilder();

				foreach (string item in value!)
				{
					if (sb.Length > 0)
					{
						sb.Append(",");
					}

					sb.Append(item);
				}

				return stringBuilder.AppendQueryStringPair(key, sb.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a 32-bit unsigned integer number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		[CLSCompliant(false)]
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, uint? value, uint? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a 64-bit unsigned integer number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		[CLSCompliant(false)]
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, ulong? value, ulong? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		/// <summary>
		///		Appends a 16-bit unsigned integer number to the parameters collection.
		/// </summary>
		/// <param name="stringBuilder">The <see cref="StringBuilder"/> object.</param>
		/// <param name="key">The query-string key.</param>
		/// <param name="value">The query-string value.</param>
		/// <param name="defaultValue">The query-string default value, used if value is null or
		///		empty. (optional)</param>
		/// <returns>
		///		The modified <see cref="StringBuilder"/> object.
		/// </returns>
		[CLSCompliant(false)]
		public static StringBuilder AppendQueryStringPair(this StringBuilder stringBuilder, string key, ushort? value, ushort? defaultValue = null)
		{
			if ((value ??= defaultValue).HasValue)
			{
				return stringBuilder.AppendQueryStringPair(key, value!.Value.ToString());
			}

			return stringBuilder;
		}

		#endregion
	}
}
