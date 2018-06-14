using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class TransportSearchByTransportIdCriteria : ICriteriaSpecification<Transport>
	{
		private readonly long _TransportId;

		public TransportSearchByTransportIdCriteria(long TransportId)
		{
            _TransportId = TransportId;
		}

		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(Transport))
				 .Add(Restrictions.Eq("TraRegistration", _TransportId).IgnoreCase()); // These need to match the DB column names, not case sensitive
		}
	}
}