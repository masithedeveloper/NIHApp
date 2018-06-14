using System;
using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Util;

namespace NIHApp.Infrastructure.Repositories
{
	public class DeviceRepository : Repository<Device>, IDeviceRepository
	{
		public DeviceRepository(ISession session) : base(session)
		{
		}

		public Device FindDeviceByCode(string deviceCode)
		{
			return (Device)FindBySpecification(new DevicesSearchByCodeCriteria(deviceCode)).FirstOrNull();
		}

		public IList<Device> FindDevicesByDate(long personId, DateTime dateTime)
		{
			return FindBySpecification(new DevicesSearchByDateCriteria(personId, dateTime));
		}

		public IList<Device> FindDevicesByPersonId(long personId)
		{
			return FindBySpecification(new DevicesSearchByPersonIdCriteria(personId)).ToList();
		}
	}
}
