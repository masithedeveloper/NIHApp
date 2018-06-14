using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;

namespace NIHApp.Infrastructure.Repositories
{
	public class SessionRepository : Repository<Session>, ISessionRepository
	{
        //----------------------------------------------------------------------------------------------------------------------------------------------------
		public SessionRepository(ISession session) : base(session)
		{
		}
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        public Session GetSessionByKey(string key)
		{
			return FindBySpecification(new SessionKeyCriteria(key)).FirstOrDefault();
		}
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        public IList<Session> FindApiSessionByPersonId(long personId)
		{
			return FindBySpecification(new ApiSessionSearchByPersonIdCriteria(personId)).ToList();
		}
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        // in case of logout, write a method to destroy a sessions find id using the GetSessionByKey
        //----------------------------------------------------------------------------------------------------------------------------------------------------
    }
}