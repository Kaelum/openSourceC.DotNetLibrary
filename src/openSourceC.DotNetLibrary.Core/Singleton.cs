using System;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///     Summary description for Singleton.
	/// </summary>
	/// <typeparam name="T">The class being wrapped.</typeparam>
	public static class Singleton<T>
		where T : class, new()
	{
		private static readonly object _singletonLock = new object();
		private static T? _instance;


		/// <summary>
		///     Singleton instance.
		/// </summary>
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_singletonLock)
					{
						if (_instance == null)
						{
							_instance = new T();
						}
					}
				}

				return _instance;
			}
		}
	}
}
