using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using NIHApp.Implementation.Helpers;

namespace NIHApp.Implementation.Extentions
{
	public static class StringExtentions
	{
		public static string UrlEncode(this string value)
		{
			return HttpUtility.UrlEncode(value);
		}

		public static string ReplaceBlankWith(this string value, string replace)
		{
			return string.IsNullOrEmpty(value) ? replace : value;
		}

		public static string Default(this string value)
		{
			return string.IsNullOrEmpty(value) ? "" : value;
		}

		public static string CleanForQuery(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			return value.Replace("'", "''");
		}

		public static string ToHash(this string value)
		{
			return MD5Hasher.GetMd5Hash(value);
		}

		public static string ToSHA256Hash(this string value)
		{
			var sha256 = new SHA256Managed();
			var bytes = Encoding.UTF8.GetBytes(value);
			var hash = sha256.ComputeHash(bytes);
			return Convert.ToBase64String(hash);
		}

		public static string StripIllegalCharacters(this string value)
		{
			return value;
		}

		public static DateTime? ToGmtDateTime(this string value)
		{
			if (value == null)
				return null;
			if (value.Length != 14)
				return null;
			int n;
			if (int.TryParse(value, out n))
				return null;

			try
			{
				var year = int.Parse("20" + value[0] + value[1]);
				var month = int.Parse(value.Substring(2, 2));
				var day = int.Parse(value.Substring(4, 2));
				var hour = int.Parse(value.Substring(6, 2));
				var minute = int.Parse(value.Substring(8, 2));
				var second = int.Parse(value.Substring(10, 2));
				var result = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
				return result;
			}
			catch
			{
				return null;
			}
		}

		public static bool IsAbsoluteHttpUrl(this string value)
		{
			Uri result;
			var createResult = Uri.TryCreate(value, UriKind.Absolute, out result);
			return createResult && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
		}

		public static bool IsArchiveFile(this string value)
		{
			var loweredValue = value.ToLowerInvariant();
			return loweredValue.EndsWith(".zip") || loweredValue.EndsWith(".gzip") || loweredValue.EndsWith(".7z") ||
					 loweredValue.EndsWith(".tar") || loweredValue.EndsWith(".rar");
		}

		public static bool IsValidEmail(this string email)
		{
			const string pattern =
				@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
			var regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.IsMatch(email);
		}
	}
}