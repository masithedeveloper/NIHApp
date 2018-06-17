using System;
using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;

namespace NIHApp.Infrastructure.Repositories
{
	public class TransportRepository : Repository<Transport>, ITransportRepository
	{
		public TransportRepository(ISession session) : base(session)
		{
		}



        public IList<Transport> FindTransportByDriverId(long driverId)
        {
            return FindBySpecification(new TransportSearchByTransportIdCriteria(driverId));
        }

        public IList<Transport> FindTransportByRegistration(string registration)
        {
            return FindBySpecification(new TransportSearchByRegistrationCriteria(registration));
        }
    }
}
