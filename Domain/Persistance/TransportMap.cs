using FluentNHibernate.Mapping;
using NIHApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Domain.Persistance
{
    public class TransportMap : ClassMap<Transport>
    {
        public TransportMap() {

            Table("Transport");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("TraId");
            Map(x => x.TraRegistration).Column("TraRegistration");
            Map(x => x.TraMake).Column("TraMake");
            Map(x => x.TraModel).Column("TraModel");
        }
    }
}
