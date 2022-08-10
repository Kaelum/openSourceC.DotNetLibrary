using System;
using System.Threading;

namespace openSourceC.DotNetLibrary.Threading
{
	/// <summary>
	///		Summary description for DelayedCallback.
	/// </summary>
	public class DelayedCallback : IDisposable
	{
		private readonly Action<object?> _callback;
		private CancellationTokenRegistration _registration;

		/// <summary></summary>
		protected readonly object syncLock = new();

		private bool _disposed;


		#region Constructors

		/// <summary>
		///		Creates an instance of DelayedCallback.
		/// </summary>
		/// <param name="millisecondsDelay"></param>
		/// <param name="callback"></param>
		public DelayedCallback(
			int millisecondsDelay,
			Action<object?> callback
		)
		{
			CancellationTokenSource = new(millisecondsDelay);
			_callback = callback;

			_registration = CancellationTokenSource.Token.UnsafeRegister(_callback, this);
		}

		/// <summary>
		///		Creates an instance of DelayedCallback.
		/// </summary>
		/// <param name="delay"></param>
		/// <param name="callback"></param>
		public DelayedCallback(
			TimeSpan delay,
			Action<object?> callback
		)
		{
			CancellationTokenSource = new(delay);
			_callback = callback;

			_registration = CancellationTokenSource.Token.UnsafeRegister(_callback, this);
		}

		#endregion

		#region Implement IDisposable

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~DelayedCallback()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		/// <summary>
		///		TODO:
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		/// <summary>
		///		TODO:
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				_disposed = true;

				if (disposing)
				{
					// TODO: dispose managed state (managed objects)

					lock (syncLock)
					{
						_registration.Unregister();
						_registration.Dispose();

						CancellationTokenSource.Dispose();
					}
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
			}
		}

		#endregion

		#region Public Properties

		/// <summary>
		///		TODO:
		/// </summary>
		public CancellationTokenSource CancellationTokenSource { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		///		Cancel the callback that is currently registered. (Thread Safe)
		/// </summary>
		public void CancelCallback()
		{
			lock (syncLock)
			{
				if (_disposed)
				{
					return;
				}

				_registration.Unregister();
			}
		}

		/// <summary>
		///		Set (Thread Safe)
		/// </summary>
		/// <param name="millisecondsDelay"></param>
		/// <param name="safeSetAction"></param>
		public void Set(int millisecondsDelay, Action? safeSetAction = null)
		{
			lock (syncLock)
			{
				if (_disposed)
				{
					return;
				}

				_registration.Unregister();

				if (safeSetAction is not null)
				{
					safeSetAction();
				}

				CancellationTokenSource = new(millisecondsDelay);

				_registration = CancellationTokenSource.Token.UnsafeRegister(_callback, this);
			}
		}

		/// <summary>
		///		Set (Thread Safe)
		/// </summary>
		/// <param name="delay"></param>
		/// <param name="safeSetAction"></param>
		public void Set(TimeSpan delay, Action? safeSetAction = null)
		{
			lock (syncLock)
			{
				if (_disposed)
				{
					return;
				}

				_registration.Unregister();

				if (safeSetAction is not null)
				{
					safeSetAction();
				}

				CancellationTokenSource = new(delay);

				_registration = CancellationTokenSource.Token.UnsafeRegister(_callback, this);
			}
		}

		#endregion
	}
}
