using System;
using System.ComponentModel.DataAnnotations;

namespace openSourceC.NetCoreLibrary.Configuration
{
	/// <summary>
	///		Represents a uniquely keyed connection settings object.
	/// </summary>
	public class ConnectionSettings : InjectorSettings
	{
		/// <summary>Gets or sets the connection string key for the parameter store.</summary>
		[Required]
		public string? ConnectionStringKey { get; set; }
	}
}
