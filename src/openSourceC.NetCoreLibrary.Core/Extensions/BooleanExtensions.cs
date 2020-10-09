using System;

namespace openSourceC.NetCoreLibrary.Extensions
{
	/// <summary>
	///		Summary description for BooleanExtensions.
	/// </summary>
	public static class BooleanExtensions
	{
		private const string TRUE_STRING = "true";
		private const string FALSE_STRING = "false";

		private const string ONE_STRING = "1";
		private const string ZERO_STRING = "0";


		/// <summary>
		///		Returns either "true" or "false".
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///		"true" when <b>true</b>, and "false" when <b>false</b>.
		/// </returns>
		public static string ToLowerString(this bool value)
		{
			return (
				value
				? TRUE_STRING
				: FALSE_STRING
			);
		}

		/// <summary>
		///		Returns either "1" or "0".
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///		"1" when <b>true</b>, and "0" when <b>false</b>.
		/// </returns>
		public static string ToOneZeroString(this bool value)
		{
			return (
				value
				? ONE_STRING
				: ZERO_STRING
			);
		}
	}
}
