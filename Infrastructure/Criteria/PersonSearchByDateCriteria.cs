using System;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
    public class PersonSearchByDateCriteria : ICriteriaSpecification<Person>
    {
        private readonly DateTime _dateTime;
        private readonly long _personId;
        public PersonSearchByDateCriteria(long personId, DateTime dateTime)
        {
            _personId = personId;
            _dateTime = dateTime;
        }

        public ICriteria Criteria(ISession session)
        {
            return session.CreateCriteria(typeof(Person))
                  .Add(Restrictions.Ge("ModifiedDate", _dateTime))
                  .Add(Restrictions.Eq("Id", _personId));
        }
    }
}
