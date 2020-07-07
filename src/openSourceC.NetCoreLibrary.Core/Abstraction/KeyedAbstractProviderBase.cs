using System;

using Microsoft.Extensions.Logging;

using openSourceC.NetCoreLibrary.Configuration;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for KeyedAbstractProviderBase&lt;TKeyedProviderSettings&gt;.
	/// </summary>
	/// <typeparam name="TKeyedProviderSettings">The keyed provider settings type.</typeparam>
	[Serializable]
	public abstract class KeyedAbstractProviderBase<TKeyedProviderSettings> : AbstractProviderBase<TKeyedProviderSettings>
		where TKeyedProviderSettings : KeyedProviderSettings, new()
	{
		[NonSerialized]
		private readonly string _key;


		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="KeyedAbstractProviderBase&lt;TKeyedProviderSettings&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TKeyedProviderSettings"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected KeyedAbstractProviderBase(ILogger logger, string[]? parentNames, TKeyedProviderSettings settings, string? nameSuffix)
			: base(logger, parentNames, settings, nameSuffix)
		{
			_key = settings.Key ?? throw new InvalidOperationException("The settings Key property is null.");
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
		}

		#endregion

		#region Protected Properties

		/// <summary>Gets or sets the provider name excluding the suffix.</summary>
		protected string ShortName { get { return _key.Substring(0, _key.Length - (base.NameSuffix == null ? 0 : base.NameSuffix.Length)); } }

		#endregion

		#region Public Properties

		/// <summary>
		///		Gets a brief, friendly description suitable for display in administrative tools or
		///		other user interfaces (UIs).
		///	</summary>
		public override string Description
		{
			get { return (string.IsNullOrEmpty(base.Description) ? _key : base.Description); }
		}

		/// <summary>
		///		Gets the friendly key name used to refer to the provider during configuration.
		///	</summary>
		public virtual string Key
		{
			get { return _key; }
		}

		#endregion
	}
}
