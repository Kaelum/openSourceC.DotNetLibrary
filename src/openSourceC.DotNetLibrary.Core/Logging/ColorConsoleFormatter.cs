using System;
using System.IO;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace openSourceC.DotNetLibrary
{
	internal sealed class ColorConsoleFormatter : ConsoleFormatter, IDisposable
	{
		private static readonly string _messagePadding = new string(' ', 4);
		private static readonly string _newLineWithMessagePadding = Environment.NewLine + _messagePadding;

		private readonly IDisposable _optionsReloadToken;
		private ColorConsoleFormatterOptions _formatterOptions;

		private readonly struct ConsoleColors
		{
			public ConsoleColors(ConsoleColor? foreground, ConsoleColor? background)
			{
				Foreground = foreground;
				Background = background;
			}

			public ConsoleColor? Foreground { get; }

			public ConsoleColor? Background { get; }
		}


		public ColorConsoleFormatter(IOptionsMonitor<ColorConsoleFormatterOptions> options)
			: base(nameof(ColorConsoleFormatterOptions)) // Case insensitive
		{
			_formatterOptions = options.CurrentValue;

			_optionsReloadToken = options.OnChange(
				(options) =>
				{
					_formatterOptions = options;
				}
			);
		}

		#region IDisposable Implementation

		public void Dispose()
		{
			_optionsReloadToken?.Dispose();
		}

		#endregion

		#region Public Methods

		public override void Write<TState>(
			in LogEntry<TState> logEntry,
			IExternalScopeProvider scopeProvider,
			TextWriter textWriter
		)
		{
			bool singleLine = _formatterOptions.SingleLine;
			string message = logEntry.Formatter!(logEntry.State, logEntry.Exception);

			if (logEntry.Exception is null && message is null)
			{
				return;
			}

			ConsoleColors consoleColors = GetLogLevelConsoleColors(logEntry.LogLevel);

			if (consoleColors.Background.HasValue)
			{
				textWriter.Write(GetBackgroundColorEscapeCode(consoleColors.Background.Value));
			}

			if (consoleColors.Foreground.HasValue)
			{
				textWriter.Write(GetForegroundColorEscapeCode(consoleColors.Foreground.Value));
			}

			if (_formatterOptions.TimestampFormat is not null)
			{
				DateTimeOffset currentDateTime = (
					_formatterOptions.UseUtcTimestamp
					? DateTimeOffset.UtcNow
					: DateTimeOffset.Now
				);

				string timestampString = currentDateTime.ToString(_formatterOptions.TimestampFormat);

				if (timestampString is not null)
				{
					textWriter.Write(timestampString);
					textWriter.Write(' ');
				}
			}

			textWriter.Write("[");
			textWriter.Write($"{GetLogLevelString(logEntry.LogLevel),-5}");
			textWriter.Write("] ");
			textWriter.Write(logEntry.Category);

			int id = logEntry.EventId.Id;
			Exception? exception = logEntry.Exception;

			if (id != 0)
			{
				textWriter.Write('[');

				Span<char> destination = stackalloc char[10];

				if (id.TryFormat(destination, out var charsWritten))
				{
					textWriter.Write(destination.Slice(0, charsWritten));
				}
				else
				{
					textWriter.Write(id.ToString());
				}

				textWriter.Write("]");
			}

			textWriter.Write(": ");
			textWriter.Write(message);

			if (!singleLine)
			{
				textWriter.Write(Environment.NewLine);
			}

			WriteScopeInformation(textWriter, scopeProvider, singleLine);

			if (consoleColors.Foreground.HasValue)
			{
				textWriter.Write("\u001B[39m\u001B[22m");
			}

			if (consoleColors.Background.HasValue)
			{
				textWriter.Write("\u001B[49m");
			}

			if (exception is not null)
			{
				WriteMessage(textWriter, exception.ToString(), singleLine);
			}

			if (singleLine)
			{
				textWriter.Write(Environment.NewLine);
			}
		}

		#endregion

		#region Private Methods

		private string GetBackgroundColorEscapeCode(ConsoleColor color)
		{
			return color switch
			{
				ConsoleColor.Black => "\u001B[40m",
				ConsoleColor.DarkRed => "\u001B[41m",
				ConsoleColor.DarkGreen => "\u001B[42m",
				ConsoleColor.DarkYellow => "\u001B[43m",
				ConsoleColor.DarkBlue => "\u001B[44m",
				ConsoleColor.DarkMagenta => "\u001B[45m",
				ConsoleColor.DarkCyan => "\u001B[46m",
				ConsoleColor.DarkGray => "\u001B[47m",
				ConsoleColor.Gray => "\u001B[100m",
				ConsoleColor.Red => "\u001B[101m",
				ConsoleColor.Green => "\u001B[102m",
				ConsoleColor.Yellow => "\u001B[103m",
				ConsoleColor.Blue => "\u001B[104m",
				ConsoleColor.Magenta => "\u001B[105m",
				ConsoleColor.Cyan => "\u001B[106m",
				ConsoleColor.White => "\u001B[107m",
				_ => "\u001B[49m",
			};
		}

		private string GetForegroundColorEscapeCode(ConsoleColor color)
		{
			return color switch
			{
				ConsoleColor.Black => "\u001B[30m",
				ConsoleColor.DarkRed => "\u001B[31m",
				ConsoleColor.DarkGreen => "\u001B[32m",
				ConsoleColor.DarkYellow => "\u001B[33m",
				ConsoleColor.DarkBlue => "\u001B[34m",
				ConsoleColor.DarkMagenta => "\u001B[35m",
				ConsoleColor.DarkCyan => "\u001B[36m",
				ConsoleColor.DarkGray => "\u001B[37m",
				ConsoleColor.Gray => "\u001B[90m",
				ConsoleColor.Red => "\u001B[91m",
				ConsoleColor.Green => "\u001B[92m",
				ConsoleColor.Yellow => "\u001B[93m",
				ConsoleColor.Blue => "\u001B[94m",
				ConsoleColor.Magenta => "\u001B[95m",
				ConsoleColor.Cyan => "\u001B[96m",
				ConsoleColor.White => "\u001B[97m",
				_ => "\u001B[39m\u001B[22m",
			};
		}

		private static string GetLogLevelString(LogLevel logLevel)
		{
			return logLevel switch
			{
				LogLevel.Trace => "TRACE",
				LogLevel.Debug => "DEBUG",
				LogLevel.Information => "INFO",
				LogLevel.Warning => "WARN",
				LogLevel.Error => "ERROR",
				LogLevel.Critical => "FATAL",
				_ => throw new ArgumentOutOfRangeException("logLevel"),
			};
		}

		private ConsoleColors GetLogLevelConsoleColors(LogLevel logLevel)
		{
			if (_formatterOptions.ColorBehavior == LoggerColorBehavior.Disabled || _formatterOptions.ColorBehavior == LoggerColorBehavior.Default && Console.IsOutputRedirected)
			{
				return new ConsoleColors(null, null);
			}

			return logLevel switch
			{
				LogLevel.Trace => new ConsoleColors(ConsoleColor.White, ConsoleColor.DarkGray),
				LogLevel.Debug => new ConsoleColors(ConsoleColor.Cyan, ConsoleColor.Black),
				LogLevel.Information => new ConsoleColors(ConsoleColor.White, ConsoleColor.Black),
				LogLevel.Warning => new ConsoleColors(ConsoleColor.Yellow, ConsoleColor.Black),
				LogLevel.Error => new ConsoleColors(ConsoleColor.Red, ConsoleColor.Black),
				LogLevel.Critical => new ConsoleColors(ConsoleColor.Yellow, ConsoleColor.Red),
				_ => new ConsoleColors(null, null),
			};
		}

		private void WriteMessage(
			TextWriter textWriter,
			string message,
			bool singleLine
		)
		{
			if (!string.IsNullOrEmpty(message))
			{
				if (singleLine)
				{
					textWriter.Write(' ');
					WriteReplacing(textWriter, Environment.NewLine, " ", message);
				}
				else
				{
					textWriter.Write(_messagePadding);
					WriteReplacing(textWriter, Environment.NewLine, _newLineWithMessagePadding, message);
					textWriter.Write(Environment.NewLine);
				}
			}

			static void WriteReplacing(TextWriter writer, string oldValue, string newValue, string message)
			{
				string value = message.Replace(oldValue, newValue);

				writer.Write(value);
			}
		}

		private void WriteScopeInformation(TextWriter textWriter, IExternalScopeProvider scopeProvider, bool singleLine)
		{
			if (!_formatterOptions.IncludeScopes || scopeProvider == null)
			{
				return;
			}

			bool paddingNeeded = !singleLine;

			scopeProvider.ForEachScope(delegate (object? scope, TextWriter state)
			{
				if (paddingNeeded)
				{
					paddingNeeded = false;
					state.Write(_messagePadding);
					state.Write("=> ");
				}
				else
				{
					state.Write(" => ");
				}
				state.Write(scope);
			}, textWriter);

			if (!paddingNeeded && !singleLine)
			{
				textWriter.Write(Environment.NewLine);
			}
		}

		#endregion
	}
}
