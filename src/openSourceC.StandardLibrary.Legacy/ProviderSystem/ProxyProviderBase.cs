using System;

namespace openSourceC.StandardLibrary.ProviderSystem
{
	/// <summary>
	///		Summary description for ProxyProviderBase.
	/// </summary>
	public abstract class ProxyProviderBase : MarshalByRefObject, IDisposable
	{
		[NonSerialized]
		private readonly OscLog _log;
		[NonSerialized]
		private readonly string[] _parentNames;
		[NonSerialized]
		private readonly string _nameSuffix;

		[NonSerialized]
		private bool _initialized;


		#region Constructors

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected ProxyProviderBase(OscLog log, string[] parentNames, string nameSuffix)
		{
			_log = log ?? throw new ArgumentNullException("log");
			_parentNames = parentNames;
			_nameSuffix = nameSuffix;
		}

		#endregion

		#region IDisposable Implementation

#if USES_UNMANGED_CODE
		/// <summary>
		///		This destructor will run only if the Dispose method does not get called.
		/// </summary>
		/// <remarks>Do not provide destructors in types derived from this class.</remarks>
		~ProxyProviderBase()
		{
			Dispose(false);
		}
#endif

		/// <summary>Gets a value indicating that this instance has been disposed.</summary>
		protected bool Disposed { get; private set; }

		/// <summary>
		///     Dispose and release all resources used by this instance.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);

#if USES_UNMANGED_CODE
			GC.SuppressFinalize(this);
#endif
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

#if USES_UNMANGED_CODE
				// Dispose of unmanaged resources.
#endif

				Disposed = true;
			}
		}

		#endregion

		#region Public Methods

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
			ProxyProviderBase baseLock = this;

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

		#region Protected Properties

		/// <summary>Gets the <see cref="T:OscLog"/> object.</summary>
		protected OscLog Log { get { return _log; } }

		/// <summary>Gets the name suffix.</summary>
		protected string NameSuffix { get { return _nameSuffix; } }

		/// <summary>Gets the names of the parent configuration elements.</summary>
		protected string[] ParentNames { get { return _parentNames; } }

		#endregion
	}
}
