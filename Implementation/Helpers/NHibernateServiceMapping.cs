using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NIHApp.Domain.Persistance;
using NHibernate;
using NHibernate.Burrow;
using NHibernate.Cfg;
using NHibernate.Event;

namespace NIHApp.Implementation.Helpers
{
    public class NHibernateServiceMapping : IConfigurator
    {
        public ISessionFactory SessionFactory;

        public virtual void Config(IBurrowConfig val)
        {
            val.ManualTransactionManagement = true;
        }
        /*
        public virtual void Config(IPersistenceUnitCfg puCfg, Configuration nhCfg)
        {
            var config = Fluently.Configure(nhCfg)
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromAppSetting("connection_string")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersonMap>())
                .ExposeConfiguration(x => x.SetProperty("current_session_context_class", "thread_static"))
                .ExposeConfiguration(x => x.SetProperty("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache"))
                .ExposeConfiguration(x => x.SetProperty("cache.use_second_level_cache", "false"))
                .ExposeConfiguration(x => x.SetProperty("cache.use_query_cache", "false"))
                .ExposeConfiguration(x => x.SetProperty("connection.isolation", "ReadUncommitted"))
                .ExposeConfiguration(x => x.SetProperty("command_timeout", "60"))
                .ExposeConfiguration(x => x.SetProperty("show_sql", "false"))
                .ExposeConfiguration(x => x.SetProperty("generate_statistics", "false"))
                .ExposeConfiguration(x => x.SetProperty("adonet.batch_size", "100"))
                .BuildConfiguration();
            SessionFactory = config.BuildSessionFactory();
            SessionFactory.Statistics.IsStatisticsEnabled = false;
        }
        */
        public virtual void Config(IPersistenceUnitCfg puCfg, Configuration nhCfg)
        {
            var config = Fluently.Configure(nhCfg)
              .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromAppSetting("connection_string")))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersonMap>())
              .ExposeConfiguration(x => x.SetProperty("current_session_context_class", "web"))
              .ExposeConfiguration(x => x.SetProperty("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache"))
              .ExposeConfiguration(x => x.SetProperty("cache.use_second_level_cache", "true"))
              .ExposeConfiguration(x => x.SetProperty("cache.use_query_cache", "true"))
              .ExposeConfiguration(x => x.SetProperty("connection.isolation", "ReadUncommitted"))
              .ExposeConfiguration(x => x.SetProperty("command_timeout", "60"))
              .ExposeConfiguration(x => x.SetProperty("show_sql", "false"))
              .ExposeConfiguration(x => x.SetProperty("generate_statistics", "false"))
              .BuildConfiguration();
            //config.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { AuditTrailListener.Instance };
            //config.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { AuditTrailListener.Instance };
            SessionFactory = config.BuildSessionFactory();
            SessionFactory.Statistics.IsStatisticsEnabled = false;
        }
    }
}
