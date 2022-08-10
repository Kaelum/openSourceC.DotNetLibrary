using System;

namespace openSourceC.DotNetLibrary.Threading
{
	/// <summary>
	///		Summary description for RefreshableKeyValue.
	/// </summary>
	public class RefreshableKeyValue : DelayedCallback
	{
		#region Constructors

		/// <summary>
		///		Create an instance of RefreshableKeyValue.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="millisecondsDelay"></param>
		/// <param name="callback"></param>
		public RefreshableKeyValue(
			string key,
			string value,
			int millisecondsDelay,
			Action<object?> callback
		) : base(
			millisecondsDelay,
			callback
		)
		{
			Key = key;
			Value = value;
		}

		/// <summary>
		///		Create an instance of RefreshableKeyValue
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="delay"></param>
		/// <param name="callback"></param>
		public RefreshableKeyValue(
			string key,
			string value,
			TimeSpan delay,
			Action<object?> callback
		) : base(
			delay,
			callback
		)
		{
			Key = key;
			Value = value;
		}

		#endregion

		#region Public Properties

		/// <summary>Gets or sets the key.</summary>
		public string Key { get; private set; }

		/// <summary>Gets or sets the value.</summary>
		public string Value { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		///		Set (Thread Safe)
		/// </summary>
		/// <param name="value"></param>
		/// <param name="millisecondsDelay"></param>
		public void Set(string value, int millisecondsDelay)
		{
			base.Set(
				millisecondsDelay,
				() =>
				{
					Value = value;
				}
			);
		}

		/// <summary>
		///		Set (Thread Safe)
		/// </summary>
		/// <param name="value"></param>
		/// <param name="delay"></param>
		public void Set(string value, TimeSpan delay)
		{
			base.Set(
				delay,
				() =>
				{
					Value = value;
				}
			);
		}

		#endregion
	}
}
