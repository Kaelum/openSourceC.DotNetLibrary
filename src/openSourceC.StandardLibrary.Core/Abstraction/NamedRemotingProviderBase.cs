using System;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for NamedRemotingProviderBase&lt;TSettingsElement&gt;.
	/// </summary>
	[Serializable]
	public abstract class NamedRemotingProviderBase : RemotingProviderBase
	{
		[NonSerialized]
		private readonly string _name;


		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="NamedRemotingProviderBase"/>.
		/// </summary>
		/// <param name="name">The name of the provider.</param>
		/// <param name="description">The description of the provider.</param>
		protected NamedRemotingProviderBase(string name, string description)
			: base(description)
		{
			_name = name;
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
