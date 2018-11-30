using System;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace openSourceC.StandardLibrary.Data.SqlClient
{
	/// <summary>
	///		Adds extension methods to the <see cref="T:SqlDataReader"/> type.
	/// </summary>
	public static class SqlDbDataReaderExtensions
	{
		#region Get DateTimeOffset Extensions

		/// <summary>
		///     Gets the value of the specified column as a <see cref="T:DateTimeOffset"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:DbDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>The value of the specified column.</returns>
		public static DateTimeOffset GetDateTimeOffset(this SqlDataReader dataReader, string name)
		{
			try { return dataReader.GetDateTimeOffset(dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		/// <summary>
		///     Gets the local time value of the specified column as a
		///     <see cref="T:Nullable&lt;DateTime&gt;"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///		A <see cref="T:Nullable&lt;DateTime&gt;"/> object consisting of the time represented
		///		by the value of the specified column.
		///	</returns>
		public static DateTime? GetDateTimeOffsetAsDateTimeLocalNullSafe(this SqlDataReader dataReader, int ordinal)
		{
			try { return dataReader.IsDBNull(ordinal) ? (DateTime?)null : dataReader.GetDateTimeOffset(ordinal).LocalDateTime; }
			catch (InvalidCastException ex) { throw new OscErrorException(string.Format(SR.Type_mismatch_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
		}

		/// <summary>
		///     Gets the local time value of the specified column as a
		///     <see cref="T:Nullable&lt;DateTime&gt;"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		A <see cref="T:Nullable&lt;DateTime&gt;"/> object consisting of the time represented
		///		by the value of the specified column.
		///	</returns>
		public static DateTime? GetDateTimeOffsetAsDateTimeLocalNullSafe(this SqlDataReader dataReader, string name)
		{
			try { return GetDateTimeOffsetAsDateTimeLocalNullSafe(dataReader, dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }

		}

		/// <summary>
		///     Gets the Coordinated Universal Time (UTC) value of the specified column as a
		///     <see cref="T:Nullable&lt;DateTime&gt;"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///		A <see cref="T:Nullable&lt;DateTime&gt;"/> object consisting of the time represented
		///		by the value of the specified column.
		///	</returns>
		public static DateTime? GetDateTimeOffsetAsDateTimeUtcNullSafe(this SqlDataReader dataReader, int ordinal)
		{
			try { return dataReader.IsDBNull(ordinal) ? (DateTime?)null : dataReader.GetDateTimeOffset(ordinal).UtcDateTime; }
			catch (InvalidCastException ex) { throw new OscErrorException(string.Format(SR.Type_mismatch_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
		}

		/// <summary>
		///     Gets the Coordinated Universal Time (UTC) value of the specified column as a
		///     <see cref="T:Nullable&lt;DateTime&gt;"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		A <see cref="T:Nullable&lt;DateTime&gt;"/> object consisting of the time represented
		///		by the value of the specified column.
		///	</returns>
		public static DateTime? GetDateTimeOffsetAsDateTimeUtcNullSafe(this SqlDataReader dataReader, string name)
		{
			try { return GetDateTimeOffsetAsDateTimeUtcNullSafe(dataReader, dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as a <see cref="T:Nullable&lt;DateTimeOffset&gt;"/>
		///		object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///		A <see cref="Nullable&lt;DateTimeOffset&gt;"/> object consisting of the time represented
		///		by the value of the specified column.
		///	</returns>
		public static DateTimeOffset? GetDateTimeOffsetNullSafe(this SqlDataReader dataReader, int ordinal)
		{
			try { return dataReader.IsDBNull(ordinal) ? (DateTimeOffset?)null : dataReader.GetDateTimeOffset(ordinal); }
			catch (InvalidCastException ex) { throw new OscErrorException(string.Format(SR.Type_mismatch_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as a <see cref="T:Nullable&lt;DateTimeOffset&gt;"/>
		///		object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		A <see cref="T:Nullable&lt;DateTimeOffset&gt;"/> object consisting of the time represented
		///		by the value of the specified column.
		///	</returns>
		public static DateTimeOffset? GetDateTimeOffsetNullSafe(this SqlDataReader dataReader, string name)
		{
			try { return GetDateTimeOffsetNullSafe(dataReader, dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		#endregion

		#region Get TimeSpan Extensions

		/// <summary>
		///     Gets the value of the specified column as a <see cref="T:TimeSpan"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:DbDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>The value of the specified column.</returns>
		public static TimeSpan GetTimeSpan(this SqlDataReader dataReader, string name)
		{
			try { return dataReader.GetTimeSpan(dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as a <see cref="T:Nullable&lt;TimeSpan&gt;"/>
		///		object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///		A <see cref="Nullable&lt;TimeSpan&gt;"/> object consisting of the timespan
		///		represented by the value of the specified column.
		///	</returns>
		public static TimeSpan? GetTimeSpanNullSafe(this SqlDataReader dataReader, int ordinal)
		{
			try { return dataReader.IsDBNull(ordinal) ? (TimeSpan?)null : dataReader.GetTimeSpan(ordinal); }
			catch (InvalidCastException ex) { throw new OscErrorException(string.Format(SR.Type_mismatch_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as a <see cref="T:Nullable&lt;TimeSpan&gt;"/>
		///		object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		A <see cref="T:Nullable&lt;TimeSpan&gt;"/> object consisting of the timespan
		///		represented by the value of the specified column.
		///	</returns>
		public static TimeSpan? GetTimeSpanNullSafe(this SqlDataReader dataReader, string name)
		{
			try { return GetTimeSpanNullSafe(dataReader, dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		#endregion

		#region Get XElement Extensions

		/// <summary>
		///     Gets the value of the specified column as an <see cref="XElement"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///		An <see cref="XElement"/> object that represents the XML contents of the specified
		///		column.
		///	</returns>
		public static XElement GetXElement(this SqlDataReader dataReader, int ordinal)
		{
			try { return XElement.Load(dataReader.GetXmlReader(ordinal), LoadOptions.PreserveWhitespace); }
			catch (InvalidCastException ex) { throw new OscErrorException(string.Format(SR.Type_mismatch_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as an <see cref="XElement"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		An <see cref="XElement"/> object that represents the XML contents of the specified
		///		column.
		///	</returns>
		public static XElement GetXElement(this SqlDataReader dataReader, string name)
		{
			try { return GetXElement(dataReader, dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as an <see cref="XElement"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///		An <see cref="XElement"/> object that represents the XML contents of the specified
		///		column.
		///	</returns>
		public static XElement GetXElementNullSafe(this SqlDataReader dataReader, int ordinal)
		{
			try { return dataReader.IsDBNull(ordinal) ? (XElement)null : XElement.Load(dataReader.GetXmlReader(ordinal), LoadOptions.PreserveWhitespace); }
			catch (InvalidCastException ex) { throw new OscErrorException(string.Format(SR.Type_mismatch_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as an <see cref="XElement"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		An <see cref="XElement"/> object that represents the XML contents of the specified
		///		column.
		///	</returns>
		public static XElement GetXElementNullSafe(this SqlDataReader dataReader, string name)
		{
			try { return GetXElementNullSafe(dataReader, dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		#endregion

		#region Get XmlElement Extensions

		/// <summary>
		///     Gets the value of the specified column as an <see cref="XmlElement"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///		An <see cref="XmlElement"/> object that represents the XML contents of the specified
		///		column.
		///	</returns>
		public static XmlElement GetXmlElement(this SqlDataReader dataReader, int ordinal)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(dataReader.GetXmlReader(ordinal));

				return doc.DocumentElement;
			}
			catch (InvalidCastException ex) { throw new OscErrorException(string.Format(SR.Type_mismatch_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as an <see cref="XmlElement"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		An <see cref="XmlElement"/> object that represents the XML contents of the specified
		///		column.
		///	</returns>
		public static XmlElement GetXmlElement(this SqlDataReader dataReader, string name)
		{
			try { return GetXmlElement(dataReader, dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as an <see cref="XmlElement"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///		An <see cref="XmlElement"/> object that represents the XML contents of the specified
		///		column.
		///	</returns>
		public static XmlElement GetXmlElementNullSafe(this SqlDataReader dataReader, int ordinal)
		{
			try
			{
				if (dataReader.IsDBNull(ordinal)) { return null; }

				XmlDocument doc = new XmlDocument();
				doc.Load(dataReader.GetXmlReader(ordinal));

				return doc.DocumentElement;
			}
			catch (InvalidCastException ex) { throw new OscErrorException(string.Format(SR.Type_mismatch_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, ordinal, dataReader.GetName(ordinal)), ex); }
		}

		/// <summary>
		///     Gets the value of the specified column as an <see cref="XmlElement"/> object.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		An <see cref="XmlElement"/> object that represents the XML contents of the specified
		///		column.
		///	</returns>
		public static XmlElement GetXmlElementNullSafe(this SqlDataReader dataReader, string name)
		{
			try { return GetXmlElementNullSafe(dataReader, dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		#endregion

		#region Get XmlReader Extensions

		/// <summary>
		///		Retrieves data of type XML as an <see cref="T:XmlReader"/>.
		/// </summary>
		/// <param name="dataReader">The <see cref="T:SqlDataReader"/> to extend.</param>
		/// <param name="name">The name of the column.</param>
		/// <returns>
		///		An <see cref="T:XmlReader"/> that contains the XML contents of the specified column.
		/// </returns>
		public static XmlReader GetXmlReader(this SqlDataReader dataReader, string name)
		{
			try { return dataReader.GetXmlReader(dataReader.GetOrdinal(name)); }
			catch (IndexOutOfRangeException ex) { throw new OscErrorException(string.Format(SR.Unknown_column, name), ex); }
			catch (Exception ex) when (!(ex is OscException)) { throw new OscErrorException(string.Format(SR.Exception_on_column, dataReader.GetOrdinal(name), name), ex); }
		}

		#endregion
	}
}
