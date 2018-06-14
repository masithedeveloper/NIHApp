using System;
using System.Net.Http;
using System.Web.Http;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Implementation.Services;
using NIHApp.Infrastructure.Interfaces;
using NIHApp.Infrastructure.Repositories;
using NIHApp.RestApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Burrow;

namespace Test
{
	[TestClass]
	public class RestApiAuth
	{
		private static BurrowFramework _burrowsFramework;
		private static ISessionFactory _sessionFactory;
		private static ISession _session;

		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			_burrowsFramework = new BurrowFramework();
			_burrowsFramework.InitWorkSpace();
			_sessionFactory = _burrowsFramework.GetSessionFactory("PersistenceUnit1");
			_session = _sessionFactory.OpenSession();
		}

		[ClassCleanup]
		public static void Cleanup()
		{
			_session.Dispose();
			_sessionFactory.Dispose();
			_burrowsFramework.CloseWorkSpace();
		}

		[TestMethod]
		public void LoginSucceed()
		{
			ApplicationConfiguration appApplicationConfiguration = new ApplicationConfiguration();
			PersonRepository personRepository = new PersonRepository(_session);
            EmailNotificationService emailNotificationService = new EmailNotificationService(appApplicationConfiguration);
            ScheduledEmailRepository scheduledEmailRepository = new ScheduledEmailRepository(_session);
            ScheduledEmailService scheduledEmailService = new ScheduledEmailService(scheduledEmailRepository, emailNotificationService, personRepository);
			DeviceRepository deviceRepository = new DeviceRepository(_session);
			SessionRepository sessionRepository = new SessionRepository(_session);
			PersonService personService = new PersonService(personRepository, deviceRepository, sessionRepository, emailNotificationService, scheduledEmailService, appApplicationConfiguration, scheduledEmailRepository);
			SessionService apiSessionService = new SessionService(sessionRepository);
			DeviceService deviceService = new DeviceService(deviceRepository, personRepository);
			AuthenticationService authenticationService = new AuthenticationService(personRepository);
           
            AuthController authController = new AuthController(apiSessionService, personService, authenticationService, deviceService)
			{
				Configuration = new HttpConfiguration(),
				Request = new HttpRequestMessage()
			};

			authController.Request.Headers.Add("XClientId", "00000000000000000000000000000000"); // Wanna make this my NIHApp API KEY

			AuthModelLight authModelLight = new AuthModelLight
			{
				EmailAddress = appApplicationConfiguration.GetSetting("unitTestEmail"),
				Password = appApplicationConfiguration.GetSetting("unitPass")
			};

			string jsonString = JsonConvert.SerializeObject(authModelLight);

			AuthModelLight result = authController.VerifyByPost(authModelLight);
			Assert.AreNotEqual(result.UserId, 0);
		}

		[TestMethod]
		public void LoginInvalidCredentials()
		{
            ApplicationConfiguration appApplicationConfiguration = new ApplicationConfiguration();
            PersonRepository personRepository = new PersonRepository(_session);
            AuthenticationService authenticationService = new AuthenticationService(personRepository);
            ScheduledEmailRepository scheduledEmailRepository = new ScheduledEmailRepository(_session);
            EmailNotificationService emailNotificationService = new EmailNotificationService(appApplicationConfiguration);
            ScheduledEmailService scheduledEmailService = new ScheduledEmailService(scheduledEmailRepository, emailNotificationService, personRepository);
			
			DeviceRepository deviceRepository = new DeviceRepository(_session);
			SessionRepository apiSessionRepository = new SessionRepository(_session);
			PersonService personService = new PersonService(personRepository, deviceRepository, apiSessionRepository, emailNotificationService, scheduledEmailService, appApplicationConfiguration, scheduledEmailRepository);
			SessionService apiSessionService = new SessionService(apiSessionRepository);
			DeviceService deviceService = new DeviceService(deviceRepository, personRepository);
			
            
            AuthController authController = new AuthController(apiSessionService, personService, authenticationService, deviceService)
			{
				Configuration = new HttpConfiguration(),
				Request = new HttpRequestMessage()
			};

			authController.Request.Headers.Add("XClientId", "00000000000000000000000000000000");

			AuthModelLight authModelLight = new AuthModelLight
			{
				EmailAddress = "empty",
				Password = "empty"
			};

			string jsonString = JsonConvert.SerializeObject(authModelLight);

			AuthModelLight result = authController.VerifyByPost(authModelLight);
			Assert.AreEqual(result.Desc, ApplicationConfiguration.LoginInvalidCredentials);
		}

		[TestMethod]
		public void CreatePerson()
		{
			ApplicationConfiguration appApplicationConfiguration = new ApplicationConfiguration();
			PersonRepository personRepository = new PersonRepository(_session);
            ScheduledEmailRepository scheduledEmailRepository = new ScheduledEmailRepository(_session);
            EmailNotificationService emailNotificationService = new EmailNotificationService(appApplicationConfiguration);
            ScheduledEmailService scheduledEmailService = new ScheduledEmailService(scheduledEmailRepository, emailNotificationService, personRepository);
			DeviceRepository deviceRepository = new DeviceRepository(_session);
			SessionRepository sessionRepository = new SessionRepository(_session);
			PersonService personService = new PersonService(personRepository, deviceRepository, sessionRepository, emailNotificationService, scheduledEmailService, appApplicationConfiguration, scheduledEmailRepository);
			SessionService sessionService = new SessionService(sessionRepository);
			DeviceService deviceService = new DeviceService(deviceRepository, personRepository);
			AuthenticationService authenticationService = new AuthenticationService(personRepository);
        
			AuthController authController = new AuthController(sessionService, personService, authenticationService, deviceService)
			{
				Configuration = new HttpConfiguration(),
				Request = new HttpRequestMessage()
			};

			authController.Request.Headers.Add("XClientId", "00000000000000000000000000000000");

			PersonRegisterModel personRegisterModel = new PersonRegisterModel()
			{
                // assign the data to model properties here
			};

			string jsonString = JsonConvert.SerializeObject(personRegisterModel);

			AuthModelLight authModelLight = authController.CreatePerson(personRegisterModel);
			Assert.AreNotEqual(authModelLight.UserId, 0);
		}
	}
}
