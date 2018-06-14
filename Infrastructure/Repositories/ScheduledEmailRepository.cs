using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Util;

namespace NIHApp.Infrastructure.Repositories
{
	public class ScheduledEmailRepository : Repository<ScheduledEmail>, IScheduledEmailRepository
	{
		public ScheduledEmailRepository(ISession session) : base(session)
		{
		}


		public IList<ScheduledEmail> GetScheduledEmailsByPersonId(long personId)
		{
			return FindBySpecification(new GetScheduledEmailByPersonIdCriteria(personId)).ToList();
		}

		public IList<ScheduledEmail> GetReadyToEmail()
		{
			return FindBySpecification(new GetReadyToEmailCriteria()).ToList();
		}
	}
}
