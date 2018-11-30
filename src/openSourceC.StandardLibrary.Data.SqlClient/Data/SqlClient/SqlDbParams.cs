using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace openSourceC.StandardLibrary.Data.SqlClient
{
	/// <summary>
	///		Adds <see cref="T:Nullable&lt;T&gt;"/> type parameter support to a
	///		<see cref="SqlCommand"/> object to make it easier to use.
	/// </summary>
	public sealed class SqlDbParams : DbParamsBase<SqlDbParams, SqlCommand, SqlParameter>
	{
		#region Public Methods

		#region Add Parameters

		#region AddAnsiStringMax

		/// <summary>
		///		Add a parameter representing a maximum-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddAnsiStringMax(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiStringMax(parameterName, true, (string)null, direction);
		}

		/// <summary>
		///		Add a parameter representing a maximum-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddAnsiStringMax(string parameterName, bool isNullable, string value, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = CreateParameter(null, parameterName, isNullable, value, direction);
			prm.SqlDbType = SqlDbType.VarChar;
			prm.Size = int.MaxValue;

			return prm;
		}

		/// <summary>
		///		Add a parameter representing a maximum-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddAnsiStringMax(string parameterName, bool isNullable, char? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiStringMax(parameterName, isNullable, value.ToString(), direction);
		}

		/// <summary>
		///		Add a parameter representing a maximum-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddAnsiStringMax(string parameterName, bool isNullable, char[] value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiStringMax(parameterName, isNullable, string.Concat(value), direction);
		}

		/// <summary>
		///		Add a parameter representing a maximum-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddAnsiStringMax(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiStringMax(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a maximum-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddAnsiStringMax(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = AddAnsiStringMax(parameterName, true, (string)null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddBinaryMax

		/// <summary>
		///		Add a parameter representing a variable-length stream of binary(max) data to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddBinaryMax(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddBinaryMax(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length stream of binary(max) data to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddBinaryMax(string parameterName, bool isNullable, byte[] value, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = CreateParameter(null, parameterName, isNullable, value, direction);
			prm.SqlDbType = SqlDbType.VarBinary;
			prm.Size = int.MaxValue;

			return prm;
		}

		/// <summary>
		///		Add a parameter representing a variable-length stream of binary(max) data to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddBinaryMax(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddBinaryMax(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length stream of binary(max) data to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddBinaryMax(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = AddBinaryMax(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddMoney

		/// <summary>
		///		Add a parameter representing values ranging from 1.0 x 10^-28 to approximately
		///		7.9 x 10^28 with 28-29 significant digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddMoney(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddMoney(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing values ranging from 1.0 x 10^-28 to approximately
		///		7.9 x 10^28 with 28-29 significant digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddMoney(string parameterName, bool isNullable, decimal? value, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = CreateParameter(null, parameterName, isNullable, value, direction);
			prm.SqlDbType = SqlDbType.Money;

			return prm;
		}

		/// <summary>
		///		Add a parameter representing values ranging from 1.0 x 10^-28 to approximately
		///		7.9 x 10^28 with 28-29 significant digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddMoney(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddMoney(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing values ranging from 1.0 x 10^-28 to approximately
		///		7.9 x 10^28 with 28-29 significant digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddMoney(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = AddMoney(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddStringMax

		/// <summary>
		///		Add a parameter representing a maximum-length Unicode character string
		///		(nvarchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddStringMax(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddStringMax(parameterName, true, (string)null, direction);
		}

		/// <summary>
		///		Add a parameter representing a maximum-length Unicode character string
		///		(nvarchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddStringMax(string parameterName, bool isNullable, string value, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = CreateParameter(null, parameterName, isNullable, value, direction);
			prm.SqlDbType = SqlDbType.NVarChar;
			prm.Size = int.MaxValue;

			return prm;
		}

		/// <summary>
		///		Add a parameter representing a maximum-length Unicode character string
		///		(nvarchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddStringMax(string parameterName, bool isNullable, char? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddStringMax(parameterName, isNullable, value.ToString(), direction);
		}

		/// <summary>
		///		Add a parameter representing a maximum-length Unicode character string
		///		(nvarchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddStringMax(string parameterName, bool isNullable, char[] value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddStringMax(parameterName, isNullable, string.Concat(value), direction);
		}

		/// <summary>
		///		Add a parameter representing a maximum-length Unicode character string
		///		(nvarchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddStringMax(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddStringMax(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a maximum-length Unicode character string
		///		(nvarchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddStringMax(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = AddStringMax(parameterName, true, (string)null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddXml

		/// <summary>
		///		Add a parameter representing a parsed representation of an XML document or fragment
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddXml(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddXml(parameterName, false, (string)null, direction);
		}

		/// <summary>
		///		Add a parameter representing a parsed representation of an XML document or fragment
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="stream">A stream that contains the value.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddXml(string parameterName, bool isNullable, Stream stream, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddXml(parameterName, isNullable, new SqlXml(stream), direction);
		}

		/// <summary>
		///		Add a parameter representing a parsed representation of an XML document or fragment
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddXml(string parameterName, bool isNullable, string value, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlXml sqlXml;

			if (string.IsNullOrWhiteSpace(value))
			{
				sqlXml = SqlXml.Null;
			}
			else
			{
				using (StringReader stringReader = new StringReader(value))
				{
					sqlXml = new SqlXml(XmlReader.Create(stringReader));
				}
			}

			return AddXml(parameterName, isNullable, sqlXml, direction);
		}

		/// <summary>
		///		Add a parameter representing a parsed representation of an XML document or fragment
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddXml(string parameterName, bool isNullable, SqlXml value, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = CreateParameter(null, parameterName, isNullable, value, direction);
			prm.SqlDbType = SqlDbType.Xml;

			return prm;
		}

		/// <summary>
		///		Add a parameter representing a parsed representation of an XML document or fragment
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="element">An <see cref="T:XElement"/> that contains the value.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddXml(string parameterName, bool isNullable, XElement element, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddXml(parameterName, isNullable, new SqlXml(element.CreateReader()), direction);
		}

		/// <summary>
		///		Add a parameter representing a parsed representation of an XML document or fragment
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="element">An <see cref="T:XmlElement"/> that contains the value.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddXml(string parameterName, bool isNullable, XmlElement element, ParameterDirection direction = ParameterDirection.Input)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				CloseOutput = false,
				ConformanceLevel = ConformanceLevel.Fragment,
				OmitXmlDeclaration = true,
				Encoding = Encoding.UTF8,
				NamespaceHandling = NamespaceHandling.OmitDuplicates,
			};

			MemoryStream stream = new MemoryStream();

			// This XmlWriter ensures that the XML is formatted correctly.
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
			{
				element.WriteTo(xmlWriter);
				xmlWriter.Flush();
			}

			stream.Seek(0L, SeekOrigin.Begin);

			return AddXml(parameterName, isNullable, new SqlXml(stream), direction);
		}

		/// <summary>
		///		Add a parameter representing a parsed representation of an XML document or fragment
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddXml(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddXml(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a parsed representation of an XML document or fragment
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <see cref="SqlParameter"/> object.</returns>
		public SqlParameter AddXml(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			SqlParameter prm = AddXml(parameterName, false, (string)null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#endregion

		#region Get Parameter Values

		/// <summary>
		///		Return the value of a parameter that is of a simple type representing values
		///		ranging from 1.0 x 10^-28 to approximately 7.9 x 10^28 with 28-29 significant digits.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="decimal"/>.</returns>
		public decimal? GetMoney(string parameterName)
		{
			try { return (decimal?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a <see cref="SqlXml"/> type.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a <see cref="SqlXml"/> object.</returns>
		public SqlXml GetXml(string parameterName)
		{
			try { return (SqlXml)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		#endregion

		#endregion

		#region Override Methods

		/// <summary>
		///		Returns the name of a parameter normalized for the command client.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>
		///		Returns the name of a parameter normalized for the command client.
		///	</returns>
		protected override string NormalizeParameterName(string parameterName)
		{
#if ALLOW_PREFIX_OVERRIDES
			if (parameterName.StartsWith("@", StringComparison.CurrentCultureIgnoreCase))
			{
				return parameterName;
			}
			else
			{
#endif
			return string.Format("@{0}", parameterName);
#if ALLOW_PREFIX_OVERRIDES
			}
#endif
		}

		#endregion
	}
}
