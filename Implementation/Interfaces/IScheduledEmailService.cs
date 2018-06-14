using System.Collections.Generic;
using NIHApp.Domain.Entities;
using NIHApp.Domain.Enums;

namespace NIHApp.Implementation.Interfaces
{
	public interface IScheduledEmailService
	{
		IList<ScheduledEmail> GetScheduledEmailsByPersonId(long personId);
		IList<ScheduledEmail> GetReadyToEmail();
		void Process(ScheduledEmail scheduledEmail);

		ScheduledEmail CreatScheduledEmail(string fromAddress, string fromName, string toAddress, string ccAddress,
			 string bccAddress, string subject, string content, bool isHtml, ScheduledEmailType type, long personId);
	}
}
