using System;
using System.ComponentModel.DataAnnotations;

namespace openSourceC.NetCoreLibrary.Configuration
{
	/// <summary>
	///		Represents a uniquely keyed provider settings object.
	/// </summary>
	[Serializable]
	public class KeyedProviderSettings : ProviderSettings
	{
		#region Attributes

		/// <summary>Gets or sets the unique key name of the provider settings.</summary>
		[Required]
		public string? Key { get; set; }

		#endregion
	}
}
