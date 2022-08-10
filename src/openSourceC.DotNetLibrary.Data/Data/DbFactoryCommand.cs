using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace openSourceC.DotNetLibrary.Data
{
	/// <summary>
	///		Summary description for DbFactoryCommand.
	/// </summary>
	/// <typeparam name="TDbFactory"></typeparam>
	/// <typeparam name="TDbFactoryCommand"></typeparam>
	/// <typeparam name="TDbParams"></typeparam>
	/// <typeparam name="TDbConnection"></typeparam>
	/// <typeparam name="TDbTransaction"></typeparam>
	/// <typeparam name="TDbCommand">The <see cref="DbCommand"/> type.</typeparam>
	/// <typeparam name="TDbParameter"></typeparam>
	/// <typeparam name="TDbDataAdapter"></typeparam>
	/// <typeparam name="TDbDataReader"></typeparam>
	public abstract class DbFactoryCommand<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader> : DbFactoryCommand, IDisposable
		where TDbFactory : DbFactory<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader>
		where TDbFactoryCommand : DbFactoryCommand<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader>
		where TDbParams : DbParamsBase<TDbParams, TDbCommand, TDbParameter>
		where TDbConnection : DbConnection
		where TDbTransaction : DbTransaction
		where TDbCommand : DbCommand
		where TDbParameter : DbParameter
		where TDbDataAdapter : DbDataAdapter
		where TDbDataReader : DbDataReader
	{
		private const int DEFAULT_COMMAND_TIMEOUT = 30;
		private const string DEFAULT_RETURN_VALUE_PARAMETER_NAME = "RETURN_VALUE";

		// Track whether Dispose has been called.
		private bool _disposed = false;


		#region Constructors

		/// <summary>
		///		Class constructor.
		/// </summary>
		protected DbFactoryCommand() { }

		#endregion

		#region Dispose

#if USES_UNMANGED_CODE
		/// <summary>
		///     Class finalizer.
		/// </summary>
		~DbManager()
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
		protected virtual void Dispose(bool disposing)
		{
			// Check to see if Dispose() has already been called.
			if (!_disposed)
			{
				// Check to see if managed resources need to be disposed of.
				if (disposing)
				{
					// Dispose managed resources.

					// Nullify references to managed resources that are not disposable.

					// Nullify references to externally created managed resources.
				}

#if USES_UNMANGED_CODE
				// Dispose of unmanaged resources here.
#endif

				_disposed = true;
			}
		}

		#endregion

		#region Protected Properties

		/// <summary>
		///		Gets or sets a value indicating that a prepared (or compiled) version of the
		///		command will be created on the data source
		/// </summary>
		protected bool AutoPrepare { get; set; }

		/// <summary>
		///     Gets the default command timeout.
		/// </summary>
		protected virtual int DefaultCommandTimeout =>
			DEFAULT_COMMAND_TIMEOUT;

		/// <summary>
		///		Gets the underlying <typeparamref name="TDbFactory"/> object.
		/// </summary>
		protected abstract TDbFactory DbFactory { get; }

		#endregion

		#region Public Properties

		/// <summary>
		///		Gets the current command.
		/// </summary>
		public abstract TDbCommand Command { get; }

		/// <summary>
		///		Gets or sets the text command to run against the data source.
		/// </summary>
		/// <value>
		///		The text command to execute. The default value is an empty string ("").
		/// </value>
		public string CommandText
		{
			get { return Command.CommandText; }
			set { Command.CommandText = value; }
		}

		/// <summary>
		///		Gets or sets the wait time before terminating the attempt to execute a command and
		///		generating an error.
		/// </summary>
		/// <value>
		///		The time (in seconds) to wait for the command to execute. The default value is 30
		///		seconds.
		/// </value>
		public int CommandTimeout
		{
			get { return Command.CommandTimeout; }
			set { Command.CommandTimeout = value; }
		}

		/// <summary>
		///		Indicates or specifies how the <see cref="CommandText" /> property is interpreted.
		/// </summary>
		/// <value>
		///		One of the <see cref="CommandType" /> values. The default is <b>Text</b>.
		/// </value>
		public CommandType CommandType
		{
			get { return Command.CommandType; }
			set { Command.CommandType = value; }
		}

		/// <summary>
		///		Gets or sets the <see cref="DbConnection"/> used by this instance of the
		///		<see cref="IDbCommand" />.
		/// </summary>
		/// <value>
		///		The connection to the data source.
		/// </value>
		public TDbConnection Connection =>
			(TDbConnection)(Command.Connection ?? throw new NullValueException($"{nameof(Command.Connection)} is null."));

		/// <summary>
		///		Gets the current <see cref="T:TDbParams"/> object.
		/// </summary>
		public abstract TDbParams Params { get; }

		/// <summary>
		///		Gets the return value from the last executed command.
		///		<para>
		///			If an exception occurred while using the reader, a <b>null</b> value will be
		///			returned.
		///		</para>
		///		<para>NOTE: May not be valid while the data reader is open.</para>
		/// </summary>
		public int? ReturnValue =>
			Params.GetInt32(ReturnValueParameterName);

		/// <summary>
		///     Gets the name of the parameter that contains the return value.
		/// </summary>
		public virtual string ReturnValueParameterName =>
			DEFAULT_RETURN_VALUE_PARAMETER_NAME;

		/// <summary>
		///		Gets or sets the transaction within which the <b>Command</b> object of a .NET
		///		Framework data provider executes.
		/// </summary>
		/// <value>
		///		The <b>Command</b> object of a .NET Framework data provider executes. The default
		///		value is a null reference (<b>Nothing</b> in Visual Basic).
		/// </value>
		public TDbTransaction Transaction
		{
			get { return (TDbTransaction)(Command.Transaction ?? throw new NullValueException($"{nameof(Command.Transaction)} is null.")); }
			set { Command.Transaction = value; }
		}

		/// <summary>
		///		Gets or sets how command results are applied to the <see cref="DataRow" /> when
		///		used by the <see cref="M:DbDataAdapter.Update" /> method of a <see cref="DbDataAdapter" />.
		/// </summary>
		/// <value>
		///		One of the <see cref="UpdateRowSource" /> values.
		/// </value>
		/// <remarks>
		///		The default is <b>Both</b> unless the command is automatically generated. Then the
		///		default is <b>None</b>.
		/// </remarks>
		public UpdateRowSource UpdatedRowSource
		{
			get { return Command.UpdatedRowSource; }
			set { Command.UpdatedRowSource = value; }
		}

		#endregion

		#region Protected Methods

		/// <summary>
		///		Ensure that the transaction properties are correctly populated when needed.
		/// </summary>
		protected void EnsureTransaction()
		{
			if (DbFactory.AmbientTransactionExists)
			{
				Connection.EnlistTransaction(System.Transactions.Transaction.Current);
			}
			else if (Command.Transaction == null && DbFactory.Transaction is not null)
			{
				Command.Transaction = DbFactory.Transaction;
			}
		}

		#endregion

		#region Public Methods

		#region ExecuteDataSet

		/// <summary>
		///		Gets a DataTable object from the executed command.
		/// </summary>
		/// <param name="dataSet">DataSet object.  null on error.</param>
		/// <returns>
		///		The number of rows successfully added to or refreshed in the <see cref="DataSet" />.
		///		This does not include rows affected by statements that do not return rows.
		/// </returns>
		public int ExecuteDataSet(DataSet dataSet)
		{
			EnsureTransaction();

			using (TDbDataAdapter dataAdapter = CreateDataAdapter())
			{
				if (AutoPrepare)
				{
					Command.Prepare();
				}

				return dataAdapter.Fill(dataSet);
			}
		}

		#endregion

		#region ExecuteNonQuery

		/// <summary>
		///		Executes a Transact-SQL statement against the connection.
		/// </summary>
		/// <returns>
		///		The number of rows affected.
		/// </returns>
		public int ExecuteNonQuery()
		{
			bool isClosed = (Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				Connection.Open();
			}

			EnsureTransaction();

			try
			{
				if (AutoPrepare)
				{
					Command.Prepare();
				}

				return Command.ExecuteNonQuery();
			}
			finally
			{
				if (isClosed)
				{
					//Connection.Close();
				}
			}
		}

		#endregion

		#region ExecuteNonQueryAsync

		/// <summary>
		///		Executes a Transact-SQL statement against the connection.
		/// </summary>
		/// <returns>
		///		The number of rows affected.
		/// </returns>
		public async Task<int> ExecuteNonQueryAsync()
		{
			bool isClosed = (Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				await Connection.OpenAsync();
			}

			EnsureTransaction();

			try
			{
				if (AutoPrepare)
				{
					Command.Prepare();
				}

				return await Command.ExecuteNonQueryAsync();
			}
			finally
			{
				if (isClosed)
				{
					//Connection.Close();
				}
			}
		}

		#endregion

		#region ExecuteReader

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" />.
		/// </summary>
		///	<returns>
		///		A <see cref="T:TDbDataReader"/> object.
		///	</returns>
		public TDbDataReader ExecuteReader()
			=> ExecuteReader(CommandBehavior.Default);

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" /> using
		///		one of the <see cref="CommandBehavior" /> values.
		/// </summary>
		/// <param name="behavior">One of the <see cref="P:CommandBehavior"/> values.</param>
		///	<returns>
		///		A <see cref="T:TDbDataReader"/> object.
		///	</returns>
		public TDbDataReader ExecuteReader(CommandBehavior behavior)
		{
			bool isClosed = (Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				Connection.Open();
			}

			EnsureTransaction();

			if (AutoPrepare)
			{
				Command.Prepare();
			}

			return (TDbDataReader)Command.ExecuteReader(behavior);
		}

		#endregion

		#region ExecuteReader with reader delegate

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" /> using
		///		one of the <see cref="CommandBehavior" /> values, and returns a <see cref="T:TResult"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the object to return.</typeparam>
		/// <param name="behavior">One of the <see cref="P:CommandBehavior"/> values.</param>
		/// <param name="readerDelegate">The delegate that processes the data reader and returns a
		///		<see cref="T:TResult"/> object.</param>
		///	<returns>
		///		A <see cref="T:TResult"/> object populated by the data reader.
		///	</returns>
		public TResult ExecuteReader<TResult>(CommandBehavior behavior, Func<TDbFactoryCommand, TDbDataReader, TResult> readerDelegate)
		{
			bool isClosed = (Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				Connection.Open();
			}

			EnsureTransaction();

			try
			{
				if (AutoPrepare)
				{
					Command.Prepare();
				}

				using (TDbDataReader dr = (TDbDataReader)Command.ExecuteReader(behavior))
				{
					try
					{
						return readerDelegate((TDbFactoryCommand)this, dr);
					}
					catch (OscException) { throw; }
					catch (Exception ex)
					{
						throw new ReaderDelegateException(this, ex);
					}
					finally
					{
						dr.Close();
					}
				}
			}
			finally
			{
				if (isClosed)
				{
					//Connection.Close();
				}
			}
		}

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" /> using
		///		one of the <see cref="CommandBehavior" /> values, and returns a <see cref="T:TResult"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the object to return.</typeparam>
		/// <param name="behavior">One of the <see cref="P:CommandBehavior"/> values.</param>
		/// <param name="readerDelegate">The delegate that processes the data reader and returns a
		///		<see cref="T:TResult"/> object.</param>
		///	<returns>
		///		A <see cref="T:TResult"/> object populated by the data reader.
		///	</returns>
		public TResult ExecuteReader<TResult>(CommandBehavior behavior, Func<TDbDataReader, TResult> readerDelegate)
			=> ExecuteReader<TResult>(behavior, (dbFactoryCommand, reader) => readerDelegate(reader));

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" />, and
		///		returns a <see cref="T:TResult"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the object to return.</typeparam>
		/// <param name="readerDelegate">The delegate that processes the data reader and returns a
		///		<see cref="T:TResult"/> object.</param>
		///	<returns>
		///		A <see cref="T:TResult"/> object populated by the data reader.
		///	</returns>
		public TResult ExecuteReader<TResult>(Func<TDbFactoryCommand, TDbDataReader, TResult> readerDelegate)
			=> ExecuteReader<TResult>(CommandBehavior.Default, readerDelegate);

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" />, and
		///		returns a <see cref="T:TResult"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the object to return.</typeparam>
		/// <param name="readerDelegate">The delegate that processes the data reader and returns a
		///		<see cref="T:TResult"/> object.</param>
		///	<returns>
		///		A <see cref="T:TResult"/> object populated by the data reader.
		///	</returns>
		public TResult ExecuteReader<TResult>(Func<TDbDataReader, TResult> readerDelegate)
			=> ExecuteReader<TResult>(CommandBehavior.Default, (dbFactoryCommand, reader) => readerDelegate(reader));

		#endregion

		#region ExecuteReaderAsync

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" />.
		/// </summary>
		///	<returns>
		///		A <see cref="T:TDbDataReader"/> object.
		///	</returns>
		public async Task<TDbDataReader> ExecuteReaderAsync()
			=> await ExecuteReaderAsync(CommandBehavior.Default);

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" /> using
		///		one of the <see cref="CommandBehavior" /> values.
		/// </summary>
		/// <param name="behavior">One of the <see cref="P:CommandBehavior"/> values.</param>
		///	<returns>
		///		A <see cref="T:TDbDataReader"/> object.
		///	</returns>
		public async Task<TDbDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			bool isClosed = (Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				await Connection.OpenAsync();
			}

			EnsureTransaction();

			if (AutoPrepare)
			{
				Command.Prepare();
			}

			return (TDbDataReader)await Command.ExecuteReaderAsync(behavior);
		}

		#endregion

		#region ExecuteReaderAsync with reader delegate

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" /> using
		///		one of the <see cref="CommandBehavior" /> values, and returns a <see cref="T:TResult"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the object to return.</typeparam>
		/// <param name="behavior">One of the <see cref="P:CommandBehavior"/> values.</param>
		/// <param name="readerDelegateAsync">The delegate that processes the data reader and returns a
		///		<see cref="T:TResult"/> object.</param>
		///	<returns>
		///		A <see cref="T:TResult"/> object populated by the data reader.
		///	</returns>
		public async Task<TResult> ExecuteReaderAsync<TResult>(CommandBehavior behavior, Func<TDbFactoryCommand, TDbDataReader, Task<TResult>> readerDelegateAsync)
		{
			bool isClosed = (Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				await Connection.OpenAsync();
			}

			EnsureTransaction();

			try
			{
				if (AutoPrepare)
				{
					Command.Prepare();
				}

				using (TDbDataReader dr = (TDbDataReader)await Command.ExecuteReaderAsync(behavior))
				{
					try
					{
						return await readerDelegateAsync((TDbFactoryCommand)this, dr);
					}
					catch (DbCommandException) { throw; }
					catch (Exception ex)
					{
						throw new ReaderDelegateException(this, ex);
					}
					finally
					{
						dr.Close();
					}
				}
			}
			finally
			{
				if (isClosed)
				{
					//Connection.Close();
				}
			}
		}

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" /> using
		///		one of the <see cref="CommandBehavior" /> values, and returns a <see cref="T:TResult"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the object to return.</typeparam>
		/// <param name="behavior">One of the <see cref="P:CommandBehavior"/> values.</param>
		/// <param name="readerDelegateAsync">The delegate that processes the data reader and returns a
		///		<see cref="T:TResult"/> object.</param>
		///	<returns>
		///		A <see cref="T:TResult"/> object populated by the data reader.
		///	</returns>
		public async Task<TResult> ExecuteReaderAsync<TResult>(CommandBehavior behavior, Func<TDbDataReader, Task<TResult>> readerDelegateAsync)
		{
			return await ExecuteReaderAsync<TResult>(behavior, (dbFactoryCommand, reader) => readerDelegateAsync(reader));
		}

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" />, and
		///		returns a <see cref="T:TResult"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the object to return.</typeparam>
		/// <param name="readerDelegateAsync">The delegate that processes the data reader and returns a
		///		<see cref="T:TResult"/> object.</param>
		///	<returns>
		///		A <see cref="T:TResult"/> object populated by the data reader.
		///	</returns>
		public async Task<TResult> ExecuteReaderAsync<TResult>(Func<TDbFactoryCommand, TDbDataReader, Task<TResult>> readerDelegateAsync)
		{
			return await ExecuteReaderAsync<TResult>(CommandBehavior.Default, readerDelegateAsync);
		}

		/// <summary>
		///		Executes the <see cref="CommandText" /> against the <see cref="Connection" />, and
		///		returns a <see cref="T:TResult"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the object to return.</typeparam>
		/// <param name="readerDelegateAsync">The delegate that processes the data reader and returns a
		///		<see cref="T:TResult"/> object.</param>
		///	<returns>
		///		A <see cref="T:TResult"/> object populated by the data reader.
		///	</returns>
		public async Task<TResult> ExecuteReaderAsync<TResult>(Func<TDbDataReader, Task<TResult>> readerDelegateAsync)
		{
			return await ExecuteReaderAsync<TResult>(CommandBehavior.Default, (dbFactoryCommand, reader) => readerDelegateAsync(reader));
		}

		#endregion

		#region ExecuteScalar

		/// <summary>
		///		Executes the query, and returns the first column of the first row in the result set
		///		returned by the query. Additional columns or rows are ignored.
		/// </summary>
		/// <returns>
		///		The first column of the first row in the result set, or a null reference
		///		(<b>Nothing</b> in Visual Basic) if the result set is empty.
		///	</returns>
		public object? ExecuteScalar()
		{
			bool isClosed = (Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				Connection.Open();
			}

			EnsureTransaction();

			try
			{
				if (AutoPrepare)
				{
					Command.Prepare();
				}

				object? ro = Command.ExecuteScalar();

				return (ro is DBNull ? null : ro);
			}
			finally
			{
				if (isClosed)
				{
					//Connection.Close();
				}
			}
		}

		#endregion

		#region ExecuteScalarAsync

		/// <summary>
		///		Executes the query, and returns the first column of the first row in the result set
		///		returned by the query. Additional columns or rows are ignored.
		/// </summary>
		/// <returns>
		///		The first column of the first row in the result set, or a null reference
		///		(<b>Nothing</b> in Visual Basic) if the result set is empty.
		///	</returns>
		public async Task<object?> ExecuteScalarAsync()
		{
			bool isClosed = (Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				await Connection.OpenAsync();
			}

			EnsureTransaction();

			try
			{
				if (AutoPrepare)
				{
					Command.Prepare();
				}

				object? ro = await Command.ExecuteScalarAsync();

				return (ro is DBNull ? null : ro);
			}
			finally
			{
				if (isClosed)
				{
					//Connection.Close();
				}
			}
		}

		#endregion

		#region Prepare

		/// <summary>
		///		Creates a prepared version of the command on an instance of SQL Server.
		/// </summary>
		public void Prepare()
		{
			Command.Prepare();
		}

		#endregion

		#endregion

		#region Protected Methods

		/// <summary>
		///		Create an instance of <see cref="T:TDbDataAdapter"/>.
		/// </summary>
		/// <returns></returns>
		protected abstract TDbDataAdapter CreateDataAdapter();

		#endregion
	}

	/// <summary>
	///		Summary description for DbFactoryCommand.
	/// </summary>
	public abstract class DbFactoryCommand
	{
		#region Public Methods

		#region ToString

		/// <summary>
		///		Converts the command to a SQL string that can be executed directly against the
		///		database.
		/// </summary>
		///	<returns>
		///		<para>
		///			A SQL execution string for the command.
		///		</para>
		///	</returns>
		public abstract override string ToString();

		#endregion

		#endregion
	}
}
