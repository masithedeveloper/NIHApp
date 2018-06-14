using System;

namespace NIHApp.Domain.Entities
{
    public class Notification : Entity
    {
        public virtual int NotId { get; set; }
        public virtual string NotMessage { get; set; }
        public virtual bool NotIsSent { get; set; }
        public virtual int NotPersonId { get; set; }
        public virtual DateTime NotTimeCreated { get; set; }
    }
}