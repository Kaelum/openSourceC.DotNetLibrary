using System;
using System.Configuration;

namespace openSourceC.DotNetLibrary.Configuration
{
	/// <summary>
	///		Represents a named provider configuration element.
	/// </summary>
	public class NamedProviderElement : ProviderElement
	{
		#region Attributes

		/// <summary>Gets or sets the name of the object configured by this class.</summary>
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base["name"]; }
			set { base["name"] = value; }
		}

		#endregion
	}
}
