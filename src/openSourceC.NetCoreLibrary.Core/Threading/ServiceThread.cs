using System;
using System.Threading;

namespace openSourceC.NetCoreLibrary.Threading
{
	/// <summary>
	///		Summary description for ServiceThread.
	/// </summary>
	public class ServiceThread : IComparable, IComparable<ServiceThread>, IEquatable<ServiceThread>
	{
		private readonly Guid _instanceId;
		private readonly IService _service;
		private readonly Thread _thread;


		#region Constructors

		/// <summary>
		///		Create an instance of ServiceThread.
		/// </summary>
		/// <param name="service">The <see cref="T:IService"/> object.</param>
		/// <param name="thread">The thread executing the <see cref="T:IService"/>.</param>
		public ServiceThread(IService service, Thread thread)
		{
			_instanceId = Guid.NewGuid();
			_service = service ?? throw new ArgumentNullException(nameof(service));
			_thread = thread ?? throw new ArgumentNullException(nameof(thread));
		}

		#endregion

		#region Public Properties

		/// <summary>Gets the unique identifier for this instance..</summary>
		public Guid InstanceId => _instanceId;

		/// <summary>Gets the <see cref="T:IService"/> object.</summary>
		public IService Service => _service;

		/// <summary>Gets the thread executing the <see cref="T:IService"/>.</summary>
		public Thread Thread => _thread;

		#endregion

		#region IComparable Implementation

		/// <summary>
		///		Compares the current instance with another object of the same type and returns an
		///		integer that indicates whether the current instance precedes, follows, or occurs in
		///		the same position in the sort order as the other object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int CompareTo(object? obj)
		{
			if (obj == null)
			{
				return 1;
			}

			if (obj is ServiceThread serviceThread)
			{
				return CompareTo(serviceThread);
			}

			throw new ArgumentException($"Argument must be an {nameof(ServiceThread)}.");
		}

		#endregion

		#region IComparable<ServiceThread> Implementation

		/// <summary>
		///		Compares the current instance with another object of the same type and returns an
		///		integer that indicates whether the current instance precedes, follows, or occurs in
		///		the same position in the sort order as the other object.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(ServiceThread? other)
			=> Compare(this, other);

		#endregion

		#region IEquatable<ServiceThread> Implementation

		/// <summary>
		///		Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(ServiceThread? other)
			=> Compare(this, other) == 0;

		#endregion

		#region Operators

		/// <summary>
		///		Define the is equal to operator.
		/// </summary>
		/// <param name="operand1"></param>
		/// <param name="operand2"></param>
		/// <returns></returns>
		public static bool operator ==(ServiceThread? operand1, ServiceThread? operand2)
			=> Compare(operand1, operand2) == 0;

		/// <summary>
		///		Define the is equal to operator.
		/// </summary>
		/// <param name="operand1"></param>
		/// <param name="operand2"></param>
		/// <returns></returns>
		public static bool operator !=(ServiceThread? operand1, ServiceThread? operand2)
			=> Compare(operand1, operand2) != 0;

		/// <summary>
		///		Define the is greater than operator.
		/// </summary>
		/// <param name="operand1"></param>
		/// <param name="operand2"></param>
		/// <returns></returns>
		public static bool operator >(ServiceThread? operand1, ServiceThread? operand2)
			=> Compare(operand1, operand2) > 0;

		/// <summary>
		///		Define the is less than operator.
		/// </summary>
		/// <param name="operand1"></param>
		/// <param name="operand2"></param>
		/// <returns></returns>
		public static bool operator <(ServiceThread? operand1, ServiceThread? operand2)
			=> Compare(operand1, operand2) < 0;

		/// <summary>
		///		Define the is greater than or equal to operator.
		/// </summary>
		/// <param name="operand1"></param>
		/// <param name="operand2"></param>
		/// <returns></returns>
		public static bool operator >=(ServiceThread? operand1, ServiceThread? operand2)
			=> Compare(operand1, operand2) >= 0;

		/// <summary>
		///		Define the is less than or equal to operator.
		/// </summary>
		/// <param name="operand1"></param>
		/// <param name="operand2"></param>
		/// <returns></returns>
		public static bool operator <=(ServiceThread? operand1, ServiceThread? operand2)
			=> Compare(operand1, operand2) <= 0;

		#endregion

		#region DefaultComparer

		/// <summary>
		///		Compares two objects and returns a value indicating whether one is less than, equal
		///		to, or greater than the other.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns></returns>
		public static int Compare(ServiceThread? x, ServiceThread? y)
		{
			if (x is null & y is null)
			{
				return 0;
			}
			else if (x is null)
			{
				return int.MinValue;
			}
			else if (y is null)
			{
				return int.MaxValue;
			}

			return x.CompareTo(y);
		}

		#endregion

		#region Overrides

		/// <summary>
		///		Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object? obj)
		{
			if (obj is ServiceThread serviceThread)
			{
				return (Compare(this, serviceThread) == 0);
			}

			return false;
		}

		/// <summary>
		///		Returns the hash code for this credential.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _instanceId.GetHashCode();
		}

		#endregion
	}
}
