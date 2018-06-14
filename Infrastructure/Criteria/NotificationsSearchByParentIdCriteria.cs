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
    public class NotificationsSearchByParentIdCriteria : ICriteriaSpecification<Notification>
    {
        private readonly long _personId;
        public NotificationsSearchByParentIdCriteria(long personId)
        {
            _personId = personId;
        }

        public ICriteria Criteria(ISession session)
        {
            return session.CreateCriteria(typeof(Notification))
                  .Add(Restrictions.Eq("NotPersonId", _personId));
        }
    }
}
