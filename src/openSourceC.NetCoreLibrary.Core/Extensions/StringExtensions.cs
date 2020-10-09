using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace openSourceC.NetCoreLibrary.Extensions
{
	/// <summary>
	///		Summary description for StringExtensions.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		///		Encodes all characters in the specified string into a UTF8 encoded sequence of bytes.
		/// </summary>
		/// <param name="obj">The string.</param>
		/// <returns></returns>
		[return: NotNullIfNotNull("obj")]
		public static byte[]? GetUTF8Bytes(this string? obj)
		{
			if (obj == null)
			{
				return null;
			}

			return Encoding.UTF8.GetBytes(obj);
		}

		/// <summary>
		///		Returns the left part of a character string with the specified number of characters.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="maxLength">Is a positive integer that specifies how many characters of the
		///		source will be returned. If maxLength is negative, an error is returned.</param>
		/// <returns>
		///		Returns a string of the maximum specified length.
		/// </returns>
		public static string Left(this string source, int maxLength)
		{
			if (source.Length <= maxLength)
			{
				return source;
			}

			return source.Substring(0, maxLength);
		}

		/// <summary>
		///		Returns the right part of a character string with the specified number of characters.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="maxLength">Is a positive integer that specifies how many characters of the
		///		source will be returned. If maxLength is negative, an error is returned.</param>
		/// <returns>
		///		Returns a string of the maximum specified length.
		/// </returns>
		public static string Right(this string source, int maxLength)
		{
			if (source.Length <= maxLength)
			{
				return source;
			}

			return source.Substring(maxLength - source.Length, maxLength);
		}

		/// <summary>
		///		Returns a count of the words in the specified string.
		/// </summary>
		/// <param name="str">The string</param>
		/// <returns>
		///		A count of the words found.
		/// </returns>
		public static int WordCount(this string str)
		{
			return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
		}
	}
}
