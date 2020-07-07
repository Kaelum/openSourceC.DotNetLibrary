using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace openSourceC.NetCoreLibrary.Data.SqlClient
{
	/// <summary>
	///		Summary description for SqlDbHelper.
	/// </summary>
	public static class SqlDbHelper
	{
		/// <summary>
		///		Converts the <see cref="SqlCommand"/> to a string that can be executed in the
		///		database query editor.
		/// </summary>
		/// <param name="command">The command to process.</param>
		///	<returns>
		///		A string representation of the command.
		///	</returns>
		public static string CommandToString(SqlCommand command)
		{
			StringBuilder declareString = new StringBuilder();
			StringBuilder setString = new StringBuilder();
			StringBuilder execString = new StringBuilder();
			StringBuilder printString = new StringBuilder();

			bool firstPass;
			string prmValue;
			string varType;


			execString = execString.AppendFormat("EXEC @rc = {0}", command.CommandText);

			if (command.Parameters.Count > 0)
			{
				firstPass = true;

				foreach (SqlParameter prm in command.Parameters)
				{
					if (prm.Direction.Equals(ParameterDirection.ReturnValue))
					{
						// Do nothing
					}
					else
					{
						if (!firstPass)
						{
							execString.Append(",");
						}

						if (
							prm.Value == null
							|| prm.Value == DBNull.Value
							|| (prm.Value is SqlXml && ((SqlXml)prm.Value).IsNull)
							|| prm.Direction.Equals(ParameterDirection.Output)
						)
						{
							prmValue = "NULL";
						}
						else
						{
							switch (prm.SqlDbType)
							{
								case SqlDbType.Char:
								case SqlDbType.NChar:
								case SqlDbType.NText:
								case SqlDbType.NVarChar:
								case SqlDbType.Text:
								case SqlDbType.VarChar:
								{
									prmValue = string.Format("'{0}'", prm.Value.ToString().Replace("'", "''"));
									break;
								}

								case SqlDbType.BigInt:
								case SqlDbType.Int:
								case SqlDbType.SmallInt:
								case SqlDbType.TinyInt:
								{
									if (prm.Value.GetType().IsEnum)
									{
										object val = Convert.ChangeType(prm.Value, Enum.GetUnderlyingType(prm.Value.GetType()));
										prmValue = string.Format("{0}", val.ToString().Replace("'", "''"));
									}
									else
									{
										prmValue = string.Format("{0}", prm.Value.ToString().Replace("'", "''"));
									}
									break;
								}

								case SqlDbType.Decimal:
								case SqlDbType.Float:
								case SqlDbType.Money:
								case SqlDbType.Real:
								case SqlDbType.SmallMoney:
								{
									prmValue = string.Format("{0}", prm.Value.ToString().Replace("'", "''"));
									break;
								}

								case SqlDbType.Binary:
								case SqlDbType.Image:
								case SqlDbType.Timestamp:
								case SqlDbType.VarBinary:
								{
									prmValue = string.Format("'{0}'", prm.Value.ToString().Replace("'", "''"));
									break;
								}

								case SqlDbType.Bit:
								{
									prmValue = ((bool)prm.Value ? "1" : "0");
									break;
								}

								case SqlDbType.Date:
								{
									prmValue = string.Format("'{0}'", ((DateTime)prm.Value).ToString("MM/dd/yyyy").Replace("'", "''"));
									break;
								}

								case SqlDbType.DateTime:
								{
									prmValue = string.Format("'{0}'", ((DateTime)prm.Value).ToString("MM/dd/yyyy HH:mm:ss.fff").Replace("'", "''"));
									break;
								}

								case SqlDbType.DateTime2:
								{
									prmValue = string.Format("'{0}'", ((DateTime)prm.Value).ToString("MM/dd/yyyy HH:mm:ss.fffffff").Replace("'", "''"));
									break;
								}

								case SqlDbType.DateTimeOffset:
								{
									prmValue = string.Format("'{0}'", ((DateTimeOffset)prm.Value).ToString("MM/dd/yyyy HH:mm:ss.fffffffzzz").Replace("'", "''"));
									break;
								}

								case SqlDbType.SmallDateTime:
								{
									prmValue = string.Format("'{0}'", ((DateTime)prm.Value).ToString("MM/dd/yyyy HH:mm").Replace("'", "''"));
									break;
								}

								case SqlDbType.Time:
								{
									prmValue = string.Format("'{0}'", ((DateTime)prm.Value).ToString("HH:mm:ss").Replace("'", "''"));
									break;
								}

								case SqlDbType.UniqueIdentifier:
								{
									prmValue = string.Format("'{0}'", ((Guid)prm.Value).ToString("D").ToUpper().Replace("'", "''"));
									break;
								}

								case SqlDbType.Variant:
								{
									prmValue = string.Format("'{0}'", prm.Value.ToString().Replace("'", "''"));
									break;
								}

								case SqlDbType.Xml:
								{
									prmValue = string.Format("'{0}'", (prm.Value is SqlXml ? ((SqlXml)prm.Value).Value : (string)prm.Value).Replace("'", "''"));
									break;
								}

								case SqlDbType.Structured:
								case SqlDbType.Udt:
								default:
								{
									throw new ApplicationException("Unknown parameter data type.");
								}
							}
						}

						if (prm.Direction.Equals(ParameterDirection.InputOutput) || prm.Direction.Equals(ParameterDirection.Output))
						{
							switch (prm.SqlDbType)
							{
								case SqlDbType.BigInt:
								{
									varType = "bigint";
									break;
								}

								case SqlDbType.Binary:
								{
									varType = string.Format("binary({0})", prm.Size);
									break;
								}

								case SqlDbType.Bit:
								{
									varType = "bit";
									break;
								}

								case SqlDbType.Char:
								{
									varType = string.Format("char({0})", prm.Size);
									break;
								}

								case SqlDbType.Date:
								{
									varType = "date";
									break;
								}

								case SqlDbType.DateTime:
								{
									varType = "datetime";
									break;
								}

								case SqlDbType.DateTime2:
								{
									varType = "datetime2";

									if (prm.Precision != 0)
									{
										varType = string.Format("{0}({1})", varType, prm.Precision);
									}
									break;
								}

								case SqlDbType.DateTimeOffset:
								{
									varType = "datetimeoffset";

									if (prm.Precision != 0)
									{
										varType = string.Format("{0}({1})", varType, prm.Precision);
									}
									break;
								}

								case SqlDbType.Decimal:
								{
									varType = "decimal";

									if (prm.Precision != 0)
									{
										varType = string.Format("{0}({1},{2})", varType, prm.Precision, prm.Scale);
									}
									break;
								}

								case SqlDbType.Float:
								{
									varType = "float";

									if (prm.Precision != 0)
									{
										varType = string.Format("{0}({1})", varType, prm.Precision);
									}
									break;
								}

								case SqlDbType.Image:
								{
									varType = "varbinary(max)";
									break;
								}

								case SqlDbType.Int:
								{
									varType = "int";
									break;
								}

								case SqlDbType.Money:
								{
									varType = "money";
									break;
								}

								case SqlDbType.NChar:
								{
									varType = string.Format("nchar({0})", prm.Size);
									break;
								}

								case SqlDbType.NText:
								{
									varType = "nvarchar(max)";
									break;
								}

								case SqlDbType.NVarChar:
								{
									varType = string.Format("nvarchar({0})", ParameterSizeToString(prm.Size));
									break;
								}

								case SqlDbType.Real:
								{
									varType = "real";
									break;
								}

								case SqlDbType.SmallDateTime:
								{
									varType = "smalldatetime";
									break;
								}

								case SqlDbType.SmallInt:
								{
									varType = "smallint";
									break;
								}

								case SqlDbType.SmallMoney:
								{
									varType = "smallmoney";
									break;
								}

								//case SqlDbType.Structured:
								//{
								//    varType = "";
								//    break;
								//}

								case SqlDbType.Text:
								{
									varType = "varchar(max)";
									break;
								}

								case SqlDbType.Time:
								{
									varType = "time";

									if (prm.Precision != 0)
									{
										varType = string.Format("{0}({1})", varType, prm.Precision);
									}
									break;
								}

								case SqlDbType.Timestamp:
								{
									varType = "timestamp";
									break;
								}

								case SqlDbType.TinyInt:
								{
									varType = "tinyint";
									break;
								}

								//case SqlDbType.Udt: // User defined type
								//{
								//    varType = "";
								//    break;
								//}

								case SqlDbType.UniqueIdentifier:
								{
									varType = "uniqueidentifier";
									break;
								}

								case SqlDbType.VarBinary:
								{
									varType = string.Format("varbinary({0})", ParameterSizeToString(prm.Size));
									break;
								}

								case SqlDbType.VarChar:
								{
									varType = string.Format("varchar({0})", ParameterSizeToString(prm.Size));
									break;
								}

								case SqlDbType.Variant:
								{
									varType = "sql_variant";
									break;
								}

								case SqlDbType.Xml:
								{
									varType = "xml";
									break;
								}

								default:
								{
									throw new ApplicationException("Unknown parameter data type.");
								}
							}

							declareString.AppendFormat("DECLARE {0}_out {1};\n", prm.ParameterName, varType);
							setString.AppendFormat("SET {0}_out = {1};\n", prm.ParameterName, prmValue);
							execString.AppendFormat("\n\t{0} = {0}_out OUTPUT", prm.ParameterName);
							printString.AppendFormat("PRINT '{0} OUTPUT: ' + CAST({0}_out AS varchar(max));\n", prm.ParameterName);
						}
						else
						{
							execString.AppendFormat("\n\t{0} = {1}", prm.ParameterName, prmValue);
						}

						firstPass = false;
					}
				}
			}

			declareString = declareString.Append("DECLARE @rc int;\n");

			if (setString.Length > 0) { setString.Append("\n"); }
			if (printString.Length > 0) { printString.Append("\n"); }

			printString.Append("PRINT 'Return Code: ' + CAST(@rc AS varchar(max));\n");

			return string.Format("{0}\n{1}{2};\n\n{3}", declareString.ToString(), setString.ToString(), execString.ToString(), printString.ToString());
		}

		#region Private Methods

		private static string ParameterSizeToString(int parameterSize)
		{
			if (parameterSize == int.MaxValue)
			{
				return "max";
			}

			return parameterSize.ToString();
		}

		#endregion
	}
}
