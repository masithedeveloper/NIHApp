using System.Configuration;
using log4net;
using NHibernate;
using NHibernate.Burrow;
using StructureMap;
using Topshelf;

namespace EmailProcessor
{
    public class Program
	{
		public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public static readonly BurrowFramework BurrowsFramework = new BurrowFramework();
		public static ISessionFactory SessionFactory = null;

		public static void Main()
		{
			SessionFactory = BurrowsFramework.GetSessionFactory("PersistenceUnit1");
			StructureMapBootStrapper.Bootstrap();
			HostFactory.Run(x =>
			{
				x.Service<IScheduledEmailCheckService>(s =>
					{
						s.ConstructUsing(name => ObjectFactory.GetInstance<IScheduledEmailCheckService>());
						s.WhenStarted(tc => tc.Start());
						s.WhenStopped(tc => tc.Stop());
					});
				x.RunAsNetworkService();
				x.SetDescription("The service that sends out all NIHApp emails");
				x.SetDisplayName(ConfigurationManager.AppSettings["service_display_name"]);
				x.SetServiceName(ConfigurationManager.AppSettings["service_name"]);
			});
		}
	}
}
