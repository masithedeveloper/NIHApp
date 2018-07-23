using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Services;
using NIHApp.Infrastructure.Interfaces;
using NIHApp.Infrastructure.Repositories;
using StructureMap.Configuration.DSL;

namespace NIHApp.Implementation.Container
{
	public class StandardRegistry : Registry
	{
		public StandardRegistry()
		{   // Pairs Interface and Implementation classes, sort  of DI 
			For(typeof(IRepository<>)).Use(typeof(Repository<>));

			For(typeof(IPersonRepository)).Use(typeof(PersonRepository));
			For(typeof(IPersonService)).Use(typeof(PersonService));
            
			For(typeof(IDeviceRepository)).Use(typeof(DeviceRepository));
			For(typeof(IDeviceService)).Use(typeof(DeviceService));

            For(typeof(ITransportRepository)).Use(typeof(TransportRepository));
            For(typeof(ITransportService)).Use(typeof(TransportService));

            For(typeof(IEventRepository)).Use(typeof(EventRepository));
            For(typeof(IEventService)).Use(typeof(EventService));

            For(typeof(INotificationRepository)).Use(typeof(NotificationRepository));
            For(typeof(INotificationService)).Use(typeof(NotificationService));

            For(typeof(ISessionService)).Use(typeof(SessionService));
			For(typeof(ISessionRepository)).Use(typeof(SessionRepository));
            
            For(typeof(IAuthenticationService)).Use(typeof(AuthenticationService));

			For(typeof(IApplicationConfiguration)).Use(typeof(ApplicationConfiguration));
            
			//For(typeof(IBlobService)).Use(typeof(BlobService));
            For(typeof(IScheduledEmailService)).Use(typeof(ScheduledEmailService));
            //For(typeof(IScheduledEmailRepository)).Use(typeof(ScheduledEmailRepository));
            //For(typeof(IEmailNotificationService)).Use(typeof(EmailNotificationService));

            For(typeof(ISMSService)).Use(typeof(SMSService));

        }
	}
}