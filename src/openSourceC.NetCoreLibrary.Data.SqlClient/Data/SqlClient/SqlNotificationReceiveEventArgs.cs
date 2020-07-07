using System;

namespace openSourceC.NetCoreLibrary.Data.SqlClient
{
	/// <summary>
	///		Represents the method that will handle a service broker receive event when the event
	///		provides data.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void SqlNotificationReceiveEventHandler(object sender, SqlNotificationReceiveEventArgs e);

	/// <summary>
	///		Summary description for SqlReceiveEventArgs.
	/// </summary>
	[Serializable]
	public class SqlNotificationReceiveEventArgs : EventArgs
	{
		/// <summary></summary>
		public byte Status { get; set; }

		/// <summary></summary>
		public byte Priority { get; set; }

		/// <summary></summary>
		public long QueuingOrder { get; set; }

		/// <summary></summary>
		public Guid ConversationGroupId { get; set; }

		/// <summary></summary>
		public Guid ConversationHandle { get; set; }

		/// <summary></summary>
		public long MessageSequenceNumber { get; set; }

		/// <summary></summary>
		public string ServiceName { get; set; }

		/// <summary></summary>
		public int ServiceId { get; set; }

		/// <summary></summary>
		public string ServiceContractName { get; set; }

		/// <summary></summary>
		public int ServiceContractId { get; set; }

		/// <summary></summary>
		public string MessageTypeName { get; set; }

		/// <summary></summary>
		public int MessageTypeId { get; set; }

		/// <summary></summary>
		public string Validation { get; set; }

		/// <summary></summary>
		public string MessageBody { get; set; }
	}
}
