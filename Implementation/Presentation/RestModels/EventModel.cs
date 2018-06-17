using NIHApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Implementation.Presentation.RestModels
{
    public class EventModel
    {   
        public EventModel() {
        }

        public EventModel(Event _event)
        {
            EvtID = _event.EvtID;
            EvtParentId = _event.EvtParentId;
            EvtDrivertId = _event.EvtDrivertId;
            EvtPickUpTime = _event.EvtPickUpTime;
            EvtDropOffTime = _event.EvtDropOffTime;
            EvtTripFromHome = _event.EvtTripFromHome;
            EvtType = _event.EvtType;
            EvtLongitude = _event.EvtLongitude;
            EvtLatitude = _event.EvtLatitude;
        }

        public long EvtID { get; set; }
        public long EvtParentId { get; set; }
        public long EvtDrivertId { get; set; }
        public DateTime EvtPickUpTime { get; set; }
        public DateTime EvtDropOffTime { get; set; }
        public bool EvtTripFromHome { get; set; }
        public string EvtType { get; set; }
        public string EvtLongitude { get; set; }
        public string EvtLatitude { get; set; }
    }
}
