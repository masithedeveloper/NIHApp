using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Interfaces
{
	public interface IAuthenticationService
	{
		long Validate(string email, string password);
		Person ValidateForPasswordChange(string email);
	}
}