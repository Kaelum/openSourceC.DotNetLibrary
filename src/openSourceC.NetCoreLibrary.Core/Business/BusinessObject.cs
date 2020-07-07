using System;

using Microsoft.Extensions.Logging;

using openSourceC.NetCoreLibrary.Configuration;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for BusinessObject.
	/// </summary>
	public abstract class BusinessObject : BusinessObjectBase
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="BusinessObject"/> class.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected BusinessObject(ILogger logger, string? nameSuffix)
			: base(logger, nameSuffix) { }

		#endregion

		#region CreateInstance (static)

		/// <summary>
		///		Creates a provider instance that implements <typeparamref name="TInterface"/>.
		/// </summary>
		/// <typeparam name="TInterface">The interface type.</typeparam>
		/// <param name="settings">The <see name="T:DbProviderElement"/> object.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments
		///		must match in number, order, and type the parameters of the constructor to invoke.
		///		If the default constructor is preferred, <paramref name="args"/> must be an empty
		///		array or null.</param>
		/// <returns>
		///		An instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public static TInterface CreateInstance<TInterface>(
			DbProviderSettings settings,
			params object[] args
		)
			where TInterface : class
		{
			return AbstractProviderBase<DbProviderSettings>.CreateInstance<TInterface>(
				settings,
				args
			);
		}

		#endregion
	}

	/// <summary>
	///		Summary description for BusinessObject&lt;TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	public abstract class BusinessObject<TRequestContext> : BusinessObjectBase
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="BusinessObject&lt;TRequestContext&gt;"/>
		///		class.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected BusinessObject(ILogger logger, TRequestContext requestContext, string? nameSuffix)
			: base(logger, nameSuffix)
		{
			RequestContext = requestContext;
		}

		#endregion

		#region CreateInstance (static)

		/// <summary>
		///		Creates a provider instance that implements <typeparamref name="TInterface"/>.
		/// </summary>
		/// <typeparam name="TInterface">The interface type.</typeparam>
		/// <param name="settings">The <see name="T:DbProviderElement"/> object.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments
		///		must match in number, order, and type the parameters of the constructor to invoke.
		///		If the default constructor is preferred, <paramref name="args"/> must be an empty
		///		array or null.</param>
		/// <returns>
		///		An instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public static TInterface CreateInstance<TInterface>(
			DbProviderSettings settings,
			params object[] args
		)
			where TInterface : class
		{
			return AbstractProviderBase<DbProviderSettings>.CreateInstance<TInterface>(
				settings,
				args
			);
		}

		#endregion

		#region Protected Properties

		/// <summary>Gets the current <see cref="T:TRequestContext"/> object.</summary>
		protected TRequestContext RequestContext { get; private set; }

		#endregion
	}

	/// <summary>
	///		Summary description for BusinessObjectBase.
	/// </summary>
	[Serializable]
	public abstract class BusinessObjectBase : IDisposable
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of BusinessObjectBase.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected BusinessObjectBase(ILogger logger, string? nameSuffix)
		{
			Logger = logger ?? throw new ArgumentNullException("log");
			NameSuffix = nameSuffix;
		}

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///		This destructor will run only if the Dispose method does not get called.
		/// </summary>
		/// <remarks>Do not provide destructors in types derived from this class.</remarks>
		~BusinessObjectBase()
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

		#region Protected Properties

		/// <summary>Gets the <see cref="T:ILogger"/> object.</summary>
		protected ILogger Logger { get; private set; }

		/// <summary>Gets the name suffix.</summary>
		protected string? NameSuffix { get; private set; }

		#endregion
	}
}
