using System;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Provides a base implementation for the extensible provider model.
	/// </summary>
	/// <remarks>
	///		The provider model is intended to encapsulate all or part of the functionality of
	///		multiple application features, such as persistence, profiles, and protected
	///		configuration. It allows the developer to create supporting classes that provide
	///		multiple implementations of the encapsulated functionality. In addition, developers can
	///		write new features using the provider model. This can be an effective way to support
	///		multiple implementations of a feature's functionality without duplicating the feature
	///		code or recoding the application layer if the implementation method needs to be changed.
	///		<para>The RemotingProviderBase class is simple, containing only a few basic methods and
	///		properties that are common to all providers. Feature-specific providers inherit from
	///		RemotingProviderBase and establish the necessary methods and properties that the
	///		implementation-specific providers for that feature must support. Implementation-specific
	///		providers inherit in turn from a feature-specific provider.</para>
	///		<para>The most important aspect of the provider model is that the implementation (for
	///		example, whether data is persisted as a text file or in a database) is abstracted from
	///		the application code. The type of the implementation-specific provider for the given
	///		feature is designated in a configuration file. The feature-level provider then reads in
	///		the type from the configuration file and acts as a factory to the feature code. The
	///		application developer can then use the feature classes in the application code. The
	///		implementation type can be swapped out in the configuration file, eliminating the need
	///		to rewrite the code to accommodate the different implementation methodology.</para>
	///		<para>This model can, and should, be applied to any kind of feature functionality that
	///		could be abstracted and implemented in multiple ways.</para>
	/// </remarks>
	[Serializable]
	public abstract class RemotingProviderBase : MarshalByRefObject, IDisposable
	{
		private bool _initialized;
		private string _description;


		#region Constructors

		/// <summary>
		///		Creates a new instance of the <see cref="T:RemotingProviderBase"/> class.
		/// </summary>
		/// <param name="description">The description of the provider.</param>
		protected RemotingProviderBase(string description)
		{
			_description = description;

			Disposed = false;
		}

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///		This destructor will run only if the Dispose method does not get called.
		/// </summary>
		/// <remarks>Do not provide destructors in types derived from this class.</remarks>
		~RemotingProviderBase()
		{
			Dispose(false);
		}

		/// <summary>Gets a value indicating that this instance has been disposed.</summary>
		protected bool Disposed { get; private set; }

		/// <summary>
		///		Dispose of this object.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

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
		protected virtual void Dispose(bool disposing)
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

				Disposed = true;
			}
		}

		#endregion

		#region Initialize

		/// <summary>
		///		Initializes the provider.
		/// </summary>
		/// <remarks>
		///		The base class implementation internally tracks the number of times the provider's
		///		<b>Initialize</b> method has been called. If a provider is initialized more than
		///		once, an <b>InvalidOperationException</b> is thrown stating that the provider is
		///		already initialized.
		///		<para>Because most feature providers call <b>Initialize</b> prior to performing
		///		provider-specific initialization, this method is a central location for preventing
		///		double initialization.</para>
		/// </remarks>
		///	<exception cref="InvalidOperationException">An attempt is made to call <b>Initialize</b>
		///		on a provider after the provider has already been initialized.</exception>
		public virtual void Initialize()
		{
			RemotingProviderBase baseLock = this;

			lock (baseLock)
			{
				if (_initialized)
				{
					throw new InvalidOperationException(SR.GetString("Provider_Already_Initialized"));
				}

				_initialized = true;
			}
		}

		#endregion

		#region Public Properties

		/// <summary>
		///		Gets a brief, friendly description suitable for display in administrative tools or
		///		other user interfaces (UIs).
		///	</summary>
		public virtual string Description
		{
			get { return _description; }
		}

		#endregion
	}
}
