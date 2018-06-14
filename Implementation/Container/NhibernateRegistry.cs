using NIHApp.Implementation.Helpers;
using NHibernate;
using NServiceBus.ObjectBuilder.StructureMap262;
using StructureMap.Configuration.DSL;

namespace NIHApp.Implementation.Container
{
	public class NhibernateRegistry : Registry
	{
		public NhibernateRegistry()
		{
			//For<ISessionFactory>().Use(ctx => NHibernateProvider.GetSessionFactory());
			For<ISession>().HybridHttpOrThreadLocalScoped().Use(ctx => NHibernateHelper.GetCurrentSession());
		}
	}

    public class NhibernateServiceRegistry : Registry
    {
        public NhibernateServiceRegistry()
        {
            For<ISession>().LifecycleIs(new NServiceBusThreadLocalStorageLifestyle()).Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());
        }
    }
}