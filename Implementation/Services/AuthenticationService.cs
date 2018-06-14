using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Extentions;
using NIHApp.Implementation.Helpers;
using NIHApp.Implementation.Interfaces;
using NIHApp.Infrastructure.Interfaces;

namespace NIHApp.Implementation.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IPersonRepository _personRepository;

		public AuthenticationService(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}

		public long Validate(string email, string password)
		{
			var login = _personRepository.FindPersonByEmail(email).FirstOrDefault();
			var standardException = new LoginFailureException(ApplicationConfiguration.LoginInvalidCredentials);
			if (email == "" && password == "")
			{
				throw new LoginFailureException("Please enter your email and password.");
			}
			if (email == "")
			{
				throw new LoginFailureException("Please enter your email.");
			}
			if (password == "")
			{
				throw new LoginFailureException("Please enter your password.");
			}

			if (login == null)
				throw standardException;

			if (!login.PerEmailVerified)
			{
				throw new LoginFailureException("Invalid Verification.");
			}

			if (!CredentialsValid(login, email, password))
			{
				CheckAndApplyLockout(login, true);
				throw standardException;
			}

			CheckAndApplyLockout(login, false);

			//if (login.Status == StatusType.Deleted || login.Status == StatusType.Inactive)
			//	if (email != null)
			//		throw standardException;

			return login.Id;
		}
        
		public Person ValidateForPasswordChange(string email)
		{
			var login = _personRepository.FindPersonByEmail(email).FirstOrDefault();
			if (login == null)
				throw new LoginFailureException(ApplicationConfiguration.LoginInvalidCredentials);
			return login;
		}

		private bool CredentialsValid(Person person, string email, string password)
		{
			if (person == null)
				return false;
			if (person.PerHashPassword == AuthenticationHelper.GetPasswordHash(email, password))
				return true;
			return false;
		}

		private void CheckAndApplyLockout(Person person, bool authenticationFailed)
		{
			person.CheckLockExpiration(ApplicationConfiguration.DefaultAccountLockoutPeriod);
			if (authenticationFailed)
			{
				if (person.PerIsLocked)
					throw new LoginFailureException($"Your account has been locked for security reasons. Please try again in {ApplicationConfiguration.DefaultAccountLockoutPeriod} minutes or reset your password.");
				var lockAquired = false;
				using (var transaction = _personRepository.Session.BeginTransaction())
				{
					person.PerLockCount++;
					if (person.PerLockCount >= ApplicationConfiguration.DefaultMaxLoginAttemptsBeforeLockout)
					{
						person.Lock();
						lockAquired = true;
					}
					_personRepository.SaveOrUpdate(person);
					transaction.Commit();
				}
				if (lockAquired)
					throw new LoginFailureException($"Your account has been locked for security reasons. Please try again in {ApplicationConfiguration.DefaultAccountLockoutPeriod} minutes or reset your password.");
			}
			else
			{
				if (person.PerIsLocked)
				{
					throw new LoginFailureException($"Your account has been locked for security reasons. Please try again in {ApplicationConfiguration.DefaultAccountLockoutPeriod} minutes or reset your password.");
				}

				using (var transaction = _personRepository.Session.BeginTransaction())
				{
					person.Unlock();
					_personRepository.SaveOrUpdate(person);
					transaction.Commit();
				}
			}
		}
    }
}