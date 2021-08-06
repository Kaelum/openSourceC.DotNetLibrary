using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace openSourceC.DotNetLibrary
{
	/// <summary>
	///		Summary description for CryptoStreamWriteDelegate.
	/// </summary>
	/// <param name="cryptoStream">The stream on which to write.</param>
	public delegate void CryptoStreamWriteDelegate(CryptoStream cryptoStream);

	/// <summary>
	///		Summary description for Encryption.
	/// </summary>
	public static class Encryption
	{
		#region Constructors

		/// <summary>
		///		Class constructor.
		/// </summary>
		static Encryption()
		{
			byte[] activationKey = {
					0x2A, 0x22, 0xB2, 0x21, 0xC2, 0xA4, 0x47, 0xCF, 0x9E, 0x18, 0xD6, 0xDF, 0x6E, 0xC3, 0xFE, 0xCD,
					0x43, 0x7D, 0xDC, 0xD5, 0xB8, 0xEE, 0x46, 0x2B, 0x93, 0x23, 0x7C, 0x62, 0x32, 0x14, 0x4A, 0xC2
				};
			byte[] activationIV = {
					0xB1, 0x63, 0x96, 0x17, 0x59, 0x15, 0x40, 0x39, 0x84, 0xE7, 0x8F, 0x14, 0x1D, 0x4A, 0x66, 0xB5
				};

			const string testString = "This is the end of the world.";

			string encrypted;
			string decrypted;


			try
			{
				// First encryption/decryption is always bad.  This is a workaround for a .NET framework bug.
				encrypted = EncryptToBase64String(testString, activationKey, activationIV);
				decrypted = DecryptFromBase64String(encrypted, activationKey, activationIV);

				// If the bug exists, the exception will be thrown.
				if (!testString.Equals(decrypted))
				{
					throw new ApplicationException(".NET Framework encryption bug detected and handled.");
				}
			}
#if DIAGNOSTICS
			catch (ApplicationException ex)
			{
				// The bug has been detected and handled.
				Debug.WriteLine(ex);
			}
#else
			catch (ApplicationException) { }
#endif
			catch (OscException) { }
#if DIAGNOSTICS
			catch (Exception ex)
			{
				// An unexpected exception has occurred.
				Debug.WriteLine(Format.Exception(ex, "An unexpected exception has occurred."));
			}
#endif
		}

		#endregion

		#region Decrypt

		/// <summary>
		///		Decrypts the data written to the <see cref="CryptoStream"/> within the delegate
		///		using the specified key and initialization vector.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <param name="cryptoStreamWriteDelegate">The delegate that writes to the
		///		<see cref="CryptoStream"/>.</param>
		/// <returns>
		///		A byte array with the decrypted data.
		///	</returns>
		public static byte[] Decrypt<TSymmetricAlgorithm>(byte[] key, byte[] iv, CryptoStreamWriteDelegate cryptoStreamWriteDelegate)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				byte[]? buffer = null;

				Decrypt<TSymmetricAlgorithm>(key, iv, cryptoStreamWriteDelegate, (memoryStream) =>
				{
					buffer = memoryStream.ToArray();
				});

				return buffer ?? throw new OscErrorException("buffer is null.");
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		/// <summary>
		///		Decrypts the data written to the <see cref="CryptoStream"/> within the delegate
		///		using the specified key and initialization vector.  The decrypted data is written to
		///		the specified stream.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="stream">The stream on which to write the decrypted data.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <param name="cryptoStreamWriteDelegate">The delegate that writes to the
		///		<see cref="CryptoStream"/>.</param>
		public static void Decrypt<TSymmetricAlgorithm>(Stream stream, byte[] key, byte[] iv, CryptoStreamWriteDelegate cryptoStreamWriteDelegate)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				Decrypt<TSymmetricAlgorithm>(key, iv, cryptoStreamWriteDelegate, (memoryStream) =>
				{
					memoryStream.WriteTo(stream);
				});
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		/// <summary>
		///		Decrypts the specified base 64 encoded string with the specified key and
		///		initialization vector and returns the decrypted data as a string.
		/// </summary>
		/// <param name="buffer">The string to decrypt.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <returns>
		///		Decrypted string.
		///	</returns>
		public static string DecryptFromBase64String(string buffer, byte[] key, byte[] iv)
		{
			// RijndaelManaged
			// DESCryptoServiceProvider
			// RC2CryptoServiceProvider
			// TripleDESCryptoServiceProvider
			return Encoding.UTF8.GetString(DecryptFromBase64String<RijndaelManaged>(buffer, key, iv));
		}

		/// <summary>
		///		Decrypts the specified base 64 encoded string using the specified key and
		///		initialization vector.  The decrypted data is written to the specified stream
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="stream">The stream on which to write the decrypted data.</param>
		/// <param name="buffer">The string to decrypt.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		public static void DecryptFromBase64String<TSymmetricAlgorithm>(Stream stream, string buffer, byte[] key, byte[] iv)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				int mod = buffer.Length % 4;
				byte[] byteArray = Convert.FromBase64String(buffer.PadRight(buffer.Length + (mod == 0 ? 0 : 4 - mod), '='));

				Decrypt<TSymmetricAlgorithm>(stream, key, iv,
					delegate (CryptoStream cryptoStream)
					{
						cryptoStream.Write(byteArray, 0, byteArray.Length);
					}
				);
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		/// <summary>
		///		Decrypts the specified base 64 encoded string using the specified key and
		///		initialization vector.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="buffer">The string to decrypt.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <returns>
		///		A byte array with the decrypted data.
		///	</returns>
		public static byte[] DecryptFromBase64String<TSymmetricAlgorithm>(string buffer, byte[] key, byte[] iv)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				int mod = buffer.Length % 4;
				byte[] byteArray = Convert.FromBase64String(buffer.PadRight(buffer.Length + (mod == 0 ? 0 : 4 - mod), '='));

				return Decrypt<TSymmetricAlgorithm>(key, iv,
					delegate (CryptoStream cryptoStream)
					{
						cryptoStream.Write(byteArray, 0, byteArray.Length);
					}
				);
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		#endregion

		#region Encrypt

		/// <summary>
		///		Encrypts the data written to the <see cref="CryptoStream"/> within the delegate
		///		using the specified key and initialization vector.  If the specified <see cref="Stream"/>
		///		is not null, the encrypted data is written to it.  If it is null, a byte array
		///		is returned.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <param name="cryptoStreamWriteDelegate">The delegate that writes to the
		///		<see cref="CryptoStream"/>.</param>
		/// <returns>
		///		If the specified <see cref="Stream"/> is null, a byte array with the encrypted
		///		data; otherwise, null.
		///	</returns>
		public static byte[] Encrypt<TSymmetricAlgorithm>(byte[] key, byte[] iv, CryptoStreamWriteDelegate cryptoStreamWriteDelegate)
				where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				byte[]? buffer = null;

				Encrypt<TSymmetricAlgorithm>(key, iv, cryptoStreamWriteDelegate, (memoryStream) =>
				{
					buffer = memoryStream.ToArray();
				});

				return buffer ?? throw new OscErrorException("buffer is null.");
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		/// <summary>
		///		Encrypts the data written to the <see cref="CryptoStream"/> within the delegate
		///		using the specified key and initialization vector.  If the specified <see cref="Stream"/>
		///		is not null, the encrypted data is written to it.  If it is null, a byte array
		///		is returned.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="stream">The stream on which to write the encrypted data.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <param name="cryptoStreamWriteDelegate">The delegate that writes to the
		///		<see cref="CryptoStream"/>.</param>
		/// <returns>
		///		If the specified <see cref="Stream"/> is null, a byte array with the encrypted
		///		data; otherwise, null.
		///	</returns>
		public static void Encrypt<TSymmetricAlgorithm>(Stream stream, byte[] key, byte[] iv, CryptoStreamWriteDelegate cryptoStreamWriteDelegate)
				where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				Encrypt<TSymmetricAlgorithm>(key, iv, cryptoStreamWriteDelegate, (memoryStream) =>
				{
					memoryStream.WriteTo(stream);
				});
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		/// <summary>
		///		Encrypts the specified string using the specified key and initialization vector.
		///		The encrypted data is written to the specified stream.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="stream">The stream on which to write the encrypted data.</param>
		/// <param name="buffer">The string to encrypt.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		public static void Encrypt<TSymmetricAlgorithm>(Stream stream, string buffer, byte[] key, byte[] iv)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				Encrypt<TSymmetricAlgorithm>(stream, key, iv,
					delegate (CryptoStream cryptoStream)
					{
						WriteStringToCryptoStream(cryptoStream, buffer);
					}
				);
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		/// <summary>
		///		Encrypts the specified string using the specified key and initialization vector.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="buffer">The string to encrypt.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <returns>
		///		A byte array with the encrypted data.
		///	</returns>
		public static byte[]? Encrypt<TSymmetricAlgorithm>(string buffer, byte[] key, byte[] iv)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				return Encrypt<TSymmetricAlgorithm>(key, iv,
					delegate (CryptoStream cryptoStream)
					{
						WriteStringToCryptoStream(cryptoStream, buffer);
					}
				);
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		/// <summary>
		///		Encrypts the specified string with the specified key and initialization vector and
		///		returns a base 64 encoded string.
		/// </summary>
		/// <param name="buffer">The string to encrypt.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <returns>Encrypted string.</returns>
		public static string EncryptToBase64String(string buffer, byte[] key, byte[] iv)
		{
			// RijndaelManaged
			// DESCryptoServiceProvider
			// RC2CryptoServiceProvider
			// TripleDESCryptoServiceProvider
			return EncryptToBase64String<RijndaelManaged>(buffer, key, iv);
		}

		/// <summary>
		///		Encrypts the specified string with the specified key and initialization vector and
		///		returns a base 64 encoded string.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="buffer">The string to encrypt.</param>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <returns>Encrypted string.</returns>
		public static string EncryptToBase64String<TSymmetricAlgorithm>(string buffer, byte[] key, byte[] iv)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				return EncryptToBase64String<TSymmetricAlgorithm>(key, iv,
					delegate (CryptoStream cryptoStream)
					{
						WriteStringToCryptoStream(cryptoStream, buffer);
					}
				);
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		/// <summary>
		///		Encrypts the data written to the <see cref="CryptoStream"/> within the delegate
		///		using the specified key and initialization vector and returns a base 64 encoded
		///		string.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <param name="cryptoStreamWriteDelegate">The delegate that writes to the
		///		<see cref="CryptoStream"/>.</param>
		/// <returns>
		///		Encrypted string.
		///	</returns>
		public static string EncryptToBase64String<TSymmetricAlgorithm>(byte[] key, byte[] iv, CryptoStreamWriteDelegate cryptoStreamWriteDelegate)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			try
			{
				byte[] buffer = Encrypt<TSymmetricAlgorithm>(key, iv, cryptoStreamWriteDelegate);
				return Convert.ToBase64String(buffer).TrimEnd("=".ToCharArray());
			}
			catch (OscException) { throw; }
			catch (Exception ex)
			{
				throw new OscErrorException("An unexpected exception has occurred.", ex);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		///		Decrypts the data written to the <see cref="CryptoStream"/> within the delegate
		///		using the specified key and initialization vector.  If the specified <see cref="Stream"/>
		///		is not null, the decrypted data is written to it.  If it is null, a byte array
		///		is returned.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <param name="cryptoStreamWriteDelegate">The delegate that writes to the
		///		<see cref="CryptoStream"/>.</param>
		/// <param name="action">The action to perform on the decrypted stream.</param>
		public static void Decrypt<TSymmetricAlgorithm>(byte[] key, byte[] iv, CryptoStreamWriteDelegate cryptoStreamWriteDelegate, Action<MemoryStream> action)
			where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			using (SymmetricAlgorithm algorithm = new TSymmetricAlgorithm())
			using (ICryptoTransform cryptoTransform = algorithm.CreateDecryptor(key, iv))
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
			{
				cryptoStreamWriteDelegate(cryptoStream);

				cryptoStream.FlushFinalBlock();
				algorithm.Clear();

				action(memoryStream);
			}
		}

		/// <summary>
		///		Encrypts the data written to the <see cref="CryptoStream"/> within the delegate
		///		using the specified key and initialization vector.  If the specified <see cref="Stream"/>
		///		is not null, the encrypted data is written to it.  If it is null, a byte array
		///		is returned.
		/// </summary>
		/// <typeparam name="TSymmetricAlgorithm">The type of the cryptographic object used to
		///		perform the symmetric algorithm.</typeparam>
		/// <param name="key">The secret key.</param>
		/// <param name="iv">The initialization vector.</param>
		/// <param name="cryptoStreamWriteDelegate">The delegate that writes to the
		///		<see cref="CryptoStream"/>.</param>
		/// <param name="action">The action to perform on the encrypted stream.</param>
		private static void Encrypt<TSymmetricAlgorithm>(byte[] key, byte[] iv, CryptoStreamWriteDelegate cryptoStreamWriteDelegate, Action<MemoryStream> action)
				where TSymmetricAlgorithm : SymmetricAlgorithm, new()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			using (SymmetricAlgorithm algorithm = new TSymmetricAlgorithm())
			using (ICryptoTransform cryptoTransform = algorithm.CreateEncryptor(key, iv))
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
			{
				cryptoStreamWriteDelegate(cryptoStream);

				cryptoStream.FlushFinalBlock();
				algorithm.Clear();

				action(memoryStream);
			}
		}

		private static void WriteStringToCryptoStream(CryptoStream cryptoStream, string buffer)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(buffer);
			cryptoStream.Write(byteArray, 0, byteArray.Length);
		}

		#endregion
	}
}
