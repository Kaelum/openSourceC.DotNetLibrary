using System;

using Microsoft.Extensions.Logging;

using openSourceC.DotNetLibrary.Configuration;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Summary description for AbstractProvider&lt;TProviderSettings&gt;.
	/// </summary>
	/// <typeparam name="TProviderSettings">The settings element type.</typeparam>
	[Serializable]
	public abstract class AbstractProvider<TProviderSettings> : AbstractProviderBase<TProviderSettings>
		where TProviderSettings : ProviderSettings, new()
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractProvider&lt;TProviderSettings&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TProviderSettings"/>
		///		object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected AbstractProvider(ILogger logger, string[]? parentNames, TProviderSettings settings, string? nameSuffix)
			: base(logger, parentNames, settings, nameSuffix) { }

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
		///		An instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public TInterface CreateInstance<TInterfaceSettingsElement, TInterface>(
			TInterfaceSettingsElement settings,
			params object[] args
		)
			where TInterfaceSettingsElement : ProviderSettings, new()
			where TInterface : class
		{
			return AbstractProvider<TInterfaceSettingsElement>.CreateInstance<TInterface>(
				settings,
				args
			);
		}

		#endregion
	}

	/// <summary>
	///		Summary description for AbstractProvider&lt;TProviderSettings, TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TProviderSettings">The settings element type.</typeparam>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	[Serializable]
	public abstract class AbstractProvider<TProviderSettings, TRequestContext> : AbstractProviderBase<TProviderSettings>
		where TProviderSettings : ProviderSettings, new()
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractProvider&lt;TProviderSettings, TRequestContext&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TProviderSettings"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected AbstractProvider(ILogger logger, TRequestContext requestContext, string[]? parentNames, TProviderSettings settings, string? nameSuffix)
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
		///		An instance that implements <typeparamref name="TInterface"/>.
		/// </returns>
		public TInterface CreateInstance<TInterfaceSettingsElement, TInterface>(
			TInterfaceSettingsElement settings,
			params object[] args
		)
			where TInterfaceSettingsElement : ProviderSettings, new()
			where TInterface : class
		{
			return AbstractProvider<TInterfaceSettingsElement, TRequestContext>.CreateInstance<TInterface>(
				settings,
				args
			);
		}

		#endregion
	}
}
