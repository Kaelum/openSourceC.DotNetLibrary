using System;

using Microsoft.Extensions.Logging;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for DbAbstractInjector.
	/// </summary>
	/// <typeparam name="TInjectorSettings"></typeparam>
	[Serializable]
	public abstract class DbAbstractInjector<TInjectorSettings> : DbAbstractInjectorBase<TInjectorSettings>
		where TInjectorSettings : class
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractInjector&lt;TInjectorSettings&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="settings">The <see name="T:DbInjectorElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected DbAbstractInjector(ILogger logger, TInjectorSettings settings, string? nameSuffix)
			: base(logger, settings, nameSuffix) { }

		#endregion
	}

	/// <summary>
	///		Summary description for DbAbstractInjector&lt;TInjectorSettings, TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TInjectorSettings"></typeparam>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	[Serializable]
	public abstract class DbAbstractInjector<TInjectorSettings, TRequestContext> : DbAbstractInjectorBase<TInjectorSettings>
		where TInjectorSettings : class
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractInjector&lt;TRequestContext&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="settings">The <see name="T:DbInjectorElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected DbAbstractInjector(ILogger logger, TRequestContext requestContext, TInjectorSettings settings, string? nameSuffix)
			: base(logger, settings, nameSuffix)
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
