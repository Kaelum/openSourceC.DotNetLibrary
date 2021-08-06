using System;
using System.Configuration;

namespace openSourceC.DotNetLibrary.Configuration
{
	/// <summary>
	///		Represents a database provider configuration element.
	/// </summary>
	public class DbProviderElement : NamedProviderElement
	{
		#region Attributes

		/// <summary>Gets or sets the application name.</summary>
		[ConfigurationProperty("applicationName", IsRequired = false)]
		public string ApplicationName
		{
			get { return (string)base["applicationName"]; }
			set { base["applicationName"] = value; }
		}

		/// <summary>Gets or sets the connection string name.</summary>
		[ConfigurationProperty("connectionStringName", IsRequired = false)]
		public string ConnectionStringName
		{
			get { return (string)base["connectionStringName"]; }
			set { base["connectionStringName"] = value; }
		}

		#endregion
	}
}
