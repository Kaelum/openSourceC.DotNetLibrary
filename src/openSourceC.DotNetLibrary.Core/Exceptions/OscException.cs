using System;
using System.Runtime.Serialization;

namespace openSourceC.DotNetLibrary
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
		public string? ExtendedMessage
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
		public string? UserMessage
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

		private void Initialize(string? userMessage)
		{
			ExtendedMessage = null;
			IsLogged = false;
			UserMessage = userMessage;
		}

		#endregion
	}
}
