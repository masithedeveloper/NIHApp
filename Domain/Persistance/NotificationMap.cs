using FluentNHibernate.Mapping;
using NIHApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Domain.Persistance
{
    public class NotificationMap : ClassMap<Notification>
    {
        public NotificationMap()
        {
            Table("Notification");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("NotId");
            Map(x => x.NotMessage).Column("NotMessage");
            Map(x => x.NotPersonId).Column("NotPersonId");
            Map(x => x.NotTimeCreated).Column("NotTimeCreated");
            Map(x => x.NotIsSent).Column("NotIsSent");
            Map(x => x.NotEventId).Column("NotEventId");
        }
    }
}
