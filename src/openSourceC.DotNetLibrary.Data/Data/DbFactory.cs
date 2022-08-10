//#define USES_UNMANGED_CODE
//#define ENABLE_CONNECTION_TIMEOUT

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace openSourceC.DotNetLibrary.Data
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
		where TDbFactoryCommand : DbFactoryCommand<TDbFactory, TDbFactoryCommand, TDbParams, TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataAdapter, TDbDataReader>
		where TDbParams : DbParamsBase<TDbParams, TDbCommand, TDbParameter>
		where TDbConnection : DbConnection
		where TDbTransaction : DbTransaction
		where TDbCommand : DbCommand
		where TDbParameter : DbParameter
		where TDbDataAdapter : DbDataAdapter
		where TDbDataReader : DbDataReader
	{
		/// <summary>Gets the default connection timeout.</summary>
		protected internal const int DEFAULT_CONNECTION_TIMEOUT = 30;

		private readonly string _connectionString;

		private TDbTransaction? _transaction;
		private readonly Stack<TDbTransaction> _transactionStack;

		// Track whether Dispose has been called.
		private bool _disposed = false;


		#region Constructors

		/// <summary>
		///		Class constructor.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		protected DbFactory(
			string connectionString
		)
		{
			_connectionString = connectionString;

			_transaction = null;
			_transactionStack = new();
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
					if (_transaction is not null)
					{
						_transaction.Rollback();
						_transaction.Dispose();
						_transaction = null;
					}
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

		#region Properties

		/// <summary>
		///		Gets a value indicating that an ambient transaction exists.
		/// </summary>
		public bool AmbientTransactionExists =>
			System.Transactions.Transaction.Current is not null;

		/// <summary>
		///		Gets the connection object of this instance.
		/// </summary>
		protected internal abstract TDbConnection Connection { get; }

		/// <summary>
		///		Gets the connection string.
		/// </summary>
		internal string ConnectionString =>
			_connectionString;

#if ENABLE_CONNECTION_TIMEOUT
		/// <summary>
		///		Gets the connection timeout value.
		/// </summary>
		public abstract int ConnectionTimeout { get; set; }
#endif

		/// <summary>
		///		Gets or sets the current <see cref="T:TDbTransaction"/>.  If a transaction exists
		///		prior to calling set, it will be destroyed and replaced with the new value.
		/// </summary>
		protected internal TDbTransaction? Transaction
		{
			get => _transaction;

			set
			{
				PushTransaction();

				_transaction = value;
			}
		}

		/// <summary>
		///		Gets the transaction stack.
		/// </summary>
		private Stack<TDbTransaction> TransactionStack =>
			_transactionStack;

		/// <summary>
		///		Gets a value indicating that the transaction stack is empty.
		/// </summary>
		private bool TransactionStackIsEmpty =>
			_transactionStack.Count == 0;

		#endregion

		#region Create Command Methods

		/// <summary>
		///		Create Command object.
		/// </summary>
		/// <param name="commandText">The command string.</param>
		/// <returns></returns>
		public abstract TDbFactoryCommand CreateStoredProcedureCommand(string commandText);

		/// <summary>
		///		Create Command object.
		/// </summary>
		/// <param name="commandText">The command string.</param>
		/// <returns></returns>
		public abstract TDbFactoryCommand CreateTableDirectCommand(string commandText);

		/// <summary>
		///		Create Command object.
		/// </summary>
		/// <param name="commandText">The command string.</param>
		/// <returns></returns>
		public abstract TDbFactoryCommand CreateTextCommand(string commandText);

		#endregion

		#region Connection Methods

		/// <summary>
		///		Gets the <see cref="T:DbProviderFactory"/> for the database provider.
		/// </summary>
		/// <returns>
		///		The <see cref="T:DbProviderFactory"/> for the database provider.
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
			if (_transaction is null)
			{
				throw new OscErrorException("Not in a transaction");
			}

			_transaction.Commit();

			PopTransaction();
		}

		/// <summary>
		///		Pops the current transaction object off the stack.
		/// </summary>
		protected virtual void PopTransaction()
		{
			if (_transaction is not null)
			{
				_transaction.Dispose();
				_transaction = null;
			}

			if (!TransactionStackIsEmpty)
			{
				_transaction = TransactionStack.Pop();
			}
		}

		/// <summary>
		///		Pushes the current transaction object on the stack.
		/// </summary>
		protected virtual void PushTransaction()
		{
			if (_transaction is not null)
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
			if (_transaction is null)
			{
				throw new OscErrorException("Not in a transaction");
			}

			_transaction.Rollback();

			PopTransaction();
		}

		#endregion
	}
}
