using System;
using System.Configuration;

namespace openSourceC.DotNetLibrary.Configuration
{
	/// <summary>
	///		Summary description for DbFactorySectionBase.
	/// </summary>
	public class DbFactorySectionBase : ConfigurationSection
	{
		#region Elements

		/// <summary></summary>
		[ConfigurationProperty("providers", IsRequired = true)]
		[ConfigurationCollection(typeof(DbProviderElementCollection))]
		public DbProviderElementCollection Providers
		{
			get { return (DbProviderElementCollection)base["providers"]; }
		}

		#endregion
	}
}
