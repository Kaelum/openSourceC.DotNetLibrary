using System;
using System.ServiceProcess;

namespace openSourceC.StandardLibrary.ServiceProcess
{
	/// <summary>
	///		Provides a base API for application access to Windows service events.
	/// </summary>
	public interface IServiceApplicationBase : IDisposable
	{
		#region Service Events

		/// <summary>
		///		Provides messages to subscribers.
		/// </summary>
		event MessageEventHandler Message;

		#endregion

		#region Service Properties

		/// <summary>Gets the current state action in effect.</summary>
		ServiceStateAction Action { get; }

		/// <summary>Gets the current state of the processor.</summary>
		ServiceControllerStatus Status { get; }

		#endregion

		#region Service Action Methods

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnContinue"/> command being sent to the service by the Service Control
		///		Manager (SCM).
		/// </summary>
		void ExecuteOnContinue();

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnCustomCommand"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		/// <param name="command"></param>
		void ExecuteOnCustomCommand(int command);

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnPause"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		void ExecuteOnPause();

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnPowerEvent"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		/// <param name="powerStatus"></param>
		/// <returns></returns>
		bool ExecuteOnPowerEvent(PowerBroadcastStatus powerStatus);

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnStop"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		/// <param name="changeDescription"></param>
		void ExecuteOnSessionChange(SessionChangeDescription changeDescription);

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnShutdown"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		void ExecuteOnShutdown();

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnStart"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		/// <param name="args"></param>
		void ExecuteOnStart(string[] args);

		/// <summary>
		///		Emulates an <see cref="ServiceBase.OnStop"/> command being sent to the service by the Service Control Manager (SCM).
		/// </summary>
		void ExecuteOnStop();

		#endregion
	}
}
