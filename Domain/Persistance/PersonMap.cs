using FluentNHibernate.Mapping;
using NIHApp.Domain.Entities;

namespace NIHApp.Domain.Persistance
{
	public class PersonMap : ClassMap<Person>
	{
		public PersonMap()
		{
			Table("Person");
			LazyLoad();
			Id(x => x.Id).GeneratedBy.Identity().Column("PerId");
			Map(x => x.PerFirstname).Column("PerFirstname");
			Map(x => x.PerLastname).Column("PerLastname");
			Map(x => x.PerEmail).Column("PerEmail");
			Map(x => x.PerPassword).Column("PerPassword");
			Map(x => x.PerHashPassword).Column("PerHashPassword");
			Map(x => x.PerDob).Column("PerDob");
			Map(x => x.PerIdNumber).Column("PerIdNumber");
            Map(x => x.PerType).Column("PerType");
            Map(x => x.PerCellPhone).Column("PerCellphone");
            Map(x => x.PerTransportId).Column("PerTransportId");
			//Map(x => x.PerMobile).Column("PerMobile");
			Map(x => x.PerEmailVerified).Column("PerEmailVerified").Not.Nullable();
			//Map(x => x.PerPasswordReset).Column("PerPasswordReset").Not.Nullable();
			//Map(x => x.PerLockCount).Column("PerLockCount").Not.Nullable();
			//Map(x => x.PerLockedAt).Column("PerLockedAt");
			//Map(x => x.PerVerifyCode).Column("PerVerifyCode");
            //HasMany(x => x.Sessions).KeyColumn("PersonId"); // I don't get this */
		}
	}
}