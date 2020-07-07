using System;

namespace openSourceC.NetCoreLibrary.Threading
{
	/// <summary>
	///		Summary description for IService.
	/// </summary>
	public interface IService
	{
		/// <summary>Gets a value indicating that the processor is fully operational.</summary>
		bool InitializationComplete { get; }


		/// <summary>
		///		Execute the processor.
		/// </summary>
		void Execute();
	}
}
