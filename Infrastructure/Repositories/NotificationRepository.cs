using NHibernate;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Infrastructure.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(ISession session) : base(session)
        {
        }

        public IList<Notification> FindNotificationsByParentId(long ParentId)
        {
            return FindBySpecification(new NotificationsSearchByParentIdCriteria(ParentId));
        }
    }
}
