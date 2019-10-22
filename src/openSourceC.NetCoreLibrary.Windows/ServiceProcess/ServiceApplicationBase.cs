using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

using openSourceC.StandardLibrary.Threading;

namespace openSourceC.StandardLibrary.ServiceProcess
{
	/// <summary>
	///		Summary description for ServiceApplicationBase.
	/// </summary>
	///	<remarks>
	///		DUE TO A BUG IN VISUAL STUDIO, THIS CLASS CAN'T BE MARKED ABSTRACT AS IT SHOULD BE IF
	///		YOU INTEND TO USE THE SERVICE DESIGNER.  ONCE THE BUG IS FIXED, THIS CLASS SHOULD BE
	///		MARKED ABSTRACT.
	///	</remarks>
	public class ServiceApplicationBase : ServiceBase, IServiceApplicationBase
	{
		/// <summary>
		///		Provides messages to subscribers.
		/// </summary>
		public event MessageEventHandler Message;

		/// <summary>Gets the <see cref="T:OscLog"/> object.</summary>
		protected OscLog Log { get; private set; }

		/// <summary>Gets or sets the current state action in effect.</summary>
		public ServiceStateAction Action { get; protected set; } = ServiceStateAction.Stop;

		private Semaphore _singleUse;
		private readonly object _singleUseLock = new object();

		private SortedList<Thread, ServiceProcessorBase> _processorThreads;
		private readonly object _processorThreadsLock = new object();


		#region Constructors

		/// <summary>
		///		DO NOT USE THIS CONSTRUCTOR!
		/// </summary>
		///	<remarks>
		///		DUE TO A BUG IN VISUAL STUDIO, THIS CONSTRUCTOR IS REQUIRED IN ORDER FOR THE SERVICE
		///		DESIGNER TO WORK.  ONCE THE BUG IS FIXED, THIS CONSTRUCTOR SHOULD BE DELETED.
		///	</remarks>
		[Obsolete("Use of this constructor is not supported.", true)]
		public ServiceApplicationBase() { }

		/// <summary>
		///		Creates a ServiceApplicationBase object.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> log to use.</param>
		public ServiceApplicationBase(OscLog log)
		{
			Log = log;

			// Forward messages from the logger to the Message event.
			Log.Message += OscLog_Message;
		}

		/// <summary>
		///		Constructor
		/// </summary>
		/// <param name="loggerName">The name of the logger to use.</param>
		public ServiceApplicationBase(string loggerName)
			: this(new OscLog(loggerName)) { }

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///		Disposes of the resources (other than memory) used by the <see cref="T:ServiceBase"/>.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		///		<b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Dispose of managed resources.
				if (_singleUse != null)
				{
					lock (_singleUseLock)
					{
						if (_singleUse != null)
						{
							_singleUse.Dispose();
							_singleUse = null;
						}
					}
				}

				if (_processorThreads != null)
				{
					lock (_processorThreadsLock)
					{
						while (_processorThreads?.Count != 0)
						{
							foreach (KeyValuePair<Thread, ServiceProcessorBase> processorThread in _processorThreads.ToArray())
							{
								if (!processorThread.Key.IsAlive || processorThread.Key.Join(10))
								{
									processorThread.Value.Dispose();

									_processorThreads.Remove(processorThread.Key);
								}
							}

							Thread.Sleep(100);
						}

						_processorThreads = null;
					}
				}

				// Remove the forwarding of messages from the logger to the Message event.
				Log.Message -= OscLog_Message;

				Message = null;
				Log = null;
			}

			// Dispose of unmanaged resources.

			base.Dispose(disposing);
		}

		#endregion

		#region Single Use Lock

		/// <summary>
		///		Gets the current status of the single use lock.
		/// </summary>
		/// <returns>
		///		<b>true</b> if the current thread owns the lock; otherwise, <b>false</b>.
		/// </returns>
		public bool SingleUseLockStatus
		{
			get
			{
				if (_singleUse == null) { return false; }

				return _singleUse.WaitOne(0);
			}
		}

		/// <summary>
		///		Blocks the current thread until the current instance obtains the single use lock,
		///		using a 32-bit signed integer to specify the time interval.
		/// </summary>
		/// <returns>
		///		<b>true</b> if successful; otherwise, <b>false</b>.
		/// </returns>
		public bool SingleUseLockWaitOne(int millisecondsTimeout)
		{
			if (_singleUse == null)
			{
				lock (_singleUseLock)
				{
					if (_singleUse == null)
					{
						// ServiceName is set after the constructor is executed.
						Type type = GetType();
						string singleUseObjectName = string.Format(@"Global\{0}: {1}, {2}", ServiceName, type.FullName, type.Assembly.GetName().Name);

						_singleUse = new Semaphore(1, 1, singleUseObjectName);
					}
				}
			}

			return _singleUse.WaitOne(millisecondsTimeout);
		}

		/// <summary>
		///		Releases the single use lock.
		/// </summary>
		public void SingleUseLockRelease()
		{
			if (_singleUse == null)
			{
				throw new InvalidOperationException("A single use lock was never requested.");
			}

			_singleUse.Release();
		}

		#endregion

		#region Processor Threads

		/// <summary>
		///		Gets the list of processor threads.
		/// </summary>
		public SortedList<Thread, ServiceProcessorBase> ProcessorThreads
		{
			get
			{
				if (_processorThreads == null)
				{
					lock (_processorThreadsLock)
					{
						if (_processorThreads == null)
						{
							_processorThreads = new SortedList<Thread, ServiceProcessorBase>(new CompareThreadByManagedThreadId());
						}
					}
				}

				return _processorThreads;
			}
		}

		/// <summary>
		///		Gets a value indicating the initialization status of <see cref="P:ProcessorThreads"/>.
		/// </summary>
		public bool ProcessorThreadsInitialized
		{
			get { return _processorThreads != null; }
		}

		#endregion

		#region ServiceBase Implementations

		/// <summary>
		///		Executes when a Stop command is sent to the service by the Service Control Manager (SCM).
		/// </summary>
		protected override void OnStop()
		{
			for (bool firstPass = true; _processorThreads?.Count != 0; firstPass = false)
			{
				// Itterate through an array copy to allow the removal of objects from the list.
				foreach (KeyValuePair<Thread, ServiceProcessorBase> processorThread in _processorThreads.ToArray())
				{
					if (firstPass && processorThread.Key.IsAlive)
					{
						Log.Info("Thread.Join: {0}", (string.IsNullOrWhiteSpace(processorThread.Key.Name) ? processorThread.Key.ManagedThreadId.ToString() : processorThread.Key.Name));
					}

					if (!processorThread.Key.IsAlive || processorThread.Key.Join(10))
					{
						processorThread.Value.Dispose();

						_processorThreads.Remove(processorThread.Key);
					}
				}

				Thread.Sleep(100);
			}

			_processorThreads = null;

			base.OnStop();
		}

		#endregion

		#region Explicit IServiceApplicationBase Implementations

		/// <summary>Gets the current state of the processor.</summary>
		public ServiceControllerStatus Status
		{
			get
			{
				if (Action == ServiceStateAction.Stop)
				{
					return (
						!ProcessorThreadsInitialized || ProcessorThreads.All(t => !t.Key.IsAlive)
						? ServiceControllerStatus.Stopped
						: ServiceControllerStatus.StopPending
					);
				}
				else if (Action == ServiceStateAction.Pause)
				{
					return (
						ProcessorThreadsInitialized && ProcessorThreads.All(t => t.Key.IsAlive)
						? ServiceControllerStatus.Paused
						: ServiceControllerStatus.PausePending
					);
				}
				else
				{
					if (!ProcessorThreadsInitialized || !ProcessorThreads.All(t => t.Key.IsAlive))
					{
						return ServiceControllerStatus.StartPending;
					}

					return (
						ProcessorThreads.Any(t => t.Value.State == ServiceProcessorState.ContinuePending)
						? ServiceControllerStatus.ContinuePending
						: ServiceControllerStatus.Running
					);
				}
			}
		}

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnContinue"/> command being sent to the service by the Service Control
		///		Manager (SCM).
		/// </summary>
		void IServiceApplicationBase.ExecuteOnContinue()
		{
			OnContinue();
		}

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnCustomCommand"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		/// <param name="command"></param>
		void IServiceApplicationBase.ExecuteOnCustomCommand(int command)
		{
			OnCustomCommand(command);
		}

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnPause"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		void IServiceApplicationBase.ExecuteOnPause()
		{
			OnPause();
		}

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnPowerEvent"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		/// <param name="powerStatus"></param>
		/// <returns></returns>
		bool IServiceApplicationBase.ExecuteOnPowerEvent(PowerBroadcastStatus powerStatus)
		{
			return OnPowerEvent(powerStatus);
		}

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnStop"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		/// <param name="changeDescription"></param>
		void IServiceApplicationBase.ExecuteOnSessionChange(SessionChangeDescription changeDescription)
		{
			OnSessionChange(changeDescription);
		}

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnShutdown"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		void IServiceApplicationBase.ExecuteOnShutdown()
		{
			OnShutdown();
		}

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnStart"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		/// <param name="args"></param>
		void IServiceApplicationBase.ExecuteOnStart(string[] args)
		{
			OnStart(args);
		}

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnStop"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		void IServiceApplicationBase.ExecuteOnStop()
		{
			OnStop();
		}

		#endregion

		#region Event Handlers

		private void OscLog_Message(object sender, MessageEventArgs e)
		{
			string message = e.ToString();

			Message?.Invoke(this, new MessageEventArgs(e.LocationInfo, e.MessageLogEntryType, message));

			//Debug.WriteLine($"ServiceApplicationBase: {message}");
		}

		#endregion
	}
}
