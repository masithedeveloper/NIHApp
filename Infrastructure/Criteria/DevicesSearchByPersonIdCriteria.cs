using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class DevicesSearchByPersonIdCriteria : ICriteriaSpecification<Device>
	{
		private readonly long _personId;

		public DevicesSearchByPersonIdCriteria(long personId)
		{
			_personId = personId;
		}

		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(Device))
				 .Add(Restrictions.Eq("Person.Id", _personId));
		}
	}
}
