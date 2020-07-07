using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace openSourceC.NetCoreLibrary.ComponentModel
{
	/// <summary>
	///		Summary description for ByteArrayConverter.
	/// </summary>
	public class ByteArrayConverter : TypeConverter
	{
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
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
		{
			if (value is string stringValue)
			{
				return HexConvert.StringToByteArray(stringValue);
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
			if (destinationType == typeof(string))
			{
				return HexConvert.ByteArrayToString((byte[]?)value);
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
				return Regex.IsMatch((string)value, @"^([0-9A-Za-z][0-9A-Za-z])*$", RegexOptions.Compiled | RegexOptions.Singleline);
			}

			return base.IsValid(context, value);
		}
	}
}
