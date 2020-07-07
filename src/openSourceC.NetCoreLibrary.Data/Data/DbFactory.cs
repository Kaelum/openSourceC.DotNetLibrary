//#define USES_UNMANGED_CODE
//#define ENABLE_CONNECTION_TIMEOUT

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace openSourceC.NetCoreLibrary.Data
{
	/// <summary>
	///		Summary description for DbFactory.
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
	public abstract class DbFactory<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader> : IDisposable
		where TDbFactory : DbFactory<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader>
		where TDbFactoryCommand : DbFactoryCommand<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader>, new()
		where TDbParams : DbParamsBase<TDbParams, TDbCommand, TDbParameter>, new()
		where TDbConnection : DbConnection
		where TDbTransaction : DbTransaction
		where TDbCommand : DbCommand
		where TDbParameter : DbParameter
		where TDbDataAdapter : DbDataAdapter, new()
		where TDbDataReader : DbDataReader
	{
		private const int _defaultConnectionTimeout = 30;

		private ConnectionStringSettings _connectionStringSettings;
		private DbProviderFactory _dbProviderFactory;
		private TDbConnection _cn;
#if ENABLE_CONNECTION_TIMEOUT
		private int _connectionTimeout;
#endif
		private TDbTransaction _transaction;
		private Stack<TDbTransaction> _transactionStack;

		// Track whether Dispose has been called.
		private bool _disposed = false;


		#region Constructors

		/// <summary>
		///		Class constructor.
		/// </summary>
		public DbFactory(string connectionStringName)
		{
			_dbProviderFactory = GetDbProviderFactory();

			ChangeConnection(connectionStringName);
		}

		#endregion

		#region Dispose

#if USES_UNMANGED_CODE
		/// <summary>
		///     Destructor.
		/// </summary>
		~DbFactory()
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
					if (_transaction != null)
					{
						_transaction.Rollback();
						_transaction.Dispose();
						_transaction = null;
					}

					if (_cn != null)
					{
						_cn.Dispose();
						_cn = null;
					}

					// Nullify references to managed resources that are not disposable.
					_dbProviderFactory = null;

					// Nullify references to externally created managed resources.
				}

#if USES_UNMANGED_CODE
				// Dispose of unmanaged resources here.
#endif

				_disposed = true;
			}
		}

		#endregion

		#region Properties

		/// <summary>
		///		Gets a value indicating that an ambient transaction exists.
		/// </summary>
		public bool AmbientTransactionExists { get { return System.Transactions.Transaction.Current != null; } }

		/// <summary>
		///		Gets the connection object of this instance.
		/// </summary>
		protected internal TDbConnection Connection
		{
			get
			{
				if (_cn == null)
				{
#if ENABLE_CONNECTION_TIMEOUT
					DbConnectionStringBuilder csb = _dbProviderFactory.CreateConnectionStringBuilder();
					csb.ConnectionString = _connectionStringSettings.ConnectionString;
					csb["Connection Timeout"] = _connectionTimeout;
#endif

					_cn = (TDbConnection)_dbProviderFactory.CreateConnection();
#if ENABLE_CONNECTION_TIMEOUT
					_cn.ConnectionString = csb.ConnectionString;
#else
					_cn.ConnectionString = _connectionStringSettings.ConnectionString;
#endif
				}

				return _cn;
			}
		}

		/// <summary>
		///		Gets the connection string.
		/// </summary>
		internal string ConnectionString
		{
			get { return _connectionStringSettings.ConnectionString; }
		}

		/// <summary>
		///		Gets the name of the connection string.
		/// </summary>
		public string ConnectionStringName
		{
			get { return _connectionStringSettings.Name; }
		}

		/// <summary>
		///		Gets the provider name property of the connection string.
		/// </summary>
		internal string ConnectionStringProviderName
		{
			get { return _connectionStringSettings.ProviderName; }
		}

#if ENABLE_CONNECTION_TIMEOUT
		/// <summary>
		///		Gets the connection timeout value.
		/// </summary>
		public int ConnectionTimeout
		{
			get { return _connectionTimeout; }
		}
#endif

		/// <summary>
		///     Gets the default connection timeout.
		/// </summary>
		protected internal virtual int DefaultConnectionTimeout
		{
			get { return _defaultConnectionTimeout; }
		}

		/// <summary>
		///		Gets or sets the current <see cref="T:TDbTransaction"/>.  If a transaction exists
		///		prior to calling set, it will be destroyed and replaced with the new value.
		/// </summary>
		protected internal TDbTransaction Transaction
		{
			get { return _transaction; }

			set
			{
				PushTransaction();

				_transaction = value;
			}
		}

		/// <summary>
		///		Gets the transaction stack.
		/// </summary>
		private Stack<TDbTransaction> TransactionStack
		{
			get
			{
				if (_transactionStack == null)
				{
					_transactionStack = new Stack<TDbTransaction>();
				}

				return _transactionStack;
			}
		}

		/// <summary>
		///		Gets a value indicating that the transaction stack is empty.
		/// </summary>
		private bool TransactionStackIsEMpty { get { return _transactionStack == null || _transactionStack.Count == 0; } }

		/// <summary>
		///		Gets a value indicating that a transaction exists.
		/// </summary>
		public bool TransactionExists { get { return _transaction != null; } }

		#endregion

		#region Create Command Methods

		/// <summary>
		///		Create Command object.
		/// </summary>
		/// <param name="commandText">The command string.</param>
		/// <returns></returns>
		public TDbFactoryCommand CreateStoredProcedureCommand(string commandText)
		{
			return DbFactoryCommand<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader>.Create((TDbFactory)this, commandText, CommandType.StoredProcedure, false);
		}

		/// <summary>
		///		Create Command object.
		/// </summary>
		/// <param name="commandText">The command string.</param>
		/// <returns></returns>
		public TDbFactoryCommand CreateTableDirectCommand(string commandText)
		{
			return DbFactoryCommand<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader>.Create((TDbFactory)this, commandText, CommandType.TableDirect, false);
		}

		/// <summary>
		///		Create Command object.
		/// </summary>
		/// <param name="commandText">The command string.</param>
		/// <returns></returns>
		public TDbFactoryCommand CreateTextCommand(string commandText)
		{
			return DbFactoryCommand<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader>.Create((TDbFactory)this, commandText, CommandType.Text, false);
		}

		#endregion

		#region Connection Methods

		/// <summary>
		///		Changes the connection.
		/// </summary>
		/// <param name="connectionStringName">The name of the connection string to use.</param>
		public void ChangeConnection(string connectionStringName)
		{
#if ENABLE_CONNECTION_TIMEOUT
			ChangeConnection(connectionStringName, DEFAULT_CONNECTION_TIMEOUT);
		}

		/// <summary>
		///		Changes the connection.
		/// </summary>
		/// <param name="connectionStringName">The name of the connection string to use.</param>
		/// <param name="connectionTimeout">The connection timeout to be used.</param>
		public void ChangeConnection(string connectionStringName, int connectionTimeout)
		{
#endif
			if (_cn != null)
			{
				while (_transaction != null)
				{
					PopTransaction();
				}

				DestroyingConnection();

				_cn.Dispose();
				_cn = null;
			}

			_connectionStringSettings = GetConnectionStringSettings(connectionStringName);

			if (_connectionStringSettings == null)
			{
				throw new OscErrorException(string.Format("Connection string ({0}) settings not found.", connectionStringName));
			}

#if ENABLE_CONNECTION_TIMEOUT
			_connectionTimeout = connectionTimeout;
#endif
		}

		/// <summary>
		///		Executes before the current connection object is destroyed.
		/// </summary>
		protected abstract void DestroyingConnection();

		/// <summary>
		///		Gets connection string for the named connection.
		/// </summary>
		/// <param name="connectionStringName">The name of the connection to use.</param>
		/// <returns>
		///		Returns a connection string.
		///	</returns>
		protected abstract ConnectionStringSettings GetConnectionStringSettings(string connectionStringName);

		/// <summary>
		///		Gets connection string for the named connection.
		/// </summary>
		/// <returns>
		///		Returns a connection string.
		///	</returns>
		protected abstract DbProviderFactory GetDbProviderFactory();

		#endregion

		#region Transaction Methods

		/// <summary>
		///		Begins a database transaction.
		/// </summary>
		public void BeginTransaction()
		{
			if (Connection.State == ConnectionState.Closed)
			{
				Connection.Open();
			}

			Transaction = (TDbTransaction)Connection.BeginTransaction();
		}

		/// <summary>
		///		Begins a database transaction with the specified <see cref="T:IsolationLevel"/> value.
		/// </summary>
		/// <param name="isolationLevel">One of the <see cref="T:IsolationLevel"/> values.</param>
		public void BeginTransaction(IsolationLevel isolationLevel)
		{
			if (Connection.State == ConnectionState.Closed)
			{
				Connection.Open();
			}

			Transaction = (TDbTransaction)Connection.BeginTransaction(isolationLevel);
		}

		/// <summary>
		///		Commits the database transaction.
		/// </summary>
		public void Commit()
		{
			if (!TransactionExists)
			{
				throw new OscErrorException("Not in a transaction");
			}

			Transaction.Commit();

			PopTransaction();
		}

		/// <summary>
		///		Pops the current transaction object off the stack.
		/// </summary>
		protected virtual void PopTransaction()
		{
			if (_transaction != null)
			{
				_transaction.Dispose();
				_transaction = null;
			}

			if (!TransactionStackIsEMpty)
			{
				_transaction = TransactionStack.Pop();
			}
		}

		/// <summary>
		///		Pushes the current transaction object on the stack.
		/// </summary>
		protected virtual void PushTransaction()
		{
			if (_transaction != null)
			{
				TransactionStack.Push(_transaction);
				_transaction = null;
			}
		}

		/// <summary>
		///		Rolls back a transaction from a pending state.
		/// </summary>
		public void Rollback()
		{
			if (!TransactionExists)
			{
				throw new OscErrorException("Not in a transaction");
			}

			Transaction.Rollback();

			PopTransaction();
		}

		#endregion
	}
}
