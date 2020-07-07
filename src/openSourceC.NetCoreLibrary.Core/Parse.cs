using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		PopulateDetailField definition.
	/// </summary>
	public delegate void PopulateDetailField(IParserDetail detail, int exportVersion, int fieldNo, string? fieldValue);

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
		/// <param name="populateDetailField">Delegate method that populates a field of the detail parameter.</param>
		/// <param name="detail">The detail object to be populated.</param>
		/// <param name="importVersion">Version of the import file format .</param>
		public static void CSVLine(PopulateDetailField populateDetailField, IParserDetail detail, int importVersion)
		{
			int currentField = 0;
			string? fieldValue;

			FieldType fieldType = FieldType.Unknown;
			int lastCommaPos = -1;
			int openQuotePos = -1;
			int closeQuotePos = -1;
			int i = -1;


			for (i = 0; i <= detail.LineText.Length; i++)
			{
				if (i == detail.LineText.Length || detail.LineText[i] == ',')
				{
					switch (fieldType)
					{
						case FieldType.Alphanumeric:
						{
							if (closeQuotePos == -1)
							{
								throw new ApplicationException(string.Format("Import detail.LineText {0}: closing double-quote not found.", detail.LineNo));
							}

							fieldValue = detail.LineText.Substring(openQuotePos + 1, closeQuotePos - openQuotePos - 1).Replace("\"\"", "\"").Trim();
							break;
						}

						case FieldType.Numeric:
						{
							fieldValue = detail.LineText.Substring(lastCommaPos + 1, i - lastCommaPos - 1).Trim();
							break;
						}

						default:
						{
							fieldValue = null;
							break;
						}
					}

					populateDetailField(detail, importVersion, currentField, fieldValue);

					currentField++;
					fieldType = FieldType.Unknown;
					lastCommaPos = i;
					openQuotePos = -1;
					closeQuotePos = -1;
					continue;
				}

				if (detail.LineText[i] == ' ')
				{
					continue;
				}

				if (detail.LineText[i] == '"')
				{
					if (fieldType != FieldType.Alphanumeric && fieldType != FieldType.Unknown)
					{
						throw new ApplicationException(string.Format("Import detail.LineText {0}: unexpected double-quote found at position {1}.", detail.LineNo, i + 1));
					}

					if (openQuotePos == -1)
					{
						fieldType = FieldType.Alphanumeric;

						openQuotePos = i;
						continue;
					}

					if (closeQuotePos == -1)
					{
						if (i + 1 < detail.LineText.Length && detail.LineText[i + 1] == '"')
						{
							i++;
							continue;
						}

						closeQuotePos = i;
						continue;
					}

					throw new OscErrorException(string.Format("Import line {0}: extra double-quote found at position {1}.", detail.LineNo, i + 1));
				}

				if (fieldType == FieldType.Unknown && openQuotePos == -1)
				{
					fieldType = FieldType.Numeric;
				}
			}
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
		///		Converts the string representation of a number to its 32-bit signed integer
		///		equivalent. A return value indicates whether the operation succeeded.</summary>
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
		///		culture-specific format to its 32-bit signed integer equivalent. A return value
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
		///		equivalent. A return value indicates whether the operation succeeded.
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
		///		culture-specific format to its 64-bit signed integer equivalent. A return value
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

	/// <summary>
	///		The IParserDetail interface needs to be implemented by any data item
	///		that will be populated during a parsing procedure.
	/// </summary>
	public interface IParserDetail
	{
		/// <summary>
		///		Gets or sets the line number of the import file.
		/// </summary>
		int LineNo { get; set; }

		/// <summary>
		///		Gets or sets the line of text from the import file.
		/// </summary>
		string LineText { get; set; }
	}
}
