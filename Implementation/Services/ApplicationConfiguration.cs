using System;
using System.Configuration;
using NIHApp.Implementation.Interfaces;

namespace NIHApp.Implementation.Services
{
	public class ApplicationConfiguration : IApplicationConfiguration
	{
		public static string LoginSubscriptionNotActive = "Login failed. Your subscription is no longer active. Please contact support for further assistance!";

		public static string LoginNotActive = "Login failed. Your login has not been activated.  Please contact support for further assistance!";

		public static string LoginCredentialsNotActive = "Invalid credentials!";

		public static string LoginInvalidCredentials = "The email and password you entered did not match our records. Please double-check and try again.";

		public static int DefaultAccountLockoutPeriod = 10;
		public static int DefaultMaxLoginAttemptsBeforeLockout = 5;
		public static int MaxScheduledEmailFailureRequests = 5;
		public static string GeneralErrorMessage = "An unforeseen error occurred processing your request. Please retry your last action. If the error persists please contact support for assistance.";
		public static string RegisterEmailDuplicate = "The email address specified has already been registered.";
		public static string RegisterEmailInvalid = "The email address specified is invalid.";
		public static string RegisterEmailNotVerified = "The email address specified has not been Verified.";

		public string GetSetting(string key)
		{
			if (ConfigurationManager.AppSettings[key] == null)
				throw new Exception($"Please set configuration key {key}. Key not found");
			return ConfigurationManager.AppSettings[key];
		}

		public string LocalTimeZone => "120";
		//public int MaxScheduledEmailFailureRequests => 5;


		public long MaxAllowedCallFailures
		{
			get
			{
				try
				{
					return long.Parse(GetSetting("max_allowed_call_failures"));
				}
				catch
				{
					return 5;
				}
			}
		}
	}
}