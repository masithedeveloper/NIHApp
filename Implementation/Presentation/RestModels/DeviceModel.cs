using System;
using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Presentation.RestModels
{
	public class DeviceModel
	{
		public DeviceModel(Device device)
		{
			ObjectId = device.Id;
			DeviceCode = device.DevFirebaseToken;
			DeviceDescription = device.DevOSVersion;
			PersonId = device.Person.Id;
			CreateDate = device.CreateDate;
			ModifiedDate = device.ModifiedDate;
		}

		public DeviceModel()
		{
		}

		public long ObjectId { get; set; }
		public string DeviceCode { get; set; }
		public string DeviceDescription { get; set; }
		public string OS { get; set; }
		public long PersonId { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}
