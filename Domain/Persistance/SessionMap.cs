using FluentNHibernate.Mapping;
using NIHApp.Domain.Entities;

namespace NIHApp.Domain.Persistance
{
	public class SessionMap : ClassMap<Session>
	{
		public SessionMap()
		{
			Table("Session");
			Id(x => x.Id).GeneratedBy.Identity().Column("SesId");
			Map(x => x.SesKey).Column("SesKey");
			//Map(x => x.SesDeviceFirebaseToken).Column("SesDeviceFirebaseToken");
			Map(x => x.SesPersonId).Column("SesPersonId");
			Map(x => x.SesIsActive).Column("SesIsActive");
			Map(x => x.CreateDate).Column("CreateDate");
            Map(x => x.ModifiedDate).Column("ModifiedDate");
            Map(x => x.SesValidDate).Column("SesValidDate");
		}
	}
}