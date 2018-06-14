using System;
using System.Security.Cryptography;
using System.Text;

namespace NIHApp.Implementation.Helpers
{
	public class MD5Hasher
	{
		// Hash an input string and return the hash as
		// a 32 character hexadecimal string.
		public static string GetMd5Hash(string input)
		{
			// Create a new instance of the MD5CryptoServiceProvider object.
			var md5Hasher = MD5.Create();

			// Convert the input string to a byte array and compute the hash.
			var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			var sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (var i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		// Verify a hash against a string.
		public static bool VerifyMd5Hash(string input, string hash)
		{
			// Hash the input.
			var hashOfInput = GetMd5Hash(input);

			// Create a StringComparer an compare the hashes.
			var comparer = StringComparer.OrdinalIgnoreCase;

			if (0 == comparer.Compare(hashOfInput, hash))
			{
				return true;
			}
			return false;
		}
	}
}