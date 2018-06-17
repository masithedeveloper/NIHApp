using NIHApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIHApp.Infrastructure.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IList<Notification> FindNotificationsByParentId(long ParentId);
    }
}
