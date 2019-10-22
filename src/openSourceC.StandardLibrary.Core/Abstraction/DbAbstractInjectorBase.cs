using System;

using Microsoft.Extensions.Logging;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for DbAbstractInjectorBase&lt;TSettingsElement&gt;.
	/// </summary>
	[Serializable]
	public abstract class DbAbstractInjectorBase<TInjectorSettings> : AbstractInjectorBase<TInjectorSettings>
		where TInjectorSettings : class
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractInjectorBase&lt;TInjectorSettings&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="settings">The <see name="T:DbInjectorElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected DbAbstractInjectorBase(ILogger logger, TInjectorSettings settings, string nameSuffix)
			: base(logger, null, settings, nameSuffix) { }

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///		Dispose(bool disposing) executes in two distinct scenarios.  If disposing equals
		///		<b>true</b>, <see cref="M:Dispose()"/> has been called directly or indirectly
		///		by a user's code.  Managed and unmanaged resources can be disposed.  If disposing
		///		equals <b>false</b>, <see cref="M:Dispose()"/> has been called by the runtime from
		///		inside the finalizer and you should not reference other objects.  Only unmanaged
		///		resources can be disposed.
		/// </summary>
		/// <param name="disposing"><b>true</b> when <see cref="M:Dispose()"/> has been called
		///		directly or indirectly by a user's code.  <b>false</b> when <see cref="M:Dispose()"/>
		///		has been called by the runtime from inside the finalizer and you should not
		///		reference other objects.</param>
		protected override void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!Disposed)
			{
				// If disposing equals true, dispose of managed resources.
				if (disposing)
				{
					// Dispose of managed resources.
				}

				// Dispose of unmanaged resources.
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Initialize

		/// <summary>
		///		Initializes the injector.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
		}

		#endregion
	}
}
