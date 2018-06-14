using NIHApp.Implementation.Container;
using StructureMap;

namespace EmailProcessor
{
	public class StructureMapBootStrapper : IBootstrapper
	{
		private static bool _hasStarted;

		public virtual void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(x =>
				x.Scan(s =>
				{
					s.TheCallingAssembly();
                    //s.AssemblyContainingType<StandardRegistry>();
                    s.AssemblyContainingType<ServiceRegistry>();
                    s.AssemblyContainingType<NhibernateServiceRegistry>();
                    s.LookForRegistries();
                    s.ExcludeType<NhibernateRegistry>();
                }));
		}

		public static void Restart()
		{
			if (_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap();
				_hasStarted = true;
			}
		}

		public static void Bootstrap()
		{
			new StructureMapBootStrapper().BootstrapStructureMap();
		}
	}
}