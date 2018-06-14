using FluentNHibernate.Mapping;
using NIHApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Domain.Persistance
{
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Table("Event");
            LazyLoad();
            Id(x => x.EvtID).GeneratedBy.Identity().Column("EvtID");
            Map(x => x.EvtDrivertId).Column("EvtDrivertId");
            Map(x => x.EvtParentId).Column("EvtParentId");
            Map(x => x.EvtType).Column("EvtType");
            Map(x => x.EvtPickUpTime).Column("EvtPickUpTime");
            Map(x => x.EvtDropOffTime).Column("EvtDropOffTime");
            Map(x => x.EvtTripFromHome).Column("EvtTripFromHome");
            Map(x => x.EvtLongitude).Column("EvtLongitude");
            Map(x => x.EvtLatitude).Column("EvtLatitude");
            Map(x => x.EvtNotification).Column("EvtNotification");
        }
    }
}
