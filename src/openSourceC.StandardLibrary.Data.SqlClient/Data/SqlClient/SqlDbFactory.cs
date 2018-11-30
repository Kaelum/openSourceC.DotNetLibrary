using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace openSourceC.StandardLibrary.Data.SqlClient
{
	/// <summary>
	///		Summary description for SqlDbFactory.
	/// </summary>
	public sealed class SqlDbFactory : DbFactory<SqlDbFactory, SqlDbCommand, SqlDbParams, SqlConnection, SqlTransaction, SqlCommand, SqlParameter, SqlDataAdapter, SqlDataReader>
	{
		private const int DEFAULT_CONNECTION_TIMEOUT = 30;

		private byte[] _filestreamTransactionContext;
		private Stack<byte[]> _filestreamTransactionContextStack;

#if SUPPORT_SQL_NOTIFICATION
		private Guid? _conversationHandle = null;

		/// <summary>
		///		Occurs when a notification is received from the listener.
		/// </summary>
		public event SqlNotificationReceiveEventHandler NotificationReceived;

		private static SortedList<string, bool> _serviceBrokerDatabases = new SortedList<string, bool>();
		private static object _serviceBrokerDatabasesLock = new object();
#endif


		#region Constructors

		/// <summary>
		///		Class constructor.
		/// </summary>
		/// <param name="connectionStringName">The name of the connection string to use.</param>
		public SqlDbFactory(string connectionStringName) : base(connectionStringName) { }

		#endregion

		#region Private Properties

		/// <summary>
		///		Gets the current transaction context of a session.
		/// </summary>
		private byte[] FilestreamTransactionContext
		{
			get
			{
				if (_filestreamTransactionContext == null)
				{
					if (!TransactionExists && !AmbientTransactionExists)
					{
						throw new InvalidOperationException("An explicit or ambient transaction is required.");
					}

					using (SqlDbCommand cmd = CreateTextCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT();"))
					{
						_filestreamTransactionContext = (byte[])cmd.ExecuteScalar();
					}

					if (_filestreamTransactionContext == null || _filestreamTransactionContext.Length == 0)
					{
						throw new InvalidOperationException("Unable to obtain a FILESTREAM transaction context.");
					}
				}

				return _filestreamTransactionContext;
			}
		}

		/// <summary>
		///		Gets the FILESTREAM transaction context stack.
		/// </summary>
		private Stack<byte[]> FilestreamTransactionContextStack
		{
			get
			{
				if (_filestreamTransactionContextStack == null)
				{
					_filestreamTransactionContextStack = new Stack<byte[]>();
				}

				return _filestreamTransactionContextStack;
			}
		}

		/// <summary>
		///		Gets a value indicating that the FILESTREAM transaction context stack is empty.
		/// </summary>
		private bool FilestreamTransactionContextStackIsEmpty { get { return _filestreamTransactionContextStack == null || _filestreamTransactionContextStack.Count == 0; } }

		#endregion

		#region Protected Overrides

		#region Connection

		/// <summary>
		///     Gets the default connection timeout.
		/// </summary>
		protected internal override int DefaultConnectionTimeout
		{
			get { return DEFAULT_CONNECTION_TIMEOUT; }
		}

		/// <summary>
		///		Executes before the current connection object is destroyed.
		/// </summary>
		protected override void DestroyingConnection()
		{
			_filestreamTransactionContext = null;
			_filestreamTransactionContextStack = null;
		}

		/// <summary>
		///		Gets a connection string.
		/// </summary>
		/// <param name="connectionStringName">The name of the connection string</param>
		/// <returns></returns>
		protected override ConnectionStringSettings GetConnectionStringSettings(string connectionStringName)
		{
			return ConfigurationManager.ConnectionStrings[connectionStringName];
		}

		/// <summary>
		///		Gets an instance of the <see cref="T:SqlClientFactory"/>.
		/// </summary>
		/// <returns></returns>
		protected override DbProviderFactory GetDbProviderFactory()
		{
			return SqlClientFactory.Instance;
		}

		#endregion

		#region SqlDependency

		/// <summary>
		///		Starts the listener for receiving dependency change notifications from the instance
		///		of SQL Server specified by the current connection using the specified SQL Server
		///		Service Broker queue.
		/// </summary>
		/// <param name="queueName">An existing SQL Server Service Broker queue to be used. If
		///		<b>null</b>, the default queue is used.</param>
		[SqlClientPermission(SecurityAction.Demand, Unrestricted = true)]
		public void DependencyStart(string queueName)
		{
			SqlDependency.Start(Connection.ConnectionString, queueName);
		}

		/// <summary>
		///		Stops a listener for a connection specified in a previous
		///		<see cref="M:DependencyStart"/> call.
		/// </summary>
		/// <param name="queueName">The SQL Server Service Broker queue that was used in a previous
		///		<see cref="M:DependencyStart"/> call.</param>
		[SqlClientPermission(SecurityAction.Demand, Unrestricted = true)]
		public void DependencyStop(string queueName)
		{
			SqlDependency.Stop(Connection.ConnectionString, queueName);
		}

		#endregion

		#region Transaction

		/// <summary>
		///		Executes when popping from the transaction stack.
		/// </summary>
		protected override void PopTransaction()
		{
			if (!FilestreamTransactionContextStackIsEmpty)
			{
				_filestreamTransactionContext = FilestreamTransactionContextStack.Pop();
			}

			base.PopTransaction();
		}

		/// <summary>
		///		Executes when pushing to the transaction stack.
		/// </summary>
		protected override void PushTransaction()
		{
			if (TransactionExists)
			{
				FilestreamTransactionContextStack.Push(_filestreamTransactionContext);
				_filestreamTransactionContext = null;
			}

			base.PushTransaction();
		}

		#endregion

		#endregion

#if SUPPORT_SQL_FILESTREAM
		#region SqlFileStream

		/// <summary>
		///		Returns a <see cref="T:SqlFileStream"/> object.
		/// </summary>
		/// <param name="pathName">The logical path to the file. The path can be retrieved by using
		///		the Transact-SQL Pathname function on the underlying FILESTREAM column in the table.</param>
		/// <param name="fileAccess">
		///		The access mode to use when opening the file. Supported
		///		<see cref="T:FileAccess"/> enumeration values are <see cref="T:FileAccess.Read"/>,
		///		<see cref="T:FileAccess.Write"/>, and <see cref="T:FileAccess.ReadWrite"/>.
		///		<para>When using <b>FileAccess.Read</b>, the <b>SqlFileStream</b> object can be used
		///		to read all of the existing data.</para>
		///		<para>When using <b>FileAccess.Write</b>, <b>SqlFileStream</b> points to a zero byte
		///		file. Existing data will be overwritten when the object is closed and the
		///		transaction is committed.</para>
		///		<para>When using <b>FileAccess.ReadWrite</b>, the <b>SqlFileStream</b> points to a
		///		file which has all the existing data in it. The handle is positioned at the
		///		beginning of the file. You can use one of the <b>System.IO</b> Seek methods to move
		///		the handle position within the file to write or append new data.</para>
		///	</param>
		/// <returns>
		///		A <see cref="T:SqlFileStream"/> object.
		/// </returns>
		public SqlFileStream GetFileStream(string pathName, FileAccess fileAccess)
		{
			return GetFileStream(pathName, fileAccess, FileOptions.Asynchronous, 0);
		}

		/// <summary>
		///		Returns a <see cref="T:SqlFileStream"/> object.
		/// </summary>
		/// <param name="pathName">The logical path to the file. The path can be retrieved by using
		///		the Transact-SQL Pathname function on the underlying FILESTREAM column in the table.</param>
		/// <param name="fileAccess">
		///		The access mode to use when opening the file. Supported
		///		<see cref="T:FileAccess"/> enumeration values are <see cref="T:FileAccess.Read"/>,
		///		<see cref="T:FileAccess.Write"/>, and <see cref="T:FileAccess.ReadWrite"/>.
		///		<para>When using <b>FileAccess.Read</b>, the <b>SqlFileStream</b> object can be used
		///		to read all of the existing data.</para>
		///		<para>When using <b>FileAccess.Write</b>, <b>SqlFileStream</b> points to a zero byte
		///		file. Existing data will be overwritten when the object is closed and the
		///		transaction is committed.</para>
		///		<para>When using <b>FileAccess.ReadWrite</b>, the <b>SqlFileStream</b> points to a
		///		file which has all the existing data in it. The handle is positioned at the
		///		beginning of the file. You can use one of the <b>System.IO</b> Seek methods to move
		///		the handle position within the file to write or append new data.</para>
		///	</param>
		///	<param name="options">Specifies the option to use while opening the file. Supported
		///		<see cref="T:FileOptions"/> values are <see cref="T:FileOptions.Asynchronous"/>,
		///		<see cref="T:FileOptions.WriteThrough"/>, <see cref="T:FileOptions.SequentialScan"/>,
		///		and <see cref="T:FileOptions.RandomAccess"/>.</param>
		///	<param name="allocationSize">The allocation size to use while creating a file. If set to
		///		0, the default value is used.</param>
		/// <returns>
		///		A <see cref="T:SqlFileStream"/> object.
		/// </returns>
		public SqlFileStream GetFileStream(string pathName, FileAccess fileAccess, FileOptions options, long allocationSize)
		{
			SqlFileStream fileStream = new SqlFileStream(pathName, FilestreamTransactionContext, fileAccess, options, allocationSize);

			return fileStream;
		}

		#endregion
#endif

#if SUPPORT_ASYNC_EXECUTE_READER && SUPPORT_SQL_NOTIFICATION
		#region SqlNotificationRequest

		/// <summary>
		///		Begin execution of a listener for a <see cref="T:SqlNotificationRequest"/> that will
		///		execute on a different <see cref="T:SqlDbFactory"/>.
		/// </summary>
		/// <param name="queueName">The Service Broker queue name.</param>
		/// <param name="timeout">The number of seconds to wait for the T-SQL RECEIVE statement to
		///		return at least 1 message, or -1 to wait indefinitely.</param>
		///	<param name="exceptionCallback">The callback method for exceptions thrown during the
		///		asynchronous execution period of this call.</param>
		/// <returns>
		///		An <see cref="T:IAsyncResult"/> that can be used to poll or wait for results, or
		///		both; this value is also needed when invoking <see cref="T:EndExecuteReader"/>,
		///		which returns a <see cref="T:SqlDataReader"/> instance which can be used to retrieve
		///		the returned rows.
		/// </returns>
		public IAsyncResult BeginNotificationListener(string queueName, int timeout, Action<Exception> exceptionCallback)
		{
			if (exceptionCallback == null) { throw new ArgumentNullException(nameof(exceptionCallback)); }

			EnsureServiceBroker();

			if (NotificationReceived == null)
			{
				throw new InvalidOperationException($"The NotificationReceived event has no subscribers.");
			}

			int commandTimeoutExtension = 120;

			StringBuilder commandText = new StringBuilder();

			if (_conversationHandle.HasValue)
			{
				commandText.AppendLine($"END CONVERSATION @ConversationHandle;");
			}

			commandText.AppendLine($"WAITFOR (RECEIVE TOP(1)");
			commandText.AppendLine($"\t[status],");
			commandText.AppendLine($"\t[priority],");
			commandText.AppendLine($"\t[queuing_order],");
			commandText.AppendLine($"\t[conversation_group_id],");
			commandText.AppendLine($"\t[conversation_handle],");
			commandText.AppendLine($"\t[message_sequence_number],");
			commandText.AppendLine($"\t[service_name],");
			commandText.AppendLine($"\t[service_id],");
			commandText.AppendLine($"\t[service_contract_name],");
			commandText.AppendLine($"\t[service_contract_id],");
			commandText.AppendLine($"\t[message_type_name],");
			commandText.AppendLine($"\t[message_type_id],");
			commandText.AppendLine($"\t[validation],");
			commandText.AppendLine($"\tCAST([message_body] AS XML) [message_body]");
			commandText.AppendLine($"FROM");
			commandText.AppendLine($"\t{queueName}");
			commandText.AppendLine($"), TIMEOUT @TimeoutMilliseconds;");

			SqlDbFactoryCommand cmd = CreateTextCommand(commandText.ToString());

			if (_conversationHandle.HasValue)
			{
				cmd.Params.AddGuid("ConversationHandle", false, _conversationHandle);
			}
			cmd.Params.AddInt64("TimeoutMilliseconds", false, (timeout == -1 ? timeout : timeout * 1000));

			cmd.CommandTimeout = (
				timeout == -1
				? 0 // CommandTimeout uses 0 (zero) to define indefinitely.
				: timeout >= int.MaxValue - commandTimeoutExtension
				? int.MaxValue
				: timeout + commandTimeoutExtension
			);

			AsyncCallback callBack = new AsyncCallback(endNotificationListener);

			_conversationHandle = null;

			return cmd.BeginExecuteReader(callBack, cmd);

			void endNotificationListener(IAsyncResult ar)
			{
				try
				{
#if DIAGNOSTICS
					Debug.WriteLine($"Listener Reader: {queueName}");
#endif

					SqlNotificationReceiveEventArgs eventArgs;

					//using (SqlDbFactoryCommand cmd = (SqlDbFactoryCommand)ar.AsyncState)
					using (cmd)
					{
						try
						{
							using (SqlDataReader reader = cmd.EndExecuteReader(ar))
							{
								if (reader.Read())
								{
									eventArgs = new SqlNotificationReceiveEventArgs();
									eventArgs.Status = reader.GetByte("status");
									eventArgs.Priority = reader.GetByte("priority");
									eventArgs.QueuingOrder = reader.GetInt64("queuing_order");
									eventArgs.ConversationGroupId = reader.GetGuid("conversation_group_id");
									eventArgs.ConversationHandle = reader.GetGuid("conversation_handle");
									eventArgs.MessageSequenceNumber = reader.GetInt64("message_sequence_number");
									eventArgs.ServiceName = reader.GetString("service_name");
									eventArgs.ServiceId = reader.GetInt32("service_id");
									eventArgs.ServiceContractName = reader.GetString("service_contract_name");
									eventArgs.ServiceContractId = reader.GetInt32("service_contract_id");
									eventArgs.MessageTypeName = reader.GetString("message_type_name");
									eventArgs.MessageTypeId = reader.GetInt32("message_type_id");
									eventArgs.Validation = reader.GetString("validation");
									eventArgs.MessageBody = reader.GetStringNullSafe("message_body");

									_conversationHandle = eventArgs.ConversationHandle;

#if DIAGNOSTICS
									Debug.WriteLine($"Listener Reader: ConversationHandle={obj.ConversationHandle}, MessageTypeName={obj.MessageTypeName}, MessageBody={obj.MessageBody}");
#endif
								}
								else
								{
									eventArgs = null;
								}
							}
						}
						catch (Exception ex)
						{
							throw new DbCommandException(cmd.Command, ex);
						}
					}

					// Send notifications now that the reader has been disposed.
					if (eventArgs != null)
					{
						NotificationReceived?.Invoke(this, eventArgs);
					}
				}
				catch (Exception ex)
				{
					exceptionCallback(ex);
				}
			}
		}

		/// <summary>
		///		Purge the specified queue.
		/// </summary>
		/// <param name="queueName">The Service Broker queue name.</param>
		/// <returns></returns>
		public void PurgeQueue(string queueName)
		{
			EnsureServiceBroker();

			StringBuilder commandText = new StringBuilder();

			if (_conversationHandle.HasValue)
			{
				commandText.AppendLine($"END CONVERSATION @ConversationHandle;");
			}

			commandText.AppendLine($"WAITFOR (RECEIVE TOP(1)");
			commandText.AppendLine($"\t[conversation_handle],");
			commandText.AppendLine($"\t[message_type_name],");
			commandText.AppendLine($"\tCAST([message_body] AS XML) [message_body]");
			commandText.AppendLine($"FROM");
			commandText.AppendLine($"\t{queueName}");
			commandText.AppendLine($"), TIMEOUT 0;");

			using (SqlDbFactoryCommand cmd = CreateTextCommand(commandText.ToString()))
			{
				if (_conversationHandle.HasValue)
				{
					cmd.Params.AddGuid("ConversationHandle", false, _conversationHandle);
				}

				_conversationHandle = null;

				cmd.ExecuteReader(processReader);
			};

			Guid? processReader(SqlDataReader reader)
			{
#if DIAGNOSTICS
				Debug.WriteLine($"PurgeQueue Reader: {queueName}");
#endif

				if (reader.Read())
				{
					SqlNotificationReceiveEventArgs obj = new SqlNotificationReceiveEventArgs();
					//obj.Status = reader.GetByte("status");
					//obj.Priority = reader.GetByte("priority");
					//obj.QueuingOrder = reader.GetInt64("queuing_order");
					//obj.ConversationGroupId = reader.GetGuid("conversation_group_id");
					obj.ConversationHandle = reader.GetGuid("conversation_handle");
					//obj.MessageSequenceNumber = reader.GetInt64("message_sequence_number");
					//obj.ServiceName = reader.GetString("service_name");
					//obj.ServiceId = reader.GetInt32("service_id");
					//obj.ServiceContractName = reader.GetString("service_contract_name");
					//obj.ServiceContractId = reader.GetInt32("service_contract_id");
					obj.MessageTypeName = reader.GetString("message_type_name");
					//obj.MessageTypeId = reader.GetInt32("message_type_id");
					//obj.Validation = reader.GetString("validation");
					obj.MessageBody = reader.GetStringNullSafe("message_body");

					_conversationHandle = obj.ConversationHandle;

#if DIAGNOSTICS
					Debug.WriteLine($"PurgeQueue Reader: ConversationHandle={obj.ConversationHandle}, MessageTypeName={obj.MessageTypeName}, MessageBody={obj.MessageBody}");
#endif
				}

				return _conversationHandle;
			}
		}

		#endregion
#endif

		#region Transaction Methods

		/// <summary>
		///		Starts a database transaction with the specified transaction name.
		/// </summary>
		/// <param name="transactionName">The name of the transaction.</param>
		public void BeginTransaction(string transactionName)
		{
			Transaction = Connection.BeginTransaction(transactionName);
		}

		/// <summary>
		///		Starts a database transaction with the specified isolation level and transaction
		///		name.
		/// </summary>
		/// <param name="isolationLevel">One of the <see cref="T:IsolationLevel"/> values.</param>
		/// <param name="transactionName">The name of the transaction.</param>
		public void BeginTransaction(IsolationLevel isolationLevel, string transactionName)
		{
			Transaction = Connection.BeginTransaction(isolationLevel, transactionName);
		}

		/// <summary>
		///		Rolls back a transaction from a pending state, and specifies the transaction or
		///		savepoint name.
		/// </summary>
		/// <param name="transactionName">The name of the transaction to roll back, or the savepoint
		///		to which to roll back.</param>
		public void Rollback(string transactionName)
		{
			if (!TransactionExists)
			{
				throw new OscErrorException("Not in a transaction");
			}

			Transaction.Rollback(transactionName);

			PopTransaction();
		}

		#endregion

		#region Internal Methods

#if SUPPORT_SQL_NOTIFICATION
		internal void EnsureServiceBroker()
		{
			SqlConnectionStringBuilder connSb = new SqlConnectionStringBuilder(ConnectionString);
			string key = $"server={connSb.DataSource};database={connSb.InitialCatalog};";

			if (!_serviceBrokerDatabases.TryGetValue(key, out bool isSupported))
			{
				lock (_serviceBrokerDatabasesLock)
				{
					if (!_serviceBrokerDatabases.TryGetValue(key, out isSupported))
					{
						using (SqlDbFactoryCommand cmd = CreateTextCommand($"SELECT [is_broker_enabled] FROM sys.databases WHERE [database_id] = DB_ID();"))
						{
							isSupported = cmd.ExecuteReader((reader) =>
							{
								if (reader.Read())
								{
									return reader.GetBoolean("is_broker_enabled");
								}

								return false;
							});
						};

						_serviceBrokerDatabases.Add(key, isSupported);
					}
				}
			}

			if (isSupported) { return; }

			throw new InvalidOperationException($"The Service Broker is not enabled on the [{connSb.DataSource}].[{connSb.InitialCatalog}] database.");
		}
#endif

		#endregion
	}
}
