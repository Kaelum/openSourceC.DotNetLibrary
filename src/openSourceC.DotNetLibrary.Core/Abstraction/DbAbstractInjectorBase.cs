using System;

using Microsoft.Extensions.Logging;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Summary description for DbAbstractInjectorBase&lt;TSettingsElement&gt;.
	/// </summary>
	[Serializable]
	public abstract class DbAbstractInjectorBase<TInjectorSettings> : AbstractInjectorBase<TInjectorSettings>
		where TInjectorSettings : class
	{
		#region Constructors

		/// <summary>
		///		Creates an instance of <see cref="DbAbstractInjectorBase&lt;TInjectorSettings&gt;"/>.
		/// </summary>
		/// <param name="logger">The <see cref="T:ILogger"/> object.</param>
		/// <param name="settings">The <see name="T:DbInjectorElement"/> object.</param>
		/// <param name="nameSuffix">The name suffix used, or <b>null</b> if not used.</param>
		protected DbAbstractInjectorBase(ILogger logger, TInjectorSettings settings, string? nameSuffix)
			: base(logger, null, settings, nameSuffix) { }

		#endregion
	}
}
