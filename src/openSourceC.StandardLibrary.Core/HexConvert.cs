using System;
using System.Text;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for HexConvert.
	/// </summary>
	public static class HexConvert
	{
		/// <summary>
		///		Converts a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <returns></returns>
		public static string ByteArrayToString(byte[] byteArray)
		{
			if (byteArray == null)
			{
				return null;
			}

			StringBuilder returnValue = new StringBuilder();

			foreach (byte digitPair in byteArray)
			{
				returnValue.AppendFormat("{0:X2}", digitPair);
			}

			return returnValue.ToString();
		}

		/// <summary>
		///		Converts a hexadecimal string to a byte array.
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		public static byte[] StringToByteArray(string hexString)
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
	}
}
