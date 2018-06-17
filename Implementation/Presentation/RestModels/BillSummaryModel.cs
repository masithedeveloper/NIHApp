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
            TotalCost = numberOfTrips * 50; // move to DB 
            From = from;
            To = to;
        }

        private long ParentId { get; set; }
        private int NumberOfTrips { get; set; }
        private double TotalCost { get; set; }
        private double RatePerTrip { get; set; }
        private DateTime From { get; set; }
        private DateTime To { get; set; }
    }
}
