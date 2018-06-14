using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class SessionKeyCriteria : ICriteriaSpecification<Session>
	{
		private readonly string _sessionKey;

		public SessionKeyCriteria(string sessionKey)
		{
			_sessionKey = sessionKey;
		}

		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(Session))
				.Add(Restrictions.Eq("SesKey", _sessionKey))
				.Add(Restrictions.Eq("SesIsActive", true));
		}
	}
}