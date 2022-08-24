using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

using Microsoft.Extensions.Logging;

namespace openSourceC.DotNetLibrary.Extensions
{
	/// <summary>
	///		Summary description for LoggingExtensions.
	/// </summary>
	public static class LoggingExtensions
	{
		#region Public Methods

		/// <summary>
		///		Set the minimum <see cref="T:LogLevel"/> of an <see cref="T:ILoggerFactory"/> object.
		/// </summary>
		/// <param name="loggerFactory">The <see cref="T:ILoggerFactory"/>.</param>
		/// <param name="level">The <see cref="T:LogLevel"/>.</param>
		public static void SetMinimumLevel(this ILoggerFactory loggerFactory, LogLevel level)
		{
			LoggerFactory internalLoggerFactory = (LoggerFactory)loggerFactory!
				.GetType()
				.GetField("_loggerFactory", BindingFlags.NonPublic | BindingFlags.Instance)!
				.GetValue(loggerFactory)!;

			LoggerFilterOptions internalFilterOptions = (LoggerFilterOptions)internalLoggerFactory
				.GetType()
				.GetField("_filterOptions", BindingFlags.NonPublic | BindingFlags.Instance)!
				.GetValue(internalLoggerFactory)!;
			internalFilterOptions.MinLevel = level;

			IDictionary internalLoggersDictionary = (IDictionary)internalLoggerFactory
				.GetType()
				.GetField("_loggers", BindingFlags.NonPublic | BindingFlags.Instance)!
				.GetValue(internalLoggerFactory)!;
			IDictionaryEnumerator loggersEnumerator = internalLoggersDictionary.GetEnumerator();

			while (loggersEnumerator.MoveNext())
			{
				object logger = loggersEnumerator.Value!;

				Array loggerArray = (Array)logger
					.GetType()
					.GetProperty("MessageLoggers")!
					.GetValue(logger)!;

				for (int i = 0; i < loggerArray.Length; i++)
				{
					object field = loggerArray.GetValue(i)!;
					var piMinLevel = field.GetType().GetProperty("MinLevel")!;
					var fiMinLevel = GetBackingField(piMinLevel)!;
					fiMinLevel.SetValue(field, level);
					loggerArray.SetValue(field, i);
				}
			}
		}

		#endregion

		#region Private Methods

		private static FieldInfo? GetBackingField(PropertyInfo pi)
		{
			if (!pi.CanRead || !(pi.GetGetMethod(true)?.IsDefined(typeof(CompilerGeneratedAttribute), true) ?? false))
				return null;

			var backingField = pi.DeclaringType!.GetField($"<{pi.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

			if (backingField == null)
				return null;

			if (!backingField.IsDefined(typeof(CompilerGeneratedAttribute), inherit: true))
				return null;

			return backingField;
		}

		#endregion
	}
}
