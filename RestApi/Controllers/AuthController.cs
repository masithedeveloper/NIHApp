using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Results;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Helpers;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Implementation.Services;

namespace NIHApp.RestApi.Controllers
{
	public class AuthController : ApiController
	{
		private readonly ISessionService _sessionService;
		private readonly IAuthenticationService _authenticationService;
		private readonly IPersonService _personService;
		private readonly IDeviceService _deviceService;
	   
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		public AuthController(ISessionService sessionService, IPersonService personService, IAuthenticationService authenticationService, IDeviceService deviceService)
		{
			_sessionService = sessionService;
			_personService = personService;
			_authenticationService = authenticationService;
			_deviceService = deviceService;
		}
       
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
		public AuthModelLight VerifyByPost(AuthModelLight authCredentials)
		{
			try
			{
				var personId = _authenticationService.Validate(authCredentials.EmailAddress, authCredentials.Password);
				var session = CreateApiSession(personId);
				authCredentials.SessionKey = Encryption.DesEncrypt(session.Id + "|" + session.SesKey + "|" + DateTime.Now.Ticks);
				authCredentials.PersonId = personId;
				authCredentials.Desc = "Authorized";
			}
			catch (Exception e)
			{
				authCredentials.PersonId = 0;
				authCredentials.Desc = e.Message;
			}
			return authCredentials;
		}
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private Session CreateApiSession(long personId)
		{
			var currentDeviceId = string.Empty;
			IEnumerable<string> deviceIds;
			Request.Headers.TryGetValues("XClientId", out deviceIds);
			var possibleDeviceIds = deviceIds as string[] ?? deviceIds.ToArray();
			if (possibleDeviceIds.Any())
				currentDeviceId = possibleDeviceIds.First();
			var apiSession = _sessionService.GenerateSession(personId, currentDeviceId);
			return apiSession;
		} 
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPut]
		public AuthModelLight CreatePerson(PersonRegisterModel personRegisterModel)
		{
			try
			{
				string errorInfo;
				var login = _personService.GetPersonByEmail(personRegisterModel.EmailAddress);

				if (login == null) // Initial Registration
				{
					var emailRegex = new Regex("^[A-Za-z0-9_\\+-]+(\\.[A-Za-z0-9_\\+-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*\\.([A-Za-z]{2,4})$");
					if (emailRegex.IsMatch(personRegisterModel.EmailAddress))
					{
						// Do extra information validation here...

						// Create Person
						personRegisterModel = _personService.CreatePerson(personRegisterModel);
						AuthModelLight authModelLight = new AuthModelLight
						{
							Desc = "Pending Registration...",
                            PersonId = personRegisterModel.ObjectId,
							EmailAddress = personRegisterModel.EmailAddress,
							Password = null, //don't need it here
							SessionKey = null // don't need it here
						};

						// Create Device
						/*DeviceModel deviceModel = new DeviceModel
						{
							PersonId = personRegisterModel.ObjectId,
							DeviceCode = personRegisterModel.DeviceCode,
							DeviceDescription = personRegisterModel.DeviceDescription,
							OS = personRegisterModel.OS
						};
						_deviceService.CreateDevice(deviceModel);*/

						return authModelLight;
					}

					errorInfo = ApplicationConfiguration.RegisterEmailInvalid;
				}
				
				else if ((personRegisterModel.ObjectId != 0) && !_personService.IsVerified(personRegisterModel.ObjectId)) // Update Registration and/or Resent Veification Code
				{
					personRegisterModel = _personService.CreatePerson(personRegisterModel);
					AuthModelLight authModelLight = new AuthModelLight
					{
						Desc = "Pending Registration...",
                        PersonId = personRegisterModel.ObjectId,
						EmailAddress = personRegisterModel.Email,
						Password = null,
						SessionKey = null
					};

					return authModelLight;
				}

                else
				{
					errorInfo = ApplicationConfiguration.RegisterEmailDuplicate;
				}

				return new AuthModelLight()
				{
					Desc = errorInfo,
                    PersonId = 0,
					EmailAddress = personRegisterModel.Email,
					Password = null,
					SessionKey = null
				};
			}
			catch (Exception e)
			{
				return new AuthModelLight()
				{
					Desc = e.Message,
                    PersonId = 0,
					EmailAddress = personRegisterModel.Email,
					Password = null,
					SessionKey = null
				};
			}
		}
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPut]
        public StatusCodeResult ForgotPassword(string emailAddress)
        {
            var emailRegex = new Regex("^[A-Za-z0-9_\\+-]+(\\.[A-Za-z0-9_\\+-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*\\.([A-Za-z]{2,4})$");
            if (emailRegex.IsMatch(emailAddress))
            {
                var person = _personService.GetPersonByEmail(emailAddress);
                if (person == null)
                    throw new InvalidDataException("Person does not exist.");
                if (!_personService.IsVerified(person.ObjectId))
                    throw new InvalidDataException(ApplicationConfiguration.RegisterEmailNotVerified);

                _personService.ChangePassword(person.ObjectId);
            }
            else
                throw new InvalidDataException(ApplicationConfiguration.RegisterEmailInvalid);
            return StatusCode(HttpStatusCode.OK);
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPut]
        public AuthModelLight MatchVerifyCode(int verifyCode, AuthModelLight authCredentials)
        {
            var person = _personService.GetPersonById(authCredentials.PersonId);
            if (person == null)
                throw new InvalidDataException("Person does not exist.");

            bool isVerified = _personService.MatchVerifyCode(authCredentials.PersonId, AuthenticationHelper.GetPasswordHash(authCredentials.EmailAddress, authCredentials.Password), verifyCode);
            authCredentials.SessionKey = isVerified.ToString();

            return authCredentials;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}