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

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IList<EventModel> GetEventsByDatesAndParentId(DateTime From, DateTime To, long ParentId)
        {
            var events = _eventRepository.FindEventsByDatesAndParentId(From, To, ParentId);
            return events.Select(x => new EventModel(x)).ToList();
        }
    }
}
