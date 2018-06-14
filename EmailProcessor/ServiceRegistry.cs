using log4net;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Services;
using NIHApp.Infrastructure.Interfaces;
using NIHApp.Infrastructure.Repositories;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace EmailProcessor
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<ISessionFactory>().Singleton().Use(Program.SessionFactory);
            For<IScheduledEmailCheckService>().Use<ScheduledEmailCheckService>();
            For(typeof(IRepository<>)).Use(typeof(Repository<>));
            For(typeof(IScheduledEmailService)).Use(typeof(ScheduledEmailService));
            For(typeof(IScheduledEmailRepository)).Use(typeof(ScheduledEmailRepository));
            For<IApplicationConfiguration>().Singleton().Use<ApplicationConfiguration>();
            For<ILog>().Singleton().Use(Program.Log);
            For<IEmailNotificationService>().Use<EmailNotificationService>();
        }
    }
}
