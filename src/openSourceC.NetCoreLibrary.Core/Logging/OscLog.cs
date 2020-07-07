//#define USE_CUSTOM_EXCEPTION_FORMAT

using System;
using System.Globalization;

using NLog;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		This object is a wrapper for log4net and includes additional features that mirror logged
	///		events to the Message event in the wrapper.  This works nicely with WPF applications
	///		where you can log events to a ListView control.
	/// </summary>
	[Serializable]
	public class OscLog
	{
		/// <summary>
		///		Provides messages to subscribers.
		/// </summary>
		public event MessageEventHandler? Message;

		private Logger _logger;

		private static readonly Type _thisDeclaringType = typeof(OscLog);

		private static string _defaultLoggerName = "default";
		private static OscLog? _defaultInstance = null;
		private static readonly object _defaultInstanceLock = new object();


		#region Constructors

		/// <summary>
		///		Static constructor.
		/// </summary>
		static OscLog() { }

		/// <summary>
		///		Constructor
		/// </summary>
		/// <param name="logger">An existing NLog <see cref="T:Logger"/> object.</param>
		public OscLog(Logger logger)
		{
			_logger = logger;
		}

		/// <summary>
		///		Constructor
		/// </summary>
		/// <param name="logger">An existing <see cref="T:OscLog"/> object.</param>
		public OscLog(OscLog logger)
			: this(logger._logger) { }

		/// <summary>
		///		Constructor
		/// </summary>
		/// <param name="loggerName">The name of the logger to use.</param>
		public OscLog(string loggerName)
		{
			_logger = LogManager.GetLogger(loggerName);
		}

		#endregion

		#region Public Properties

		/// <summary>Gets the logger name.</summary>
		public string LoggerName
		{
			get { return _logger.Name; }
		}

		#endregion

		#region Instance

		/// <summary>
		///		Gets or sets the default logger name.  Setting to <b>null</b>, causes a default
		///		configuration setting to be used, or "default" if the setting does not exist.
		/// </summary>
		public static string DefaultLoggerName
		{
			get { return _defaultLoggerName; }

			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new InvalidOperationException($"Invalid default logger name: {value}");
				}
				else if (_defaultLoggerName != value)
				{
					lock (_defaultInstanceLock)
					{
						_defaultLoggerName = value;
						_defaultInstance = null;
					}
				}
			}
		}

		/// <summary>
		///		Gets the default (singleton) instance.  If defined, it uses the "DefaultLoggerName"
		///		application setting for the logger name; otherwise, "default" is used.
		///	</summary>
		public static OscLog Instance
		{
			get
			{
				if (_defaultInstance == null)
				{
					lock (_defaultInstanceLock)
					{
						if (_defaultInstance == null)
						{
							_defaultInstance = new OscLog(DefaultLoggerName);
						}
					}
				}

				return _defaultInstance;
			}
		}

		/// <summary>
		///		Sets the default logger to an existing logger instance.
		/// </summary>
		/// <param name="logger">An existing <see cref="T:OscLog"/> object.</param>
		public static void SetDefaultLogger(OscLog logger)
		{
			lock (_defaultInstanceLock)
			{
				_defaultLoggerName = logger.LoggerName;
				_defaultInstance = logger;
			}
		}

		#endregion

		#region Debug

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		public void Debug(Exception exception)
		{
			if (_logger.IsDebugEnabled)
			{
				Debug(exception, null);
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Debug, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Debug, _logger.Name, exception, CultureInfo.CurrentCulture, null);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Debug);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Debug(Exception exception, IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsDebugEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Debug, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Debug, _logger.Name, exception, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Debug);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Debug(Exception exception, string format, params object[] args)
		{
			if (_logger.IsDebugEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Debug, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Debug, _logger.Name, exception, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Debug);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		public void Debug(Exception exception, string? message)
		{
			if (_logger.IsDebugEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Debug, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Debug, _logger.Name, exception, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Debug);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Debug(IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsDebugEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Debug, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Debug, _logger.Name, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Debug);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Debug(string format, params object[] args)
		{
			if (_logger.IsDebugEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Debug, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Debug, _logger.Name, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Debug);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		public void Debug(string message)
		{
			if (_logger.IsDebugEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Debug, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Debug, _logger.Name, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Debug);
			}
		}

		#endregion

		#region Error

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		public void Error(Exception exception)
		{
			if (_logger.IsErrorEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Error, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Error, _logger.Name, exception, CultureInfo.CurrentCulture, null);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Error);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Error(Exception exception, IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsErrorEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Error, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Error, _logger.Name, exception, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Error);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Error(Exception exception, string format, params object[] args)
		{
			if (_logger.IsErrorEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Error, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Error, _logger.Name, exception, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Error);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		public void Error(Exception exception, string message)
		{
			if (_logger.IsErrorEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Error, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Error, _logger.Name, exception, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Error);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Error(IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsErrorEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Error, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Error, _logger.Name, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Error);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Error(string format, params object[] args)
		{
			if (_logger.IsErrorEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Error, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Error, _logger.Name, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Error);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		public void Error(string message)
		{
			if (_logger.IsErrorEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Error, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Error, _logger.Name, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Error);
			}
		}

		#endregion

		#region Fatal

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		public void Fatal(Exception exception)
		{
			if (_logger.IsFatalEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Fatal, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Fatal, _logger.Name, exception, CultureInfo.CurrentCulture, null);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Fatal);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Fatal(Exception exception, IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsFatalEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Fatal, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Fatal, _logger.Name, exception, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Fatal);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Fatal(Exception exception, string format, params object[] args)
		{
			if (_logger.IsFatalEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Fatal, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Fatal, _logger.Name, exception, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Fatal);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		public void Fatal(Exception exception, string message)
		{
			if (_logger.IsFatalEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Fatal, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Fatal, _logger.Name, exception, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Fatal);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Fatal(IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsFatalEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Fatal, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Fatal, _logger.Name, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Fatal);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Fatal(string format, params object[] args)
		{
			if (_logger.IsFatalEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Fatal, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Fatal, _logger.Name, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Fatal);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		public void Fatal(string message)
		{
			if (_logger.IsFatalEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Fatal, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Fatal, _logger.Name, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Fatal);
			}
		}

		#endregion

		#region Info

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		public void Info(Exception exception)
		{
			if (_logger.IsInfoEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				LogEventInfo logEventInfo =
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Info, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Info, _logger.Name, exception, CultureInfo.CurrentCulture, null);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Information);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Info(Exception exception, IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsInfoEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				LogEventInfo logEventInfo =
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Info, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Info, _logger.Name, exception, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Information);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Info(Exception exception, string format, params object[] args)
		{
			if (_logger.IsInfoEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				LogEventInfo logEventInfo =
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Info, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Info, _logger.Name, exception, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Information);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		public void Info(Exception exception, string message)
		{
			if (_logger.IsInfoEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Info, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Info, _logger.Name, exception, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Information);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Info(IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsInfoEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Info, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Info, _logger.Name, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Information);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Info(string format, params object[] args)
		{
			if (_logger.IsInfoEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Info, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Info, _logger.Name, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Information);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		public void Info(string message)
		{
			if (_logger.IsInfoEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Info, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Info, _logger.Name, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Information);
			}
		}

		#endregion

		#region Trace

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		public void Trace(Exception exception)
		{
			if (_logger.IsTraceEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				LogEventInfo logEventInfo =
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Trace, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Trace, _logger.Name, exception, CultureInfo.CurrentCulture, null);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Trace);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Trace(Exception exception, IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsTraceEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Trace, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Trace, _logger.Name, exception, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Trace);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Trace(Exception exception, string format, params object[] args)
		{
			if (_logger.IsTraceEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Trace, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Trace, _logger.Name, exception, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Trace);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		public void Trace(Exception exception, string message)
		{
			if (_logger.IsTraceEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Trace, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Trace, _logger.Name, exception, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Trace);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Trace(IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsTraceEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Trace, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Trace, _logger.Name, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Trace);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Trace(string format, params object[] args)
		{
			if (_logger.IsTraceEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Trace, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Trace, _logger.Name, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Trace);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		public void Trace(string message)
		{
			if (_logger.IsTraceEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Trace, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Trace, _logger.Name, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Trace);
			}
		}

		#endregion

		#region Warn

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		public void Warn(Exception exception)
		{
			if (_logger.IsWarnEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Warn, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Warn, _logger.Name, exception, CultureInfo.CurrentCulture, null);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Warning);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Warn(Exception exception, IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsWarnEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Warn, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Warn, _logger.Name, exception, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Warning);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Warn(Exception exception, string format, params object[] args)
		{
			if (_logger.IsWarnEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Warn, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Warn, _logger.Name, exception, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Warning);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		public void Warn(Exception exception, string message)
		{
			if (_logger.IsWarnEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Warn, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Warn, _logger.Name, exception, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Warning);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Warn(IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsWarnEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Warn, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Warn, _logger.Name, provider, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Warning);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Warn(string format, params object[] args)
		{
			if (_logger.IsWarnEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Warn, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Warn, _logger.Name, CultureInfo.CurrentCulture, format, args);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Warning);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		public void Warn(string message)
		{
			if (_logger.IsWarnEnabled)
			{
#if USE_CUSTOM_EXCEPTION_FORMAT
				_logger.Log(_thisDeclaringType, new LogEventInfo { Level = LogLevel.Warn, Message = GetLoggerMessage(exception, message), });
#else
				LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Warn, _logger.Name, CultureInfo.CurrentCulture, message);
#endif

				LogEvent(logEventInfo, MessageLogEntryType.Warning);
			}
		}

		#endregion

		#region Private Methods

		private void LogEvent(LogEventInfo logEventInfo, MessageLogEntryType messageLogEntryType)
		{
			_logger.Log(_thisDeclaringType, logEventInfo);

			MessageEventArgs messageEventArgs = new MessageEventArgs(_thisDeclaringType, messageLogEntryType, logEventInfo.FormattedMessage, logEventInfo.Exception);

			System.Diagnostics.Debug.WriteLine(messageEventArgs.ToString());

			Message?.Invoke(this, messageEventArgs);
		}

		private string GetLoggerMessage(Exception? exception, string? message)
		{
			if (exception == null)
			{
				LocationInfo location = new LocationInfo(_thisDeclaringType);

				return $"{location.ClassName}.{location.MethodName}: line {location.LineNumber}: {message}";
			}
			else
			{
				return Format.Exception(exception, message);
			}
		}

		#endregion
	}
}
