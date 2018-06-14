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

        public virtual long Id { get; set; }
        public virtual string Registration { get; set; }
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
    }
}