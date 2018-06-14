using System;
using System.Collections.Generic;
using NIHApp.Domain.Entities;

namespace NIHApp.Infrastructure.Interfaces
{
	public interface IDeviceRepository : IRepository<Device>
	{
		Device FindDeviceByCode(string deviceCode);
		IList<Device> FindDevicesByDate(long personId, DateTime dateTime);
		IList<Device> FindDevicesByPersonId(long personId);
	}
}
