using System;

namespace openSourceC.NetCoreLibrary.ServiceProcess
{
	/// <summary>
	///		Describes the current state of the processor.
	/// </summary>
	public enum ServiceProcessorState
	{
		/// <summary>The processor continue is pending.</summary>
		ContinuePending = 5,

		/// <summary>The processor has been initialized but has not yet been started.</summary>
		Created = 0,

		/// <summary>The processor is paused.</summary>
		Paused = 7,

		/// <summary>The processor pause is pending.</summary>
		PausePending = 6,

		/// <summary>The processor is running.</summary>
		Running = 4,

		/// <summary>The processor is starting.</summary>
		StartPending = 2,

		/// <summary>The processor is stopped.</summary>
		Stopped = 1,

		/// <summary>The processor is stopping.</summary>
		StopPending = 3,
	}

	/// <summary>
	///		The service state actions.
	/// </summary>
	public enum ServiceStateAction
	{
		/// <summary>Place the service into a paused state.</summary>
		Pause = 1,

		/// <summary>Place the service into a running state.</summary>
		Run = 2,

		/// <summary>Place the service into a stopped state.</summary>
		Stop = 0,
	}
}
