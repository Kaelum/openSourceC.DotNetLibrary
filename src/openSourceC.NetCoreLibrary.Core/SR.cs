using System;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	internal partial class SR
	{
		/// <summary>
		///		Looks up a localized resource object and returns its value.
		/// </summary>
		/// <param name="name">The name of the localized resource object.</param>
		/// <returns>
		///		A localized resource object.
		/// </returns>
		internal static object? GetObject(string name)
		{
			return ResourceManager.GetObject(name, Culture);
		}

		/// <summary>
		///		Looks up a localized resource string and returns its value.
		/// </summary>
		/// <param name="name">The name of the localized resource string.</param>
		/// <returns>
		///		A localized resource string.
		/// </returns>
		internal static string? GetString(string name)
		{
			return ResourceManager.GetString(name, Culture);
		}

		/// <summary>
		///		Looks up a localized resource string and uses its value as the format string for the
		///		specified arguments.
		/// </summary>
		/// <param name="name">The name of the localized resource string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>
		///		A copy of the localized resource string in which the format items have been replaced
		///		by the string representation of the corresponding objects in <i>args</i>.
		/// </returns>
		internal static string? GetString(string name, params object?[] args)
		{
			string? format = ResourceManager.GetString(name, Culture);

			if (format == null || args.Length <= 0)
			{
				return format;
			}

			return string.Format(Culture, format, args);
		}
	}
}
