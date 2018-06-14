using System.Collections.Generic;
using NIHApp.Domain.Entities;

namespace NIHApp.Infrastructure.Interfaces
{
	public interface IScheduledEmailRepository : IRepository<ScheduledEmail>
	{
		IList<ScheduledEmail> GetScheduledEmailsByPersonId(long personId);
		IList<ScheduledEmail> GetReadyToEmail();
	}
}
