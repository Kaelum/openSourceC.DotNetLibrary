using System;

using Microsoft.Extensions.Logging;

using openSourceC.DotNetLibrary.Configuration;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Summary description for DbProxyProvider.
	/// </summary>
	public abstract class DbProxyProvider : ProxyProviderBase
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="DbProxyProvider"/> class.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected DbProxyProvider(ILogger logger, string? nameSuffix)
			: base(logger, null, nameSuffix) { }

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
	///		Summary description for DbProxyProvider&lt;TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	public abstract class DbProxyProvider<TRequestContext> : ProxyProviderBase
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="DbProxyProvider&lt;TRequestContext&gt;"/>
		///		class.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected DbProxyProvider(ILogger logger, TRequestContext requestContext, string? nameSuffix)
			: base(logger, null, nameSuffix)
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
}
