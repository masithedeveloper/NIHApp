using System.Data.SqlClient;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class GetScheduledEmailByPersonIdCriteria : ICriteriaSpecification<ScheduledEmail>
	{
		private readonly long _personId;
		public GetScheduledEmailByPersonIdCriteria(long personId)
		{
			_personId = personId;
		}

		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(ScheduledEmail))
								  .Add(Restrictions.Eq("Person.Id", _personId))
								  .AddOrder(Order.Desc("CreateDate"));
		}
	}
}
