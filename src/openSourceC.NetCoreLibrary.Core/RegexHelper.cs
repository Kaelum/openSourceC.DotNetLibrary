using System;
using System.Text.RegularExpressions;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for RegexHelper.
	/// </summary>
	public static class RegexHelper
	{
		/// <summary>
		///		Use this to parse fully qualified type names.
		/// </summary>
		public static Regex ParseType = new Regex(@"^(?<type>.*?(?:\[.*?\])*?),\s*(?<assembly>.*?)\s*$", RegexOptions.None);
	}
}
