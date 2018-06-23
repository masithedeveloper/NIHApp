using System;
using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Presentation.RestModels
{
	public class TransportModel
    {
		public TransportModel()
		{
		}

		public TransportModel(Transport transport)
		{
            Id = transport.Id;
            Make = transport.TraMake;
            Model = transport.TraModel;
            Registration = transport.TraRegistration;
        }

        public  long Id { get; set; }
        public  string Registration { get; set; }
        public  string Make { get; set; }
        public  string Model { get; set; }
    }
}