using System;

namespace openSourceC.StandardLibrary
{
	/// <summary>
	///		A simple structure to wrap a class reference after ensuring that the reference is not null.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public struct NonNullable<T>
		where T : class, new()
	{
		private T _value;


		/// <summary>
		///		Check and wrap a value.
		///	</summary>
		/// <param name = "obj">The object to check and wrap.</param>
		/// <remarks>
		///		To pass a non null value use ?? operator.
		///	</remarks>
		/// <exception cref="System.ArgumentNullException">
		///		If obj is a null reference.
		///	</exception>
		public NonNullable(T obj)
		{
			_value = obj ?? throw new ArgumentNullException("obj", "The value is null");
		}

		/// <summary>Gets or sets the value of the wrapped object.</summary>
		public T Value
		{
			get { return _value; }

			set { _value = value ?? throw new NullValueException("The value is null"); }
		}

		/// <summary>
		///		Get a String representation of the wrapped value.
		/// </summary>
		/// <returns>
		///		The result of the wrapped value's ToString().
		/// </returns>
		public override string ToString()
		{
			return (_value.ToString());
		}

		/// <summary>
		///		Implicit wrapping of the value.
		/// </summary>
		/// <param name="obj">The object to check and wrap.</param>
		/// <returns>
		///		The wrapped value.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		///		If obj is a null reference.
		///	</exception>
		public static implicit operator NonNullable<T>(T obj)
		{
			return (new NonNullable<T>(obj));
		}

		/// <summary>
		///		Implicit unwrapping of the value.
		/// </summary>
		/// <param name="obj">The object to check and wrap.</param>
		/// <returns>
		///		The unwrapped value.
		/// </returns>
		public static implicit operator T(NonNullable<T> obj)
		{
			return (obj.Value);
		}
	}
}
