using System;
using System.Collections.Specialized;
using System.Configuration;

namespace openSourceC.NetCoreLibrary.Configuration
{
	/// <summary>
	///		Represents an named configuration element.
	/// </summary>
	public class NamedElement : ConfigurationElement
	{
		private NameValueCollection _parameters;


		#region Attributes

		/// <summary>Gets a collection of user-defined parameters.</summary>
		/// <value>A <see cref="NameValueCollection"/> of parameters for the setting.</value>
		/// <remarks>
		///		Use the <b>Parameters</b> property to access the <see cref="NameValueCollection"/>
		///		parameters for this <see cref="NamedElement"/> object.
		///	</remarks>
		public NameValueCollection Parameters
		{
			get
			{
				if (_parameters == null)
				{
					NamedElement settings = this;

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

		/// <summary>Gets or sets the name of the object configured by this class.</summary>
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base["name"]; }
			set { base["name"] = value; }
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
