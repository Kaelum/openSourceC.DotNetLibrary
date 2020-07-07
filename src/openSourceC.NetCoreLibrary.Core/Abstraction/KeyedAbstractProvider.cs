using System;

using Microsoft.Extensions.Logging;

using openSourceC.NetCoreLibrary.Configuration;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for KeyedAbstractProvider&lt;TKeyedProviderSettings&gt;.
	/// </summary>
	/// <typeparam name="TKeyedProviderSettings">The keyed provider settings type.</typeparam>
	[Serializable]
	public abstract class KeyedAbstractProvider<TKeyedProviderSettings> : KeyedAbstractProviderBase<TKeyedProviderSettings>
		where TKeyedProviderSettings : KeyedProviderSettings, new()
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="KeyedAbstractProvider&lt;TSettingsElement&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TKeyedProviderSettings"/>
		///		object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected KeyedAbstractProvider(ILogger logger, string[]? parentNames, TKeyedProviderSettings settings, string? nameSuffix)
			: base(logger, parentNames, settings, nameSuffix) { }

		#endregion

		#region CreateInstance

		/// <summary>
		///		Creates a provider instance that implements <typeparamref name="TInterface"/>.
		/// </summary>
		/// <typeparam name="TInterfaceKeyedProviderSettings">The keyed provider settings type.</typeparam>
		/// <typeparam name="TInterface">The interface type.</typeparam>
		/// <param name="settings">The <typeparamref name="TInterfaceKeyedProviderSettings"/>
		///		object.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments
		///		must match in number, order, and type the parameters of the constructor to invoke.
		///		If the default constructor is preferred, <paramref name="args"/> must be an empty
		///		array or null.</param>
		/// <returns>
		///		A <see cref="T:KeyedAbstractProvider&lt;TSettingsElement&gt;"/>
		///		instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public TInterface CreateInstance<TInterfaceKeyedProviderSettings, TInterface>(
			TInterfaceKeyedProviderSettings settings,
			params object[] args
		)
			where TInterfaceKeyedProviderSettings : KeyedProviderSettings, new()
			where TInterface : class
		{
			return AbstractProviderBase<TInterfaceKeyedProviderSettings>.CreateInstance<TInterface>(
				settings,
				args
			);
		}

		#endregion
	}

	/// <summary>
	///		Summary description for KeyedAbstractProvider&lt;TKeyedProviderSettings, TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TKeyedProviderSettings">The settings element type.</typeparam>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	[Serializable]
	public abstract class KeyedAbstractProvider<TKeyedProviderSettings, TRequestContext> : KeyedAbstractProviderBase<TKeyedProviderSettings>
		where TKeyedProviderSettings : KeyedProviderSettings, new()
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="KeyedAbstractProvider&lt;TSettingsElement, TRequestContext&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TKeyedProviderSettings"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected KeyedAbstractProvider(ILogger logger, TRequestContext requestContext, string[]? parentNames, TKeyedProviderSettings settings, string? nameSuffix)
			: base(logger, parentNames, settings, nameSuffix)
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
		/// <typeparam name="TInterfaceSettingsElement">The settings element type.</typeparam>
		/// <typeparam name="TInterface">The interface type.</typeparam>
		/// <param name="settings">The <typeparamref name="TInterfaceSettingsElement"/>
		///		object.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments
		///		must match in number, order, and type the parameters of the constructor to invoke.
		///		If the default constructor is preferred, <paramref name="args"/> must be an empty
		///		array or null.</param>
		/// <returns>
		///		A <see cref="T:KeyedAbstractProvider&lt;TSettingsElement&gt;"/>
		///		instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public TInterface CreateInstance<TInterfaceSettingsElement, TInterface>(
			TInterfaceSettingsElement settings,
			params object[] args
		)
			where TInterfaceSettingsElement : KeyedProviderSettings, new()
			where TInterface : class
		{
			return AbstractProviderBase<TInterfaceSettingsElement>.CreateInstance<TInterface>(
				settings,
				args
			);
		}

		#endregion
	}
}
