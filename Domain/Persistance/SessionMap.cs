using FluentNHibernate.Mapping;
using NIHApp.Domain.Entities;

namespace NIHApp.Domain.Persistance
{
	public class SessionMap : ClassMap<Session>
	{
		public SessionMap()
		{
			Table("Session");
			Id(x => x.SesId).GeneratedBy.Identity().Column("SesId");
			Map(x => x.SesKey).Column("SesKey");
            Map(x => x.SesDeviceActive).Column("SesDeviceActive");
            Map(x => x.SesPersonId).Column("SesPersonId");
			Map(x => x.SesIsActive).Column("SesIsActive");
			Map(x => x.SesCreatedDate).Column("SesCreatedDate");
            Map(x => x.SesModifiedDate).Column("SesModifiedDate");
            Map(x => x.SesValidDate).Column("SesValidDate");
		}
	}
}