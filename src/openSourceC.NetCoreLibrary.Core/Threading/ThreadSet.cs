using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace openSourceC.NetCoreLibrary.Threading
{
	/// <summary>
	///		Summary description for ThreadSet.
	/// </summary>
	public class ThreadSet : IDisposable
	{
		private readonly CancellationTokenSource _cancellationTokenSource;
		private readonly List<ThreadSetItem> _threads;
		private readonly CountdownEvent _servicesCountdownEvent;


		#region Constructors

		/// <summary>
		///		Create an instance of ThreadSet.
		/// </summary>
		public ThreadSet()
		{
			_cancellationTokenSource = new();
			_threads = new List<ThreadSetItem>();

			// Prime the countdown with 1 event, which will be signaled in the Start() method.
			_servicesCountdownEvent = new CountdownEvent(1);
		}

		/// <summary>
		///		Create an instance of ThreadSet.
		/// </summary>
		/// <param name="cancellationTokenSource"></param>
		public ThreadSet(CancellationTokenSource cancellationTokenSource)
		{
			_cancellationTokenSource = cancellationTokenSource;
			_threads = new List<ThreadSetItem>();

			// Prime the countdown with 1 event, which will be signaled in the Start() method.
			_servicesCountdownEvent = new CountdownEvent(1);
		}

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///		This destructor will run only if the Dispose method does not get called.
		/// </summary>
		/// <remarks>Do not provide destructors in types derived from this class.</remarks>
		~ThreadSet()
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

					Debug.WriteLine($"{nameof(ThreadSet)}-DISPOSE-START ({currentThread.ManagedThreadId.ToString()})");

					if ((_threads.Count != 0))
					{
						Cancel();
					}

					while (_threads.Count != 0)
					{
						Join(10);
					}

					// Dispose of managed resources.
					_servicesCountdownEvent.Dispose();

					Debug.WriteLine($"{nameof(ThreadSet)}-DISPOSE-END ({currentThread.ManagedThreadId.ToString()})");
				}

				// Dispose of unmanaged resources.
			}
		}

		#endregion

		#region Public Properties

		/// <summary>Gets a value indicating whether all threads are alive.</summary>
		public bool AllAlive => _threads.All(t => t.Thread.IsAlive);

		/// <summary>Gets a value indicating whether any thread is alive.</summary>
		public bool AnyAlive => _threads.Any(t => t.Thread.IsAlive);

		/// <summary>Gets the number of threads.</summary>
		public int Count => _threads.Count;

		#endregion

		#region Public Methods

		/// <summary>
		///		Communicates a request to cancel all threads.
		/// </summary>
		public void Cancel()
		{
			_cancellationTokenSource.Cancel();
		}

		/// <summary>
		///		Create a new <see cref="T:Thread"/> using the specified <see cref="T:ThreadStart"/>
		///		and add it to the set.
		/// </summary>
		/// <remarks>
		///		Each thread should signal the <see cref="T:CountdownEvent"/> once, and only once, to
		///		indicate that the thread has successfully started.  The <see cref="M:Wait()"/>
		///		methods can then be used to allow the system to wait until all threads are fully
		///		operational.
		///		<para>NOTE: There is no guarantee that all threads will successfully start.</para>
		/// </remarks>
		/// <param name="start">A <see cref="T:ParameterizedThreadStart"/> delegate that represents
		///		the method to be invoked when this thread begins executing.</param>
		/// <param name="name">The name of the thread, or <b>null</b>. (Optional, defaults to null)</param>
		public void CreateAdd(ParameterizedThreadStart start, string? name = null)
		{
			// Add 1 to the countdown for the thread being added.
			if (!_servicesCountdownEvent.TryAddCount())
			{
				throw new InvalidOperationException("Unable to increment countdown events.");
			}

			if (start == null)
			{
				throw new ArgumentNullException(nameof(start));
			}

			lock (((ICollection)_threads).SyncRoot)
			{
				ThreadSetItem threadSetItem = new(
					new Thread(start)
					{
						Name = (name is not null ? name : start.Method.Name),
					}
				);

				_threads.Add(threadSetItem);
			}
		}

		/// <summary>
		///		Create a new <see cref="T:Thread"/> using the specified <see cref="T:ThreadStart"/>
		///		and add it to the set.
		/// </summary>
		/// <remarks>
		///		Each thread should signal the <see cref="T:CountdownEvent"/> once, and only once, to
		///		indicate that the thread has successfully started.  The <see cref="M:Wait()"/>
		///		methods can then be used to allow the system to wait until all threads are fully
		///		operational.
		///		<para>NOTE: There is no guarantee that all threads will successfully start.</para>
		/// </remarks>
		/// <param name="start">A <see cref="T:ParameterizedThreadStart"/> delegate that represents
		///		the method to be invoked when this thread begins executing.</param>
		/// <param name="parameter">The parameter object to pass to the invoked method.</param>
		/// <param name="name">The name of the thread, or <b>null</b>. (Optional, defaults to null)</param>
		public void CreateAdd<TParameter>(ParameterizedThreadStart start, TParameter parameter, string? name = null)
		{
			// Add 1 to the countdown for the thread being added.
			if (!_servicesCountdownEvent.TryAddCount())
			{
				throw new InvalidOperationException("Unable to increment countdown events.");
			}

			if (start == null)
			{
				throw new ArgumentNullException(nameof(start));
			}

			lock (((ICollection)_threads).SyncRoot)
			{
				ParameterizedThreadSetItem<TParameter> threadSetItem = new(
					new Thread(start)
					{
						Name = (name is not null ? name : start.Method.Name),
					},
					parameter
				);

				_threads.Add(threadSetItem);
			}
		}

		/// <summary>
		///		Blocks the calling thread until each thread represented by this instance terminates,
		///		while continuing to perform standard COM and SendMessage pumping.
		/// </summary>
		public void Join()
		{
			lock (((ICollection)_threads).SyncRoot)
			{
				for (int i = _threads.Count; i-- != 0;)
				{
					_threads[i].Thread.Join();
					_threads.RemoveAt(i);
				}
			}
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

			lock (((ICollection)_threads).SyncRoot)
			{
				for (int i = _threads.Count; i-- != 0;)
				{
					Thread thread = _threads[i].Thread;

					if (thread.Join(millisecondsTimeout))
					{
						_threads.RemoveAt(i);
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

			lock (((ICollection)_threads).SyncRoot)
			{
				for (int i = _threads.Count; i-- != 0;)
				{
					Thread thread = _threads[i].Thread;

					if (thread.Join(timeout))
					{
						_threads.RemoveAt(i);
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
		///		Close adds and start threads.
		/// </summary>
		public void Start()
		{
			_servicesCountdownEvent.Signal();

			lock (((ICollection)_threads).SyncRoot)
			{
				foreach (ThreadSetItem threadSetItem in _threads)
				{
					ThreadSetParameter threadSetParameter = threadSetItem.GetThreadSetParameter(_cancellationTokenSource.Token, _servicesCountdownEvent);

					threadSetItem.Thread.Start(threadSetParameter);
				}
			}
		}

		/// <summary>
		///		Blocks the current thread until all of the threads have signaled their readiness
		///		while observing the <see cref="T:CancellationToken"/>.
		/// </summary>
		/// <param name="cancellationToken"></param>
		public void Wait(CancellationToken cancellationToken)
		{
			_servicesCountdownEvent.Wait(cancellationToken);
		}

		/// <summary>
		///		Blocks the current thread until all of the threads have signaled their readiness, or
		///		the timeout expires.
		/// </summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or
		///		<see cref="P:Timeout.Infinite"/>(-1) to wait indefinitely.</param>
		///	<returns>
		///		<b>true</b> if all of the threads have signaled their readiness; otherwise,
		///		<b>false</b>.
		///	</returns>
		public bool Wait(int millisecondsTimeout)
		{
			return _servicesCountdownEvent.Wait(millisecondsTimeout);
		}

		/// <summary>
		///		Blocks the current thread until all of the threads have signaled their readiness, or
		///		the timeout expires, while observing the <see cref="T:CancellationToken"/>.
		/// </summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or
		///		<see cref="P:Timeout.Infinite"/>(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken"></param>
		///	<returns>
		///		<b>true</b> if all of the threads have signaled their readiness; otherwise,
		///		<b>false</b>.
		///	</returns>
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			return _servicesCountdownEvent.Wait(millisecondsTimeout, cancellationToken);
		}

		#endregion

		#region Private Classes

		private class ParameterizedThreadSetItem<TParameter> : ThreadSetItem
		{
			public ParameterizedThreadSetItem(Thread thread, TParameter parameter)
				: base(thread)
			{
				Parameter = parameter;
			}

			public TParameter Parameter;

			public override ThreadSetParameter GetThreadSetParameter(CancellationToken cancellationToken, CountdownEvent countdownEvent)
			{
				return new ThreadSetParameter<TParameter>(cancellationToken, countdownEvent, Parameter);
			}
		}

		private class ThreadSetItem
		{
			public ThreadSetItem(Thread thread)
			{
				Thread = thread;
			}

			public Thread Thread;

			public virtual ThreadSetParameter GetThreadSetParameter(CancellationToken cancellationToken, CountdownEvent countdownEvent)
			{
				return new ThreadSetParameter(cancellationToken, countdownEvent);
			}
		}

		#endregion
	}
}
