﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	/// Summary description for Parse.
	/// </summary>
	public static class Parse
	{
		private enum FieldType
		{
			Unknown,
			Alphanumeric,
			Numeric
		}


		/// <summary>
		///		Parse a CSV line.
		/// </summary>
		/// <param name="line">The line number.</param>
		/// <param name="text">The line text.</param>
		/// <param name="capacity">The number of columns that are expected.  (Default: 0)</param>
		/// <param name="version">File format version. (Default: 0)</param>
		/// <returns>
		///		A <see cref="T:List&lt;T&gt;"/> of strings, which contains the values of each of the
		///		CSV columns, or null, if unable to parse any values.
		/// </returns>
		public static List<string?>? CSVLine(int line, string text, int capacity = 0, int version = 0)
		{
			List<string?> columns = new(capacity);

			FieldType fieldType = FieldType.Unknown;
			int lastCommaPos = -1;
			int openQuotePos = -1;
			int closeQuotePos = -1;


			for (int index = 0; index <= text.Length; index++)
			{
				if (
					index == text.Length
					|| (
						text[index] == ','
						&& (
							(openQuotePos == -1 && closeQuotePos == -1)
							|| (openQuotePos != -1 && closeQuotePos != -1)
						)
					)
				)
				{
					switch (fieldType)
					{
						case FieldType.Alphanumeric:
						{
							if (closeQuotePos == -1)
							{
								throw new FormatException($"Import line {line} [{text}]: closing double-quote not found.");
							}

							columns.Add(text.Substring(openQuotePos + 1, closeQuotePos - openQuotePos - 1).Replace("\"\"", "\"").Trim());
							break;
						}

						case FieldType.Numeric:
						{
							columns.Add(text.Substring(lastCommaPos + 1, index - lastCommaPos - 1).Trim());
							break;
						}

						default:
						{
							columns.Add(null);
							break;
						}
					}

					fieldType = FieldType.Unknown;
					lastCommaPos = index;
					openQuotePos = -1;
					closeQuotePos = -1;
					continue;
				}

				if (text[index] == ' ')
				{
					continue;
				}

				if (text[index] == '"')
				{
					if (fieldType != FieldType.Alphanumeric && fieldType != FieldType.Unknown)
					{
						throw new FormatException($"Import line {line} [{text}]: unexpected double-quote found at position {index + 1}.");
					}

					if (openQuotePos == -1)
					{
						fieldType = FieldType.Alphanumeric;

						openQuotePos = index;
						continue;
					}

					if (closeQuotePos == -1)
					{
						// Check for double double-quotes within alphanumeric text.
						if (index + 1 < text.Length && text[index + 1] == '"')
						{
							index++;
							continue;
						}

						closeQuotePos = index;
						continue;
					}

					throw new FormatException($"Import line {line}: extra double-quote found at position {index + 1}.");
				}

				if (fieldType == FieldType.Unknown && openQuotePos == -1)
				{
					fieldType = FieldType.Numeric;
				}
			}

			if (columns.Count == 0)
			{
				return null;
			}

			return columns;
		}

		/// <summary>
		///		Converts a Guid string to a <see cref="Nullable&lt;Guid&gt;"/>.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static Guid? NullableGuid(string? s)
		{
			try
			{
				if (s == null)
				{
					return null;
				}

				return new Guid(s);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		///		Converts the string representation of a logical value to its <see cref="T:bool"/>
		///		equivalent.</summary>
		/// <param name="s">A string containing the value to convert.</param>
		/// <returns>
		///		A nullable <see cref="T:bool"/> equivalent to the logical value contained in s, if
		///		the conversion succeeded, and null if the conversion failed. The conversion fails if
		///		the s parameter is not of the correct format.
		///	</returns>
		public static bool? NullableBool(string? s)
		{
			return (TryNullableBool(s, out bool? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a number to its 32-bit signed integer
		///		equivalent. A return value indicates whether the operation succeeded.</summary>
		/// <param name="s">A string containing a number to convert. </param>
		/// <returns>
		///		Returns a nullable 32-bit signed integer value equivalent to the number contained
		///		in s, if the conversion succeeded, or null if the conversion failed. The conversion
		///		fails if the s parameter is not of the correct format, or represents a number less
		///		than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.
		///		This parameter is passed uninitialized.
		/// </returns>
		public static int? NullableInt(string? s)
		{
			return (TryNullableInt(s, out int? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a number in a specified style and
		///		culture-specific format to its 32-bit signed integer equivalent.
		/// </summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies
		///		culture-specific formatting information about s.</param>
		/// <returns>
		///		Returns a nullable 32-bit signed integer value equivalent to the number contained
		///		in s, if the conversion succeeded, or null if the conversion failed. The conversion
		///		fails if the s parameter is not of the correct format, or represents a number less
		///		than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.
		///		This parameter is passed uninitialized.
		/// </returns>
		public static int? NullableInt(string? s, IFormatProvider provider)
		{
			return (TryNullableInt(s, NumberStyles.Integer, provider, out int? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a number in a specified style and
		///		culture-specific format to its 32-bit signed integer equivalent.
		/// </summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" />
		///		values that indicates the permitted format of s. A typical value to specify is
		///		<see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <returns>
		///		Returns a nullable 32-bit signed integer value equivalent to the number contained
		///		in s, if the conversion succeeded, or null if the conversion failed. The conversion
		///		fails if the s parameter is not of the correct format, or represents a number less
		///		than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.
		///		This parameter is passed uninitialized.
		/// </returns>
		public static int? NullableInt(string? s, NumberStyles style)
		{
			return (TryNullableInt(s, style, NumberFormatInfo.CurrentInfo, out int? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a number in a specified style and
		///		culture-specific format to its 32-bit signed integer equivalent.
		/// </summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" />
		///		values that indicates the permitted format of s. A typical value to specify is
		///		<see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies
		///		culture-specific formatting information about s.</param>
		/// <returns>
		///		Returns a nullable 32-bit signed integer value equivalent to the number contained
		///		in s, if the conversion succeeded, or null if the conversion failed. The conversion
		///		fails if the s parameter is not of the correct format, or represents a number less
		///		than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.
		///		This parameter is passed uninitialized.
		/// </returns>
		public static int? NullableInt(string? s, NumberStyles style, IFormatProvider provider)
		{
			return (TryNullableInt(s, style, provider, out int? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a number to its 64-bit signed integer
		///		equivalent. A return value indicates whether the operation succeeded.</summary>
		/// <param name="s">A string containing a number to convert. </param>
		/// <returns>
		///		Returns a nullable 64-bit signed integer value equivalent to the number contained
		///		in s, if the conversion succeeded, or null if the conversion failed. The conversion
		///		fails if the s parameter is not of the correct format, or represents a number less
		///		than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.
		///		This parameter is passed uninitialized.
		/// </returns>
		public static long? NullableLong(string? s)
		{
			return (TryNullableLong(s, out long? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a number in a specified culture-specific
		///		format to its 64-bit signed integer equivalent.
		/// </summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies
		///		culture-specific formatting information about s.</param>
		/// <returns>
		///		Returns a nullable 64-bit signed integer value equivalent to the number contained
		///		in s, if the conversion succeeded, or null if the conversion failed. The conversion
		///		fails if the s parameter is not of the correct format, or represents a number less
		///		than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.
		///		This parameter is passed uninitialized.
		/// </returns>
		public static long? NullableLong(string? s, IFormatProvider provider)
		{
			return (TryNullableLong(s, NumberStyles.Integer, provider, out long? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a number in a specified style to its 64-bit
		///		signed integer equivalent.
		/// </summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" />
		///		values that indicates the permitted format of s. A typical value to specify is
		///		<see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <returns>
		///		Returns a nullable 64-bit signed integer value equivalent to the number contained
		///		in s, if the conversion succeeded, or null if the conversion failed. The conversion
		///		fails if the s parameter is not of the correct format, or represents a number less
		///		than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.
		///		This parameter is passed uninitialized.
		/// </returns>
		public static long? NullableLong(string? s, NumberStyles style)
		{
			return (TryNullableLong(s, style, NumberFormatInfo.CurrentInfo, out long? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a number in a specified style and
		///		culture-specific format to its 64-bit signed integer equivalent.
		/// </summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" />
		///		values that indicates the permitted format of s. A typical value to specify is
		///		<see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies
		///		culture-specific formatting information about s.</param>
		/// <returns>
		///		Returns a nullable 64-bit signed integer value equivalent to the number contained
		///		in s, if the conversion succeeded, or null if the conversion failed. The conversion
		///		fails if the s parameter is not of the correct format, or represents a number less
		///		than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.
		///		This parameter is passed uninitialized.
		/// </returns>
		public static long? NullableLong(string? s, NumberStyles style, IFormatProvider provider)
		{
			return (TryNullableLong(s, style, provider, out long? result) ? result : null);
		}

		/// <summary>
		///		Converts the string representation of a logical value to its <see cref="T:bool"/>
		///		equivalent. The return value indicates whether the operation succeeded.</summary>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="nullableResult">When this method returns, contains the nullable
		///		<see cref="T:bool"/> equivalent to the logical value contained in s, if the
		///		conversion succeeded, or null if the conversion failed. The conversion fails if the
		///		s parameter is not of the correct format. This parameter is passed uninitialized.</param>
		/// <returns>
		///		true if s was converted successfully; otherwise, false.
		///	</returns>
		public static bool TryNullableBool(string? s, out bool? nullableResult)
		{
			if (string.IsNullOrEmpty(s))
			{
				nullableResult = null;
				return true;
			}

			if (bool.TryParse(s, out bool result))
			{
				nullableResult = result;
				return true;
			}

			if (TryNullableInt(s, out int? intResult))
			{
				nullableResult = (intResult != 0);
				return true;
			}

			nullableResult = null;
			return false;
		}

		/// <summary>
		///		Converts the string representation of a number to its 32-bit signed integer
		///		equivalent. The return value indicates whether the operation succeeded.</summary>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="nullableResult">When this method returns, contains the nullable 32-bit
		///		signed integer value equivalent to the number contained in s, if the conversion
		///		succeeded, or null if the conversion failed. The conversion fails if the s parameter
		///		is not of the correct format, or represents a number less than
		///		<see cref="F:System.Int32.MinValue" /> or greater than
		///		<see cref="F:System.Int32.MaxValue" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///		true if s was converted successfully; otherwise, false.
		///	</returns>
		public static bool TryNullableInt(string? s, out int? nullableResult)
		{
			return TryNullableInt(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out nullableResult);
		}

		/// <summary>
		///		Converts the string representation of a number in a specified style and
		///		culture-specific format to its 32-bit signed integer equivalent. The return value
		///		indicates whether the operation succeeded.
		/// </summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" />
		///		values that indicates the permitted format of s. A typical value to specify is
		///		<see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that supplies
		///		culture-specific formatting information about s. </param>
		/// <param name="nullableResult">When this method returns, contains the nullable 32-bit
		///		signed integer value equivalent to the number contained in s, if the conversion
		///		succeeded, or null if the conversion failed. The conversion fails if the s parameter
		///		is not of the correct format, or represents a number less than
		///		<see cref="F:System.Int32.MinValue" /> or greater than
		///		<see cref="F:System.Int32.MaxValue" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///		true if s was converted successfully; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">
		///		style is not a <see cref="T:System.Globalization.NumberStyles" /> value.
		///		-or-
		///		style is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />
		///		and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.
		///	</exception>
		public static bool TryNullableInt(string? s, NumberStyles style, IFormatProvider provider, out int? nullableResult)
		{
			if (string.IsNullOrEmpty(s))
			{
				nullableResult = null;
				return true;
			}

			if (int.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out int result))
			{
				nullableResult = result;
				return true;
			}

			nullableResult = null;
			return false;
		}

		/// <summary>
		///		Converts the string representation of a number to its 64-bit signed integer
		///		equivalent. The return value indicates whether the operation succeeded.
		/// </summary>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="nullableResult">When this method returns, contains the nullable 64-bit
		///		signed integer value equivalent to the number contained in s, if the conversion
		///		succeeded, or null if the conversion failed. The conversion fails if the s parameter
		///		is not of the correct format, or represents a number less than
		///		<see cref="F:System.Int64.MinValue" /> or greater than
		///		<see cref="F:System.Int64.MaxValue" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///		true if s was converted successfully; otherwise, false.
		///	</returns>
		public static bool TryNullableLong(string? s, out long? nullableResult)
		{
			return TryNullableLong(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out nullableResult);
		}

		/// <summary>
		///		Converts the string representation of a number in a specified style and
		///		culture-specific format to its 64-bit signed integer equivalent. The return value
		///		indicates whether the operation succeeded.
		/// </summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" />
		///		values that indicates the permitted format of s. A typical value to specify is
		///		<see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that supplies
		///		culture-specific formatting information about s. </param>
		/// <param name="nullableResult">When this method returns, contains the nullable 64-bit
		///		signed integer value equivalent to the number contained in s, if the conversion
		///		succeeded, or null if the conversion failed. The conversion fails if the s parameter
		///		is not of the correct format, or represents a number less than
		///		<see cref="F:System.Int64.MinValue" /> or greater than
		///		<see cref="F:System.Int64.MaxValue" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///		true if s was converted successfully; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">
		///		style is not a <see cref="T:System.Globalization.NumberStyles" /> value.
		///		-or-
		///		style is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />
		///		and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.
		///	</exception>
		public static bool TryNullableLong(string? s, NumberStyles style, IFormatProvider provider, out long? nullableResult)
		{
			if (string.IsNullOrEmpty(s))
			{
				nullableResult = null;
				return true;
			}

			if (long.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out long result))
			{
				nullableResult = result;
				return true;
			}

			nullableResult = null;
			return false;
		}
	}
}
