using System;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
	public class DevicesSearchByCodeCriteria : ICriteriaSpecification<Device>
	{
		private readonly string _deviceCode;

		public DevicesSearchByCodeCriteria(string deviceCode)
		{
			_deviceCode = deviceCode;
		}
		public ICriteria Criteria(ISession session)
		{
			return session.CreateCriteria(typeof(Device))
				 .Add(Restrictions.Eq("DeviceCode", _deviceCode));
		}
	}
}
