using FluentNHibernate.Mapping;
using NIHApp.Domain.Entities;
using NIHApp.Domain.Enums;

namespace NIHApp.Domain.Persistance
{
    public class ScheduledEmailMap : ClassMap<ScheduledEmail>
    {
        public ScheduledEmailMap()
        {
            Table("[ScheduledEmail]");
            Id(x => x.Id).GeneratedBy.Identity().Column("SchEmailId");
            References(x => x.Person).Column("SchPerId").Nullable();
            Map(x => x.SchFromAddress).Column("SchFromEmailAddress").Not.Nullable();
            Map(x => x.SchFromName).Column("SchFromName").Not.Nullable();
            Map(x => x.SchToEmailAddress).Column("SchToEmailAddress").Not.Nullable();
            Map(x => x.SchBccEmailAddress).Column("SchBccEmailAddress").Not.Nullable();
            Map(x => x.SchCcEmailAddress).Column("SchCcEmailAddress").Not.Nullable();
            Map(x => x.SchSubject).Column("SchSubject").Not.Nullable();
            Map(x => x.SchContent).Column("SchContent").Not.Nullable().CustomType("StringClob").CustomSqlType("ntext");
            Map(x => x.SchIsHtml).Column("SchIsHtml").Not.Nullable();
            Map(x => x.SchType).Column("SchType").CustomType<ScheduledEmailType>();
            Map(x => x.SchSendAt).Column("SchSendAt");
            Map(x => x.SchEmailed).Column("SchEmailed");
            Map(x => x.SchReady).Column("SchReady");
            Map(x => x.SchFailureCount).Column("SchFailureCount");
            Map(x => x.SchLastFailureReason).Column("SchLastFailureReason").CustomType("StringClob").CustomSqlType("ntext");
            Map(x => x.CreateDate).Column("CreateDate");
            Map(x => x.ModifiedDate).Column("ModifiedDate");
        }
    }
}
