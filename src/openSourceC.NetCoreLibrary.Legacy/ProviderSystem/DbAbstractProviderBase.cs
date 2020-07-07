using System;

using openSourceC.NetCoreLibrary.Configuration;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for DbAbstractProviderBase&lt;TSettingsElement&gt;.
	/// </summary>
	[Serializable]
	public abstract class DbAbstractProviderBase : NamedAbstractProviderBase<DbProviderElement>
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractProviderBase"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="settings">The <see name="T:DbProviderElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected DbAbstractProviderBase(OscLog log, DbProviderElement settings, string nameSuffix)
			: base(log, null, settings, nameSuffix) { }

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
		///		Initializes the provider.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
		}

		#endregion

		#region Protected Properties

		/// <summary>Gets the application name.</summary>
		protected string ApplicationName { get { return SettingsElement.ApplicationName; } }

		/// <summary>Gets the connection string name.</summary>
		protected string ConnectionStringName { get { return SettingsElement.ConnectionStringName; } }

		#endregion
	}
}
