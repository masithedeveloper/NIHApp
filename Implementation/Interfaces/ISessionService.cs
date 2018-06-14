using NIHApp.Domain.Entities;
using NIHApp.Implementation.Presentation.RestModels;

namespace NIHApp.Implementation.Interfaces
{
	public interface ISessionService
	{
		Session GenerateSession(long personId, string deviceCode);
		Session GetSession(string key);
	}
}