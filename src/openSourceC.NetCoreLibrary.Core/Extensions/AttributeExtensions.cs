using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace openSourceC.NetCoreLibrary.Extensions
{
	/// <summary>
	///		Summary description for EnumExtensions.
	/// </summary>
	public static class AttributeExtensions
	{
		#region ActionNameAttribute

		/// <summary>
		///		Gets value of the <see cref="T:ActionNameAttribute"/> for the specified enumerator
		///		member.
		/// </summary>
		/// <param name="enumerator">The enumerator value.</param>
		/// <returns>
		///		The value of the <see cref="T:ActionNameAttribute"/> for the specified enumerator
		///		member if it exists; otherwise, the member name.
		/// </returns>
		public static string GetActionName(this Enum enumerator)
		{
			return (
				enumerator.GetType().GetField(enumerator.ToString())?.GetCustomAttributes(typeof(ActionNameAttribute), true).SingleOrDefault() is ActionNameAttribute attribute
				? attribute.ActionName
				: enumerator.ToString()
			);
		}

		#endregion

		#region DisplayAttribute

		/// <summary>
		///		Gets value of the <see cref="T:DisplayAttribute"/> for the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>
		///		The value of the <see cref="T:DisplayAttribute"/> for the specified object if it
		///		exists; otherwise, <b>null</b>.
		/// </returns>
		public static string? GetDisplay(this object obj)
		{
			return (
				obj.GetType().GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault() is DisplayAttribute attribute
				? attribute.Description
				: null
			);
		}

		#endregion

		#region DescriptionAttribute

		/// <summary>
		///		Gets value of the <see cref="T:DescriptionAttribute"/> for the specified enumerator
		///		member.
		/// </summary>
		/// <param name="enumerator">The enumerator value.</param>
		/// <returns>
		///		The value of the <see cref="T:DescriptionAttribute"/> for the specified enumerator
		///		member if it exists; otherwise, the lowercase value of the member name.
		/// </returns>
		public static string GetDescription(this Enum enumerator)
		{
			return (
				enumerator.GetType().GetField(enumerator.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), true).SingleOrDefault() is DescriptionAttribute attribute
				? attribute.Description
				: enumerator.ToString().ToLowerInvariant()
			);
		}

		#endregion

		#region EnumMemberAttribute

		/// <summary>
		///		Gets value of the <see cref="T:EnumMemberAttribute"/> for the specified enumerator
		///		member.
		/// </summary>
		/// <param name="enumerator">The enumerator value.</param>
		/// <returns>
		///		The value of the <see cref="T:EnumMemberAttribute"/> for the specified enumerator
		///		member if it exists; otherwise, the lowercase value of the member name.
		/// </returns>
		public static string GetEnumMember(this Enum enumerator)
		{
			return (
				enumerator.GetType().GetField(enumerator.ToString())?.GetCustomAttributes(typeof(EnumMemberAttribute), true).SingleOrDefault() is EnumMemberAttribute attribute
				? attribute.Value
				: enumerator.ToString().ToLowerInvariant()
			);
		}

		#endregion

		#region EnumMemberGuidAttribute

		/// <summary>
		///		Gets value of the <see cref="T:EnumMemberGuidAttribute"/> for the specified
		///		enumerator member.
		/// </summary>
		/// <param name="enumerator">The enumerator value.</param>
		/// <returns>
		///		The value of the <see cref="T:EnumMemberGuidAttribute"/> for the specified
		///		enumerator member if it exists; otherwise, null.
		/// </returns>
		public static Guid? GetEnumMemberGuid(this Enum enumerator)
		{
			return (
				enumerator.GetType().GetField(enumerator.ToString())?.GetCustomAttributes(typeof(EnumMemberGuidAttribute), true).SingleOrDefault() is EnumMemberGuidAttribute attribute
				? attribute.Value
				: (Guid?)null
			);
		}

		#endregion

		#region RelatedTypeAttribute

		/// <summary>
		///		Gets value of the <see cref="T:RelatedTypeAttribute"/> for the specified enumerator
		///		member.
		/// </summary>
		/// <param name="enumerator">The enumerator value.</param>
		/// <returns>
		///		The value of the <see cref="T:RelatedTypeAttribute"/> for the specified enumerator
		///		member if it exists; otherwise, null.
		/// </returns>
		public static Type? GetRelatedType(this Enum enumerator)
		{
			return (
				enumerator.GetType().GetField(enumerator.ToString())?.GetCustomAttributes(typeof(RelatedTypeAttribute), true).SingleOrDefault() is RelatedTypeAttribute attribute
				? attribute.Type
				: null
			);
		}

		#endregion

		#region XmlEnumAttribute

		/// <summary>
		///		Gets value of the <see cref="T:XmlEnumAttribute"/> for the specified enumerator
		///		member.
		/// </summary>
		/// <param name="enumerator">The enumerator value.</param>
		/// <returns>
		///		The value of the <see cref="T:XmlEnumAttribute"/> for the specified enumerator
		///		member if it exists; otherwise, the lowercase value of the member name.
		/// </returns>
		public static string GetXmlEnum(this Enum enumerator)
		{
			return (
				enumerator.GetType().GetField(enumerator.ToString())?.GetCustomAttributes(typeof(XmlEnumAttribute), true).SingleOrDefault() is XmlEnumAttribute attribute
				? attribute.Name
				: enumerator.ToString().ToLowerInvariant()
			);
		}

		#endregion
	}
}
