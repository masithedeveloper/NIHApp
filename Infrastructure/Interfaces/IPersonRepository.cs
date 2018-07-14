using System;
using System.Collections.Generic;
using NIHApp.Domain.Entities;

namespace NIHApp.Infrastructure.Interfaces
{
	public interface IPersonRepository : IRepository<Person>
	{
		IList<Person> FindPersonByEmail(string email);
		IList<Person> FindPersonsByDate(DateTime modifiedAt, long personId);
        IList<Person> FindParentsListByDriverId(long driverId);
        IList<Person> FindDriversByType();
    }
}
