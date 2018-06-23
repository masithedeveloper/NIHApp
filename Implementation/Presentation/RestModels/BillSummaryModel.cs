using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Implementation.Presentation.RestModels
{
    public class BillSummaryModel
    {
        public BillSummaryModel()
        { }

        public BillSummaryModel(long parentId, int numberOfTrips, DateTime from, DateTime to)
        {
            ParentId = parentId;
            NumberOfTrips = numberOfTrips;
            RatePerTrip = 50;
            TotalCost = numberOfTrips * RatePerTrip; // move to DB 
            From = from;
            To = to;
        }

        public long ParentId { get; set; }
        public int NumberOfTrips { get; set; }
        public double TotalCost { get; set; }
        public double RatePerTrip { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
