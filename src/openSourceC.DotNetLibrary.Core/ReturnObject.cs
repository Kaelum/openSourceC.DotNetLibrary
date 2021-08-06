using System;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Summary description for ReturnObject.
	/// </summary>
	[Serializable]
	public class ReturnObject
	{
		#region Class Constructors

		/// <summary>
		///		Class constructor.
		/// </summary>
		public ReturnObject() { IsException = false; }

		/// <summary>
		///		Class constructor.
		/// </summary>
		/// <param name="ex">The exception used to create the exception message.</param>
		public ReturnObject(Exception ex)
		{
			ProcessException(ex);
		}

		/// <summary>
		///		Class constructor.
		/// </summary>
		/// <param name="ex">The exception used to create the exception message.</param>
		/// <param name="includeDebugMessage">A <b>true</b> value will set the <see cref="P:DebugMessage"/>
		///		property, while a <b>false</b> value will set it to null.</param>
		public ReturnObject(Exception ex, bool includeDebugMessage)
		{
			ProcessException(ex, includeDebugMessage);
		}

		/// <summary>
		///		Class constructor.
		/// </summary>
		/// <param name="ex">The exception used to create the exception message.</param>
		/// <param name="returnCode">Return code.</param>
		public ReturnObject(Exception ex, int? returnCode)
		{
			ProcessException(ex, returnCode);
		}

		/// <summary>
		///		Class constructor.
		/// </summary>
		/// <param name="ex">The exception used to create the exception message.</param>
		/// <param name="returnCode">Return code.</param>
		/// <param name="includeDebugMessage">A <b>true</b> value will set the <see cref="P:DebugMessage"/>
		///		property, while a <b>false</b> value will set it to null.</param>
		public ReturnObject(Exception ex, int? returnCode, bool includeDebugMessage)
		{
			ProcessException(ex, returnCode, includeDebugMessage);
		}

		#endregion

		#region Public Properties

		/// <summary>Gets or sets the detailed exception message with debug information.</summary>
		public string? DebugMessage { get; set; }

		/// <summary>Gets or sets the exception message.</summary>
		public string? ExceptionMessage { get; set; }

		/// <summary>Gets or sets a vlue indicating that an exception occured.</summary>
		public bool IsException { get; set; }

		/// <summary>Gets or sets the return code.</summary>
		public int? ReturnCode { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		///		Sets the properties based on the supplied parameters.
		/// </summary>
		/// <param name="ex">The exception used to create the exception message.</param>
		public void ProcessException(Exception ex)
		{
			ProcessException(ex, null, false);
		}

		/// <summary>
		///		Sets the properties based on the supplied parameters.
		/// </summary>
		/// <param name="ex">The exception used to create the exception message.</param>
		/// <param name="includeDebugMessage">A <b>true</b> value will set the <see cref="P:DebugMessage"/>
		///		property, while a <b>false</b> value will set it to null.</param>
		public void ProcessException(Exception ex, bool includeDebugMessage)
		{
			ProcessException(ex, null, includeDebugMessage);
		}

		/// <summary>
		///		Sets the properties based on the supplied parameters.
		/// </summary>
		/// <param name="ex">The exception used to create the exception message.</param>
		/// <param name="returnCode">Return code.</param>
		public void ProcessException(Exception ex, int? returnCode)
		{
			ProcessException(ex, returnCode, false);
		}

		/// <summary>
		///		Sets the properties based on the supplied parameters.
		/// </summary>
		/// <param name="ex">The exception used to create the exception message.</param>
		/// <param name="returnCode">Return code.</param>
		/// <param name="includeDebugMessage">A <b>true</b> value will set the <see cref="P:DebugMessage"/>
		///		property, while a <b>false</b> value will set it to null.</param>
		public void ProcessException(Exception ex, int? returnCode, bool includeDebugMessage)
		{
			IsException = true;

			ExceptionMessage = ex.Message;
			ReturnCode = returnCode;

			if (includeDebugMessage)
			{
				if (ex is OscException)
				{
					DebugMessage = Format.Exception(ex.InnerException, ex.Message);
				}
				else
				{
					DebugMessage = Format.Exception(ex);
				}
			}
			else
			{
				DebugMessage = null;
			}
		}

		#endregion
	}
}
