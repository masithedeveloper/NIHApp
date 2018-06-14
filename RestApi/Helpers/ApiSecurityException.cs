using System;
using System.Web;

namespace NIHApp.RestApi.Helpers
{
	public class ApiSecurityException : Exception
	{
		public ApiSecurityException() : base("Invalid call context. Authentication is required for this service.")
		{
			HttpContext.Current.Response.Headers.Add(NIHAppHeader.SessionValidHeader, "0");
		}
	}
}