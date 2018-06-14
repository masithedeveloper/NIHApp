using System;
using System.Net.Http;
using System.Web.Http;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Implementation.Services;
using NIHApp.Infrastructure.Repositories;
using NIHApp.RestApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Burrow;

namespace Test
{
	[TestClass]
	public class RestApiPerson
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
		public void GetPersonById()
		{
            // ARRANGE
            ApplicationConfiguration appApplicationConfiguration = new ApplicationConfiguration();
            PersonRepository personRepository = new PersonRepository(_session);
			DeviceRepository deviceRepository = new DeviceRepository(_session);
			SessionRepository sessionRepository = new SessionRepository(_session);
            ScheduledEmailRepository scheduledEmailRepository = new ScheduledEmailRepository(_session);
            EmailNotificationService emailNotificationService = new EmailNotificationService(appApplicationConfiguration);
            ScheduledEmailService scheduledEmailService = new ScheduledEmailService(scheduledEmailRepository, emailNotificationService, personRepository);
            PersonService personService = new PersonService(personRepository, deviceRepository, sessionRepository, emailNotificationService, scheduledEmailService, appApplicationConfiguration, scheduledEmailRepository);

            SessionService sessionService = new SessionService(sessionRepository);
			PersonController personController = new PersonController(sessionService, personService)
			{
				Configuration = new HttpConfiguration(),
				Request = new HttpRequestMessage()
			};

			personController.Request.Headers.Add("XClientId", Guid.NewGuid().ToString());
			personController.Request.RequestUri = new Uri("http://localhost?xerxes=1"); // Authentication shortcut

			// ACT
			var personModel = personController.GetPersonById(Convert.ToInt32(appApplicationConfiguration.GetSetting("personId")));

			// ASSERT
			Assert.IsNotNull(personModel);
		}

		[TestMethod]
		public void UpdatePerson()
		{
			// ARRANGE
			ApplicationConfiguration appApplicationConfiguration = new ApplicationConfiguration();
			PersonRepository personRepository = new PersonRepository(_session);
			DeviceRepository deviceRepository = new DeviceRepository(_session);
			SessionRepository sessionRepository = new SessionRepository(_session);
            ScheduledEmailRepository scheduledEmailRepository = new ScheduledEmailRepository(_session);
            EmailNotificationService emailNotificationService = new EmailNotificationService(appApplicationConfiguration);
            ScheduledEmailService scheduledEmailService = new ScheduledEmailService(scheduledEmailRepository, emailNotificationService, personRepository);
            PersonService personService = new PersonService(personRepository, deviceRepository, sessionRepository, emailNotificationService, scheduledEmailService, appApplicationConfiguration, scheduledEmailRepository);

            SessionService sessionService = new SessionService(sessionRepository);
			PersonController personController = new PersonController(sessionService, personService)
			{
				Configuration = new HttpConfiguration(),
				Request = new HttpRequestMessage()
			};

			personController.Request.Headers.Add("XClientId", Guid.NewGuid().ToString());
			personController.Request.RequestUri = new Uri("http://localhost?xerxes=1"); // Authentication shortcut

			PersonModel personModel = new PersonModel()
			{
				ObjectId = Convert.ToInt32(appApplicationConfiguration.GetSetting("personId")),
				Name = "Name",
				Surname = "Surname",
				Email = appApplicationConfiguration.GetSetting("unitTestEmail"),
				CreateDate = new DateTime(2016, 09, 15, 9, 00, 00),
				ModifiedDate = new DateTime(2016, 09, 15, 9, 00, 00),
			};

			// ACT
			personModel = personController.Update(personModel);

			// ASSERT
			Assert.AreNotEqual(personModel.ModifiedDate, new DateTime(2016, 09, 15, 9, 00, 00));
		}

		[TestMethod]
		public void DeletePerson()
		{
            // ARRANGE
            ApplicationConfiguration appApplicationConfiguration = new ApplicationConfiguration();
            PersonRepository personRepository = new PersonRepository(_session);
			DeviceRepository deviceRepository = new DeviceRepository(_session);
			SessionRepository sessionRepository = new SessionRepository(_session);
            ScheduledEmailRepository scheduledEmailRepository = new ScheduledEmailRepository(_session);
            EmailNotificationService emailNotificationService = new EmailNotificationService(appApplicationConfiguration);
            ScheduledEmailService scheduledEmailService = new ScheduledEmailService(scheduledEmailRepository, emailNotificationService, personRepository);
            PersonService personService = new PersonService(personRepository, deviceRepository, sessionRepository, emailNotificationService, scheduledEmailService, appApplicationConfiguration, scheduledEmailRepository);

            SessionService apiSessionService = new SessionService(sessionRepository);
			PersonController personController = new PersonController(apiSessionService, personService)
			{
				Configuration = new HttpConfiguration(),
				Request = new HttpRequestMessage()
			};

			personController.Request.Headers.Add("XClientId", Guid.NewGuid().ToString());
			personController.Request.RequestUri = new Uri("http://localhost?xerxes=1"); // Authentication shortcut

			// ACT
			personController.Delete(20000);

			// ASSERT
			var personModel = personController.GetPersonById(20000);
			Assert.IsNull(personModel);
		}
	}
}
