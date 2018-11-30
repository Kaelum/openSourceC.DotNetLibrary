using System;
using System.Configuration;

namespace openSourceC.StandardLibrary.Configuration
{
	/// <summary>
	///		Represents a collection of named configuration elements.
	///	</summary>
	[ConfigurationCollection(typeof(NamedElement))]
	public class NamedElementCollection : NamedElementCollection<NamedElement> { }

	/// <summary>
	///		Provides a base implementation for a generic collection of named configuration elements
	///		that derive from <see cref="T:NamedElement"/>.
	///	</summary>
	///	<typeparam name="TSettingsElement">The collection element type.</typeparam>
	public abstract class NamedElementCollection<TSettingsElement> : ConfigurationElementCollection
		where TSettingsElement : NamedElement, new()
	{
		#region Constructors

		/// <summary>
		///		Creates a new instance of a <see cref="T:NamedElementCollection"/> class.
		///	</summary>
		public NamedElementCollection() : base(StringComparer.Ordinal) { }

		#endregion

		#region Index Accessors

		/// <summary>
		///		Gets or sets a value at the specified index in the
		///		<see cref="T:NamedElementCollection&lt;TSettings&gt;"/> collection.
		///	</summary>
		/// <param name="index">The index of the <typeparamref name="TSettingsElement"/> to return.</param>
		/// <value>The specified <typeparamref name="TSettingsElement"/>.</value>
		/// <remarks>
		///		Use the <see cref="P:this"/> property to get or set a specified
		///		<typeparamref name="TSettingsElement"/> object contained within this
		///		<see cref="T:NamedElementCollection&lt;TSettings&gt;"/> class
		/// </remarks>
		public TSettingsElement this[int index]
		{
			get { return (TSettingsElement)BaseGet(index); }

			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}

				BaseAdd(index, value);
			}
		}

		/// <summary>
		///		Gets an item from the collection.
		///	</summary>
		/// <param name="key">A string reference to the <typeparamref name="TSettingsElement"/> object
		///		within the collection.</param>
		/// <value>A <typeparamref name="TSettingsElement"/> object.</value>
		public new TSettingsElement this[string key]
		{
			get { return (TSettingsElement)BaseGet(key); }
		}

		#endregion

		#region Override Methods (protected)

		/// <summary>
		///		Creates a new <see cref="T:ConfigurationElement"/>.
		/// </summary>
		/// <returns>
		///		A new <typeparamref name="TSettingsElement"/>.
		/// </returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new TSettingsElement();
		}

		/// <summary>
		///		Gets the element key for the specified configuration element.
		/// </summary>
		/// <param name="element">The <see cref="T:ConfigurationElement"/> to return the key for.</param>
		/// <returns>
		///		An <see cref="object"/> that acts as the key for the specified
		///		<see cref="T:ConfigurationElement"/>.
		/// </returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TSettingsElement)element).Name;
		}

		#endregion
	}
}
