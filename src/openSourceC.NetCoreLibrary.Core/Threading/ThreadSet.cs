using System;
using System.Collections.Generic;
using System.Threading;

namespace openSourceC.NetCoreLibrary.Threading
{
	/// <summary>
	///		Summary description for ThreadSet.
	/// </summary>
	public class ThreadSet : SortedSet<Thread>
	{
		/// <summary>
		///		Create and instance of ThreadSet.
		/// </summary>
		public ThreadSet()
			: base(new CompareThreadByManagedThreadId()) { }

		/// <summary>
		///		Create a new <see cref="T:Thread"/> using the specified <see cref="T:ThreadStart"/>,
		///		start the thread, and add it to the set.
		/// </summary>
		/// <param name="start">A <see cref="T:ThreadStart"/> delegate that represents the method
		///		to be invoked when this thread begins executing.</param>
		/// <param name="name">The name of the thread, or <b>null</b>.</param>
		public void CreateAdd(ThreadStart start, string? name = null)
		{
			if (start == null)
			{
				throw new ArgumentNullException(nameof(start));
			}

			Thread thread = new Thread(start)
			{
				Name = name,
			};

			thread.Start();

			base.Add(thread);
		}
	}
}
