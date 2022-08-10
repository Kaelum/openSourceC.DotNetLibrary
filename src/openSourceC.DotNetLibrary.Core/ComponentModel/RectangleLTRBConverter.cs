using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace openSourceC.DotNetLibrary.ComponentModel
{
	/// <summary>
	///		Summary description for RectangleLTRBConverter.
	/// </summary>
	public class RectangleLTRBConverter : TypeConverter
	{
		private const string MATCH_PATTERN = @"^\[?<left>\d{1,},?<top>\d{1,}-?<right>\d{1,},?<bottom>\d{1,}\]$";

		private static readonly Regex _matchRegex = new Regex(MATCH_PATTERN, RegexOptions.Compiled | RegexOptions.Singleline);


		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
		{
			if (destinationType == typeof(string))
			{
				return true;
			}

			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string stringValue)
			{
				Match match = _matchRegex.Match(stringValue);
				int left = int.Parse(match.Groups["left"].Value);
				int top = int.Parse(match.Groups["top"].Value);
				int right = int.Parse(match.Groups["right"].Value);
				int bottom = int.Parse(match.Groups["bottom"].Value);

				return Rectangle.FromLTRB(left, top, right, bottom);
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is Rectangle rectangle)
			{
				return string.Format("[{0},{1}-{2},{3}]", rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override bool IsValid(ITypeDescriptorContext? context, object? value)
		{
			if (value is string)
			{
				return _matchRegex.IsMatch((string)value);
			}

			return base.IsValid(context, value);
		}
	}
}
