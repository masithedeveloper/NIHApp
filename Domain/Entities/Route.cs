using System;
using NIHApp.Domain.Enums;

namespace NIHApp.Domain.Entities
{
	public class Route : EntityIsolated
	{
		internal Route()
		{
		}

		public Route(string _DeparturePointName, string _DestinationPointName)
		{
            DeparturePointName = _DeparturePointName;
            DestinationPointName = _DestinationPointName;
		}

        public virtual string DeparturePointName { set; get; }
        public virtual string DestinationPointName { set; get; }
    }
}