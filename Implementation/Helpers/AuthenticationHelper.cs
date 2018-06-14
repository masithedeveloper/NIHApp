using NIHApp.Implementation.Extentions;

namespace NIHApp.Implementation.Helpers
{
	public class AuthenticationHelper
	{
		public static string GetPasswordHash(string email, string plainTextPassword)
		{
			const string passwordSalt = "hmmmmm...th1s 1s very s@lty. I love AppointMate."; // Masi needs to test this
			var passwordToHash = passwordSalt + email.ToLower() + plainTextPassword;
			return passwordToHash.ToHash();
		}
	}
}
