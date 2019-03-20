using System;

using openSourceC.StandardLibrary.Configuration;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for DbAbstractInjector.
	/// </summary>
	[Serializable]
	public abstract class DbAbstractInjector : DbAbstractInjectorBase
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractInjector"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="settings">The <see name="T:DbInjectorElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected DbAbstractInjector(OscLog log, DbInjectorSettings settings, string nameSuffix)
			: base(log, settings, nameSuffix) { }

		#endregion
	}

	/// <summary>
	///		Summary description for DbAbstractInjector&lt;TRequestContext&gt;.
	/// </summary>
	/// <typeparam name="TRequestContext">The <typeparamref name="TRequestContext"/> type.</typeparam>
	[Serializable]
	public abstract class DbAbstractInjector<TRequestContext> : DbAbstractInjectorBase
		where TRequestContext : struct
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractInjector&lt;TRequestContext&gt;"/>.
		/// </summary>
		/// <param name="log">The <see cref="T:OscLog"/> object.</param>
		/// <param name="requestContext">The current <typeparamref name="TRequestContext"/> object.</param>
		/// <param name="settings">The <see name="T:DbInjectorElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix to use, or null is not used.</param>
		protected DbAbstractInjector(OscLog log, TRequestContext requestContext, DbInjectorSettings settings, string nameSuffix)
			: base(log, settings, nameSuffix)
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
