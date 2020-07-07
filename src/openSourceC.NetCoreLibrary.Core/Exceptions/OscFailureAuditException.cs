using System;
using System.Runtime.Serialization;

namespace openSourceC.NetCoreLibrary
{
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
}
