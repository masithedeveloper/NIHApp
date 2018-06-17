﻿using System;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
    public class ParentsSerchByDriverCriteria : ICriteriaSpecification<Person>
    {
        private readonly DateTime _dateTime;
        private readonly long _personId;
        public ParentsSerchByDriverCriteria(long driverId)
        {
            _personId = driverId;

        }

        public ICriteria Criteria(ISession session)
        {
            return session.CreateCriteria(typeof(Person))
                  .Add(Restrictions.Eq("Id", _personId));
        }
    }
}
