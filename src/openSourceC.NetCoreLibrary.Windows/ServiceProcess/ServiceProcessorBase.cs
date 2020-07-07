#define _USES_UNMANGED_CODE

using System;

namespace openSourceC.NetCoreLibrary.ServiceProcess
{
	/// <summary>
	///		Summary description of ServiceProcessorBase.
	/// </summary>
	///	<remarks>
	///		DUE TO A BUG IN VISUAL STUDIO, THIS CLASS CAN'T BE MARKED ABSTRACT AS IT SHOULD BE IF
	///		YOU INTEND TO USE THE SERVICE DESIGNER.  ONCE THE BUG IS FIXED, THIS CLASS SHOULD BE
	///		MARKED ABSTRACT.
	///	</remarks>
	public class ServiceProcessorBase : IDisposable
	{
		/// <summary>
		///		Provides formatted messages to subscribers, like a ListView control in a WPF window.
		/// </summary>
		public event MessageEventHandler? Message;

		/// <summary>Gets the <see cref="T:OscLog"/> object.</summary>
		protected OscLog Log { get; private set; }

		/// <summary>Gets the current state of the processor.</summary>
		public ServiceProcessorState State { get; protected set; } = ServiceProcessorState.Created;


		#region Constructors

		/// <summary>
		///		!!! DO NOT USE !!!
		/// </summary>
		///	<remarks>
		///		DUE TO A BUG IN VISUAL STUDIO, THIS CONSTRUCTOR IS REQUIRED IN ORDER FOR THE SERVICE
		///		DESIGNER TO WORK.  ONCE THE BUG IS FIXED, THIS CONSTRUCTOR SHOULD BE DELETED.
		///	</remarks>
		[Obsolete("Use of this constructor is not allowed.", true)]
		public ServiceProcessorBase() : this(OscLog.Instance) { }

		/// <summary>
		///		Creates an instance of ServiceProcessorBase using the specified
		///		<see cref="T:OscLog"/> object for messaging.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> log to use.</param>
		public ServiceProcessorBase(OscLog log)
		{
			Log = log;
			Log.Message += OscLog_Message;
		}

		/// <summary>
		///		Creates an instance of ServiceProcessorBase using the specified logger for
		///		messaging.
		/// </summary>
		/// <param name="loggerName">The name of the logger to use.</param>
		public ServiceProcessorBase(string loggerName)
			: this(new OscLog(loggerName)) { }

		#endregion

		#region IDisposable Implementation

#if USES_UNMANGED_CODE
		/// <summary>
		///		Destructor this instance.
		/// </summary>
		~ServiceProcessorBase()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(false);
		}
#endif

		/// <summary>Gets a value indicating that this instance has been disposed.</summary>
		protected bool Disposed { get; private set; }

		/// <summary>
		///     Dispose and release all resources used by this instance.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);

#if USES_UNMANGED_CODE
			GC.SuppressFinalize(this);
#endif
		}

		/// <summary>
		///		Dispose(bool disposing) executes in two distinct scenarios.  If disposing equals
		///		<b>true</b>, <see cref="M:Dispose()"/> has been called directly or indirectly
		///		by a user's code.  Managed and unmanaged resources can be disposed.  If disposing
		///		equals <b>false</b>, <see cref="M:Dispose()"/> has been called by the runtime from
		///		inside the finalizer and you should not reference other objects.  Only unmanaged
		///		resources can be disposed.
		/// </summary>
		/// <param name="disposing"><b>true</b> when <see cref="M:Dispose()"/> has been called
		///		directly or indirectly by a user's code.  <b>false</b> when <see cref="M:Dispose()"/>
		///		has been called by the runtime from inside the finalizer and you should not
		///		reference other objects.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!Disposed)
			{
				if (disposing)
				{
					// Dispose managed state (managed objects).
					Log.Message -= OscLog_Message;
				}

#if USES_UNMANGED_CODE
				// Free unmanaged resources (unmanaged objects) and override a finalizer below.
				// Set large fields to null.
#endif

				Disposed = true;
			}
		}

		#endregion

		#region Event Handlers

		private void OscLog_Message(object sender, MessageEventArgs e)
		{
			string message = e.ToString();

			Message?.Invoke(this, new MessageEventArgs(e.LocationInfo, e.MessageLogEntryType, message));

			//Debug.WriteLine($"ServiceProcessorBase.OscLog_Message: {message}");
		}

		#endregion
	}
}
