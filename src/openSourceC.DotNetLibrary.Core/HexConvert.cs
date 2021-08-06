using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Summary description for HexConvert.
	/// </summary>
	public static class HexConvert
	{
		#region Convert byte array to hex string

		/// <summary>
		///		Converts a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("byteArray")]
		public static string? ByteArrayToString(byte[]? byteArray)
		{
			if (byteArray == null)
			{
				return null;
			}

			return ByteArrayToString(byteArray, 0, byteArray.Length);
		}

		/// <summary>
		///		Converts a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("byteArray")]
		public static string? ByteArrayToString(byte[]? byteArray, int offset, int length)
		{
			if (byteArray == null)
			{
				return null;
			}

			if (offset < 0 || offset >= byteArray.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(offset));
			}

			if (length < 0 || offset + length > byteArray.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(length));
			}

			StringBuilder returnValue = new StringBuilder();

			for (int i = offset; i < length; i++)
			{
				byte digitPair = byteArray[i];
				returnValue.AppendFormat("{0:X2}", digitPair);
			}

			return returnValue.ToString();
		}

		/// <summary>
		///		Converts a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("byteArray")]
		public static string? HexEncode(this byte[] byteArray)
		{
			return ByteArrayToString(byteArray, 0, byteArray.Length);
		}

		/// <summary>
		///		Converts a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("byteArray")]
		public static string? HexEncode(this byte[] byteArray, int offset, int length)
		{
			return ByteArrayToString(byteArray, offset, length);
		}

		#endregion

		#region Convert hex string to byte array

		/// <summary>
		///		Converts a hexadecimal string to a byte array.
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("hexString")]
		public static byte[]? HexDecode(this string? hexString)
		{
			return StringToByteArray(hexString);
		}

		/// <summary>
		///		Converts a hexadecimal string to a byte array.
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("hexString")]
		public static byte[]? StringToByteArray(string? hexString)
		{
			if (hexString == null)
			{
				return null;
			}

			if ((hexString.Length & 1) != 0)
			{
				throw new ArgumentException("String must contain an even number of digits.", "hexString");
			}

			byte[] returnValue = new byte[hexString.Length / 2];

			for (int i = 0; i * 2 < hexString.Length; i++)
			{
				returnValue[i] = byte.Parse(hexString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
			}

			return returnValue;
		}

		#endregion

		#region ToHexDump

		/// <summary>
		///		Converts a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("byteArray")]
		public static string? DumpByteArray(byte[]? byteArray)
		{
			const int lineBlockSize = 8;
			const int bytesPerLine = 16;
			const int linesPerPage = 16;
			const int pageSize = linesPerPage * bytesPerLine;

			if (byteArray == null)
			{
				return null;
			}

			StringBuilder returnValue = new StringBuilder();

			for (int page = 0; page * pageSize < byteArray.Length; page++)
			{
				if (page != 0)
				{
					returnValue.AppendLine();
				}

				for (int line = 0; line < linesPerPage && page * pageSize + line * bytesPerLine < byteArray.Length; line++)
				{
					returnValue.Append($"{(page * linesPerPage + line) * bytesPerLine:X4} :");

					for (int index = 0; index < bytesPerLine; index++)
					{
						if (index % lineBlockSize == 0)
						{
							returnValue.Append("  ");
						}

						int byteIndex = (page * linesPerPage + line) * bytesPerLine + index;

						if (byteIndex < byteArray.Length)
						{
							returnValue.Append($"  {byteArray[byteIndex]:X2}");
						}
						else
						{
							returnValue.Append("    ");
						}
					}

					returnValue.Append("    ");

					for (int index = 0; index < bytesPerLine; index++)
					{
						int byteIndex = (page * linesPerPage + line) * bytesPerLine + index;

						if (byteIndex < byteArray.Length)
						{
							byte arrayByte = byteArray[byteIndex];

							if (arrayByte < 0x20 || arrayByte > 0x7F)
							{
								returnValue.Append(".");
							}
							else
							{
								returnValue.Append(Convert.ToChar(arrayByte));
							}
						}
						else
						{
							returnValue.Append(" ");
						}
					}

					returnValue.AppendLine();
				}
			}

			return returnValue.ToString();
		}

		/// <summary>
		///		Converts a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull("byteArray")]
		public static string? ToHexDump(this byte[]? byteArray)
		{
			if (byteArray == null)
			{
				return null;
			}

			return DumpByteArray(byteArray);
		}

		#endregion
	}
}
