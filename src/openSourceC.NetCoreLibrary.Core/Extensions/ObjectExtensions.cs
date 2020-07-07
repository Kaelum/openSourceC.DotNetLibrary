using System;
using System.Reflection;

namespace openSourceC.NetCoreLibrary.Extensions
{
	/// <summary>
	///		Summary description of ObjectExtensions.
	/// </summary>
	public static class ObjectExtensions
	{
		/// <summary>
		///		Gets the value of the specified property.
		/// </summary>
		/// <param name="obj">The object being extended.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <returns>
		///		The value of the property.
		/// </returns>
		public static object? GetProperty(this object obj, string propertyName)
		{
			return GetProperty(obj, propertyName, false, true);
		}

		/// <summary>
		///		Gets the value of the specified property.
		/// </summary>
		/// <param name="obj">The object being extended.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="ignoreCase"><b>true</b> to ignore case, or <b>false</b> to regard case.</param>
		/// <returns>
		///		The value of the property.
		/// </returns>
		public static object? GetProperty(this object obj, string propertyName, bool ignoreCase)
		{
			return GetProperty(obj, propertyName, ignoreCase, true);
		}

		/// <summary>
		///		Gets the value of the specified property.
		/// </summary>
		/// <param name="obj">The object being extended.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="ignoreCase"><b>true</b> to ignore case, or <b>false</b> to regard case.</param>
		/// <param name="throwIfNotFound"><b>true</b> to throw an exception if the property does not
		///		exist, or <b>false</b> to return null.</param>
		/// <returns>
		///		The value of the property.
		/// </returns>
		public static object? GetProperty(this object obj, string propertyName, bool ignoreCase, bool throwIfNotFound)
		{
			BindingFlags bindingFlags = (BindingFlags.Instance | BindingFlags.Public | (ignoreCase ? BindingFlags.IgnoreCase : 0));
			PropertyInfo? propertyInfo = obj.GetType().GetProperty(propertyName, bindingFlags);

			if (propertyInfo == null)
			{
				if (throwIfNotFound)
				{
					throw new InvalidOperationException(string.Format("The {0} type does not have public property: {1}", obj.GetType().FullName, propertyName));
				}

				return null;
			}

			return propertyInfo.GetValue(obj);
		}

		/// <summary>
		///		Gets the <see cref="T:Type"/> of the specified property.
		/// </summary>
		/// <param name="obj">The object being extended.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <returns>
		///		The value of the property.
		/// </returns>
		public static Type GetPropertyType(this object obj, string propertyName)
		{
			return GetPropertyType(obj, propertyName, false);
		}

		/// <summary>
		///		Gets the <see cref="T:Type"/> of the specified property.
		/// </summary>
		/// <param name="obj">The object being extended.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="ignoreCase"><b>true</b> to ignore case; <b>false</b> to regard case.</param>
		/// <returns>
		///		The value of the property.
		/// </returns>
		public static Type GetPropertyType(this object obj, string propertyName, bool ignoreCase)
		{
			BindingFlags bindingFlags = (BindingFlags.Instance | BindingFlags.Public | (ignoreCase ? BindingFlags.IgnoreCase : 0));
			PropertyInfo? propertyInfo = obj.GetType().GetProperty(propertyName, bindingFlags);

			if (propertyInfo == null)
			{
				throw new InvalidOperationException(string.Format("The {0} type does not have public property: {1}", obj.GetType().FullName, propertyName));
			}

			return propertyInfo.PropertyType;
		}

		/// <summary>
		///		Sets the value of the specified property.
		/// </summary>
		/// <param name="obj">The object being extended.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="value">The new property value.</param>
		public static void SetProperty(this object obj, string propertyName, object value)
		{
			SetProperty(obj, propertyName, false, value);
		}

		/// <summary>
		///		Sets the value of the specified property.
		/// </summary>
		/// <param name="obj">The object being extended.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="ignoreCase"><b>true</b> to ignore case; <b>false</b> to regard case.</param>
		/// <param name="value">The new property value.</param>
		public static void SetProperty(this object obj, string propertyName, bool ignoreCase, object value)
		{
			BindingFlags bindingFlags = (BindingFlags.Instance | BindingFlags.Public | (ignoreCase ? BindingFlags.IgnoreCase : 0));
			PropertyInfo? propertyInfo = obj.GetType().GetProperty(propertyName, bindingFlags);

			if (propertyInfo == null)
			{
				throw new InvalidOperationException(string.Format("The {0} type does not have public property: {1}", obj.GetType().FullName, propertyName));
			}

			propertyInfo.SetValue(obj, value);
		}
	}
}
