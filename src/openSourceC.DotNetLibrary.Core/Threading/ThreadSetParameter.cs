using System;
using System.Threading;

namespace openSourceC.DotNetLibrary.Threading
{
	/// <summary>
	///		Summary description for ThreadSetParameter.
	/// </summary>
	public class ThreadSetParameter<TParamter> : ThreadSetParameter
	{
		/// <summary>
		///		Create an instance of ThreadSetParameter.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <param name="countdownEvent"></param>
		/// <param name="parameter"></param>
		public ThreadSetParameter(
			CancellationToken cancellationToken,
			CountdownEvent countdownEvent,
			TParamter parameter
		) : base(
			cancellationToken,
			countdownEvent
		)
		{
			Parameter = parameter;
		}

		/// <summary></summary>
		public TParamter Parameter { get; private set; }
	}

	/// <summary>
	///		Summary description for ThreadSetParameter.
	/// </summary>
	public class ThreadSetParameter
	{
		private readonly CountdownEvent _countdownEvent;
		private bool _fullyOperationalSignaled;
		private bool _shutdownSignaled;


		/// <summary>
		///		Create an instance of ThreadSetParameter.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <param name="countdownEvent"></param>
		public ThreadSetParameter(
			CancellationToken cancellationToken,
			CountdownEvent countdownEvent
		)
		{
			CancellationToken = cancellationToken;

			_countdownEvent = countdownEvent;
			_fullyOperationalSignaled = false;
		}

		/// <summary></summary>
		public CancellationToken CancellationToken { get; private set; }

		/// <summary></summary>
		public bool FullyOperationalSignaled => _fullyOperationalSignaled;

		/// <summary></summary>
		public bool ShutdownSignaled => _shutdownSignaled;

		/// <summary>
		///		Signal that the thread is fully operational.
		/// </summary>
		/// <remarks>
		///		Can only be signaled once.  Successive signals are ignored.
		/// </remarks>
		public void SignalFullyOperational()
		{
			if (!_fullyOperationalSignaled)
			{
				lock (_countdownEvent)
				{
					if (!_fullyOperationalSignaled && !_shutdownSignaled)
					{
						_countdownEvent.Signal();
					}

					if (!_fullyOperationalSignaled)
					{
						_fullyOperationalSignaled = true;
					}
				}
			}
		}

		/// <summary>
		///		Signal that the thread has shutdown.
		/// </summary>
		/// <remarks>
		///		Can only be signaled once.  Successive signals are ignored.
		/// </remarks>
		public void SignalShutdown()
		{
			if (!_shutdownSignaled)
			{
				lock (_countdownEvent)
				{
					if (!_fullyOperationalSignaled && !_shutdownSignaled)
					{
						_countdownEvent.Signal();
					}

					if (!_shutdownSignaled)
					{
						_shutdownSignaled = true;
					}
				}
			}
		}
	}
}
