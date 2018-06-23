using System;
using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Infrastructure.Interfaces;

namespace NIHApp.Implementation.Services
{
	public class DeviceService : IDeviceService
	{
		private readonly IDeviceRepository _deviceRepository;
		private readonly IPersonRepository _personRepository;

		public DeviceService(IDeviceRepository deviceRepository, IPersonRepository personRepository)
		{
			_deviceRepository = deviceRepository;
			_personRepository = personRepository;
		}

		public DeviceModel GetDeviceByCode(string deviceCode)
		{
			var device = _deviceRepository.FindDeviceByCode(deviceCode);
			var deviceModel = new DeviceModel(device);
			return deviceModel;
		}

		public IList<DeviceModel> GetDevicesByDate(long personId, DateTime dateTime)
		{
			var personDevices = _deviceRepository.FindDevicesByDate(personId, dateTime);
			return personDevices.Select(x => new DeviceModel(x)).ToList();
		}

		public IList<DeviceModel> GetDevicesByPersonId(long personId)
		{
			var personDevices = _deviceRepository.FindDevicesByPersonId(personId);
			return personDevices.Select(x => new DeviceModel(x)).ToList();
		}

		public DeviceModel CreateDevice(DeviceModel deviceModel)
		{
			var newDevice = new Device()
			{
				DevFirebaseToken = deviceModel.DeviceCode,
				Person = _personRepository.Get(deviceModel.PersonId),
				DevOSVersion = deviceModel.DeviceDescription,
				DevPlatform = deviceModel.OS,
				CreateDate = DateTime.Now,
				ModifiedDate = DateTime.Now
			};

			using (var transaction = _deviceRepository.Session.BeginTransaction())
			{
				_deviceRepository.Save(newDevice);
				transaction.Commit();
			}

			deviceModel = new DeviceModel(newDevice);

			return deviceModel;
		}

		public DeviceModel UpdateDevice(DeviceModel deviceModel)
		{
			var updatedDevice = _deviceRepository.Get(deviceModel.ObjectId);

			updatedDevice.Person = _personRepository.Get(deviceModel.PersonId);
			updatedDevice.DevFirebaseToken = deviceModel.DeviceDescription;
			updatedDevice.DevOSVersion = deviceModel.OS;
			updatedDevice.ModifiedDate = deviceModel.ModifiedDate; // DateTime.Now

			using (var transaction = _deviceRepository.Session.BeginTransaction())
			{
				_deviceRepository.SaveOrUpdate(updatedDevice);
				transaction.Commit();
			}

			deviceModel = new DeviceModel(updatedDevice);

			return deviceModel;
		}

		public void DeleteDevice(long deviceId)
		{
			var deletedDevice = _deviceRepository.Get(deviceId);
            using (var transaction = _deviceRepository.Session.BeginTransaction())
            {
                _deviceRepository.Delete(deletedDevice);
                transaction.Commit();
            }
        }
	}
}
