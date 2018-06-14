using NIHApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace NIHApp.Infrastructure.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        IList<Event> FindEventsByDatesAndParentId(DateTime From, DateTime To, long ParentId);
    }
}
