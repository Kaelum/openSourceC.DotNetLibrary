using System;

using Microsoft.Extensions.Logging;

using openSourceC.DotNetLibrary.Configuration;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Summary description for DbAbstractProvider.
	/// </summary>
	[Serializable]
	public abstract class DbAbstractProvider : DbAbstractProviderBase
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractProvider"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="settings">The <see name="T:DbProviderElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected DbAbstractProvider(ILogger logger, DbProviderSettings settings, string? nameSuffix)
			: base(logger, settings, nameSuffix) { }

		#endregion

		#region CreateInstance

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
		public new TInterface CreateInstance<TInterface>(
			DbProviderSettings settings,
			params object[] args
		)
			where TInterface : class
		{
			return DbAbstractProviderBase.CreateInstance<TInterface>(
				settings,
				args
			);
		}

		#endregion
	}

	/// <summary>
	///		Summary description for DbAbstractProvider&lt;TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	[Serializable]
	public abstract class DbAbstractProvider<TRequestContext> : DbAbstractProviderBase
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractProvider&lt;TRequestContext&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="settings">The <see name="T:DbProviderElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected DbAbstractProvider(ILogger logger, TRequestContext requestContext, DbProviderSettings settings, string? nameSuffix)
			: base(logger, settings, nameSuffix)
		{
			RequestContext = requestContext;
		}

		#endregion

		#region Protected Properties

		/// <summary>Gets the current <see cref="T:TRequestContext"/> object.</summary>
		protected TRequestContext RequestContext { get; private set; }

		#endregion

		#region CreateInstance

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
		public new TInterface CreateInstance<TInterface>(
			DbProviderSettings settings,
			params object[] args
		)
			where TInterface : class
		{
			return DbAbstractProviderBase.CreateInstance<TInterface>(
				settings,
				args
			);
		}

		#endregion
	}
}
