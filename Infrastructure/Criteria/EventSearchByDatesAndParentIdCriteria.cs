using NHibernate;
using NHibernate.Criterion;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Infrastructure.Criteria
{
    public class EventSearchByDatesAndParentIdCriteria : ICriteriaSpecification<Event>
    {
        private readonly DateTime _from;
        private readonly DateTime _to;
        private readonly long _parentId;
        
        public EventSearchByDatesAndParentIdCriteria(DateTime From, DateTime To, long ParentId)
        {
            _from = From;
            _to = To;
            _parentId = ParentId;
        }

        public ICriteria Criteria(ISession session)
        {
            return session.CreateCriteria(typeof(Event))
                 .Add(Restrictions.Ge("EvtDateCreated", _from)) // this is the first of the current month
                 .Add(Restrictions.Le("EvtDateCreated", _to)) // this is now
                  .Add(Restrictions.Eq("EvtParentId", _parentId));
        }
    }
}
