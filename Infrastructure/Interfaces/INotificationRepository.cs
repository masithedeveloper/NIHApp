using NIHApp.Domain.Entities;
using System.Collections.Generic;

namespace NIHApp.Infrastructure.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IList<Notification> FindNotificationsByParentId(long ParentId);
    }
}
