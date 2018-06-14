using System;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Helpers;
using NIHApp.Implementation.Interfaces;
using NIHApp.Infrastructure.Interfaces;

namespace NIHApp.Implementation.Services
{
	public class SessionService : ISessionService
	{
		private readonly ISessionRepository _apiSessionRepository;

		public SessionService(ISessionRepository apiSessionRepository)
		{
			_apiSessionRepository = apiSessionRepository;
		}

		public Session GenerateSession(long personId, string deviceCode)
		{
			var sessionKey = MD5Hasher.GetMd5Hash(Guid.NewGuid().ToString());
			var session = new Session(deviceCode, sessionKey, personId, 87360); // 10 Years
			_apiSessionRepository.Save(session);
			return session;
		}

		public Session GetSession(string key)
		{
			return _apiSessionRepository.GetSessionByKey(key);
		}
	}
}