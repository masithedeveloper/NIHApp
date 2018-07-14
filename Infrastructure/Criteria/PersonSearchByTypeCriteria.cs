using System;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
    public class PersonSearchByTypeCriteria : ICriteriaSpecification<Person>
    {
        public PersonSearchByTypeCriteria(){}

        public ICriteria Criteria(ISession session)
        {
            return session.CreateCriteria(typeof(Person))
                  .Add(Restrictions.Eq("PerType", false)); // thing gets all drivers
        }
    }
}
