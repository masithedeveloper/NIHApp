using NIHApp.Domain.Entities;
using NIHApp.Domain.Enums;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Implementation.Services
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;
        private INotificationService _notificationService;
        private IDeviceRepository _deviceRepository;
        private IPersonService _personService;
        private ISMSService _smsService;

        public EventService(IEventRepository eventRepository, INotificationService notificationService, IDeviceRepository deviceRepository, IPersonService personService, ISMSService smsService)
        {
            _eventRepository = eventRepository;
            _notificationService = notificationService;
            _deviceRepository = deviceRepository;
            _personService = personService;
            _smsService = smsService;
        }

        public EventModel CreateEvent(EventModel eventModel)
        {
            var MessageBody = ""; // SMS/Notification message body
            var Now = DateTime.Now;
            if (eventModel.EvtType.ToString().ToLower().Equals(EventType.PickUp.ToString().ToLower()))
            {   
                if (eventModel.EvtTripFromHome)
                    MessageBody = "Your child has been picked up from home at " + Now;
                else
                    MessageBody = "Your child has been picked up from school at " + Now;
                eventModel.EvtPickUpTime = Now;
            }

            else if (eventModel.EvtType.ToString().ToLower().Equals(EventType.DropOff.ToString().ToLower()))
            {
                if (eventModel.EvtTripFromHome)
                    MessageBody = "Your child has been dropped off from home at " + Now;
                else
                    MessageBody = "Your child has been dropped off from school at " + Now;
                eventModel.EvtDropOffTime = Now;
            }

            var _event = new Event
            {
                EvtParentId = eventModel.EvtParentId,
                EvtDriverId = eventModel.EvtDriverId,
                EvtPickUpTime = eventModel.EvtPickUpTime, // could be null depending on event type
                EvtDropOffTime = eventModel.EvtDropOffTime, // could be null depending on event type
                EvtTripFromHome = eventModel.EvtTripFromHome,
                EvtType = eventModel.EvtType,
                EvtLongitude = eventModel.EvtLongitude, // these depend on the device location service, will be implemented in 2nd phase  
                EvtLatitude = eventModel.EvtLatitude,
                EvtDateCreated = Now,
            };

            using (var transaction = _eventRepository.Session.BeginTransaction())
            {
                _eventRepository.Save(_event);
                transaction.Commit();
            }

            var newEventModel = new EventModel(_event);

            // send a notifocation 

            // check if the device is exist
            IList<Device> devices = _deviceRepository.FindDevicesByPersonId(_event.EvtParentId);
            foreach (DeviceModel device in devices.Select(x => new DeviceModel(x)).ToList()) {
                _notificationService.NotifyAsync(device.DeviceCode, _event.EvtType, MessageBody, newEventModel);
            }
            PersonModel personModel =  _personService.GetPersonById(_event.EvtParentId);
            var smsModel = new SMSModel
            {
                message = MessageBody,
                recipientNumber = personModel.PerCellPhone
            };

            _smsService.sendSingleMessage(smsModel);
            return newEventModel;
        }

        public BillSummaryModel GetTripsBillForTheCurrentMonth(long ParentId)
        {
            DateTime now = DateTime.Now;
            var From = new DateTime(now.Year, now.Month, 1);
            var To = From.AddMonths(1).AddDays(-1);

            var events = _eventRepository.FindEventsByDatesAndParentId(From, To, ParentId);
            return new BillSummaryModel(ParentId, events.Count, From, To);
        }
    }
}
