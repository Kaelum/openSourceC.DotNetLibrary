using System;
using System.ComponentModel.DataAnnotations;

namespace openSourceC.StandardLibrary.Configuration
{
	/// <summary>
	///		Represents a uniquely keyed connection settings object.
	/// </summary>
	public class ConnectionSettings : InjectorSettings
	{
		/// <summary>Gets or sets the connection string.</summary>
		[Required]
		public string ConnectionString { get; set; }
	}
}
