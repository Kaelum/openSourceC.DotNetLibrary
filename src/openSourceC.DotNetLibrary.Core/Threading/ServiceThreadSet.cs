using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using ThreadState = System.Threading.ThreadState;

namespace openSourceC.DotNetLibrary.Threading
{
	/// <summary>
	///		Summary description for ServiceThreadSet.
	/// </summary>
	public class ServiceThreadSet : IDisposable
	{
		private const int SLEEP_10_MILLISECONDS = 10;
		private const int SLEEP_100_MILLISECONDS = 100;

		private readonly Dictionary<Guid, ServiceThread> _serviceThreads;


		#region Constructors

		/// <summary>
		///		Create and instance of ServiceThreadSet.
		/// </summary>
		public ServiceThreadSet()
		{
			_serviceThreads = new Dictionary<Guid, ServiceThread>();
		}

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///		This destructor will run only if the Dispose method does not get called.
		/// </summary>
		/// <remarks>Do not provide destructors in types derived from this class.</remarks>
		~ServiceThreadSet()
		{
			Dispose(false);
		}

		/// <summary>Gets a value indicating that this instance has been disposed.</summary>
		protected bool Disposed { get; private set; }

		/// <summary>
		///		Dispose of this object.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
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
			// Check to see if Dispose has already been called.
			if (!Disposed)
			{
				Disposed = true;

				// If disposing equals true, dispose of managed resources.
				if (disposing)
				{
					Thread currentThread = Thread.CurrentThread;

					Debug.WriteLine($"{nameof(ServiceThreadSet)}-DISPOSE-START ({currentThread.ManagedThreadId.ToString()})");

					while (_serviceThreads.Count != 0)
					{
						Join(10);
					}

					Debug.WriteLine($"{nameof(ServiceThreadSet)}-DISPOSE-END ({currentThread.ManagedThreadId.ToString()})");
				}

				// Dispose of unmanaged resources.
			}
		}

		#endregion

		#region Public Properties

		/// <summary>Gets a value indicating whether all threads are alive.</summary>
		public bool AllAlive => _serviceThreads.All(t => (t.Value.Thread.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0);

		/// <summary>Gets a value indicating whether any thread is alive.</summary>
		public bool AnyAlive => _serviceThreads.Any(t => (t.Value.Thread.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0);

		/// <summary>Gets a value indicating whether all threads are fully operational.</summary>
		public bool AllFullyOperational => _serviceThreads.All(t => t.Value.Service.IsFullyOperational);

		/// <summary>Gets the number of threads.</summary>
		public int Count => _serviceThreads.Count;

		#endregion

		#region Public Methods

		/// <summary>
		///		Add the specified <see cref="T:IService"/> object to the service thread set and
		///		start executing the <see cref="M:IService.Execute()"/> method.
		/// </summary>
		/// <param name="service">The <see cref="T:IService"/> object.</param>
		/// <returns>
		///		The unique service thread instance identifier.
		/// </returns>
		public Guid AddStart(IService service)
		{
			ServiceThread serviceThread;

			lock (((ICollection)_serviceThreads).SyncRoot)
			{
				serviceThread = new ServiceThread(
					service ?? throw new ArgumentNullException(nameof(service)),
					new Thread(service.Execute)
					{
						//Name = "ServiceMethod Processing",
					}
				);

				serviceThread.Thread.Start();

				_serviceThreads.Add(serviceThread.InstanceId, serviceThread);
			}

			return serviceThread.InstanceId;
		}

		/// <summary>
		///		Blocks the calling thread until each thread represented by this instance terminates,
		///		while continuing to perform standard COM and SendMessage pumping.
		/// </summary>
		public void Join()
		{
			lock (((ICollection)_serviceThreads).SyncRoot)
			{
				foreach (ServiceThread serviceThread in _serviceThreads.Values.ToArray())
				{
					serviceThread.Thread.Join();

					_serviceThreads.Remove(serviceThread.InstanceId);
				}
			}
		}

		/// <summary>
		///		Blocks the calling thread until the specified service thread instance terminates,
		///		while continuing to perform standard COM and SendMessage pumping.
		/// </summary>
		/// <param name="instanceId">The unique service thread instance identifier to remove.</param>
		/// <returns>
		///		<b>true</b> if the service thread instance is successfully removed; otherwise,
		///		<b>false</b>. This method also returns false if the instance was not found.
		/// </returns>
		public bool Join(Guid instanceId)
		{
			bool removed = false;

			lock (((ICollection)_serviceThreads).SyncRoot)
			{
				if (_serviceThreads.TryGetValue(instanceId, out ServiceThread? serviceThread))
				{
					serviceThread.Thread.Join();

					removed = _serviceThreads.Remove(instanceId);
				}
			}

			return removed;
		}

		/// <summary>
		///		Blocks the calling thread until each thread represented by this instance terminates
		///		or the specified time elapses for each thread, while continuing to perform standard
		///		COM and SendMessage pumping.
		/// </summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for each thread to
		///		terminate.</param>
		///	<returns>
		///		<b>true</b> if all of the threads have terminated; <b>false</b> if any thread has
		///		not terminated after the amount of time specified by the
		///		<paramref name="millisecondsTimeout"/> parameter has elapsed.
		///	</returns>
		public bool Join(int millisecondsTimeout)
		{
			bool terminated = true;

			lock (((ICollection)_serviceThreads).SyncRoot)
			{
				foreach (ServiceThread serviceThread in _serviceThreads.Values.ToArray())
				{
					if (serviceThread.Thread.Join(millisecondsTimeout))
					{
						_serviceThreads.Remove(serviceThread.InstanceId);
					}
					else
					{
						terminated = false;
					}
				}
			}

			return terminated;
		}

		/// <summary>
		///		Blocks the calling thread until each thread represented by this instance terminates
		///		or the specified time elapses for each thread, while continuing to perform standard
		///		COM and SendMessage pumping.
		/// </summary>
		/// <param name="timeout">A TimeSpan set to the amount of time to wait for each thread to
		///		terminate.</param>
		///	<returns>
		///		<b>true</b> if all of the threads have terminated; <b>false</b> if any thread has
		///		not terminated after the amount of time specified by the <paramref name="timeout"/>
		///		parameter has elapsed.
		///	</returns>
		public bool Join(TimeSpan timeout)
		{
			bool terminated = true;

			lock (((ICollection)_serviceThreads).SyncRoot)
			{
				foreach (ServiceThread serviceThread in _serviceThreads.Values.ToArray())
				{
					if (serviceThread.Thread.Join(timeout))
					{
						_serviceThreads.Remove(serviceThread.InstanceId);
					}
					else
					{
						terminated = false;
					}
				}
			}

			return terminated;
		}

		#endregion
	}
}
