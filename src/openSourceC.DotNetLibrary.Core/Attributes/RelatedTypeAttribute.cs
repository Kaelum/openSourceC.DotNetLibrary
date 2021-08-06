using System;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Allows the assignment of a related <see cref="T:Type"/> to an enumeration member.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class RelatedTypeAttribute : Attribute
	{
		/// <summary>
		///		Initializes a new instance of <see cref="T:RelatedTypeAttribute"/>.
		/// </summary>
		/// <param name="type">The related type.</param>
		public RelatedTypeAttribute(Type type)
		{
			Type = type;
		}

		/// <summary>Gets the related type.</summary>
		public Type Type { get; private set; }
	}
}
