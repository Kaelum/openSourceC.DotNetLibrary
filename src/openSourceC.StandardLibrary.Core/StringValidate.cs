using System;
using System.Text.RegularExpressions;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for StringValidate.
	/// </summary>
	public static class StringValidate
	{
		private static Regex _alphaPattern = new Regex(@"[^A-Z]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static Regex _alphaNumericPattern = new Regex(@"[^A-Z0-9]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static Regex _einPattern = new Regex(@"^\d{2}-\d{7}$", RegexOptions.Compiled);
		private static Regex _emailPattern = new Regex(@"^([a-zA-Z][\w\.-]+)@((?:[a-zA-Z][\w-]+\.)+(?:[a-zA-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static Regex _integerPattern = new Regex(@"^(?:-|\+)?(?:\d+|\d{1,3}(?:,\d{3})*)$", RegexOptions.Compiled);
		private static Regex _nadpPhonePattern = new Regex(@"^(\+?1[- ]?)?((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( x\d{0,6})?$", RegexOptions.Compiled);
		private static Regex _ssnPattern = new Regex(@"^\d{3}-\d{2}-\d{4}$", RegexOptions.Compiled);
		private static Regex _zipCodePattern = new Regex(@"^\d{5}(?:-\d{4})?$", RegexOptions.Compiled);

		private static Regex _fiveDigitPattern = new Regex(@"^\d{5}$", RegexOptions.Compiled);
		private static Regex _nineDigitPattern = new Regex(@"^\d{9}$", RegexOptions.Compiled);


		/// <summary>
		///		Determines if a string only contains alphabetic characters.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if the specified STR to check is alpha; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsAlpha(string input)
		{
			return !_alphaPattern.IsMatch(input);
		}

		/// <summary>
		///		Determines if a string only contains alphabetic or numeric characters.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if [is alpha numeric] [the specified STR to check]; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsAlphaNumeric(string input)
		{
			return !_alphaNumericPattern.IsMatch(input);
		}

		/// <summary>
		///		Determines if a string is a formatted Employer Identification Number.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if input is a formatted Employer Identification Number; otherwise,
		/// 	<b>false</b>.
		/// </returns>
		public static bool IsEIN(string input)
		{
			return IsEIN(input, false);
		}

		/// <summary>
		///		Determines if a input is an Employer Identification Number.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="allowUnformatted">Allow unformatted numbers to validate.</param>
		/// <returns>
		/// 	<b>true</b> if input is an Employer Identification Number; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsEIN(string input, bool allowUnformatted)
		{
			return (_einPattern.IsMatch(input) || (allowUnformatted ? _nineDigitPattern.IsMatch(input) : false));
		}

		/// <summary>
		/// Determines whether [is valid email] [the specified email].
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if [is valid email] [the specified email]; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsEmail(string input)
		{
			// Return true if strIn is in valid e-mail format.
			return _emailPattern.IsMatch(input);
		}

		/// <summary>
		///		Determines whether the specified input is an integer.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if the specified STR input is integer; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsInteger(string input)
		{
			return _integerPattern.IsMatch(input);
		}

		/// <summary>
		///		Determines whether or not the specified input string is a valid IP
		///		address.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if the specified input string is a valid IP
		/// 	address; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsIp(string input)
		{
			string decimalOctetPattern = @"[0-2]?[0-9]?[0-9]";
			string decimalIpPattern = string.Format(@"{0}\.{0}\.{0}\.{0}", decimalOctetPattern);
			string ipPatern = string.Format(@"{0}", decimalIpPattern);
			Regex ipPattern = new Regex(ipPatern, RegexOptions.Compiled);

			return !ipPattern.IsMatch(input);
		}

		/// <summary>
		/// Determines whether the specified STR input is input.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if the specified STR input is input; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsNumber(string input)
		{
			Regex objNotNumberPattern = new Regex(@"[^0-9.-]", RegexOptions.Compiled);
			Regex objTwoDotPattern = new Regex(@"[0-9]*[.][0-9]*[.][0-9]*", RegexOptions.Compiled);
			Regex objTwoMinusPattern = new Regex(@"[0-9]*[-][0-9]*[-][0-9]*", RegexOptions.Compiled);
			string strValidRealPattern = @"^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
			string strValidIntegerPattern = @"^([-]|[0-9])[0-9]*$";
			Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")", RegexOptions.Compiled);

			return !objNotNumberPattern.IsMatch(input) && !objTwoDotPattern.IsMatch(input) && !objTwoMinusPattern.IsMatch(input) && objNumberPattern.IsMatch(input);
		}

		/// <summary>
		///		Determines whether or not phone input is a valid NADP input.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if input is a valid phone input; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsNadpPhoneNumber(string input)
		{
			return _nadpPhonePattern.IsMatch(input);
		}

		/// <summary>
		///		Determines if a input is a formatted Social Security Number.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <returns>
		/// 	<b>true</b> if input is a formatted Social Security Number; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsSSN(string input)
		{
			return IsSSN(input, false);
		}

		/// <summary>
		///		Determines if a input is a Social Security Number.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="allowUnformatted">Allow unformatted numbers to validate.</param>
		/// <returns>
		/// 	<b>true</b> if input is a Social Security Number; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsSSN(string input, bool allowUnformatted)
		{
			return (_ssnPattern.IsMatch(input) || (allowUnformatted ? _nineDigitPattern.IsMatch(input) : false));
		}

		/// <summary>
		/// Determines whether [is zip code plus4] [the specified zip code].
		/// Function to test for Zip Plus 4 Codes.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="allowUnformatted">Allow unformatted numbers to validate.</param>
		/// <returns>
		/// 	<b>true</b> if [is zip code plus4] [the specified zip code]; otherwise, <b>false</b>.
		/// </returns>
		public static bool IsZipCode(string input, bool allowUnformatted)
		{
			return (_zipCodePattern.IsMatch(input) || (allowUnformatted ? (_fiveDigitPattern.IsMatch(input) || _nineDigitPattern.IsMatch(input)) : false));
		}
	}
}
