using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace openSourceC.NetCoreLibrary.Extensions
{
	/// <summary>
	///		Extension methods for arrays, or that return arrays.
	/// </summary>
	public static class ArrayExtensions
	{
		/// <summary>
		///		EndsWith
		/// </summary>
		/// <param name="array"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool EndsWith(this byte[] array, byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			return array.AsSpan().EndsWith(value);
		}

		/// <summary>
		///		EndsWith
		/// </summary>
		/// <param name="array"></param>
		/// <param name="value"></param>
		/// <param name="startIndex"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static bool EndsWith(this byte[] array, byte[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range.  Must be non-negative and less than the size of the collection.");
			}

			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}

			return array.AsSpan(startIndex).EndsWith(value);
		}

		/// <summary>
		///		UTF8 decodes the specified byte array
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("array")]
		public static string? GetUTF8DecodedString(this byte[]? array)
		{
			if (array == null)
			{
				return null;
			}

			return Encoding.UTF8.GetString(array);
		}

		/// <summary>
		///		index
		/// </summary>
		/// <param name="array"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int IndexOf(this byte[] array, byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			return array.AsSpan().IndexOf(value);
		}

		/// <summary>
		///		index
		/// </summary>
		/// <param name="array"></param>
		/// <param name="value"></param>
		/// <param name="startIndex"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static int IndexOf(this byte[] array, byte[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range.  Must be non-negative and less than the size of the collection.");
			}

			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}

			return array.AsSpan(startIndex).IndexOf(value);
		}

		/// <summary>
		///		StartsWith
		/// </summary>
		/// <param name="array"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool StartsWith(this byte[] array, byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			return array.AsSpan().StartsWith(value);
		}

		/// <summary>
		///		StartsWith
		/// </summary>
		/// <param name="array"></param>
		/// <param name="value"></param>
		/// <param name="startIndex"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static bool StartsWith(this byte[] array, byte[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range.  Must be non-negative and less than the size of the collection.");
			}

			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}

			return array.AsSpan(startIndex).StartsWith(value);
		}

		#region Private Methods

		private static int IndexOf<T>(ReadOnlySpan<T> searchSpace, int searchSpaceLength, ReadOnlySpan<T> value, int valueLength)
			where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}

			ReadOnlySpan<T> value2 = value.Slice(0);
			ReadOnlySpan<T> source = value.Slice(1);
			int num = valueLength - 1;
			int num2 = searchSpaceLength - num;
			int searchSpaceIndex = 0;

			while (num2 > 0)
			{
				int position = searchSpace.Slice(searchSpaceIndex).IndexOf<T>(value);

				if (position == -1)
				{
					break;
				}

				num2 -= position;
				searchSpaceIndex += position;

				if (num2 <= 0)
				{
					break;
				}

				if (SequenceEqual(searchSpace.Slice(searchSpaceIndex + 1, source.Length), source))
				{
					return searchSpaceIndex;
				}

				num2--;
				searchSpaceIndex++;
			}

			return -1;
		}

		private static bool SequenceEqual<T>(ReadOnlySpan<T> first, ReadOnlySpan<T> second)
			where T : IEquatable<T>
		{
			if (first == second)
			{
				return true;
			}

			if (first.Length != second.Length)
			{
				return false;
			}

			for (int i = 0; i < first.Length; i++)
			{
				if (!first[i].Equals(second[i]))
				{
					return false;
				}
			}

			return true;
		}

		#endregion
	}
}
