using System;
using System.Reflection;

using Microsoft.Extensions.Logging;

using openSourceC.StandardLibrary.Configuration;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for AbstractProviderBase&lt;TProviderSettings&gt;.
	/// </summary>
	/// <typeparam name="TProviderSettings">The provider settings type.</typeparam>
	[Serializable]
	public abstract class AbstractProviderBase<TProviderSettings> : AbstractProviderBase
		where TProviderSettings : ProviderSettings, new()
	{
		[NonSerialized]
		private string _appDomainName;
		[NonSerialized]
		private readonly ILogger _logger;
		[NonSerialized]
		private string[] _parentNames;
		[NonSerialized]
		private readonly TProviderSettings _settings;
		[NonSerialized]
		private readonly string _nameSuffix;


		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractProviderBase&lt;TProviderSettings&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TProviderSettings"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected AbstractProviderBase(ILogger logger, string[] parentNames, TProviderSettings settings, string nameSuffix)
			: base("TODO: description" /*settings.Parameters["description"]*/)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
		///		Initializes the provider.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();

#if DIAGNOSTICS
			Debug.WriteLine($"Provider: {SettingsElement.Type}");

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

		#region CreateInstance (static)

		/// <summary>
		///		Creates a provider instance that implements <typeparamref name="TInterface"/>.
		/// </summary>
		/// <typeparam name="TInterface">The interface type.</typeparam>
		/// <param name="appDomain">The <see cref="T:AppDomain"/> to instantiate the provider in, or
		///		<b>null</b> to use the current <see cref="T:AppDomain"/>.</param>
		/// <param name="settings">The <typeparamref name="TProviderSettings"/> object.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments
		///		must match in number, order, and type the parameters of the constructor to invoke.
		///		If the default constructor is preferred, <paramref name="args"/> must be an empty
		///		array or null.</param>
		/// <returns>
		///		An instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public static TInterface CreateInstance<TInterface>(
			AppDomain appDomain,
			TProviderSettings settings,
			params object[] args
		)
			where TInterface : class
		{
			try
			{
				if (settings == null)
				{
					throw new ArgumentNullException(nameof(settings));
				}

#if DIAGNOSTICS
				Debug.WriteLine($"Provider: {settings.Type}");

				if (settings.ElementInformation != null && settings.ElementInformation.Properties != null)
				{
					foreach (PropertyInformation pi in settings.ElementInformation.Properties)
					{
						try { Debug.WriteLine($"\tProperty: {pi.Name} = {pi.Value}"); }
						catch { }
					}
				}
#endif

#if true
				Type providerType = Type.GetType(settings.Type);

				object instance = Activator.CreateInstance(
					providerType,
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding,
					null,
					args,
					null,
					null
				);
#else
				Match match = RegexHelper.ParseType.Match(settings.Type);
				//AssemblyLoadContext assemblyLoadContext = AssemblyLoadContext.GetLoadContext(Assembly.GetEntryAssembly());
				//AssemblyName assName = AssemblyLoadContext.GetAssemblyName("");

				if (!match.Success)
				{
					throw new InvalidOperationException($"Invalid type name: {settings.Type}");
				}

				string typeFullName = match.Groups["type"].Value;
				string assemblyFullName = match.Groups["assembly"].Value;

				if (appDomain == null)
				{
					appDomain = AppDomain.CurrentDomain;
				}

				Activator.CreateInstance()
				ObjectHandle objHandle = appDomain.CreateInstance(
					assemblyFullName,
					typeFullName,
					false,
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding,
					null,
					args,
					null,
					null
				);

				object instance = objHandle.Unwrap();
#endif

#if DIAGNOSTICS
				//if (RemotingServices.IsTransparentProxy(instance))
				//{
				//	Debug.WriteLine("The unwrapped object is a proxy.");
				//}
				//else
				//{
				//	Debug.WriteLine("The unwrapped object is not a proxy!");
				//}
#endif

				if (!(instance is TInterface interfaceInstance))
				{
					throw new OscErrorException(string.Format("{0} does not derive from {1}.", settings.Type, typeof(TInterface)));
				}

				if (interfaceInstance is AbstractProviderBase<TProviderSettings> providerInstance)
				{
					providerInstance.AppDomainName = appDomain.FriendlyName;
					providerInstance.Initialize();
				}

				return interfaceInstance;
			}
			catch (Exception ex)
			{
				string exceptionMessage = $"Unable to create an instance of {typeof(TInterface)} from the '{settings.Type}' provider.";

				throw new OscErrorException(exceptionMessage, ex);
			}
		}

		#endregion

		#region Protected Properties

		/// <summary>Gets the <see cref="P:AppDomain.Name"/> used.</summary>
		protected string AppDomainName
		{
			get { return _appDomainName; }
			private set { _appDomainName = value; }
		}

		/// <summary>Gets the <see cref="T:ILogger"/> object.</summary>
		protected ILogger Logger { get { return _logger; } }

		/// <summary>Gets the name suffix.</summary>
		protected string NameSuffix { get { return _nameSuffix; } }

		/// <summary>Gets the names of the parent configuration elements.</summary>
		protected string[] ParentNames { get { return _parentNames; } }

		/// <summary>Gets the <typeparamref name="TProviderSettings"/> object.</summary>
		protected TProviderSettings SettingsElement { get { return _settings; } }

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
	///		<para>The <see cref="T:AbstractProviderBase"/> class is simple, containing only a few
	///		basic methods and properties that are common to all providers. Feature-specific
	///		providers inherit from <see cref="T:AbstractProviderBase"/> and establish the necessary
	///		methods and properties that the implementation-specific providers for that feature must
	///		support. Implementation-specific providers inherit in turn from a feature-specific
	///		provider.</para>
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
	public abstract class AbstractProviderBase : IDisposable
	{
		private bool _initialized;
		private readonly string _description;


		#region Constructors

		/// <summary>
		///		Creates a new instance of the <see cref="T:AbstractProviderBase"/> class.
		/// </summary>
		/// <param name="description">The description of the provider.</param>
		protected AbstractProviderBase(string description)
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
		~AbstractProviderBase()
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
			AbstractProviderBase baseLock = this;

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
