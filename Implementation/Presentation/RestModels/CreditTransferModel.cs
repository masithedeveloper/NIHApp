using System;
using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Presentation.RestModels
{
	public class CreditTransferModel
    {
		public CreditTransferModel()
		{
		}

        public  string transferFromEmailAddress { get; set; }
        public  string transferToEmailAddress { get; set; }
        public  int numberOfCreditsToTransfer { get; set; }


    }
}