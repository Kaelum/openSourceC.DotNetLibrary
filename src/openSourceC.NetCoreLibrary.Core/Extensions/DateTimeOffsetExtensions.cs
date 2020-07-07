using System;
using System.Text;

namespace openSourceC.NetCoreLibrary.Extensions
{
	/// <summary>
	///		Summary description for DateTimeOffsetExtensions.
	/// </summary>
	public static class DateTimeOffsetExtensions
	{
		/// <summary>
		///		Gets a friendly date string in relation to todays date.
		/// </summary>
		/// <param name="obj">The <see cref="T:DateTimeOffset"/> object.</param>
		/// <returns>
		///		A friendly date string in relation to todays date.
		/// </returns>
		public static string ToFriendlyString(this DateTimeOffset obj)
		{
			DateTime today = DateTime.Today;
			int daysDiff = (int)today.Date.Subtract(obj.Date).TotalDays;
			DateTime lastYearCutoff = today.Date.AddYears(-1).AddDays(1);
			StringBuilder sb = new StringBuilder();

			if (daysDiff == 0)
			{
				sb.Append("Today");
			}
			else if (daysDiff == 1)
			{
				sb.Append("Yesterday");
			}
			else if (daysDiff < 6)
			{
				sb.Append(obj.DayOfWeek);
			}
			else if (obj >= lastYearCutoff)
			{
				sb.Append(obj.ToString("MMMM d"));
			}
			else
			{
				sb.Append(obj.ToString("MMMM d, yyyy"));
			}

			sb.AppendFormat(" at {0:hh:mm:ss tt}", obj);
			return sb.ToString();
		}

		/// <summary>
		///		Gets a friendly date string in relation to todays date.
		/// </summary>
		/// <param name="obj">The nullable <see cref="T:DateTimeOffset"/> object.</param>
		/// <returns>
		///		A friendly date string in relation to todays date.
		/// </returns>
		public static string? ToFriendlyString(this DateTimeOffset? obj)
		{
			if (obj.HasValue)
			{
				return ToFriendlyString(obj.Value);
			}

			return null;
		}
	}
}
