using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class PersonSearchByEmailCriteria : ICriteriaSpecification<Person>
	{
		private readonly string _email;

		public PersonSearchByEmailCriteria(string email)
		{
			_email = email;
		}

		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(Person))
				 .Add(Restrictions.Eq("PerEmail", _email).IgnoreCase()); // These need to match the DB column names, not case sensitive
		}
	}
}