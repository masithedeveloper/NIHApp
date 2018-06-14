using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class ApiSessionSearchByPersonIdCriteria : ICriteriaSpecification<Session>
	{
		private readonly long _personId;

		public ApiSessionSearchByPersonIdCriteria(long personId)
		{
			_personId = personId;
		}

		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(Session))
				.Add(Restrictions.Eq("SesPersonId", _personId));
		}
	}
}