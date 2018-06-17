using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.RestApi.Helpers;

namespace NIHApp.RestApi.Controllers
{
	public class PersonController : BaseController
	{
		public PersonController(ISessionService sessionService, IPersonService personService) : base(sessionService, personService)
		{
		}

		[HttpGet]
		public PersonModel GetPersonById(int personId)
		{
			var person = _personService.GetPersonById(personId);
			if (person == null)
				throw new InvalidDataException("Person does not exist.");

			if (!IsValidPersonRequest(personId))
				throw new ApiSecurityException();
        
			return person;
		}

        [HttpGet]
        [Route("person/GetParentsListByDriverId/{DriverId}")]
        public IList<PersonModel> GetParentsListByDriverId(long DriverId)
        {
            var parentsList = _personService.GetParentsListByDriverId(DriverId);
            if (parentsList == null)
                throw new InvalidDataException("Person does not exist.");

            if (!IsValidPersonRequest(DriverId))
                throw new ApiSecurityException();

            return parentsList;
        }

        [HttpPut]
		public StatusCodeResult ChangePassword(long personId, string newPassword)
		{
            if(CurrentSession.SesPersonId != personId)
                throw new InvalidDataException("Unauthorised access to Person.");
            var person = _personService.GetPersonById(personId);
			if (person == null)
				throw new InvalidDataException("Person does not exist.");
			if (newPassword.Trim().Length < 4)
				throw new InvalidDataException("Invalid New Pasword.");

			_personService.ChangePassword(person.ObjectId, newPassword);
            return StatusCode(HttpStatusCode.OK);
		}

		[HttpPut]
		public PersonModel Update(PersonModel personModel)
		{
			var person = _personService.GetPersonById(personModel.ObjectId);
			if (person == null)
				throw new InvalidDataException("Person does not exist.");

			if (!IsValidPersonRequest(personModel.ObjectId))
				throw new ApiSecurityException();

			personModel = _personService.UpdatePerson(personModel);
			if (personModel == null)
				throw new InvalidDataException("Person update failed.");

			return personModel;
		}

		[HttpDelete]
		public StatusCodeResult Delete(int personId)
		{
			var person = _personService.GetPersonById(personId);
			if (person == null)
				throw new InvalidDataException("Person does not exist.");

			if (!IsValidPersonRequest(personId))
				throw new ApiSecurityException();

			_personService.DeletePerson(personId);

            return StatusCode(HttpStatusCode.OK);
        }
	}
}