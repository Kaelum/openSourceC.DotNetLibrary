using System;
using System.ComponentModel.DataAnnotations;

namespace openSourceC.StandardLibrary.Configuration
{
	/// <summary>
	///		Represents a provider settings object.
	/// </summary>
	public class ProviderSettings //: ConfigurationElement
	{
		//private NameValueCollection _parameters;


		#region Attributes

		///// <summary>Gets a collection of user-defined parameters.</summary>
		///// <value>A <see cref="NameValueCollection"/> of parameters for the setting.</value>
		///// <remarks>
		/////		Use the <b>Parameters</b> property to access the <see cref="NameValueCollection"/>
		/////		parameters for this <see cref="ProviderSettings"/> object.
		/////	</remarks>
		//public NameValueCollection Parameters
		//{
		//	get
		//	{
		//		if (_parameters == null)
		//		{
		//			ProviderSettings settings = this;

		//			lock (settings)
		//			{
		//				if (_parameters == null)
		//				{
		//					_parameters = new NameValueCollection(StringComparer.Ordinal);
		//				}
		//			}
		//		}

		//		return _parameters;
		//	}
		//}

		/// <summary>Gets or sets the type of the object configured by this class.</summary>
		[Required]
		public string Type { get; set; }

		#endregion

		//#region Override Methods

		///// <summary>
		/////		 Invoked when an unknown attribute is encountered while deserializing the
		/////		 ConfigurationElement object.
		///// </summary>
		///// <param name="name">The name of the unrecognized attribute.</param>
		///// <param name="value">The value of the unrecognized attribute.</param>
		///// <returns>
		/////		<b>true</b> to signify that deserialization succeeded.
		///// </returns>
		//protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		//{
		//	ConfigurationProperty property = new ConfigurationProperty(name, typeof(string));
		//	base[property] = value;
		//	Parameters[name] = value;

		//	return true;
		//}

		//#endregion
	}
}
