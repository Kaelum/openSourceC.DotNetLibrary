using System;
using System.Runtime.Serialization;

namespace openSourceC.DotNetLibrary
{
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
}
