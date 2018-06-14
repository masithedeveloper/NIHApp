using NHibernate;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Infrastructure.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(ISession session) : base(session) {
        }

        public IList<Event> FindEventsByDatesAndParentId(DateTime From, DateTime To, long ParentId)
        {
            return FindBySpecification(new EventSearchByDatesAndParentIdCriteria(From, To, ParentId));
        }
    }
}
