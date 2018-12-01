using System;

namespace openSourceC.StandardLibrary.Extensions
{
	/// <summary>
	///		Summary description for StringExtensions.
	/// </summary>
	public static class StringExtensions
	{
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
