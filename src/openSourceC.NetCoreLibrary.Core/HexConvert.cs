using System;
using System.Text;

namespace openSourceC.NetCoreLibrary
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
		public static byte[]? HexDecode(this string hexString)
		{
			return StringToByteArray(hexString);
		}

		/// <summary>
		///		Converts a hexadecimal string to a byte array.
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
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
	}
}
