//#define USE_EXCEPTION_TOSTRING

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace openSourceC.NetCoreLibrary
{
	/// <summary>
	///		Summary description for Format.
	/// </summary>
	public static class Format
	{
		private static Regex _nonDigit = new Regex(@"\D", RegexOptions.Compiled);


		/// <summary>
		///		Formats an Employer Identification Number.
		/// </summary>
		/// <param name="input">The number to format.</param>
		/// <returns>
		///		A correctly formatted Employer Identification Number.
		///	</returns>
		public static string EIN(string input)
		{
			return EIN(input, false);
		}

		/// <summary>
		///		Formats an Employer Identification Number with or without formatting.
		/// </summary>
		/// <param name="input">The number to format.</param>
		/// <param name="removeFormatting">A value indicating that an unformatted value is to be
		///		returned.</param>
		/// <returns>
		///		A correctly formatted Employer Identification Number.
		///	</returns>
		public static string EIN(string input, bool removeFormatting)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}

			// Check to see if input is a formatted EIN.
			if (StringValidate.IsEIN(input, false))
			{
				if (removeFormatting)
				{
					return _nonDigit.Replace(input, string.Empty);
				}
				else
				{
					return input;
				}
			}

			// Check to see if input is an EIN, formatted or not. - Unformatted
			if (StringValidate.IsEIN(input, true))
			{
				if (removeFormatting)
				{
					return input;
				}
				else
				{
					return string.Format("{0}-{1}", input.Substring(0, 2), input.Substring(2, 7));
				}
			}

			throw new ArgumentException("Parameter is not a valid Employer Identification Number.", "input");
		}

		/// <summary>
		///		Returns a null for empty strings.
		/// </summary>
		/// <param name="input">The number to format.</param>
		/// <returns>
		///		A null if input is empty or null; otherwise, input.
		///	</returns>
		public static string? EmptyAsNull(string input)
		{
			return (string.IsNullOrEmpty(input) ? null : input);
		}

		/// <summary>
		///     Formats a message for logging from an exception.
		/// </summary>
		/// <param name="e">The Exception object to format.</param>
		/// <retvalue>
		///     <para>A nicely formatted exception string, including message and StackTrace information.</para>
		/// </retvalue>
		public static string Exception(Exception e)
		{
			return Exception(e, null);
		}

		/// <summary>
		///     Formats a message for logging from an exception.
		/// </summary>
		/// <param name="e">The Exception object to format.</param>
		/// <param name="catchMsg">The string to prepend to the exception information.</param>
		/// <retvalue>
		///     <para>A nicely formatted exception string, including message and StackTrace information.</para>
		/// </retvalue>
		//[RegistryPermission(SecurityAction.Assert)]
		public static string Exception(Exception? e, string? catchMsg)
		{
			StringBuilder strBuilder;
			StackTrace st;
			StackFrame? sf;
			MethodBase? mb;


			strBuilder = new StringBuilder();

			if (e == null)
			{
				if (!string.IsNullOrEmpty(catchMsg))
				{
					strBuilder.Append(catchMsg);
				}
			}
			else
			{
				st = new StackTrace(e, true);

				if (st.FrameCount > 0)
				{
					sf = st.GetFrame(st.FrameCount - 1);
					mb = sf?.GetMethod();

					if (mb != null && mb.DeclaringType != null)
					{
						strBuilder.AppendFormat("{0}.{1}", mb.DeclaringType.FullName, mb.Name);
					}

#if DEBUG
					if (sf != null && sf.GetFileLineNumber() != 0)
					{
						strBuilder.AppendFormat(": line {0}", sf.GetFileLineNumber());
					}
#endif
				}

				if (!string.IsNullOrEmpty(catchMsg))
				{
					strBuilder.AppendFormat(": {0}", catchMsg);
				}
			}

#if USE_EXCEPTION_TOSTRING
			if (strBuilder.Length != 0)
			{
				strBuilder
					.Append(Environment.NewLine)
					.Append(Environment.NewLine);
			}

			string exceptionToString = e.ToString();
			exceptionToString = exceptionToString == null ? null : exceptionToString.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);

			strBuilder.Append(exceptionToString);
#endif

			while (e != null)
			{
#if !USE_EXCEPTION_TOSTRING
				if (strBuilder.Length != 0)
				{
					strBuilder
						.Append(Environment.NewLine)
						.Append(Environment.NewLine);
				}

				strBuilder.Append(e.GetType());

				if (e is System.Runtime.InteropServices.ExternalException)
				{
					strBuilder.AppendFormat(" (0x{0,8:X})", ((System.Runtime.InteropServices.ExternalException)e).ErrorCode);
				}

				string message = e.Message;
				string? stackTrace = (e.StackTrace?.Replace("\r\n", "\n").Replace("\n", Environment.NewLine));

				strBuilder
					.Append(": ")
					.AppendLine(message)
					.Append(Environment.NewLine)
					//.AppendLine("Stack Trace:")
					.AppendLine(stackTrace);
#endif

				if (e is OscException ex)
				{
					if (!string.IsNullOrWhiteSpace(ex.ExtendedMessage))
					{
						strBuilder
							.Append(Environment.NewLine)
							.Append(Environment.NewLine)
							.Append("Extended Message:")
							.Append(Environment.NewLine)
							.Append(ex.ExtendedMessage);
					}
				}
				//else if (e is System.Data.SqlClient.SqlException ex)
				//{
				//	strBuilder
				//		.Append(Environment.NewLine)
				//		.Append(Environment.NewLine)
				//		.Append("SQL Errors:")
				//		.Append(Environment.NewLine);

				//	for (int i = 0; i < ex.Errors.Count; i++)
				//	{
				//		strBuilder.AppendFormat("\t{1}: Msg {2}, Level {3}, State {4}, Line {5}{0}\t\tSource: {6}{0}\t\tProcedure: {7}{0}\t\tMessage: {8}",
				//			Environment.NewLine,
				//			i,
				//			ex.Errors[i].Number,
				//			ex.Errors[i].Class,
				//			ex.Errors[i].State,
				//			ex.Errors[i].LineNumber,
				//			ex.Errors[i].Source,
				//			ex.Errors[i].Procedure,
				//			ex.Errors[i].Message
				//		);
				//	}
				//}

				e = e.InnerException;
			}

			return strBuilder.ToString();
		}

		/// <summary>
		///		Format a persons name.
		/// </summary>
		///	<param name="format">The format to use.</param>
		///	<param name="FirstName">First name.</param>
		///	<param name="MiddleName">Middle name.</param>
		///	<param name="LastName">Last name.</param>
		///	<returns>
		///		<para>
		///			A formatted string representation of a person's name.
		///		</para>
		///	</returns>
		public static string Name(NameFormatEnum format, string FirstName, string MiddleName, string LastName)
		{
			StringBuilder name = new StringBuilder();


			FirstName = (FirstName == null ? string.Empty : FirstName.Trim());
			MiddleName = (MiddleName == null ? string.Empty : MiddleName.Trim());
			LastName = (LastName == null ? string.Empty : LastName.Trim());

			switch (format)
			{
				case NameFormatEnum.FirstLast:
				{
					if (!FirstName.Equals(string.Empty))
					{
						name.Append(FirstName);
					}

					if (!LastName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(LastName);
					}

					break;
				}

				case NameFormatEnum.FirstMiddleLast:
				{
					if (!FirstName.Equals(string.Empty))
					{
						name.Append(FirstName);
					}

					if (!MiddleName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(MiddleName);

						if (MiddleName.Length == 1)
						{
							name.Append(".");
						}
					}

					if (!LastName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(LastName);
					}

					break;
				}

				case NameFormatEnum.FirstMILast:
				{
					if (!FirstName.Equals(string.Empty))
					{
						name.Append(FirstName);
					}

					if (!MiddleName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(MiddleName.Substring(0, 1)).Append(".");
					}

					if (!LastName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(LastName);
					}

					break;
				}

				case NameFormatEnum.LastFirstMI:
				{
					if (!LastName.Equals(string.Empty))
					{
						name.Append(LastName);

						if ((!FirstName.Equals(string.Empty)) || (!MiddleName.Equals(string.Empty)))
						{
							name.Append(",");
						}
					}

					if (!FirstName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(FirstName);
					}

					if (!MiddleName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(MiddleName.Substring(0, 1)).Append(".");
					}

					break;
				}

				case NameFormatEnum.LastFirstMiddle:
				{
					if (!LastName.Equals(string.Empty))
					{
						name.Append(LastName);

						if ((!FirstName.Equals(string.Empty)) || (!MiddleName.Equals(string.Empty)))
						{
							name.Append(",");
						}
					}

					if (!FirstName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(FirstName);
					}

					if (!MiddleName.Equals(string.Empty))
					{
						if (name.Length > 0)
						{
							name.Append(" ");
						}

						name.Append(MiddleName);

						if (MiddleName.Length == 1)
						{
							name.Append(".");
						}
					}

					break;
				}

				default:
				{
					goto case NameFormatEnum.LastFirstMI;
				}
			}

			return name.ToString();
		}

		/// <summary>
		///		Formats a Social Security Number.
		/// </summary>
		/// <param name="input">The number to format.</param>
		/// <returns>
		///		A correctly formatted Social Security Number.
		/// </returns>
		public static string SSN(string input)
		{
			return SSN(input, false);
		}

		/// <summary>
		///		Formats a Social Security Number with or without formatting.
		/// </summary>
		/// <param name="input">The number to format.</param>
		/// <param name="removeFormatting">A value indicating that an unformatted value is to be
		///		returned.</param>
		/// <returns>
		///		A correctly formatted Social Security Number.
		/// </returns>
		public static string SSN(string input, bool removeFormatting)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}

			// Check to see if input is a formatted SSN.
			if (StringValidate.IsSSN(input, false))
			{
				if (removeFormatting)
				{
					return _nonDigit.Replace(input, string.Empty);
				}
				else
				{
					return input;
				}
			}

			// Check to see if input is an SSN, formatted or not. - Unformatted
			if (StringValidate.IsSSN(input, true))
			{
				if (removeFormatting)
				{
					return input;
				}
				else
				{
					return string.Format("{0}-{1}-{2}", input.Substring(0, 3), input.Substring(3, 2), input.Substring(5, 4));
				}
			}

			throw new ArgumentException("Parameter is not a valid Social Security Number.", "input");
		}

		/// <summary>
		///		Formats a string using title case.
		/// </summary>
		/// <param name="input">The string to format.</param>
		/// <returns>
		///		A correctly formatted title case string.
		/// </returns>
		public static string TitleCase(string input)
		{
			StringBuilder output = new StringBuilder();
			string[] wordArray = input.Split(new char[] { ' ' });

			for (int i = 0; i < wordArray.Length; i++)
			{
				if (i > 0)
				{
					output.Append(" ");
				}

				output.Append(wordArray[i].Substring(0, 1).ToUpper());

				if (wordArray[i].Length > 1)
				{
					output.Append(wordArray[i].Substring(1).ToLower());
				}
			}

			return output.ToString();
		}

		/// <summary>
		///		Formats a zip code.
		/// </summary>
		/// <param name="input">The number to format.</param>
		/// <returns>
		///		A correctly formatted zip code.
		///	</returns>
		public static string ZipCode(string input)
		{
			return ZipCode(input, false);
		}

		/// <summary>
		///		Formats a zip code with or without formatting.
		/// </summary>
		/// <param name="input">The number to format.</param>
		/// <param name="removeFormatting">A value indicating that an unformatted value is to be
		///		returned.</param>
		/// <returns>
		///		A correctly formatted zip.
		///	</returns>
		public static string ZipCode(string input, bool removeFormatting)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}

			// Check to see if input is a formatted EIN.
			if (StringValidate.IsZipCode(input, false))
			{
				if (removeFormatting)
				{
					return _nonDigit.Replace(input, string.Empty);
				}
				else
				{
					return input;
				}
			}

			// Check to see if input is an EIN, formatted or not. - Unformatted
			if (StringValidate.IsZipCode(input, true))
			{
				if (removeFormatting)
				{
					return input;
				}
				else
				{
					if (input.Length > 5)
					{
						return string.Format("{0}-{1}", input.Substring(0, 5), input.Substring(5, 4));
					}
					else
					{
						return string.Format("{0}", input);
					}
				}
			}

			throw new ArgumentException("Parameter is not a valid Employer Identification Number.", "input");
		}
	}
}
