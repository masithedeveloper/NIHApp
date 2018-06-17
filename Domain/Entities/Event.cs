using System;

namespace NIHApp.Domain.Entities
{
    public class Event : Entity
    {
        public virtual long EvtID { get; set;}
        public virtual long EvtParentId { get; set; }
        public virtual long EvtDrivertId { get; set; }
        public virtual DateTime EvtPickUpTime { get; set; }
        public virtual DateTime EvtDropOffTime { get; set; }
        public virtual bool EvtTripFromHome { get; set; }
        public virtual string EvtType { get; set; }
        public virtual string EvtLongitude { get; set; }
        public virtual string EvtLatitude { get; set; }
    }
}
