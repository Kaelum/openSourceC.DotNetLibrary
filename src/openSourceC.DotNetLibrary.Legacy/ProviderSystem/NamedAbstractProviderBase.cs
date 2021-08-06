using System;

using openSourceC.DotNetLibrary.Configuration;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Summary description for NamedAbstractProviderBase&lt;TSettingsElement&gt;.
	/// </summary>
	/// <typeparam name="TSettingsElement">The settings element type.</typeparam>
	[Serializable]
	public abstract class NamedAbstractProviderBase<TSettingsElement> : AbstractProviderBase<TSettingsElement>
		where TSettingsElement : NamedProviderElement, new()
	{
		[NonSerialized]
		private string _name;


		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="NamedAbstractProviderBase&lt;TSettingsElement&gt;"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TSettingsElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected NamedAbstractProviderBase(OscLog log, string[] parentNames, TSettingsElement settings, string nameSuffix)
			: base(log, parentNames, settings, nameSuffix)
		{
			_name = settings.Name;
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
		protected string ShortName { get { return _name.Substring(0, _name.Length - (base.NameSuffix == null ? 0 : base.NameSuffix.Length)); } }

		#endregion

		#region Public Properties

		/// <summary>
		///		Gets a brief, friendly description suitable for display in administrative tools or
		///		other user interfaces (UIs).
		///	</summary>
		public override string Description
		{
			get { return (string.IsNullOrEmpty(base.Description) ? _name : base.Description); }
		}

		/// <summary>
		///		Gets the friendly name used to refer to the provider during configuration.
		///	</summary>
		public virtual string Name
		{
			get { return _name; }
		}

		#endregion
	}
}
