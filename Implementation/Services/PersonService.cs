using System;
using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Domain.Enums;
using NIHApp.Implementation.Helpers;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using NHibernate.Util;

namespace NIHApp.Implementation.Services
{
	public class PersonService : IPersonService
	{
		private readonly IPersonRepository _personRepository;
		private readonly IEmailNotificationService _notificationService;
		private readonly IScheduledEmailService _scheduledEmailService;
		private readonly IApplicationConfiguration _applicationConfiguration;
		private readonly IScheduledEmailRepository _scheduledEmailRepository;
		private readonly IDeviceRepository _deviceRepository;
		private readonly ISessionRepository _sessionRepository;
		private readonly Random _random;

		public PersonService(IPersonRepository personRepository, IDeviceRepository deviceRepository, ISessionRepository sessionRepository, IEmailNotificationService emailNotificationService, IScheduledEmailService scheduledEmailService, IApplicationConfiguration applicationConfiguration, IScheduledEmailRepository scheduledEmailRepository)
		{
			_random = new Random((int)DateTime.Now.Ticks);
			_personRepository = personRepository;
			_deviceRepository = deviceRepository;
			_sessionRepository = sessionRepository;
			_notificationService = emailNotificationService;
			_scheduledEmailService = scheduledEmailService;
			_applicationConfiguration = applicationConfiguration;
			_scheduledEmailRepository = scheduledEmailRepository;
		}

		public bool ChangePassword(long personId, string newPassword = "")
		{
			var person = _personRepository.Get(personId);
			string password;

			if (newPassword == string.Empty) // Generate New Password
			{
				const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
				password = new string(Enumerable.Repeat(chars, 8).Select(s => s[_random.Next(s.Length)]).ToArray());
				person.PerPasswordReset = true;
			}
			else // Change Password
			{
				password = newPassword;
				person.PerPasswordReset = false;
			}

			person.PerHashPassword = AuthenticationHelper.GetPasswordHash(person.PerEmail, password);

			using (var transaction = _personRepository.Session.BeginTransaction())
			{
				_personRepository.Save(person);
				transaction.Commit();
			}

			if (newPassword == string.Empty) // Email New Generated Password
			{
				var emailSubject = _notificationService.GetForgotPasswordSubject();
				var emailBody = _notificationService.GetForgotPasswordMailBody(person, password);
				var fromAddress = _applicationConfiguration.GetSetting("from_address");
				var fromName = _applicationConfiguration.GetSetting("from_name");
				_scheduledEmailService.CreatScheduledEmail(fromAddress, fromName, person.PerEmail, string.Empty, string.Empty, emailSubject, emailBody, true, ScheduledEmailType.ForgotPassword, person.Id);
			}

			return true;
		}

		public bool IsPasswordReset(long personId)
		{
			var person = _personRepository.Get(personId);
			return person.PerPasswordReset;
		}

		public bool IsVerified(long personId)
		{
			var person = _personRepository.Get(personId);
			return person.PerEmailVerified;
		}

		public bool RequireUpgrade(long personId)
		{
			var person = _personRepository.Get(personId);
			return (person.PerVerifyCode == 0);
		}

		public bool MatchVerifyCode(long personId, string hashPassword, int verifyCode)
		{
			var person = _personRepository.Get(personId);
			if (person.PerHashPassword != hashPassword)
				throw new Exception("Invalid Credentials.");

			if (person.PerVerifyCode == verifyCode)
			{
				person.PerEmailVerified = true;
				using (var transaction = _personRepository.Session.BeginTransaction())
				{
					_personRepository.Save(person);
					transaction.Commit();
				}
			}

			return (person.PerVerifyCode == verifyCode);
		}

		public PersonModel GetPersonById(long personId)
		{
			var person = _personRepository.Get(personId);
			return person == null ? null : new PersonModel(person);
		}

		public PersonModel GetPersonByEmail(string email)
		{
			var person = _personRepository.FindPersonByEmail(email).FirstOrDefault();
			return person == null ? null : new PersonModel(person);
		}

		public IList<PersonModel> GetPersonByDate(long personId, DateTime modifiedAfter)
		{
			var persons = _personRepository.FindPersonsByDate(modifiedAfter, personId);
			return persons.Select(x => new PersonModel(x)).ToList();
		}

        public IList<PersonModel> GetParentsListByDriverId(long driverId)
        {
            var persons = _personRepository.FindParentsListByDriverId(driverId);
            return persons.Select(x => new PersonModel(x)).ToList();
        }

        /// <summary>
        /// Person Registration
        /// </summary>
        /// <param name="personRegisterModel">Person Registration Model</param>
        /// <returns>Registered Person</returns>
        public PersonRegisterModel CreatePerson(PersonRegisterModel personRegisterModel)
		{
			var person = new Person
			{	
                PerFirstname = personRegisterModel.PerFirstname,
                PerLastname = personRegisterModel.PerLastname,
                PerCellPhone = personRegisterModel.PerCellPhone,
                PerEmail = personRegisterModel.EmailAddress,
                PerPassword = personRegisterModel.Password, // remove this as a later stage
                PerHashPassword = AuthenticationHelper.GetPasswordHash(personRegisterModel.EmailAddress, personRegisterModel.Password),
				//VerifyCode = 9999,
				PerVerifyCode = (short)_random.Next(1000, 9999),
				CreateDate = DateTime.Now,
				ModifiedDate = DateTime.Now,
                PerDob = DateTime.Now,
                PerType = personRegisterModel.PerType,
                PerEmailVerified = true
            };

			using (var transaction = _personRepository.Session.BeginTransaction())
			{
				_personRepository.Save(person);
				transaction.Commit();
			}

			personRegisterModel = new PersonRegisterModel(person)
			{
				//Password = personRegisterModel.Password,
				Password = "********",
				DeviceCode = personRegisterModel.DeviceCode,
				DeviceDescription = personRegisterModel.DeviceDescription,
				OS = personRegisterModel.OS
			};

			//Send Registration Mail
			/*var emailSubject = _notificationService.GetRegisterSubject();
			var emailBody = _notificationService.GetRegisterMailBody(person);
			var fromAddress = _applicationConfiguration.GetSetting("from_address");
			var fromName = _applicationConfiguration.GetSetting("from_name");
			_scheduledEmailService.CreatScheduledEmail(fromAddress, fromName, person.PerEmail, string.Empty, string.Empty, emailSubject, emailBody, true, ScheduledEmailType.Registration, person.Id);*/

			return personRegisterModel;
		}

		public PersonRegisterModel UpgradePerson(PersonRegisterModel personRegisterModel)
		{
			var personList = _personRepository.FindPersonByEmail(personRegisterModel.PerEmailAddress);

			foreach (var person in personList)
				DeletePerson(person.Id);

			return CreatePerson(personRegisterModel);
		}

		/// <summary>
		/// Modify Person Registration
		/// </summary>
		/// <param name="personRegisterModel">Person Registration Model</param>
		/// <returns>Updated Registered Person</returns>
		public PersonRegisterModel UpdatePerson(PersonRegisterModel personRegisterModel)
		{
			var person = _personRepository.Get(personRegisterModel.PerId);
			var scheduledEmail = (ScheduledEmail)_scheduledEmailService.GetScheduledEmailsByPersonId(personRegisterModel.PerId).FirstOrNull();

			// Verification Code Email exception occured
			if ((scheduledEmail != null) && !scheduledEmail.SchReady && !scheduledEmail.SchEmailed && (person.PerEmail == personRegisterModel.PerEmailAddress))
			{
				throw new Exception("Email Error");
			}

			person.PerEmail = personRegisterModel.PerEmailAddress;
			person.PerFirstname = personRegisterModel.PerFirstname;
			person.PerLastname = personRegisterModel.PerLastname;
			//person.PerIdNumber = personRegisterModel.;
			person.PerHashPassword = AuthenticationHelper.GetPasswordHash(personRegisterModel.PerEmailAddress, personRegisterModel.Password);
			//person.VerifyCode = 9999;
			person.PerVerifyCode = (short)_random.Next(1000, 9999);
			person.CreateDate = personRegisterModel.CreateDate;
			person.ModifiedDate = personRegisterModel.ModifiedDate;

			using (var transaction = _personRepository.Session.BeginTransaction())
			{
				_personRepository.SaveOrUpdate(person);
				transaction.Commit();
			}

			personRegisterModel = new PersonRegisterModel(person)
			{
				//Password = personRegisterModel.Password,
				Password = "********",
				DeviceCode = personRegisterModel.DeviceCode,
				DeviceDescription = personRegisterModel.DeviceDescription,
				OS = personRegisterModel.OS
			};

			//Send Registration Mail
			var emailSubject = _notificationService.GetRegisterSubject();
			var emailBody = _notificationService.GetRegisterMailBody(person);
			var fromAddress = _applicationConfiguration.GetSetting("from_address");
			var fromName = _applicationConfiguration.GetSetting("from_name");
			_scheduledEmailService.CreatScheduledEmail(fromAddress, fromName, person.PerEmail, string.Empty, string.Empty, emailSubject, emailBody, true, ScheduledEmailType.Registration, person.Id);

			return personRegisterModel;
		}

		public PersonModel UpdatePerson(PersonModel personModel)
		{
			var person = _personRepository.Get(personModel.PerId);
			person.PerFirstname = personModel.PerFirstname;
			person.PerLastname = personModel.PerLastname;
			person.ModifiedDate = personModel.ModifiedDate;

			using (var transaction = _personRepository.Session.BeginTransaction())
			{
				_personRepository.SaveOrUpdate(person);
				transaction.Commit();
			}
			personModel = new PersonModel(person);

			return personModel;
		}

		public void DeletePerson(long personId)
		{
			var person = _personRepository.Get(personId);
			using (var transaction = _personRepository.Session.BeginTransaction())
			{
				// delete dependancies 
                // cascade delete all other children

				IList<Device> devices = _deviceRepository.FindDevicesByPersonId(person.Id);
				foreach (var device in devices)
				{
					_deviceRepository.Delete(device);
				}

				IList<Session> apiSessions = _sessionRepository.FindApiSessionByPersonId(person.Id);
				foreach (var apiSession in apiSessions)
				{
					_sessionRepository.Delete(apiSession);
				}

				IList<ScheduledEmail> scheduledEmails = _scheduledEmailRepository.GetScheduledEmailsByPersonId(person.Id);
				foreach (var scheduledEmail in scheduledEmails)
				{
					_scheduledEmailRepository.Delete(scheduledEmail);
				}

				_personRepository.Delete(person);
				transaction.Commit();
			}
		}

        public Person GetPersonEntityById(long personId)
        {
            return _personRepository.Get(personId);
        }

    
    }
}