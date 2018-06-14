﻿using NIHApp.Implementation.Presentation.RestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Implementation.Interfaces
{
    public interface INotificationService
    {
        IList<NotificationModel> GetNotificationsByParentId(long parentId);
    }
}
