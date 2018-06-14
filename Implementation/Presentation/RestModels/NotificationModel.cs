using NIHApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Implementation.Presentation.RestModels
{
    public class NotificationModel
    {
        public NotificationModel(Notification notification )
        {
            ObjectId = notification.NotId;
            NotMessage = notification.NotMessage;
            NotIsSent = notification.NotIsSent;
            NotPersonId = notification.NotPersonId;
            NotTimeCreated = notification.NotTimeCreated;
        }

        public NotificationModel()
        {

        }

        public long ObjectId { get; set; }
        public string NotMessage { get; set; }
        public bool NotIsSent { get; set; }
        public int NotPersonId { get; set; }
        public DateTime NotTimeCreated { get; set; }
    }
}
