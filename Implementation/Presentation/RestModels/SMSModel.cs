using System;
using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Presentation.RestModels
{
	public class SMSModel
    {
		public SMSModel()
		{
		}

        public  string recipientNumber { get; set; }
        public  string message { get; set; }
    }
}