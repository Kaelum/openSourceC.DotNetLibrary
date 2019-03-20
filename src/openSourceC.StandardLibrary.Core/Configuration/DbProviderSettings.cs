using System;
using System.ComponentModel.DataAnnotations;

namespace openSourceC.StandardLibrary.Configuration
{
	/// <summary>
	///		Represents a uniquely keyed database provider settings object.
	/// </summary>
	[Serializable]
	public class DbProviderSettings : KeyedProviderSettings
	{
		#region Attributes

		/// <summary>Gets or sets the application name.</summary>
		public string ApplicationName { get; set; }

		/// <summary>Gets or sets the connection key name.</summary>
		[Required]
		public string ConnectionKey { get; set; }

		#endregion
	}
}
