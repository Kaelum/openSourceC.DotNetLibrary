using System;
using System.Runtime.Serialization;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Defines the base class for exceptions that are compatible with the framework's logging
	///		system.
	/// </summary>
	[Serializable]
	public abstract class OscException : Exception
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="OscException" /> class.
		/// </summary>
		public OscException()
			: base() { Initialize(null); }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public OscException(string message)
			: base(message) { Initialize(null); }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		public OscException(string message, string userMessage)
			: base(message) { Initialize(userMessage); }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		/// c   atch block that handles the inner exception.</param>
		public OscException(string message, Exception innerException)
			: base(message, innerException) { Initialize(null); }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		/// c   atch block that handles the inner exception.</param>
		public OscException(string message, string userMessage, Exception innerException)
			: base(message, innerException) { Initialize(userMessage); }

		/// <summary>
		///     Initializes a new instance of the <see cref="OscException" />
		///     class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected OscException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion

		#region Public Properties

		/// <summary> Gets or sets the extended message.</summary>
		public string ExtendedMessage
		{
			get { return Data["ExtendedMessage"] as string; }
			set { Data["ExtendedMessage"] = value; }
		}

		/// <summary>Gets or sets a value indicating whether or not the exception has been logged.</summary>
		public bool IsLogged
		{
			get { return (bool)(Data["IsLogged"] ?? false); }
			set { Data["IsLogged"] = value; }
		}

		/// <summary> Gets or sets the user friendly message.</summary>
		public string UserMessage
		{
			get { return Data["UserMessage"] as string; }
			set { Data["UserMessage"] = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		///
		/// </summary>
		/// <param name="lastError"></param>
		/// <returns></returns>
		public static int HResultFromLastError(int lastError)
		{
			if (lastError < 0)
			{
				return lastError;
			}

			return (((lastError & 0xffff) | 0x70000) | -2147483648);
		}

		#endregion

		#region Private Methods

		private void Initialize(string userMessage)
		{
			ExtendedMessage = null;
			IsLogged = false;
			UserMessage = userMessage;
		}

		#endregion
	}

	/// <summary>
	///		Defines the base class for error exceptions that are compatible with the framework's
	///		logging system.
	/// </summary>
	[Serializable]
	public class OscErrorException : OscException
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="OscErrorException" /> class.
		/// </summary>
		public OscErrorException() { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscErrorException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public OscErrorException(string message)
			: base(message) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		public OscErrorException(string message, string userMessage)
			: base(message, userMessage) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscErrorException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public OscErrorException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscErrorException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		/// c   atch block that handles the inner exception.</param>
		public OscErrorException(string message, string userMessage, Exception innerException)
			: base(message, userMessage, innerException) { }

		/// <summary>
		///     Initializes a new instance of the <see cref="OscErrorException" />
		///     class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected OscErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion
	}

	/// <summary>
	///		Defines the base class for failure audit exceptions that are compatible with the
	///		framework's logging system.
	/// </summary>
	[Serializable]
	public class OscFailureAuditException : OscException
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="OscFailureAuditException" /> class.
		/// </summary>
		public OscFailureAuditException() { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscFailureAuditException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public OscFailureAuditException(string message)
			: base(message) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscFailureAuditException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		public OscFailureAuditException(string message, string userMessage)
			: base(message, userMessage) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscFailureAuditException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public OscFailureAuditException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscFailureAuditException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		/// c   atch block that handles the inner exception.</param>
		public OscFailureAuditException(string message, string userMessage, Exception innerException)
			: base(message, userMessage, innerException) { }

		/// <summary>
		///     Initializes a new instance of the <see cref="OscFailureAuditException" />
		///     class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected OscFailureAuditException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion
	}

	/// <summary>
	///		Defines the base class for information exceptions that are compatible with the
	///		framework's logging system.
	/// </summary>
	[Serializable]
	public class OscInformationException : OscException
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="OscInformationException" /> class.
		/// </summary>
		public OscInformationException() { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscInformationException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public OscInformationException(string message)
			: base(message) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscInformationException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		public OscInformationException(string message, string userMessage)
			: base(message, userMessage) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscInformationException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public OscInformationException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscInformationException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		/// c   atch block that handles the inner exception.</param>
		public OscInformationException(string message, string userMessage, Exception innerException)
			: base(message, userMessage, innerException) { }

		/// <summary>
		///     Initializes a new instance of the <see cref="OscInformationException" />
		///     class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected OscInformationException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion
	}

	/// <summary>
	///		Defines the base class for success audit exceptions that are compatible with the
	///		framework's logging system.
	/// </summary>
	[Serializable]
	public class OscSuccessAuditException : OscException
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="OscSuccessAuditException" /> class.
		/// </summary>
		public OscSuccessAuditException() { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscSuccessAuditException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public OscSuccessAuditException(string message)
			: base(message) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscSuccessAuditException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		public OscSuccessAuditException(string message, string userMessage)
			: base(message, userMessage) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscSuccessAuditException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public OscSuccessAuditException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscSuccessAuditException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		/// c   atch block that handles the inner exception.</param>
		public OscSuccessAuditException(string message, string userMessage, Exception innerException)
			: base(message, userMessage, innerException) { }

		/// <summary>
		///     Initializes a new instance of the <see cref="OscSuccessAuditException" />
		///     class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected OscSuccessAuditException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion
	}

	/// <summary>
	///		Defines the base class for warning exceptions that are compatible with the framework's
	///		logging system.
	/// </summary>
	[Serializable]
	public class OscWarningException : OscException
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="OscWarningException" /> class.
		/// </summary>
		public OscWarningException() { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscWarningException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public OscWarningException(string message)
			: base(message) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscWarningException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		public OscWarningException(string message, string userMessage)
			: base(message, userMessage) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscWarningException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public OscWarningException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscWarningException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		/// c   atch block that handles the inner exception.</param>
		public OscWarningException(string message, string userMessage, Exception innerException)
			: base(message, userMessage, innerException) { }

		/// <summary>
		///     Initializes a new instance of the <see cref="OscWarningException" />
		///     class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected OscWarningException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion
	}
}
