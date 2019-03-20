using System;

using openSourceC.StandardLibrary.Configuration;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for AbstractInjector&lt;TInjectorSettings&gt;.
	/// </summary>
	/// <typeparam name="TInjectorSettings">The settings element type.</typeparam>
	[Serializable]
	public abstract class AbstractInjector<TInjectorSettings> : AbstractInjectorBase<TInjectorSettings>
		where TInjectorSettings : InjectorSettings, new()
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractInjector&lt;TInjectorSettings&gt;"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TInjectorSettings"/>
		///		object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected AbstractInjector(OscLog log, string[] parentNames, TInjectorSettings settings, string nameSuffix)
			: base(log, parentNames, settings, nameSuffix) { }

		#endregion
	}

	/// <summary>
	///		Summary description for AbstractInjector&lt;TInjectorSettings, TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TInjectorSettings">The settings element type.</typeparam>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	[Serializable]
	public abstract class AbstractInjector<TInjectorSettings, TRequestContext> : AbstractInjectorBase<TInjectorSettings>
		where TInjectorSettings : InjectorSettings, new()
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="AbstractInjector&lt;TInjectorSettings, TRequestContext&gt;"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="parentNames">The names of the parent configuration elements.</param>
		/// <param name="settings">The <typeparamref name="TInjectorSettings"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected AbstractInjector(OscLog log, TRequestContext requestContext, string[] parentNames, TInjectorSettings settings, string nameSuffix)
			: base(log, parentNames, settings, nameSuffix)
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
