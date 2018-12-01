using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Permissions;

namespace openSourceC.StandardLibrary.Data.SqlClient
{
	/// <summary>
	///		Summary description for SqlDbCommand.
	/// </summary>
	public sealed class SqlDbCommand : DbFactoryCommand<SqlDbFactory, SqlDbCommand, SqlDbParams, SqlConnection, SqlTransaction, SqlCommand, SqlParameter, SqlDataAdapter, SqlDataReader>
	{
#if SUPPORT_ASYNC_EXECUTE_READER

		#region BeginExecuteReader

		/// <summary>
		///		Initiates the asynchronous execution of the Transact-SQL statement or stored
		///		procedure that is described by this <see cref="T:SqlCommand" />, and retrieves one
		///		or more result sets from the server.
		///	</summary>
		/// <returns>
		///		An <see cref="T:IAsyncResult" /> that can be used to poll or wait for results, or
		///		both; this value is also needed when invoking
		///		<see cref="M:SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a
		///		<see cref="T:SqlDataReader" /> instance that can be used to retrieve the returned
		///		rows.
		///	</returns>
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader() =>
			BeginExecuteReader(null, null, CommandBehavior.Default);

		/// <summary>
		///		Initiates the asynchronous execution of the Transact-SQL statement or stored
		///		procedure that is described by this <see cref="T:SqlCommand"/> and retrieves one or
		///		more result sets from the server, given a callback procedure and state information.
		/// </summary>
		/// <param name="callback">An <see cref="T:AsyncCallback"/> delegate that is invoked when
		///		the command's execution has completed. Pass <b>null</b> (<b>Nothing</b> in Microsoft
		///		Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback
		///		procedure. Retrieve this object from within the callback procedure using the
		///		<see cref="P:AsyncState"/> property.</param>
		/// <returns>
		///		An <see cref="T:IAsyncResult"/> that can be used to poll or wait for results, or
		///		both; this value is also needed when invoking <see cref="T:EndExecuteReader"/>,
		///		which returns a <see cref="T:SqlDataReader"/> instance which can be used to retrieve
		///		the returned rows.
		/// </returns>
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject) =>
			BeginExecuteReader(callback, stateObject, CommandBehavior.Default);

		/// <summary>
		///		Initiates the asynchronous execution of the Transact-SQL statement or stored
		///		procedure that is described by this <see cref="T:SqlCommand"/>, using one of the
		///		<see cref="T:CommandBehavior"/> values, and retrieving one or more result sets from
		///		the server, given a callback procedure and state information.
		/// </summary>
		/// <param name="callback">An <see cref="T:AsyncCallback"/> delegate that is invoked when
		///		the command's execution has completed. Pass <b>null</b> (<b>Nothing</b> in Microsoft
		///		Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback
		///		procedure. Retrieve this object from within the callback procedure using the
		///		<see cref="P:AsyncState"/> property.</param>
		/// <param name="behavior">One of the <see cref="T:CommandBehavior"/> values, indicating
		///		options for statement execution and data retrieval.</param>
		/// <returns>
		///		An <see cref="T:IAsyncResult"/> that can be used to poll or wait for results, or
		///		both; this value is also needed when invoking <see cref="T:EndExecuteReader"/>,
		///		which returns a <see cref="T:SqlDataReader"/> instance which can be used to retrieve
		///		the returned rows.
		/// </returns>
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior)
		{
			bool isClosed = (Command.Connection.State == ConnectionState.Closed);

			if (isClosed)
			{
				Command.Connection.Open();
			}

			EnsureTransaction();

			try
			{
				if (AutoPrepare)
				{
					Command.Prepare();
				}

				return Command.BeginExecuteReader(callback, stateObject, behavior);
			}
			finally
			{
				if (isClosed)
				{
					//_cmd.Connection.Close();
				}
			}
		}

		/// <summary>
		///		Initiates the asynchronous execution of the Transact-SQL statement or stored
		///		procedure that is described by this <see cref="T:SqlCommand"/> using one of the
		///		<see cref="T:CommandBehavior"/> values.
		/// </summary>
		/// <param name="behavior">One of the <see cref="T:CommandBehavior"/> values, indicating
		///		options for statement execution and data retrieval.</param>
		/// <returns>
		///		An <see cref="T:IAsyncResult" /> that can be used to poll, wait for results, or
		///		both; this value is also needed when invoking
		///		<see cref="M:SqlCommand.EndExecuteReader(IAsyncResult)" />, which returns a
		///		<see cref="T:SqlDataReader" /> instance that can be used to retrieve the returned
		///		rows.
		/// </returns>
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(CommandBehavior behavior) =>
			BeginExecuteReader(null, null, behavior);

		#endregion

		#region EndExecuteReader

		/// <summary>
		///		Finishes asynchronous execution of a Transact-SQL statement, returning the requested
		///		<see cref="T:SqlDataReader"/>.</summary>
		/// <param name="asyncResult">The <see cref="T:IAsyncResult"/> returned by the call to
		///		<see cref="M:SqlCommand.BeginExecuteReader"/>.</param>
		/// <returns>
		///		A <see cref="T:SqlDataReader"/> object that can be used to retrieve the requested
		///		rows.
		///	</returns>
		public SqlDataReader EndExecuteReader(IAsyncResult asyncResult)
		{
			return Command.EndExecuteReader(asyncResult);
		}

		#endregion

#endif

		#region SqlDependency

		/// <summary>
		///		Register a <see cref="T:SqlDependency"/> using the current command object along with
		///		the specified event handler, options, and timeout.
		/// </summary>
		/// <param name="eventHandler">The event handler for the OnChange event.</param>
		public void RegisterDependency(OnChangeEventHandler eventHandler)
		{
			RegisterDependency(eventHandler, null, 0);
		}

		/// <summary>
		///		Register a <see cref="T:SqlDependency"/> using the current command object along with
		///		the specified event handler, options, and timeout.
		/// </summary>
		/// <remarks>
		///		The value of the <see cref="P:SqlNotificationRequest.Options"/> parameter has the
		///		following format:
		///
		///		<code>service=&lt;service-name&gt;{;(local database=&lt;database&gt;|broker instance =&lt;broker instance&gt;)}</code>
		///
		///		For example, if you use the service "myservice" in the database "AdventureWorks" the
		///		format is:
		///
		///		<code>service=myservice;local database = AdventureWorks</code>
		///
		///		The SQL Server Service Broker service must be previously configured on the server.
		///		In addition, a Service Broker service and queue must be defined and security access
		///		granted as needed.  See the SQL Server 2005 documentation for more information.
		/// </remarks>
		/// <param name="eventHandler">The event handler for the OnChange event.</param>
		/// <param name="options">The notification request options to be used by this dependency.
		///		<b>null</b> to use the default service.</param>
		/// <param name="timeout">The time-out for this notification in seconds. The default is 0,
		///		indicating that the server's time-out should be used.</param>
		[SqlClientPermission(SecurityAction.Demand, Unrestricted = true)]
		public void RegisterDependency(OnChangeEventHandler eventHandler, string options, int timeout)
		{
			SqlDependency dependency = new SqlDependency(Command, options, timeout);
			dependency.OnChange += eventHandler;
		}

		#endregion

		#region SqlNotificationRequest

		/// <summary>
		///		Clear the <see cref="P:SqlCommand.Notification"/> object.
		/// </summary>
		public void ClearNotification()
		{
			Command.Notification = null;
		}

		/// <summary>
		///		Register a new <see cref="P:SqlCommand.Notification"/> object.
		/// </summary>
		/// <param name="userData">A string that contains an application-specific identifier for
		///		this notification. It is not used by the notifications infrastructure, but it allows
		///		you to associate notifications with the application state. The value indicated in
		///		this parameter is included in the Service Broker queue message.</param>
		/// <param name="options">A string that contains the Service Broker service name where
		///		notification messages are posted. If the connection does not define the database,
		///		it must include a database name or a Service Broker instance GUID that restricts the
		///		scope of the service name lookup to a particular database.
		///		<para>For more information about the format of the <i>options parameter</i>, see
		///		<see cref="P:SqlNotificationRequest.Options"/>.</para></param>
		/// <param name="timeout">The time, in seconds, to wait for a notification message. 0 (zero)
		///		defaults to the value set on the server.</param>
		public void RegisterNotification(string userData, string options, int timeout)
		{
			Command.Notification = new SqlNotificationRequest(userData, options, timeout);
		}

		#endregion

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
		public override string ToString()
		{
			return SqlDbHelper.CommandToString(Command);
		}

		#endregion
	}
}
