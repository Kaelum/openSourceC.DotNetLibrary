using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		Summary description for LocationInfo.
	/// </summary>
	[Serializable]
	public class LocationInfo
	{
		private const string DEFAULT_FOR_MISSING_OR_NULL_VALUE = "?";


		#region Constructors

		/// <summary>
		///		Constructor.
		///		<para>The <see cref="T:StackTrace"/> will be traced back to the point before the
		///		call into the <paramref name="calledType"/> class to get the location information
		///		of the caller.</para>
		/// </summary>
		/// <param name="calledType">The <see cref="T:Type"/> of the class that was called from the
		///		main code.</param>
		public LocationInfo(Type calledType)
		{
			ClassName = DEFAULT_FOR_MISSING_OR_NULL_VALUE;
			MethodName = DEFAULT_FOR_MISSING_OR_NULL_VALUE;
			FileName = DEFAULT_FOR_MISSING_OR_NULL_VALUE;
			LineNumber = DEFAULT_FOR_MISSING_OR_NULL_VALUE;

			if (calledType == null) { return; }

			try
			{
				StackTrace trace = new StackTrace(true);
				StackFrame frame = null;
				int index = 0;

				while (index < trace.FrameCount)
				{
					frame = trace.GetFrame(index);

					if (frame != null && frame.GetMethod().DeclaringType == calledType)
					{
						break;
					}

					index++;
				}

				while (index < trace.FrameCount)
				{
					frame = trace.GetFrame(index);

					if (frame != null && frame.GetMethod().DeclaringType != calledType)
					{
						break;
					}

					index++;
				}

				if (index >= trace.FrameCount || frame == null) { return; }

				MethodBase method = frame.GetMethod();

				if (method != null)
				{
					MethodName = method.Name;

					if (method.DeclaringType != null)
					{
						ClassName = method.DeclaringType.FullName;
					}
				}

				FileName = frame.GetFileName();
				LineNumber = frame.GetFileLineNumber().ToString(NumberFormatInfo.InvariantInfo);
			}
			catch (SecurityException)
			{
				try
				{
					string message = "LocationInfo: Security exception while trying to get caller stack frame. Error Ignored. Location Information Not Available.";

					Console.Out.WriteLine(message);
					Trace.WriteLine(message);
				}
				catch { }

			}
		}

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="className"></param>
		/// <param name="methodName"></param>
		/// <param name="fileName"></param>
		/// <param name="lineNumber"></param>
		public LocationInfo(string className, string methodName, string fileName, string lineNumber)
		{
			ClassName = (string.IsNullOrWhiteSpace(className) ? DEFAULT_FOR_MISSING_OR_NULL_VALUE : className);
			MethodName = (string.IsNullOrWhiteSpace(methodName) ? DEFAULT_FOR_MISSING_OR_NULL_VALUE : methodName);
			FileName = (string.IsNullOrWhiteSpace(fileName) ? DEFAULT_FOR_MISSING_OR_NULL_VALUE : fileName);
			LineNumber = (string.IsNullOrWhiteSpace(lineNumber) ? DEFAULT_FOR_MISSING_OR_NULL_VALUE : lineNumber);
		}

		#endregion

		#region Public Properties

		/// <summary>Gets the name of the class.</summary>
		public string ClassName { get; private set; }

		/// <summary>Gets the name of the file.</summary>
		public string FileName { get; private set; }

		/// <summary>Gets the line number within the file.</summary>
		public string LineNumber { get; private set; }

		/// <summary>Gets the name of the method.</summary>
		public string MethodName { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		///		Gets the location information in the following format:
		///		<para>&lt;Class Name&gt;.&lt;Method Name&gt;(&lt;File Name&gt;:&lt;Line Number&gt;)</para>
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Concat(ClassName, '.', MethodName, '(', FileName, ':', LineNumber, ')');
		}

		#endregion
	}
}
