using System;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class DevicesSearchByDateCriteria : ICriteriaSpecification<Device>
	{
		private readonly DateTime _dateTime;
		private readonly long _personId;

		public DevicesSearchByDateCriteria(long personId, DateTime dateTime)
		{
			_dateTime = dateTime;
			_personId = personId;
		}

		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(Device))
				 .Add(Restrictions.Ge("ModifiedDate", _dateTime))
				 .Add(Restrictions.Eq("Person.Id", _personId));
		}
	}
}
