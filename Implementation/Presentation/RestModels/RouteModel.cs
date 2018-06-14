using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Presentation.RestModels
{
    public class RouteModel
    {
        public RouteModel(Route route)
        {
            DeparturePointName = route.DeparturePointName;
            DestinationPointName = route.DestinationPointName;
        }

        public string DeparturePointName { set; get; }
        public string DestinationPointName { set; get; }
    }
}
