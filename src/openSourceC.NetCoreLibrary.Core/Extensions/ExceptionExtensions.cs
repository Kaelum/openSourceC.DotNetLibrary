using System;
using System.Text;

namespace openSourceC.NetCoreLibrary.Extensions
{
	/// <summary>
	///		Summary description for ExceptionExtensions.
	/// </summary>
	public static class ExceptionExtensions
	{
		/// <summary>
		///		Returns the concatenated list of <see cref="P:Exception.Message"/> properties from
		///		the root exception through all of the inner exceptions.
		/// </summary>
		/// <param name="source">The exception.</param>
		/// <returns>
		///		Returns the concatenated list of <see cref="P:Exception.Message"/> properties from
		///		the root exception through all of the inner exceptions.
		/// </returns>
		public static string GetMesages(this Exception source)
		{
			Exception? e = source;
			StringBuilder messages = new StringBuilder(source.Message);

			while ((e = e.InnerException) != null)
			{
				messages.Append(" ---> ").Append(e.Message);
			}

			return messages.ToString();
		}
	}
}
