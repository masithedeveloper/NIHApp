using System;
using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;

namespace NIHApp.Infrastructure.Repositories
{
	public class PersonRepository : Repository<Person>, IPersonRepository
	{
		public PersonRepository(ISession session) : base(session)
		{
		}

        public IList<Person> FindParentsListByDriverId(long driverId)
        {
            return FindBySpecification(new ParentsSerchByDriverCriteria(driverId));
        }

        public IList<Person> FindPersonByEmail(string email)
		{
			return FindBySpecification(new PersonSearchByEmailCriteria(email));
		}

		public IList<Person> FindPersonsByDate(DateTime modifiedAt, long personId)
		{
			return FindBySpecification(new PersonSearchByDateCriteria(personId, modifiedAt));
		}

	}
}
