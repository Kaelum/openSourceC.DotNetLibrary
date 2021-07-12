using System;

using Microsoft.Extensions.Logging;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Provides a base implementation for the extensible injector model.
	/// </summary>
	/// <remarks>
	///		The injector model is intended to encapsulate all or part of the functionality of
	///		multiple application features, such as persistence, profiles, and protected
	///		configuration. It allows the developer to create supporting classes that provide
	///		multiple implementations of the encapsulated functionality. In addition, developers can
	///		write new features using the injector model. This can be an effective way to support
	///		multiple implementations of a feature's functionality without duplicating the feature
	///		code or recoding the application layer if the implementation method needs to be changed.
	///		<para>The <see cref="T:AbstractInjectorBase"/> class is simple, containing only a few
	///		basic methods and properties that are common to all injectors. Feature-specific
	///		injectors inherit from <see cref="T:AbstractInjectorBase"/> and establish the necessary
	///		methods and properties that the implementation-specific injectors for that feature must
	///		support. Implementation-specific injectors inherit in turn from a feature-specific
	///		injector.</para>
	///		<para>The most important aspect of the injector model is that the implementation (for
	///		example, whether data is persisted as a text file or in a database) is abstracted from
	///		the application code. The type of the implementation-specific injector for the given
	///		feature is designated in a configuration file. The feature-level injector then reads in
	///		the type from the configuration file and acts as a factory to the feature code. The
	///		application developer can then use the feature classes in the application code. The
	///		implementation type can be swapped out in the configuration file, eliminating the need
	///		to rewrite the code to accommodate the different implementation methodology.</para>
	///		<para>This model can, and should, be applied to any kind of feature functionality that
	///		could be abstracted and implemented in multiple ways.</para>
	/// </remarks>
	/// <typeparam name="TInjectorSettings">The injector settings type.</typeparam>
	[Serializable]
	public abstract class AbstractInjectorBase<TInjectorSettings> : IDisposable
		where TInjectorSettings : class
	{
		[NonSerialized]
		private readonly ILogger _logger;
		[NonSerialized]
		private string[]? _parentNames;
		[NonSerialized]
		private readonly TInjectorSettings _settings;
		[NonSerialized]
		private readonly string? _nameSuffix;

		[NonSerialized]
		private bool _initialized;
		[NonSerialized]
		private readonly string? _description;

		[NonSerialized]
		private readonly AbstractInjectorBase<TInjectorSettings> _thisLock;


		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractInjectorBase&lt;TInjectorSettings&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TInjectorSettings"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected AbstractInjectorBase(ILogger logger, string[]? parentNames, TInjectorSettings settings, string? nameSuffix)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_parentNames = parentNames;
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_nameSuffix = nameSuffix;

			_description = "TODO: description";// settings.Parameters["description"]

			_thisLock = this;
		}

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///		This destructor will run only if the Dispose method does not get called.
		/// </summary>
		/// <remarks>Do not provide destructors in types derived from this class.</remarks>
		~AbstractInjectorBase()
		{
			Dispose(false);
		}

		/// <summary>Gets a value indicating that this instance has been disposed.</summary>
		protected bool Disposed { get; private set; } = false;

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
				Disposed = true;

				// If disposing equals true, dispose of managed resources.
				if (disposing)
				{
					// Dispose of managed resources.
				}

				// Dispose of unmanaged resources.
			}
		}

		#endregion

		#region Initialize

		/// <summary>
		///		Initializes the injector.
		/// </summary>
		/// <remarks>
		///		The base class implementation internally tracks whether the injector's
		///		<see cref="M:Initialize()"/> method has been called and only returns <b>true</b> to
		///		that caller that actually initializes the object.  This method potentially blocks
		///		all use of the object while initializing the object.
		///		<para>Because most feature injectors call <b>Initialize</b> prior to performing
		///		injector-specific initialization, this method is a central location for preventing
		///		double initialization.</para>
		/// </remarks>
		///	<returns>
		///		<b>true</b> if the object is initialized, or <b>false</b> if it is already
		///		initialized.
		///	</returns>
		public bool Initialize()
		{
			if (!_initialized)
			{
				lock (_thisLock)
				{
					if (_initialized)
					{
						return false;
					}

					_initialized = true;

					Initialization();
				}
			}

			return false;
		}

		/// <summary>
		///		The virtual method called to initialize the object.
		/// </summary>
		protected virtual void Initialization()
		{
#if DIAGNOSTICS
			// This code needs to either be updated, or removed.
			if (SettingsElement.ElementInformation is not null && SettingsElement.ElementInformation.Properties is not null)
			{
				foreach (PropertyInformation pi in SettingsElement.ElementInformation.Properties)
				{
					try { Debug.WriteLine($"\tProperty: {pi.Name} = {pi.Value}"); }
					catch { }
				}
			}
#endif
		}

		#endregion

		#region Protected Properties

		/// <summary>Gets the <see cref="T:ILogger"/> object.</summary>
		protected ILogger Logger => _logger;

		/// <summary>Gets the name suffix.</summary>
		protected string? NameSuffix => _nameSuffix;

		/// <summary>Gets the names of the parent configuration elements.</summary>
		protected string[]? ParentNames => _parentNames;

		/// <summary>Gets the <typeparamref name="TInjectorSettings"/> object.</summary>
		protected TInjectorSettings Settings => _settings;

		#endregion

		#region Protected Methods

		/// <summary>
		///		Extends the <see cref="P:ParentNames"/> property with the specified names.
		/// </summary>
		/// <param name="args">The names to appended to <see cref="P:ParentNames"/>.</param>
		/// <returns>
		///		The extended list of parent element names.
		/// </returns>
		protected string[] ExtendParentNames(params string[] args)
		{
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args));
			}

			string[] newParentNames = new string[(_parentNames?.Length ?? 0) + args.Length];

			_parentNames?.CopyTo(newParentNames, 0);

			if (args.Length != 0)
			{
				args.CopyTo(newParentNames, (_parentNames?.Length ?? 0));
			}

			return _parentNames = newParentNames;
		}

		#endregion

		#region Public Properties

		/// <summary>
		///		Gets a brief, friendly description suitable for display in administrative tools or
		///		other user interfaces (UIs).
		///	</summary>
		public virtual string? Description => _description;

		/// <summary>
		///		Gets a value indicating that the object has been initialized.
		/// </summary>
		public bool Initialized => _initialized;

		#endregion
	}
}
