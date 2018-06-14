using System;

namespace NIHApp.Implementation.Helpers
{
	public class LoginFailureException : Exception
	{
		public LoginFailureException(string message) : base(message)
		{
		}
	}
}