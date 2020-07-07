using System;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for AbstractProvider&lt;TSettingsElement&gt;.
	/// </summary>
	/// <typeparam name="TSettingsElement">The settings element type.</typeparam>
	[Serializable]
	public abstract class AbstractProvider<TSettingsElement> : AbstractProviderBase<TSettingsElement>
		where TSettingsElement : ProviderElement, new()
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractProvider&lt;TSettingsElement&gt;"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TSettingsElement"/>
		///		object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected AbstractProvider(OscLog log, string[] parentNames, TSettingsElement settings, string nameSuffix)
			: base(log, parentNames, settings, nameSuffix) { }

		#endregion

		#region CreateInstance

		/// <summary>
		///		Creates a provider instance that implements <typeparamref name="TInterface"/>.
		/// </summary>
		/// <typeparam name="TInterfaceSettingsElement">The settings element type.</typeparam>
		/// <typeparam name="TInterface">The interface type.</typeparam>
		/// <param name="appDomain">The <see cref="T:AppDomain"/> to instantiate the provider in, or
		///		<b>null</b> to use the current <see cref="T:AppDomain"/>.</param>
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
			AppDomain appDomain,
			TInterfaceSettingsElement settings,
			params object[] args
		)
			where TInterfaceSettingsElement : ProviderElement, new()
			where TInterface : class
		{
			return AbstractProvider<TInterfaceSettingsElement>.CreateInstance<TInterface>(
				appDomain,
				settings,
				args
			);
		}

		#endregion
	}

	/// <summary>
	///		Summary description for AbstractProvider&lt;TSettingsElement, TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TSettingsElement">The settings element type.</typeparam>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	[Serializable]
	public abstract class AbstractProvider<TSettingsElement, TRequestContext> : AbstractProviderBase<TSettingsElement>
		where TSettingsElement : ProviderElement, new()
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractProvider&lt;TSettingsElement, TRequestContext&gt;"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TSettingsElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected AbstractProvider(OscLog log, TRequestContext requestContext, string[] parentNames, TSettingsElement settings, string nameSuffix)
			: base(log, parentNames, settings, nameSuffix)
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
		/// <param name="appDomain">The <see cref="T:AppDomain"/> to instantiate the provider in, or
		///		<b>null</b> to use the current <see cref="T:AppDomain"/>.</param>
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
			AppDomain appDomain,
			TInterfaceSettingsElement settings,
			params object[] args
		)
			where TInterfaceSettingsElement : ProviderElement, new()
			where TInterface : class
		{
			return AbstractProvider<TInterfaceSettingsElement, TRequestContext>.CreateInstance<TInterface>(
				appDomain,
				settings,
				args
			);
		}

		#endregion
	}
}
