using System;
using System.Threading;

namespace openSourceC.DotNetLibrary.Threading
{
	/// <summary>
	///		Summary description for ParameterizedThreadSetItem.
	/// </summary>
	/// <typeparam name="TParameter"></typeparam>
	public class ParameterizedThreadSetItem<TParameter> : ThreadSetItem
	{
		private readonly TParameter _parameter;


		#region Constructors

		/// <summary>
		///		Create an instance of ParameterizedThreadSetItem.
		/// </summary>
		/// <param name="thread"></param>
		/// <param name="parameter"></param>
		public ParameterizedThreadSetItem(Thread thread, TParameter parameter)
			: base(thread)
		{
			_parameter = parameter;
		}

		#endregion

		#region Public Properties

		/// <summary>Gets the <see cref="T:ThreadSetItem"/> parameter.</summary>
		public TParameter Parameter => _parameter;

		#endregion

		#region Public Methods

		/// <summary>
		///		Get the <see cref="T:ThreadSetParameter"/>.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <param name="countdownEvent"></param>
		/// <returns></returns>
		public override ThreadSetParameter GetThreadSetParameter(CancellationToken cancellationToken, CountdownEvent countdownEvent)
		{
			if (threadSetParameter is null)
			{
				lock (thread)
				{
					if (threadSetParameter is null)
					{
						threadSetParameter = new ThreadSetParameter<TParameter>(cancellationToken, countdownEvent, Parameter);
					}
				}
			}

			return threadSetParameter;
		}

		#endregion
	}

	/// <summary>
	///		Summary description for ThreadSetItem.
	/// </summary>
	public class ThreadSetItem
	{
		/// <summary></summary>
		protected readonly Thread thread;
		/// <summary></summary>
		protected ThreadSetParameter? threadSetParameter;


		/// <summary>
		///		Create an instance of ThreadSetItem.
		/// </summary>
		/// <param name="thread"></param>
		public ThreadSetItem(Thread thread)
		{
			this.thread = thread;
		}

		/// <summary>Gets the <see cref="T:Thread"/>.</summary>
		public Thread Thread => thread;

		/// <summary>Gets a value indicating that fully operational was signaled.</summary>
		public bool FullyOperationalSignaled => threadSetParameter?.FullyOperationalSignaled ?? false;

		/// <summary>Gets a value indicating that shutdown was signaled.</summary>
		public bool ShutdownSignaled => threadSetParameter?.ShutdownSignaled ?? false;

		/// <summary>
		///		Get the <see cref="T:ThreadSetParameter"/>.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <param name="countdownEvent"></param>
		/// <returns></returns>
		public virtual ThreadSetParameter GetThreadSetParameter(CancellationToken cancellationToken, CountdownEvent countdownEvent)
		{
			if (threadSetParameter is null)
			{
				lock (thread)
				{
					if (threadSetParameter is null)
					{
						threadSetParameter = new ThreadSetParameter(cancellationToken, countdownEvent);
					}
				}
			}

			return threadSetParameter;
		}
	}
}
