using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Helpers;
using NIHApp.Implementation.Interfaces;
using NIHApp.RestApi.Helpers;

namespace NIHApp.RestApi.Controllers
{
	public class BaseController : ApiController
	{
		protected readonly IPersonService _personService; // dependency injection, these are loaded on StandardRegistry

        private readonly ISessionService _sessionService;
		private Person _person;

		public BaseController(ISessionService sessionService, IPersonService personService)
		{
			_sessionService = sessionService;
			_personService = personService;
		}
		public string CurrentClientId { get; set; }

		public Session CurrentSession { get; set; }

		protected Person LoggedInUser
		{
			get
			{
				if (_person == null)
				{
					var user = (UserPrincipal)HttpContext.Current.User;
					_person = user.User;
				}
				return _person;
			}
		}

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            // check the api 
            if (controllerContext.Request.RequestUri.OriginalString.IndexOf("F748AE85-9AC5-46A9-B1F7-E76390BB3A85", StringComparison.Ordinal) > 0)
            { 
                if (controllerContext.Request.RequestUri.OriginalString.IndexOf("xerxes=1", StringComparison.Ordinal) > 0)
                {
                    if (controllerContext.Request.RequestUri.OriginalString.IndexOf("uid", StringComparison.Ordinal) > 0)
                    {
                        var personId = Convert.ToInt64(ControllerContext.Request.RequestUri.ParseQueryString().Get("uid"));
                        var person = _personService.GetPersonEntityById(personId);
                        var userPrincipal = new UserPrincipal(person);
                        Thread.CurrentPrincipal = userPrincipal;
                        HttpContext.Current.User = userPrincipal;
                    }
                    HttpContext.Current.Response.AddHeader(NIHAppHeader.SessionValidHeader, "1");
                    return;
                }
            }

			IEnumerable<string> possibleSessionIds;
			var sessionId = string.Empty;
			controllerContext.Request.Headers.TryGetValues("XSessionId", out possibleSessionIds);
			if (possibleSessionIds == null)
				throw new ApiSecurityException();
			var sessionIds = possibleSessionIds as string[] ?? possibleSessionIds.ToArray();
			if (sessionIds.Any())
				sessionId = sessionIds.First();

			IEnumerable<string> possibleClientIds;
			var clientId = string.Empty;
			controllerContext.Request.Headers.TryGetValues("XClientId", out possibleClientIds);
			if (possibleClientIds == null)
				throw new ApiSecurityException();

			var clientIds = possibleClientIds as string[] ?? possibleClientIds.ToArray();
			if (clientIds.Any())
				clientId = clientIds.First();

			if (clientId != string.Empty && sessionId != string.Empty)
			{
				var decrypted = Encryption.DesDecrypt(sessionId);
				var parts = decrypted.Split(char.Parse("|"));
				var _sessionId = long.Parse(parts[0]);
				var sessionKey = parts[1];
				var session = _sessionService.GetSession(sessionKey);
				if (session != null && session.SesDeviceFirebaseToken == clientId)
				{
					if (_sessionId == session.Id)
					{
						CurrentSession = session;
						CurrentClientId = clientId;
						var user = _personService.GetPersonEntityById(session.SesPersonId);
                        var userPrincipal = new UserPrincipal(user);
						Thread.CurrentPrincipal = userPrincipal;
						HttpContext.Current.User = userPrincipal;
						HttpContext.Current.Response.AddHeader(NIHAppHeader.SessionValidHeader, "1");
						return;
					}
				}
			}
			throw new ApiSecurityException();
		}

		protected bool IsValidPersonRequest(long personId)
		{
			//using bypass auth will not create a session
			if (CurrentSession == null)
				return true;
			return CurrentSession.SesPersonId == personId;
		}
	}
}
