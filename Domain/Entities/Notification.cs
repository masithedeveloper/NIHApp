using System;

namespace NIHApp.Domain.Entities
{
    public class Notification : Entity
    {
        public virtual long NotId { get; set; }
        public virtual string NotMessage { get; set; }
        public virtual bool NotIsSent { get; set; }
        public virtual long NotPersonId { get; set; }
        public virtual DateTime NotTimeCreated { get; set; }
        public virtual long NotEventId { get; set; }
        
    }
}