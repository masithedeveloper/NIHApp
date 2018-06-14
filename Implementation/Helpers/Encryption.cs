using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NIHApp.Implementation.Helpers
{
	public class Encryption
	{
		private Encryption()
		{
		}

		public static string DesEncrypt(string originalString)
		{
			var key1 = Encoding.ASCII.GetBytes("TheHouse");
			var key2 = Encoding.ASCII.GetBytes("TheHouse");
			if (string.IsNullOrEmpty(originalString))
				return string.Empty;

			DESCryptoServiceProvider cryptoProvider;
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			StreamWriter writer = null;
			try
			{
				cryptoProvider = new DESCryptoServiceProvider();
				memoryStream = new MemoryStream();
				cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(key1, key2), CryptoStreamMode.Write);
				writer = new StreamWriter(cryptoStream);
				writer.Write(originalString);
				writer.Flush();
				cryptoStream.FlushFinalBlock();
				writer.Flush();
				return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			finally
			{
				if (writer != null)
				{
					writer.Close();
					writer.Dispose();
				}
				if (cryptoStream != null)
				{
					cryptoStream.Close();
					cryptoStream.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
					memoryStream.Dispose();
				}
			}
		}

		public static string DesDecrypt(string cryptedString)
		{
			var key1 = Encoding.ASCII.GetBytes("TheHouse");
			var key2 = Encoding.ASCII.GetBytes("TheHouse");
			if (string.IsNullOrEmpty(cryptedString))
				return string.Empty;

			DESCryptoServiceProvider cryptoProvider;
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			StreamReader reader = null;
			try
			{
				cryptoProvider = new DESCryptoServiceProvider();
				memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
				cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(key1, key2), CryptoStreamMode.Read);
				reader = new StreamReader(cryptoStream);
				return reader.ReadToEnd();
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
					reader.Dispose();
				}
				if (cryptoStream != null)
				{
					cryptoStream.Close();
					cryptoStream.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
					memoryStream.Dispose();
				}
			}
		}

		public static string Encrypt(QueryString queryString)
		{
			var result = string.Empty;
			string nm;
			string val;
			foreach (string name in queryString)
			{
				nm = name;
				val = queryString[name];
				result += nm + "=" + val + "|";
			}
			return DesEncrypt(result);
			//return HttpUtility.UrlEncode(DesEncrypt(result));
		}

		public static QueryString Decrypt(string value)
		{
			var result = new QueryString();
			//value = DesDecrypt(HttpUtility.UrlDecode(value).Replace(" ", "+"));
			value = DesDecrypt(value.Replace(" ", "+"));
			var parts = value.Split(char.Parse("|"));
			foreach (var part in parts)
			{
				if (part == string.Empty)
					continue;
				var items = part.Split(char.Parse("="));
				result.Add(items[0], items[1]);
			}
			return result;
		}

		public static string DeHex(string hexstring)
		{
			var sb = new StringBuilder(hexstring.Length / 2);
			for (var i = 0; i <= hexstring.Length - 1; i = i + 2)
			{
				sb.Append((char)int.Parse(hexstring.Substring(i, 2), NumberStyles.HexNumber));
			}
			return sb.ToString();
		}

		public static string Hex(string sData)
		{
			string temp;
			var sb = new StringBuilder(sData.Length * 2);
			for (var i = 0; i < sData.Length; i++)
			{
				if (sData.Length - (i + 1) > 0)
				{
					temp = sData.Substring(i, 2);
					if ((temp != @"\n") && (temp == @"\b") && (temp == @"\r") && (temp == @"\c") && (temp == @"\\") && (temp == @"\0") && (temp == @"\t"))
					{
						sb.Append(string.Format("{0:X2}", (int)sData.ToCharArray()[i]));
						i--;
					}
				}
				else
				{
					sb.Append(string.Format("{0:X2}", (int)sData.ToCharArray()[i]));
				}
				i++;
			}
			return sb.ToString();
		}

		public static string GenerateEncryption(string email, DateTime linkdate)
		{
			var holder = email + "$" + linkdate;
			return DesEncrypt(holder);
		}

		public static string GenerateDecryption(string userkey)
		{
			return DesDecrypt(userkey);
		}
	}
}