using System;

namespace openSourceC.DotNetLibrary.Threading
{
	/// <summary>
	///		Summary description for ThreadSetItemStatus.
	/// </summary>
	public class ThreadSetItemStatus
	{
		private readonly ThreadSetItem _item;


		/// <summary>
		///		Create an instance of ThreadSetItemStatus.
		/// </summary>
		/// <param name="item"></param>
		public ThreadSetItemStatus(ThreadSetItem item)
		{
			_item = item;
		}

		/// <summary>Gets the <see cref="T:Thread"/>.</summary>
		public string Name => _item.Thread.Name ?? "<noname>";

		/// <summary>Gets a value indicating that the execution status of the thread.</summary>
		public bool IsActive => _item.Thread.IsAlive;

		/// <summary>Gets a value indicating that fully operational was signaled.</summary>
		public bool FullyOperationalSignaled => _item.FullyOperationalSignaled;

		/// <summary>Gets a value indicating that shutdown was signaled.</summary>
		public bool ShutdownSignaled => _item.ShutdownSignaled;
	}
}
