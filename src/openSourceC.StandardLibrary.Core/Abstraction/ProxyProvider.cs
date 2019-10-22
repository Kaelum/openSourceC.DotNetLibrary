using System;

using Microsoft.Extensions.Logging;

using openSourceC.StandardLibrary.Configuration;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for ProxyProvider.
	/// </summary>
	public abstract class ProxyProvider : ProxyProviderBase
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="ProxyProvider"/> class.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected ProxyProvider(ILogger logger, string[] parentNames, string nameSuffix)
			: base(logger, parentNames, nameSuffix) { }

		#endregion

		#region CreateInstance (static)

		/// <summary>
		///		Creates a provider instance that implements <typeparamref name="TInterface"/>.
		/// </summary>
		/// <typeparam name="TKeyedProviderSettings">The settings element type.</typeparam>
		/// <typeparam name="TInterface">The interface type.</typeparam>
		/// <param name="appDomain">The <see cref="T:AppDomain"/> instantiate the provider is, or
		///		<b>null</b> to use the current <see cref="T:AppDomain"/>.</param>
		/// <param name="settings">The <typeparamref name="TKeyedProviderSettings"/>
		///		object.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments
		///		must match in number, order, and type the parameters of the constructor to invoke.
		///		If the default constructor is preferred, <paramref name="args"/> must be an empty
		///		array or null.</param>
		/// <returns>
		///		A <see cref="T:KeyedAbstractProvider&lt;TKeyedProviderSettings&gt;"/>
		///		instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public static TInterface CreateInstance<TKeyedProviderSettings, TInterface>(
			AppDomain appDomain,
			TKeyedProviderSettings settings,
			params object[] args
		)
			where TKeyedProviderSettings : KeyedProviderSettings, new()
			where TInterface : class
		{
			return KeyedAbstractProvider<TKeyedProviderSettings>.CreateInstance<TInterface>(
				appDomain,
				settings,
				args
			);
		}

		#endregion
	}

	/// <summary>
	///		Summary description for ProxyProvider&lt;TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	public abstract class ProxyProvider<TRequestContext> : ProxyProviderBase
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="ProxyProvider&lt;TRequestContext&gt;"/>
		///		class.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected ProxyProvider(ILogger logger, TRequestContext requestContext, string[] parentNames, string nameSuffix)
			: base(logger, parentNames, nameSuffix)
		{
			RequestContext = requestContext;
		}

		#endregion

		#region CreateInstance (static)

		/// <summary>
		///		Creates a provider instance that implements <typeparamref name="TInterface"/>.
		/// </summary>
		/// <typeparam name="TKeyedProviderSettings">The settings element type.</typeparam>
		/// <typeparam name="TInterface">The interface type.</typeparam>
		/// <param name="appDomain">The <see cref="T:AppDomain"/> instantiate the provider is, or
		///		<b>null</b> to use the current <see cref="T:AppDomain"/>.</param>
		/// <param name="settings">The <typeparamref name="TKeyedProviderSettings"/>
		///		object.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments
		///		must match in number, order, and type the parameters of the constructor to invoke.
		///		If the default constructor is preferred, <paramref name="args"/> must be an empty
		///		array or null.</param>
		/// <returns>
		///		A <see cref="T:KeyedAbstractProvider&lt;TKeyedProviderSettings&gt;"/>
		///		instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public static TInterface CreateInstance<TKeyedProviderSettings, TInterface>(
			AppDomain appDomain,
			TKeyedProviderSettings settings,
			params object[] args
		)
			where TKeyedProviderSettings : KeyedProviderSettings, new()
			where TInterface : class
		{
			return KeyedAbstractProvider<TKeyedProviderSettings, TRequestContext>.CreateInstance<TInterface>(
				appDomain,
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
