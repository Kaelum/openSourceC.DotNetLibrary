using System;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Name formatting enum.
	/// </summary>
	public enum NameFormatEnum : byte
	{
		/// <summary>Default format (Last, First MI).</summary>
		Default,
		/// <summary>First Middle Last.</summary>
		FirstMiddleLast,
		/// <summary>First MI Last.</summary>
		FirstMILast,
		/// <summary>First Last.</summary>
		FirstLast,
		/// <summary>Last, First MI.</summary>
		LastFirstMI,
		/// <summary>Last, First Middle.</summary>
		LastFirstMiddle
	}
}
