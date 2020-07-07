using System;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Supplies an explicit <see cref="T:System.Guid" /> for <see cref="Enum"/> element.
	///	</summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class EnumMemberGuidAttribute : Attribute
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="T:EnumMemberGuidAttribute" /> class
		///		with the specified GUID.
		///	</summary>
		/// <param name="guid">The <see cref="T:System.Guid" /> to be assigned. </param>
		public EnumMemberGuidAttribute(string guid)
		{
			Value = new Guid(guid);
		}

		/// <summary>Gets the <see cref="T:System.Guid" /> of the element.</summary>
		public Guid Value { get; private set; }
	}
}
