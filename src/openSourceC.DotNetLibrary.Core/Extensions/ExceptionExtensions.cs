using System;
using System.Text;

namespace openSourceC.DotNetLibrary.Extensions
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
		public static string GetMessages(this Exception source)
		{
			Exception e = source;
			StringBuilder messages = new(e.Message);

			while (e.InnerException is not null)
			{
				e = e.InnerException;

				messages.Append(" ---> ").Append(e.Message);
			}

			return messages.ToString();
		}
	}
}
