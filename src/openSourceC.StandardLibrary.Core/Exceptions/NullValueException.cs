using System;
using System.Runtime.Serialization;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		The exceptions that is thrown when a non-null value is expected.
	/// </summary>
	[Serializable]
	public class NullValueException : OscErrorException
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="NullValueException" /> class.
		/// </summary>
		public NullValueException() { }

		/// <summary>
		///		Initializes a new instance of the <see cref="NullValueException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public NullValueException(string message)
			: base(message) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="OscException" />
		///		class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		public NullValueException(string message, string userMessage)
			: base(message, userMessage) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="NullValueException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public NullValueException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="NullValueException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		/// c   atch block that handles the inner exception.</param>
		public NullValueException(string message, string userMessage, Exception innerException)
			: base(message, userMessage, innerException) { }

		/// <summary>
		///     Initializes a new instance of the <see cref="NullValueException" />
		///     class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected NullValueException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion
	}
}
