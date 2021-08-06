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
		///		Create and instance of ThreadSetParameter.
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
		/// <summary>
		///		Create and instance of ThreadSetParameter.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <param name="countdownEvent"></param>
		public ThreadSetParameter(
			CancellationToken cancellationToken,
			CountdownEvent countdownEvent
		)
		{
			CancellationToken = cancellationToken;
			CountdownEvent = countdownEvent;
		}

		/// <summary></summary>
		public CancellationToken CancellationToken { get; private set; }

		/// <summary></summary>
		public CountdownEvent CountdownEvent { get; private set; }
	}
}
