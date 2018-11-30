using System;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Represents the method that will handle a message event.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void MessageEventHandler(object sender, MessageEventArgs e);

	/// <summary>
	///		Summary description for MessageEventArgs.
	/// </summary>
	[Serializable]
	public class MessageEventArgs : EventArgs
	{
		#region Constructors

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="locationInfo"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="exception"></param>
		public MessageEventArgs(LocationInfo locationInfo, MessageLogEntryType messageLogEntryType, Exception exception)
			: this(locationInfo, messageLogEntryType, null, exception) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="locationInfo"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="exception"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public MessageEventArgs(LocationInfo locationInfo, MessageLogEntryType messageLogEntryType, Exception exception, IFormatProvider provider, string format, params object[] args)
			: this(locationInfo, messageLogEntryType, string.Format(provider, format, args), exception) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="locationInfo"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="exception"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public MessageEventArgs(LocationInfo locationInfo, MessageLogEntryType messageLogEntryType, Exception exception, string format, params object[] args)
			: this(locationInfo, messageLogEntryType, exception, null, format, args) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="locationInfo"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public MessageEventArgs(LocationInfo locationInfo, MessageLogEntryType messageLogEntryType, IFormatProvider provider, string format, params object[] args)
			: this(locationInfo, messageLogEntryType, string.Format(provider, format, args)) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="locationInfo"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public MessageEventArgs(LocationInfo locationInfo, MessageLogEntryType messageLogEntryType, string format, params object[] args)
			: this(locationInfo, messageLogEntryType, (IFormatProvider)null, format, args) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="locationInfo"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="message"></param>
		public MessageEventArgs(LocationInfo locationInfo, MessageLogEntryType messageLogEntryType, string message)
			: this(locationInfo, messageLogEntryType, message, (Exception)null) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="locationInfo"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public MessageEventArgs(LocationInfo locationInfo, MessageLogEntryType messageLogEntryType, string message, Exception exception)
		{
			if (exception == null)
			{
				EventLogEvent = new EventLogEvent(message, messageLogEntryType);
			}
			else
			{
				EventLogEvent = new EventLogEvent(exception, message);
			}

			EventLogEvent.LocationInfo = locationInfo;

			LocationInfo = locationInfo;
			MessageLogEntryType = messageLogEntryType;
			Message = message;
		}

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="callerType"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="exception"></param>
		public MessageEventArgs(Type callerType, MessageLogEntryType messageLogEntryType, Exception exception)
			: this(callerType, messageLogEntryType, null, exception) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="callerType"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="exception"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public MessageEventArgs(Type callerType, MessageLogEntryType messageLogEntryType, Exception exception, IFormatProvider provider, string format, params object[] args)
			: this(callerType, messageLogEntryType, string.Format(provider, format, args), exception) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="callerType"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="exception"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public MessageEventArgs(Type callerType, MessageLogEntryType messageLogEntryType, Exception exception, string format, params object[] args)
			: this(callerType, messageLogEntryType, exception, null, format, args) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="callerType"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="provider"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public MessageEventArgs(Type callerType, MessageLogEntryType messageLogEntryType, IFormatProvider provider, string format, params object[] args)
			: this(callerType, messageLogEntryType, string.Format(provider, format, args)) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="callerType"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public MessageEventArgs(Type callerType, MessageLogEntryType messageLogEntryType, string format, params object[] args)
			: this(callerType, messageLogEntryType, (IFormatProvider)null, format, args) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="callerType"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="message"></param>
		public MessageEventArgs(Type callerType, MessageLogEntryType messageLogEntryType, string message)
			: this(new LocationInfo(callerType), messageLogEntryType, message) { }

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="callerType"></param>
		/// <param name="messageLogEntryType"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public MessageEventArgs(Type callerType, MessageLogEntryType messageLogEntryType, string message, Exception exception)
			: this(new LocationInfo(callerType), messageLogEntryType, message, exception) { }

		#endregion

		#region Public Properties

		/// <summary>Gets the Event Log event object.</summary>
		public EventLogEvent EventLogEvent { get; private set; }

		/// <summary>Gets the calling method.</summary>
		public LocationInfo LocationInfo { get; private set; }

		/// <summary>Gets the log entry type.</summary>
		public MessageLogEntryType MessageLogEntryType { get; private set; }

		/// <summary>Gets the message.</summary>
		public string Message { get; private set; }

		#endregion

		#region ToString()

		/// <summary>
		///		Returns a string representing the current object using a default message format.
		/// </summary>
		/// <returns>
		///		A string representing the current object using a default message format.
		/// </returns>
		public override string ToString()
		{
			return $"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff}: {LocationInfo.ClassName}.{LocationInfo.MethodName}:{LocationInfo.LineNumber}: {Message}";
		}

		#endregion
	}
}
