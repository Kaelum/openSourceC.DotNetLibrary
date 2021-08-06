using System;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Allows the assignment of an MVC controller action name to an enumeration member.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ActionNameAttribute : Attribute
	{
		/// <summary>
		///		Initializes a new instance of <see cref="T:ActionNameAttribute"/>.
		/// </summary>
		/// <param name="actionName">The action name.</param>
		public ActionNameAttribute(string actionName)
		{
			ActionName = actionName;
		}

		/// <summary>Gets the action name.</summary>
		public string ActionName { get; private set; }
	}
}
