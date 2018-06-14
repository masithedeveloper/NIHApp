using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace NIHApp.Implementation.Helpers
{
	public class QueryString : NameValueCollection
	{
		public QueryString()
		{
		}

		public QueryString(NameValueCollection clone)
			: base(clone)
		{
		}

		public string Document { get; private set; }

		public static QueryString FromCurrent()
		{
			return FromUrl(HttpContext.Current.Request.Url.AbsoluteUri);
		}

		public static QueryString FromUrl(string url)
		{
			// added per Richard's comment
			url = HttpUtility.UrlDecode(url);

			if (url != null)
			{
				var parts = url.Split("?".ToCharArray());
				var qs = new QueryString();
				qs.Document = parts[0];

				if (parts.Length == 1)
					return qs;

				var keys = parts[1].Split("&".ToCharArray());
				foreach (var key in keys)
				{
					var part = key.Split("=".ToCharArray());
					if (part.Length == 1)
						qs.Add(part[0], "");
					qs.Add(part[0], part[1]);
				}

				return qs;
			}

			return null;
		}

		public void ClearAllExcept(string except)
		{
			ClearAllExcept(new[] { except });
		}

		public void ClearAllExcept(string[] except)
		{
			var toRemove = new ArrayList();
			foreach (var s in AllKeys)
			{
				foreach (var e in except)
				{
					if (s.ToLower() == e.ToLower())
						if (!toRemove.Contains(s))
							toRemove.Add(s);
				}
			}

			foreach (string s in toRemove)
				Remove(s);
		}

		public override void Add(string name, string value)
		{
			if (this[name] != null)
				this[name] = value;
			else
				base.Add(name, value);
		}

		public override string ToString()
		{
			return ToString(false);
		}

		public string ToString(bool includeUrl)
		{
			var parts = new string[Count];
			var keys = AllKeys;
			for (var i = 0; i < keys.Length; i++)
				parts[i] = keys[i] + "=" + this[keys[i]];
			var url = string.Join("&", parts);
			url = HttpUtility.UrlEncode(url);
			if (url != null && !url.StartsWith("?"))
				url = "?" + url;
			if (includeUrl)
				url = Document + url;

			return url;
		}
	}
}