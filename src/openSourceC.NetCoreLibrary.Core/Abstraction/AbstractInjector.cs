using System;

using Microsoft.Extensions.Logging;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for AbstractInjector&lt;TInjectorSettings&gt;.
	/// </summary>
	/// <typeparam name="TInjectorSettings">The settings element type.</typeparam>
	[Serializable]
	public abstract class AbstractInjector<TInjectorSettings> : AbstractInjectorBase<TInjectorSettings>
		where TInjectorSettings : class
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractInjector&lt;TInjectorSettings&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TInjectorSettings"/>
		///		object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected AbstractInjector(ILogger logger, string[]? parentNames, TInjectorSettings settings, string? nameSuffix)
			: base(logger, parentNames, settings, nameSuffix) { }

		#endregion
	}

	/// <summary>
	///		Summary description for AbstractInjector&lt;TInjectorSettings, TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TInjectorSettings">The settings element type.</typeparam>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	[Serializable]
	public abstract class AbstractInjector<TInjectorSettings, TRequestContext> : AbstractInjectorBase<TInjectorSettings>
		where TInjectorSettings : class
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractInjector&lt;TInjectorSettings, TRequestContext&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TInjectorSettings"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected AbstractInjector(ILogger logger, TRequestContext requestContext, string[]? parentNames, TInjectorSettings settings, string? nameSuffix)
			: base(logger, parentNames, settings, nameSuffix)
		{
			RequestContext = requestContext;
		}

		#endregion

		#region Protected Properties

		/// <summary>Gets the current <see cref="T:TRequestContext"/> object.</summary>
		protected TRequestContext RequestContext { get; private set; }

		#endregion
	}
}
