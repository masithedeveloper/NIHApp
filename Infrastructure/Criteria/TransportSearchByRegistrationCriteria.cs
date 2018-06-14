using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class TransportSearchByRegistrationCriteria : ICriteriaSpecification<Transport>
	{
		private readonly string _registration;

		public TransportSearchByRegistrationCriteria(string registration)
		{
            _registration = registration;
		}

		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(Transport))
				 .Add(Restrictions.Eq("TraRegistration", _registration).IgnoreCase()); // These need to match the DB column names, not case sensitive
		}
	}
}