using FluentNHibernate.Mapping;
using NIHApp.Domain.Entities;

namespace NIHApp.Domain.Persistance
{
	public class DeviceMap : ClassMap<Device>
	{
		public DeviceMap()
		{
			Table("Device");
			LazyLoad();
			Id(x => x.Id).GeneratedBy.Identity().Column("DevId");
			Map(x => x.DevFirebaseToken).Column("DevFirebaseToken");
			Map(x => x.DevPlatform).Column("DevPlatform");
			Map(x => x.DevOSVersion).Column("DevOSVersion");
			Map(x => x.CreateDate).Column("DevCreateDate");
			Map(x => x.ModifiedDate).Column("DevModifiedDate");
			References(x => x.Person).Column("DevPersonId");
		}
	}
}
