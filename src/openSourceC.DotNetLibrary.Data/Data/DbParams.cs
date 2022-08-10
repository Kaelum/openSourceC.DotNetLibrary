//#define USES_UNMANGED_CODE

using System;
using System.Data;
using System.Data.Common;

namespace openSourceC.DotNetLibrary.Data
{
	/// <summary>
	///		Adds <see cref="T:Nullable&lt;T&gt;"/> type parameter support to a
	///		<typeparamref name="TDbCommand"/> object to make it easier to use.
	/// </summary>
	/// <typeparam name="TDbCommand">The <see cref="DbCommand"/> type.</typeparam>
	/// <typeparam name="TDbParameter"></typeparam>
	public class DbParams<TDbCommand, TDbParameter> : IDisposable
		where TDbCommand : DbCommand
		where TDbParameter : DbParameter, IDbDataParameter
	{
		/// <summary>The current <see cref="T:TDbCommand"/> object.</summary>
		private TDbCommand _cmd;

		// Track whether Dispose has been called.
		private bool _disposed = false;


		#region Constructors

		/// <summary>
		///		Creates an instance of DbParams.
		/// </summary>
		public DbParams(TDbCommand cmd)
		{
			_cmd = cmd;
		}

		#endregion

		#region Dispose

#if USES_UNMANGED_CODE
		/// <summary>
		///     Class finalizer.
		/// </summary>
		~DbParams()
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}
#endif

		/// <summary>
		///     Releases all resources used by this instance.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);

#if USES_UNMANGED_CODE
			GC.SuppressFinalize(this);
#endif
		}

		/// <summary>
		///     Releases the unmanaged resources used by this instance and optionally
		///     releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		///     If true, the method has been called directly or indirectly by a user's code.
		///     Managed and unmanaged resources can be disposed. If false, the method has been
		///     called by the runtime from inside the finalizer and you should not reference
		///     other objects. Only unmanaged resources can be disposed.
		/// </param>
		protected void Dispose(bool disposing)
		{
			// Check to see if Dispose() has already been called.
			if (!_disposed)
			{
				// Check to see if managed resources need to be disposed of.
				if (disposing)
				{
					// Dispose managed resources.
				}

#if USES_UNMANGED_CODE
				// Dispose of unmanaged resources here.
#endif

				_disposed = true;
			}
		}

		#endregion

		#region Public Properties

		/// <summary>
		///		Gets the <typeparamref name="TDbCommand"/> object that was wrapped.
		/// </summary>
		public TDbCommand DbCommand
		{
			get { return _cmd; }
		}

		/// <summary>
		///		Gets a value indicating whether or not the value of the parameter is
		///		<see cref="DBNull"/>.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>
		///		Returns <b>true</b> if the value of the parameter is <see cref="DBNull"/>,
		///		<b>false</b> if it is not.
		///	</returns>
		public bool IsNull(string parameterName)
		{
			return (_cmd.Parameters[parameterName].Value == DBNull.Value);
		}

		#endregion

		#region Public Methods

		#region Add Parameters

		#region AddAnsiString

		/// <summary>
		///		Add a parameter representing a variable-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiString(string parameterName, int size, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiString(parameterName, size, true, (string?)null, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiString(string parameterName, int size, bool isNullable, string? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.AnsiString, parameterName, size, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiString(string parameterName, int size, bool isNullable, char? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiString(parameterName, size, isNullable, value.ToString(), direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiString(string parameterName, int size, bool isNullable, char[]? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiString(parameterName, size, isNullable, (value == null ? null : string.Concat(value)), direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiString(string parameterName, int size, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiString(parameterName, size, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length non-Unicode character string
		///		(varchar(max)), to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiString(string parameterName, int size, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddAnsiString(parameterName, size, true, (string?)null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddAnsiStringFixedLength

		/// <summary>
		///		Add a parameter representing a fixed length ANSI string to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiStringFixedLength(string parameterName, int size, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiStringFixedLength(parameterName, size, true, (string?)null, direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length ANSI string to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiStringFixedLength(string parameterName, int size, bool isNullable, string? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.AnsiStringFixedLength, parameterName, size, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length ANSI string to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiStringFixedLength(string parameterName, int size, bool isNullable, char? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiStringFixedLength(parameterName, size, isNullable, value.ToString(), direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length ANSI string to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiStringFixedLength(string parameterName, int size, bool isNullable, char[]? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiStringFixedLength(parameterName, size, isNullable, (value == null ? null : string.Concat(value)), direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length ANSI string to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiStringFixedLength(string parameterName, int size, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddAnsiStringFixedLength(parameterName, size, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length ANSI string to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddAnsiStringFixedLength(string parameterName, int size, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddAnsiStringFixedLength(parameterName, size, true, (string?)null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddBinary

		/// <summary>
		///		Add a parameter representing a variable-length stream of binary data to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddBinary(string parameterName, int size, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddBinary(parameterName, size, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length stream of binary data to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddBinary(string parameterName, int size, bool isNullable, byte[]? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Binary, parameterName, size, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length stream of binary data to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddBinary(string parameterName, int size, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddBinary(parameterName, size, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable-length stream of binary data to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddBinary(string parameterName, int size, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddBinary(parameterName, size, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddBoolean

		/// <summary>
		///		Add a parameter representing Boolean values of <b>true</b> or <b>false</b> to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddBoolean(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddBoolean(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing Boolean values of <b>true</b> or <b>false</b> to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddBoolean(string parameterName, bool isNullable, bool? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Boolean, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing Boolean values of <b>true</b> or <b>false</b> to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddBoolean(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddBoolean(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing Boolean values of <b>true</b> or <b>false</b> to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddBoolean(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddBoolean(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddByte

		/// <summary>
		///		Add a parameter representing 8-bit unsigned integer ranging in value from 0 to 255
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddByte(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddByte(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing 8-bit unsigned integer ranging in value from 0 to 255
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddByte(string parameterName, bool isNullable, byte? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Byte, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing 8-bit unsigned integer ranging in value from 0 to 255
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddByte(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddByte(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing 8-bit unsigned integer ranging in value from 0 to 255
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddByte(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddByte(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddCurrency

		/// <summary>
		///		Add a parameter representing a currency value ranging from -2^63
		///		(or -922,337,203,685,477.5808) to 2^63 - 1 (or +922,337,203,685,477.5807) with an
		///		accuracy to a ten-thousandth of a currency unit to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddCurrency(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddCurrency(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing a currency value ranging from -2^63
		///		(or -922,337,203,685,477.5808) to 2^63 - 1 (or +922,337,203,685,477.5807) with an
		///		accuracy to a ten-thousandth of a currency unit to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddCurrency(string parameterName, bool isNullable, decimal? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Currency, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a currency value ranging from -2^63
		///		(or -922,337,203,685,477.5808) to 2^63 - 1 (or +922,337,203,685,477.5807) with an
		///		accuracy to a ten-thousandth of a currency unit to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddCurrency(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddCurrency(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a currency value ranging from -2^63
		///		(or -922,337,203,685,477.5808) to 2^63 - 1 (or +922,337,203,685,477.5807) with an
		///		accuracy to a ten-thousandth of a currency unit to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddCurrency(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddCurrency(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddDate

		/// <summary>
		///		Add a date and time data parameter ranging in value from January 1, 1753 to
		///		December 31, 9999 to an accuracy of 3.33 milliseconds to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDate(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDate(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a date and time data parameter ranging in value from January 1, 1753 to
		///		December 31, 9999 to an accuracy of 3.33 milliseconds to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDate(string parameterName, bool isNullable, DateTime? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Date, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a date and time data parameter ranging in value from January 1, 1753 to
		///		December 31, 9999 to an accuracy of 3.33 milliseconds to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDate(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDate(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a date and time data parameter ranging in value from January 1, 1753 to
		///		December 31, 9999 to an accuracy of 3.33 milliseconds to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDate(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddDate(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddDateTime

		/// <summary>
		///		Add a parameter representing a date and time value to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDateTime(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDateTime(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing a date and time value to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDateTime(string parameterName, bool isNullable, DateTime? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.DateTime, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a date and time value to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDateTime(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDateTime(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a date and time value to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDateTime(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddDateTime(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddDateTimeOffset

		/// <summary>
		///		Add a parameter representing a date and time value to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDateTimeOffset(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDateTimeOffset(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing a date and time value to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDateTimeOffset(string parameterName, bool isNullable, DateTimeOffset? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.DateTimeOffset, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a date and time value to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDateTimeOffset(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDateTimeOffset(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a date and time value to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDateTimeOffset(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddDateTimeOffset(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddDecimal

		/// <summary>
		///		Add a parameter representing values ranging from 1.0 x 10^-28 to approximately
		///		7.9 x 10^28 with 28-29 significant digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="precision">Gets or sets the maximum number of digits used to represent the value.</param>
		///	<param name="scale">Gets or sets the number of decimal places to which value is resolved.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDecimal(string parameterName, byte precision, byte scale, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDecimal(parameterName, precision, scale, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing values ranging from 1.0 x 10^-28 to approximately
		///		7.9 x 10^28 with 28-29 significant digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="precision">Gets or sets the maximum number of digits used to represent the value.</param>
		///	<param name="scale">Gets or sets the number of decimal places to which value is resolved.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDecimal(string parameterName, byte precision, byte scale, bool isNullable, decimal? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Decimal, parameterName, precision, scale, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing values ranging from 1.0 x 10^-28 to approximately
		///		7.9 x 10^28 with 28-29 significant digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="precision">Gets or sets the maximum number of digits used to represent the value.</param>
		///	<param name="scale">Gets or sets the number of decimal places to which value is resolved.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDecimal(string parameterName, byte precision, byte scale, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDecimal(parameterName, precision, scale, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing values ranging from 1.0 x 10^-28 to approximately
		///		7.9 x 10^28 with 28-29 significant digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="precision">Gets or sets the maximum number of digits used to represent the value.</param>
		///	<param name="scale">Gets or sets the number of decimal places to which value is resolved.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDecimal(string parameterName, byte precision, byte scale, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddDecimal(parameterName, precision, scale, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddDouble

		/// <summary>
		///		Add a floating point parameter representing values ranging from approximately
		///		5.0 x 10^-324 to 1.7 x 10^308 with a precision of 15-16 digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDouble(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDouble(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a floating point parameter representing values ranging from approximately
		///		5.0 x 10^-324 to 1.7 x 10^308 with a precision of 15-16 digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDouble(string parameterName, bool isNullable, double? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Double, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a floating point parameter representing values ranging from approximately
		///		5.0 x 10^-324 to 1.7 x 10^308 with a precision of 15-16 digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDouble(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddDouble(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a floating point parameter representing values ranging from approximately
		///		5.0 x 10^-324 to 1.7 x 10^308 with a precision of 15-16 digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddDouble(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddDouble(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddEnum

		/// <summary>
		///		Add a parameter representing a <typeparamref name="TEnum"/> type to the command
		///		object.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddEnum<TEnum>(string parameterName, ParameterDirection direction = ParameterDirection.Input)
			where TEnum : struct, Enum
		{
			return AddEnum<TEnum>(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing a <typeparamref name="TEnum"/> type to the command
		///		object.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddEnum<TEnum>(string parameterName, bool isNullable, TEnum value, ParameterDirection direction = ParameterDirection.Input)
			where TEnum : struct, Enum
		{
			DbType dbType = GetEnumDbType(typeof(TEnum));

			return CreateParameter(dbType, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a <typeparamref name="TEnum"/> type to the command
		///		object.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddEnum<TEnum>(string parameterName, bool isNullable, TEnum? value, ParameterDirection direction = ParameterDirection.Input)
			where TEnum : struct, Enum
		{
			DbType dbType = GetEnumDbType(typeof(TEnum));

			return CreateParameter(dbType, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a <typeparamref name="TEnum"/> type to the command
		///		object.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddEnum<TEnum>(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
			where TEnum : struct, Enum
		{
			return AddEnum<TEnum>(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a <typeparamref name="TEnum"/> type to the command
		///		object.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddEnum<TEnum>(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
			where TEnum : struct, Enum
		{
			TDbParameter prm = AddEnum<TEnum>(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddFloat

		/// <summary>
		///		Add a floating point parameter representing values ranging from approximately
		///		1.5 x 10^-45 to 3.4 x 10^38 with a precision of 7 digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddFloat(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddFloat(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a floating point parameter representing values ranging from approximately
		///		1.5 x 10^-45 to 3.4 x 10^38 with a precision of 7 digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddFloat(string parameterName, bool isNullable, float? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Single, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a floating point parameter representing values ranging from approximately
		///		1.5 x 10^-45 to 3.4 x 10^38 with a precision of 7 digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddFloat(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddFloat(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a floating point parameter representing values ranging from approximately
		///		1.5 x 10^-45 to 3.4 x 10^38 with a precision of 7 digits to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddFloat(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddFloat(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddGuid

		/// <summary>
		///		Add a globally unique identifier (or GUID) parameter to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddGuid(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddGuid(parameterName, false, (Guid?)null, direction);
		}

		/// <summary>
		///		Add a globally unique identifier (or GUID) parameter to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddGuid(string parameterName, bool isNullable, Guid? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Guid, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a globally unique identifier (or GUID) parameter to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddGuid(string parameterName, bool isNullable, string value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddGuid(parameterName, isNullable, string.IsNullOrEmpty(value) ? (Guid?)null : new Guid(value), direction);
		}

		/// <summary>
		///		Add a globally unique identifier (or GUID) parameter to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddGuid(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddGuid(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a globally unique identifier (or GUID) parameter to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddGuid(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddGuid(parameterName, false, (Guid?)null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddInt16

		/// <summary>
		///		Add a parameter representing signed 16-bit integers with values between -32,768 and
		///		32,767 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt16(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddInt16(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 16-bit integers with values between -32,768 and
		///		32,767 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt16(string parameterName, bool isNullable, short? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Int16, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 16-bit integers with values between -32,768 and
		///		32,767 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt16(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddInt16(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 16-bit integers with values between -32,768 and
		///		32,767 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt16(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddInt16(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddInt32

		/// <summary>
		///		Add a parameter representing signed 32-bit integers with values between
		///		-2,147,483,648 and 2,147,483,647 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt32(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddInt32(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 32-bit integers with values between
		///		-2,147,483,648 and 2,147,483,647 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt32(string parameterName, bool isNullable, int? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Int32, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 32-bit integers with values between
		///		-2,147,483,648 and 2,147,483,647 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt32(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddInt32(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 32-bit integers with values between
		///		-2,147,483,648 and 2,147,483,647 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt32(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddInt32(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddInt64

		/// <summary>
		///		Add a parameter representing signed 64-bit integers with values between
		///		-9,223,372,036,854,775,808 and 9,223,372,036,854,775,807 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt64(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddInt64(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 64-bit integers with values between
		///		-9,223,372,036,854,775,808 and 9,223,372,036,854,775,807 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt64(string parameterName, bool isNullable, long? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Int64, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 64-bit integers with values between
		///		-9,223,372,036,854,775,808 and 9,223,372,036,854,775,807 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt64(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddInt64(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 64-bit integers with values between
		///		-9,223,372,036,854,775,808 and 9,223,372,036,854,775,807 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddInt64(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddInt64(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddSByte

		/// <summary>
		///		Add a parameter representing signed 8-bit integers with values between -128 and 127
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddSByte(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddSByte(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 8-bit integers with values between -128 and 127
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		[CLSCompliant(false)]
		public TDbParameter AddSByte(string parameterName, bool isNullable, sbyte? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.SByte, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 8-bit integers with values between -128 and 127
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddSByte(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddSByte(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing signed 8-bit integers with values between -128 and 127
		///		to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddSByte(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddSByte(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddString

		/// <summary>
		///		Add a parameter representing a variable length Unicode character string to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddString(string parameterName, int size, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddString(parameterName, size, false, (string?)null, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable length Unicode character string to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddString(string parameterName, int size, bool isNullable, string? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.String, parameterName, size, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable length Unicode character string to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddString(string parameterName, int size, bool isNullable, char? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddString(parameterName, size, isNullable, (value.HasValue ? value.ToString() : null), direction);
		}

		/// <summary>
		///		Add a parameter representing a variable length Unicode character string to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddString(string parameterName, int size, bool isNullable, char[] value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddString(parameterName, size, isNullable, string.Concat(value), direction);
		}

		/// <summary>
		///		Add a parameter representing a variable length Unicode character string to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddString(string parameterName, int size, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddString(parameterName, size, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a variable length Unicode character string to the
		///		command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddString(string parameterName, int size, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddString(parameterName, size, false, (string?)null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddStringFixedLength

		/// <summary>
		///		Add a parameter representing a fixed length Unicode character string to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddStringFixedLength(string parameterName, int size, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddStringFixedLength(parameterName, size, false, (string?)null, direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length Unicode character string to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddStringFixedLength(string parameterName, int size, bool isNullable, string? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.StringFixedLength, parameterName, size, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length Unicode character string to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddStringFixedLength(string parameterName, int size, bool isNullable, char? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddStringFixedLength(parameterName, size, isNullable, (value.HasValue ? value.ToString() : null), direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length Unicode character string to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddStringFixedLength(string parameterName, int size, bool isNullable, char[] value, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddStringFixedLength(parameterName, size, isNullable, string.Concat(value), direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length Unicode character string to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddStringFixedLength(string parameterName, int size, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddStringFixedLength(parameterName, size, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a fixed length Unicode character string to the command
		///		object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddStringFixedLength(string parameterName, int size, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddStringFixedLength(parameterName, size, false, (string?)null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddTimeSpan

		/// <summary>
		///		Add a timespan data parameter based on a 24-hour clock. The value range is 00:00:00
		///		through 23:59:59.9999999 with an accuracy of 100 nanoseconds.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddTimeSpan(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddTimeSpan(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a timespan data parameter based on a 24-hour clock. The value range is 00:00:00
		///		through 23:59:59.9999999 with an accuracy of 100 nanoseconds.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddTimeSpan(string parameterName, bool isNullable, TimeSpan? value, ParameterDirection direction = ParameterDirection.Input)
		{
			DateTime? dateTime = (value.HasValue ? DateTime.UnixEpoch.AddTicks(value.Value.Ticks) : (DateTime?)null);

			return CreateParameter(DbType.Time, parameterName, isNullable, dateTime, direction);
		}

		/// <summary>
		///		Add a timespan data parameter based on a 24-hour clock. The value range is 00:00:00
		///		through 23:59:59.9999999 with an accuracy of 100 nanoseconds.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddTimeSpan(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddTimeSpan(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a timespan data parameter based on a 24-hour clock. The value range is 00:00:00
		///		through 23:59:59.9999999 with an accuracy of 100 nanoseconds.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddTimeSpan(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddTimeSpan(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddUInt16

		/// <summary>
		///		Add a parameter representing unsigned 16-bit integers with values between 0 and
		///		65,535 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt16(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddUInt16(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 16-bit integers with values between 0 and
		///		65,535 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		[CLSCompliant(false)]
		public TDbParameter AddUInt16(string parameterName, bool isNullable, ushort? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.UInt16, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 16-bit integers with values between 0 and
		///		65,535 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt16(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddUInt16(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 16-bit integers with values between 0 and
		///		65,535 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt16(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddUInt16(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddUInt32

		/// <summary>
		///		Add a parameter representing unsigned 32-bit integers with values between 0 and
		///		4,294,967,295 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt32(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddUInt32(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 32-bit integers with values between 0 and
		///		4,294,967,295 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		[CLSCompliant(false)]
		public TDbParameter AddUInt32(string parameterName, bool isNullable, uint? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.UInt32, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 32-bit integers with values between 0 and
		///		4,294,967,295 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt32(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddUInt32(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 32-bit integers with values between 0 and
		///		4,294,967,295 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt32(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddUInt32(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddUInt64

		/// <summary>
		///		Add a parameter representing unsigned 64-bit integers with values between 0 and
		///		18,446,744,073,709,551,615 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt64(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddUInt64(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 64-bit integers with values between 0 and
		///		18,446,744,073,709,551,615 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		[CLSCompliant(false)]
		public TDbParameter AddUInt64(string parameterName, bool isNullable, ulong? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.UInt64, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 64-bit integers with values between 0 and
		///		18,446,744,073,709,551,615 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt64(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddUInt64(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing unsigned 64-bit integers with values between 0 and
		///		18,446,744,073,709,551,615 to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddUInt64(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddUInt64(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#region AddVariant

		/// <summary>
		///		Add a parameter representing a variant type to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddVariant(string parameterName, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddVariant(parameterName, true, null, direction);
		}

		/// <summary>
		///		Add a parameter representing a variant type to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddVariant(string parameterName, bool isNullable, object? value, ParameterDirection direction = ParameterDirection.Input)
		{
			return CreateParameter(DbType.Object, parameterName, isNullable, value, direction);
		}

		/// <summary>
		///		Add a parameter representing a variant type to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddVariant(string parameterName, string sourceColumn, ParameterDirection direction = ParameterDirection.Input)
		{
			return AddVariant(parameterName, sourceColumn, false, direction);
		}

		/// <summary>
		///		Add a parameter representing a variant type to the command object.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		///	<param name="sourceColumnIsNullable">Indicates whether the source column is nullable.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		///	<returns>Returns a <typeparamref name="TDbParameter"/> object.</returns>
		public TDbParameter AddVariant(string parameterName, string sourceColumn, bool sourceColumnIsNullable, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = AddVariant(parameterName, true, null, direction);
			prm.SourceColumn = sourceColumn;
			prm.SourceColumnNullMapping = sourceColumnIsNullable;

			return prm;
		}

		#endregion

		#endregion

		#region Get Parameter Values

		/// <summary>
		///		Return the value of a parameter that is of a variable-length stream of binary data.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a <see cref="byte"/> array.</returns>
		public byte[]? GetBinary(string parameterName)
		{
			try { return (byte[]?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a simple type representing Boolean
		///		values of <b>true</b> or <b>false</b>.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="bool"/>.</returns>
		public bool? GetBoolean(string parameterName)
		{
			try { return (bool?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a 8-bit unsigned integer ranging in
		///		value from 0 to 255.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="byte"/>.</returns>
		public byte? GetByte(string parameterName)
		{
			try
			{
				object? obj = GetValue(parameterName);
				return (obj is decimal ? (byte?)(decimal?)obj : (byte?)obj);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a currency value ranging from
		///		-2^63 (or -922,337,203,685,477.5808) to 2^63 -1 (or +922,337,203,685,477.5807) with
		///		an accuracy to a ten-thousandth of a currency unit.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="decimal"/>.</returns>
		public decimal? GetCurrency(string parameterName)
		{
			try { return (decimal?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a type representing a date and time value.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <param name="kind">One of the <see cref="DateTimeKind"/> values.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="DateTime"/>.</returns>
		public DateTime? GetDateTime(string parameterName, DateTimeKind kind)
		{
			try
			{
				object? value = GetValue(parameterName);
				return (value == null ? (DateTime?)null : DateTime.SpecifyKind((DateTime)value, kind));
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a type representing a date and time value.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="DateTimeOffset"/>.</returns>
		public DateTimeOffset? GetDateTimeOffset(string parameterName)
		{
			try { return (DateTimeOffset?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a simple type representing values
		///		ranging from 1.0 x 10^-28 to approximately 7.9 x 10^28 with 28-29 significant digits.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="decimal"/>.</returns>
		public decimal? GetDecimal(string parameterName)
		{
			try { return (decimal?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a floating point type representing
		///		values ranging from approximately 5.0 x 10^-324 to 1.7 x 10^308 with a precision of
		///		15-16 digits.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="double"/>.</returns>
		public double? GetDouble(string parameterName)
		{
			try { return (double?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a globally unique identifier (or GUID) type.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="Guid"/>.</returns>
		public Guid? GetGuid(string parameterName)
		{
			try { return (Guid?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///     Return the value of a parameter that is of a <typeparamref name="TEnum"/> type.
		/// </summary>
		/// <typeparam name="TEnum">The enum type to return.</typeparam>
		///	<param name="parameterName">The name of the parameter.</param>
		/// <returns>Returns the value of the parameter as a nullable <typeparamref name="TEnum"/>.</returns>
		public TEnum? GetEnum<TEnum>(string parameterName)
			where TEnum : struct, Enum
		{
			try { return (TEnum?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of an integral type representing signed
		///		16-bit integers with values between -32,768 and 32,767.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="short"/>.</returns>
		public short? GetInt16(string parameterName)
		{
			try
			{
				object? obj = GetValue(parameterName);
				return (obj is decimal ? (short?)(decimal?)obj : (short?)obj);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of an integral type representing signed
		///		32-bit integers with values between -2,147,483,648 and 2,147,483,647.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="int"/>.</returns>
		public int? GetInt32(string parameterName)
		{
			try
			{
				object? obj = GetValue(parameterName);
				return (obj is decimal ? (int?)(decimal?)obj : (int?)obj);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of an integral type representing signed
		///		64-bit integers with values between -9,223,372,036,854,775,808 and 9,223,372,036,854,775,807.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="long"/>.</returns>
		public long? GetInt64(string parameterName)
		{
			try
			{
				object? obj = GetValue(parameterName);
				return (obj is decimal ? (long?)(decimal?)obj : (long?)obj);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a integral type representing signed
		///		8-bit integer with values between -128 and 127.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="sbyte"/>.</returns>
		[CLSCompliant(false)]
		public sbyte? GetSByte(string parameterName)
		{
			try
			{
				object? obj = GetValue(parameterName);
				return (obj is decimal ? (sbyte?)(decimal?)obj : (sbyte?)obj);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a floating point type representing
		///		values ranging from approximately 1.5 x 10^-45 to 3.4 x 10^38 with a precision of
		///		7 digits.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="float"/>.</returns>
		public float? GetSingle(string parameterName)
		{
			try { return (float?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a character string type.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a string.</returns>
		public string? GetString(string parameterName)
		{
			try { return (string?)GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a type representing a timespan value.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="DateTime"/>.</returns>
		public TimeSpan? GetTimeSpan(string parameterName)
		{
			try
			{
				object? value = GetValue(parameterName);
				return (value == null ? (TimeSpan?)null : ((DateTime)value).TimeOfDay);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of an integral type representing signed
		///		16-bit integers with values between -32,768 and 32,767.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="ushort"/>.</returns>
		[CLSCompliant(false)]
		public ushort? GetUInt16(string parameterName)
		{
			try
			{
				object? obj = GetValue(parameterName);
				return (obj is decimal ? (ushort?)(decimal?)obj : (ushort?)obj);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of an integral type representing unsigned
		///		32-bit integers with values between 0 and 4,294,967,295.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="uint"/>.</returns>
		[CLSCompliant(false)]
		public uint? GetUInt32(string parameterName)
		{
			try
			{
				object? obj = GetValue(parameterName);
				return (obj is decimal ? (uint?)(decimal?)obj : (uint?)obj);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of an integral type representing unsigned
		///		64-bit integers with values between 0 and 18,446,744,073,709,551,615.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter as a nullable <see cref="ulong"/>.</returns>
		[CLSCompliant(false)]
		public ulong? GetUInt64(string parameterName)
		{
			try
			{
				object? obj = GetValue(parameterName);
				return (obj is decimal ? (ulong?)(decimal?)obj : (ulong?)obj);
			}
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		/// <summary>
		///		Return the value of a parameter that is of a variant type.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>Returns the value of the parameter.</returns>
		public object? GetVariant(string parameterName)
		{
			try { return GetValue(parameterName); }
			catch (InvalidCastException ex) { throw new InvalidCastException(string.Format(SR.Type_mismatch_on_parameter, NormalizeParameterName(parameterName)), ex); }
		}

		#endregion

		#endregion

		#region Protected Methods

		/// <summary>
		///		Create and returns a <typeparamref name="TDbParameter"/> object.
		/// </summary>
		/// <param name="dbType">The <see cref="DbType"/>.</param>
		///	<param name="parameterName">The name of the parameter to create.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		/// <returns>
		///		A <typeparamref name="TDbParameter"/> object.
		/// </returns>
		protected TDbParameter CreateParameter(DbType? dbType, string parameterName, bool isNullable, object? value, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = (TDbParameter)_cmd.CreateParameter();
			prm.ParameterName = NormalizeParameterName(parameterName);

			if (dbType.HasValue)
			{
				prm.DbType = dbType.Value;
			}

			prm.IsNullable = (isNullable | direction == ParameterDirection.Output | direction == ParameterDirection.ReturnValue);
			prm.Direction = direction;
			prm.Value = (value ?? DBNull.Value);

			_cmd.Parameters.Add(prm);

			return prm;
		}

		/// <summary>
		///		Create and returns a <typeparamref name="TDbParameter"/> object.
		/// </summary>
		/// <param name="dbType">The <see cref="DbType"/>.</param>
		///	<param name="parameterName">The name of the parameter to create.</param>
		///	<param name="size"> The size of the parameter (width of the column). </param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		/// <returns>
		///		A <typeparamref name="TDbParameter"/> object.
		/// </returns>
		protected TDbParameter CreateParameter(DbType? dbType, string parameterName, int size, bool isNullable, object? value, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = CreateParameter(dbType, parameterName, isNullable, value, direction);
			prm.Size = size;

			return prm;
		}

		/// <summary>
		///		Create and returns a <typeparamref name="TDbParameter"/> object.
		/// </summary>
		/// <param name="dbType">The <see cref="DbType"/>.</param>
		///	<param name="parameterName">The name of the parameter to create.</param>
		///	<param name="precision">Gets or sets the maximum number of digits used to represent the value.</param>
		///	<param name="scale">Gets or sets the number of decimal places to which value is resolved.</param>
		///	<param name="isNullable">Indicates whether the parameter accepts null values.</param>
		///	<param name="value">The value of the parameter.</param>
		///	<param name="direction">The parameter direction. (optional)</param>
		/// <returns>
		///		A <typeparamref name="TDbParameter"/> object.
		/// </returns>
		protected TDbParameter CreateParameter(DbType? dbType, string parameterName, byte precision, byte scale, bool isNullable, object? value, ParameterDirection direction = ParameterDirection.Input)
		{
			TDbParameter prm = CreateParameter(dbType, parameterName, isNullable, value, direction);
			prm.Precision = precision;
			prm.Scale = scale;

			return prm;
		}

		/// <summary>
		///		Returns the value of a parameter.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>
		///		Returns the parameter value.
		///	</returns>
		protected object? GetValue(string parameterName)
		{
			TDbParameter prm = (TDbParameter)_cmd.Parameters[NormalizeParameterName(parameterName)];

			return (prm.Value == DBNull.Value ? null : prm.Value);
		}

		/// <summary>
		///		Returns the name of a parameter normalized for the command client.
		/// </summary>
		///	<param name="parameterName">The name of the parameter.</param>
		///	<returns>
		///		Returns the name of a parameter normalized for the command client.
		///	</returns>
		protected virtual string NormalizeParameterName(string parameterName)
		{
			return parameterName;
		}

		#endregion

		#region Private Methods

		private DbType GetEnumDbType(Type enumType)
		{
			Type underlyingType = Enum.GetUnderlyingType(enumType);

			if (underlyingType.Equals(typeof(long))) { return DbType.Int64; }
			else if (underlyingType.Equals(typeof(ulong))) { return DbType.UInt64; }
			else if (underlyingType.Equals(typeof(int))) { return DbType.Int32; }
			else if (underlyingType.Equals(typeof(uint))) { return DbType.UInt32; }
			else if (underlyingType.Equals(typeof(short))) { return DbType.Int16; }
			else if (underlyingType.Equals(typeof(ushort))) { return DbType.UInt16; }
			else if (underlyingType.Equals(typeof(byte))) { return DbType.Byte; }
			else if (underlyingType.Equals(typeof(sbyte))) { return DbType.SByte; }
			else
			{
				throw new OscErrorException(string.Format("Unexpected TEnum ({0}) value type of {1}.", enumType.FullName, Enum.GetUnderlyingType(enumType).FullName));
			}
		}

		#endregion
	}
}
