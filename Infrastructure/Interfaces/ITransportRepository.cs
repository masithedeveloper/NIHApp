using System;
using System.Collections.Generic;
using NIHApp.Domain.Entities;

namespace NIHApp.Infrastructure.Interfaces
{
	public interface ITransportRepository : IRepository<Transport>
	{
		IList<Transport> FindTransportByRegistration(string registration);
		IList<Transport> FindTransportByDriverId(long driverId);
	}
}
