using System;

using Microsoft.Extensions.Logging.Console;

namespace openSourceC.DotNetLibrary
{
	internal class ColorConsoleFormatterOptions : ConsoleFormatterOptions
	{
		public LoggerColorBehavior ColorBehavior { get; set; }

		public bool SingleLine { get; set; }
	}
}
