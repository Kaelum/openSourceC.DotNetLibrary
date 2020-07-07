using System;
using System.Collections.Specialized;
using System.Configuration;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Represents an unnamed provider configuration element.
	/// </summary>
	public class ProviderElement : ConfigurationElement
	{
		private NameValueCollection _parameters;


		#region Attributes

		/// <summary>Gets a collection of user-defined parameters.</summary>
		/// <value>A <see cref="NameValueCollection"/> of parameters for the setting.</value>
		/// <remarks>
		///		Use the <b>Parameters</b> property to access the <see cref="NameValueCollection"/>
		///		parameters for this <see cref="ProviderElement"/> object.
		///	</remarks>
		public NameValueCollection Parameters
		{
			get
			{
				if (_parameters == null)
				{
					ProviderElement settings = this;

					lock (settings)
					{
						if (_parameters == null)
						{
							_parameters = new NameValueCollection(StringComparer.Ordinal);
						}
					}
				}

				return _parameters;
			}
		}

		/// <summary>Gets or sets the type of the object configured by this class.</summary>
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get { return (string)base["type"]; }
			set { base["type"] = value; }
		}

		#endregion

		#region Override Methods

		/// <summary>
		///		 Invoked when an unknown attribute is encountered while deserializing the
		///		 ConfigurationElement object.
		/// </summary>
		/// <param name="name">The name of the unrecognized attribute.</param>
		/// <param name="value">The value of the unrecognized attribute.</param>
		/// <returns>
		///		<b>true</b> to signify that deserialization succeeded.
		/// </returns>
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ConfigurationProperty property = new ConfigurationProperty(name, typeof(string));
			base[property] = value;
			Parameters[name] = value;

			return true;
		}

		#endregion
	}
}
