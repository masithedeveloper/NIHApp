using System;
using System.Collections.Generic;
using NIHApp.Implementation.Presentation.RestModels;

namespace NIHApp.Implementation.Interfaces
{
	public interface IDeviceService
	{
		DeviceModel GetDeviceByCode(string deviceCode);
		IList<DeviceModel> GetDevicesByDate(long personId, DateTime dateTime);
		IList<DeviceModel> GetDevicesByPersonId(long personId);
		DeviceModel CreateDevice(DeviceModel deviceModel);
		DeviceModel UpdateDevice(DeviceModel deviceModel);
		void DeleteDevice(long deviceId);
	}
}
