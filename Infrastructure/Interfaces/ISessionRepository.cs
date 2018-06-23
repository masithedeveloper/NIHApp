using System.Collections.Generic;
using NIHApp.Domain.Entities;

namespace NIHApp.Infrastructure.Interfaces
{
	public interface ISessionRepository : IRepository<Session>
	{
		Session GetSessionByKey(string key);

		IList<Session> FindApiSessionByPersonId(long personId);
	}
}