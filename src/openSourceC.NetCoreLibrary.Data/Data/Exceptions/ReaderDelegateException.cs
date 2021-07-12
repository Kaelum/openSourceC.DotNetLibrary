using System;
using System.Runtime.Serialization;

namespace openSourceC.NetCoreLibrary.Data
{
	/// <summary>
	///		The exception that is thrown when an exception occurs within a reader delegate..
	/// </summary>
	[Serializable]
	public class ReaderDelegateException : OscErrorException
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="ReaderDelegateException" /> class.
		/// </summary>
		public ReaderDelegateException()
			: base() { SaveParameters(null); }

		/// <summary>
		///		Initializes a new instance of the <see cref="ReaderDelegateException" />
		///		class with the command that caused the exception.
		/// </summary>
		/// <param name="command">The <see cref="DbFactoryCommand"/> that caused the exception.</param>
		public ReaderDelegateException(DbFactoryCommand command)
			: base() { SaveParameters(command); }

		/// <summary>
		///		Initializes a new instance of the <see cref="ReaderDelegateException" />
		///		class with the command that caused the exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="command">The <see cref="DbFactoryCommand"/> that caused the exception.</param>
		public ReaderDelegateException(string message, DbFactoryCommand command)
			: base(message) { SaveParameters(command); }

		/// <summary>
		///		Initializes a new instance of the <see cref="ReaderDelegateException" />
		///		class with the command that caused the exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="command">The <see cref="DbFactoryCommand"/> that caused the exception.</param>
		public ReaderDelegateException(string message, string userMessage, DbFactoryCommand command)
			: base(message, userMessage) { SaveParameters(command); }

		/// <summary>
		///		Initializes a new instance of the <see cref="ReaderDelegateException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="command">The <see cref="DbFactoryCommand"/> that caused the exception.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public ReaderDelegateException(DbFactoryCommand command, Exception innerException)
			: base(innerException.Message, innerException) { SaveParameters(command); }

		/// <summary>
		///		Initializes a new instance of the <see cref="ReaderDelegateException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="command">The <see cref="DbFactoryCommand"/> that caused the exception.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public ReaderDelegateException(string message, DbFactoryCommand command, Exception innerException)
			: base(message, innerException) { SaveParameters(command); }

		/// <summary>
		///		Initializes a new instance of the <see cref="ReaderDelegateException" />
		///		class with a specified error message and a reference to the
		///		inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="userMessage">A user friendly message that can sent to the user.</param>
		/// <param name="command">The <see cref="DbFactoryCommand"/> that caused the exception.</param>
		/// <param name="innerException">The exception that is the cause
		///     of the current exception. If the innerException parameter is
		///     not a null reference, the current exception is raised in a
		///     catch block that handles the inner exception.</param>
		public ReaderDelegateException(string message, string userMessage, DbFactoryCommand command, Exception innerException)
			: base(message, userMessage, innerException) { SaveParameters(command); }

		/// <summary>
		///     Initializes a new instance of the <see cref="ReaderDelegateException" />
		///     class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected ReaderDelegateException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion

		#region Private Methods

		private void SaveParameters(DbFactoryCommand command)
		{
			if (command is not null)
			{
				ExtendedMessage = command.ToString();
			}
		}

		#endregion
	}
}
