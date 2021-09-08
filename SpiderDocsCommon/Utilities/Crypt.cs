using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

//---------------------------------------------------------------------------------
namespace Spider.Security
{
	public enum CryptProvider
	{
		DES,
		TripleDES
	}

//---------------------------------------------------------------------------------
	/// <summary>
	/// <para>Generic string encryption class</para>
	/// </summary>
	public class Crypt
	{
		string _key;
		SymmetricAlgorithm _algorithm;

//---------------------------------------------------------------------------------
		/// <param name="key">Any string which is used as a password for encryption and decryption.</param>
		public Crypt(string key)
		{
			_key = key;
			_algorithm = new TripleDESCryptoServiceProvider();
			_algorithm.Mode = CipherMode.CBC;
		}

//---------------------------------------------------------------------------------
		public string Encrypt(string PlainText)
		{
			byte[] plainByte = ASCIIEncoding.ASCII.GetBytes(PlainText);
			byte[] keyByte = GetKey();

			_algorithm.Key = keyByte;
			SetIV();

			ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();
			MemoryStream _memoryStream = new MemoryStream();
			CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

			_cryptoStream.Write(plainByte, 0, plainByte.Length);
			_cryptoStream.FlushFinalBlock();

			byte[] cryptoByte = _memoryStream.ToArray();

			return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
		}

//---------------------------------------------------------------------------------
		public string Decrypt(string EncryptedText)
		{
			string ans = null;
			
			byte[] cryptoByte = Convert.FromBase64String(EncryptedText);
			byte[] keyByte = GetKey();

			_algorithm.Key = keyByte;
			SetIV();
			ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();

			try
			{
				MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
				CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);
				StreamReader _streamReader = new StreamReader(_cryptoStream);

				ans = _streamReader.ReadToEnd();
			}
			catch{}

			return ans;
		}

//---------------------------------------------------------------------------------
		byte[] GetKey()
		{
			string salt = string.Empty;

			if(_algorithm.LegalKeySizes.Length > 0)
			{
				int keySize = _key.Length * 8;
				int minSize = _algorithm.LegalKeySizes[0].MinSize;
				int maxSize = _algorithm.LegalKeySizes[0].MaxSize;
				int skipSize = _algorithm.LegalKeySizes[0].SkipSize;

				if(keySize > maxSize)
				{
					_key = _key.Substring(0, maxSize / 8);

				}else if(keySize < maxSize)
				{
					int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;

					if(keySize < validSize)
						_key = _key.PadRight(validSize / 8, '*');
				}
			}

			PasswordDeriveBytes key = new PasswordDeriveBytes(_key, ASCIIEncoding.ASCII.GetBytes(salt));

			return key.GetBytes(_key.Length);
		}

//---------------------------------------------------------------------------------
		void SetIV()
		{
		   _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
		}

//---------------------------------------------------------------------------------
	}
}

