using System;
using System.Reflection;

using openSourceC.StandardLibrary.Configuration;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for AbstractInjectorBase&lt;TInjectorSettings&gt;.
	/// </summary>
	/// <typeparam name="TInjectorSettings">The injector settings type.</typeparam>
	[Serializable]
	public abstract class AbstractInjectorBase<TInjectorSettings> : AbstractInjectorBase
		where TInjectorSettings : InjectorSettings, new()
	{
		[NonSerialized]
		private string _appDomainName;
		[NonSerialized]
		private readonly OscLog _log;
		[NonSerialized]
		private string[] _parentNames;
		[NonSerialized]
		private readonly TInjectorSettings _settings;
		[NonSerialized]
		private readonly string _nameSuffix;


		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractInjectorBase&lt;TInjectorSettings&gt;"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TInjectorSettings"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected AbstractInjectorBase(OscLog log, string[] parentNames, TInjectorSettings settings, string nameSuffix)
			: base("TODO: description" /*settings.Parameters["description"]*/)
		{
			_log = log ?? throw new ArgumentNullException(nameof(log));
			_parentNames = parentNames;
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_nameSuffix = nameSuffix;
		}

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

#if DIAGNOSTICS
			if (SettingsElement.ElementInformation != null && SettingsElement.ElementInformation.Properties != null)
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

		/// <summary>Gets the <see cref="P:AppDomain.Name"/> used.</summary>
		protected string AppDomainName
		{
			get { return _appDomainName; }
			private set { _appDomainName = value; }
		}

		/// <summary>Gets the <see cref="T:OscLog"/> object.</summary>
		protected OscLog Log { get { return _log; } }

		/// <summary>Gets the name suffix.</summary>
		protected string NameSuffix { get { return _nameSuffix; } }

		/// <summary>Gets the names of the parent configuration elements.</summary>
		protected string[] ParentNames { get { return _parentNames; } }

		/// <summary>Gets the <typeparamref name="TInjectorSettings"/> object.</summary>
		protected TInjectorSettings SettingsElement { get { return _settings; } }

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
			int parentNamesCount = (_parentNames == null ? 0 : _parentNames.Length);
			string[] newParentNames = new string[parentNamesCount + args.Length];

			if (parentNamesCount != 0) { _parentNames.CopyTo(newParentNames, 0); }
			if (args.Length != 0) { args.CopyTo(newParentNames, (_parentNames == null ? 0 : _parentNames.Length)); }

			return newParentNames;
		}

		#endregion
	}

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
	[Serializable]
	public abstract class AbstractInjectorBase : IDisposable
	{
		private bool _initialized;
		private readonly string _description;


		#region Constructors

		/// <summary>
		///		Creates a new instance of the <see cref="T:AbstractInjectorBase"/> class.
		/// </summary>
		/// <param name="description">The description of the injector.</param>
		protected AbstractInjectorBase(string description)
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
		~AbstractInjectorBase()
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
		///		Initializes the injector.
		/// </summary>
		/// <remarks>
		///		The base class implementation internally tracks the number of times the injector's
		///		<b>Initialize</b> method has been called. If a injector is initialized more than
		///		once, an <b>InvalidOperationException</b> is thrown stating that the injector is
		///		already initialized.
		///		<para>Because most feature injectors call <b>Initialize</b> prior to performing
		///		injector-specific initialization, this method is a central location for preventing
		///		double initialization.</para>
		/// </remarks>
		///	<exception cref="InvalidOperationException">An attempt is made to call <b>Initialize</b>
		///		on a injector after the injector has already been initialized.</exception>
		public virtual void Initialize()
		{
			AbstractInjectorBase baseLock = this;

			lock (baseLock)
			{
				if (_initialized)
				{
					throw new InvalidOperationException(SR.GetString("Injector_Already_Initialized"));
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
