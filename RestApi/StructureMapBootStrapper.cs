using NIHApp.Implementation.Container;
using StructureMap;

namespace NIHApp.RestApi
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
					s.AssemblyContainingType<StandardRegistry>();
					s.AssemblyContainingType<NhibernateRegistry>();
					s.LookForRegistries();
                    s.ExcludeType<NhibernateServiceRegistry>();
                }
					)
				);
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