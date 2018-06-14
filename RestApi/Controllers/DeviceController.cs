using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.RestApi.Helpers;

namespace NIHApp.RestApi.Controllers
{
	public class DeviceController : BaseController
	{
		private readonly IDeviceService _deviceService;

		public DeviceController(ISessionService apiSessionService, IPersonService personService, IDeviceService deviceService) : base(apiSessionService, personService)
		{
			_deviceService = deviceService;
		}

		[HttpGet]
		public IList<DeviceModel> GetDevicesByDate(long personId, DateTime modifiedAfter)
		{
			var person = _personService.GetPersonById(personId);
			if (person == null)
				throw new InvalidDataException("Person does not exist.");

			if (!IsValidPersonRequest(personId))
				throw new ApiSecurityException();

			var devicesList = _deviceService.GetDevicesByDate(personId, modifiedAfter);

			return devicesList;
		}

		[HttpPost]
		public DeviceModel Create(DeviceModel deviceModel)
		{
			var person = _personService.GetPersonById(deviceModel.PersonId);
			if (person == null)
				throw new InvalidDataException("Person does not exist.");

			if (!IsValidPersonRequest(deviceModel.PersonId))
				throw new ApiSecurityException();

			var device = _deviceService.CreateDevice(deviceModel);
			return device;
		}

		[HttpPut]
		public DeviceModel Update(DeviceModel deviceModel)
		{
			if (_personService.GetPersonById(deviceModel.PersonId) == null)
				throw new Exception("Person does not exist.");

			if (!IsValidPersonRequest(deviceModel.PersonId))
				throw new ApiSecurityException();

			if (_deviceService.GetDeviceByCode(deviceModel.DeviceCode) == null)
				throw new InvalidDataException("Device does not exist.");

			var device = _deviceService.UpdateDevice(deviceModel);
			if (device == null)
				throw new InvalidDataException("Device update failed.");

			return device;
		}

		[HttpDelete]
		public void Delete(long personId, string deviceCode)
		{
			var person = _personService.GetPersonById(personId);
			if (person == null)
				throw new InvalidDataException("Person does not exist.");

			if (!IsValidPersonRequest(personId))
				throw new ApiSecurityException();

			var device = _deviceService.GetDeviceByCode(deviceCode);
			if (device == null)
				throw new InvalidDataException("Device does not exist.");

			_deviceService.DeleteDevice(device.ObjectId);
		}
	}
}
