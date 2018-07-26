using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.RestApi.Helpers;

namespace NIHApp.RestApi.Controllers
{
	public class EventController : ApiController
	{
		private readonly IEventService _eventService;
        private readonly IPersonService _personService;

        public EventController(ISessionService apiSessionService, IPersonService personService, IEventService eventService)// : base(apiSessionService, personService)
		{
            _eventService = eventService;
            _personService = personService;
        }

		[HttpGet]
		public BillSummaryModel GetTripsBillForTheCurrentMonth(long parentId)
		{
			var person = _personService.GetPersonById(parentId);
			if (person == null)
				throw new InvalidDataException("Parent does not exist.");

			/*if (!IsValidPersonRequest(parentId))
				throw new ApiSecurityException();
            */
			return _eventService.GetTripsBillForTheCurrentMonth(parentId);
		}

		[HttpPost]
		public EventModel Create(EventModel eventModel)
		{
            // check for driver
			var driver = _personService.GetPersonById(eventModel.EvtDriverId);
			if (driver == null)
				throw new InvalidDataException("Driver does not exist.");

			/*if (!IsValidPersonRequest(eventModel.EvtDriverId))
				throw new ApiSecurityException();*/

            // check for parent
            var parent = _personService.GetPersonById(eventModel.EvtParentId);
            if (parent == null)
                throw new InvalidDataException("parent does not exist.");
            
            var _event = _eventService.CreateEvent(eventModel); // create event service 

			return _event;
		}
	}
}
