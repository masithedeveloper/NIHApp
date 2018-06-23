using NIHApp.Implementation.Presentation.RestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Implementation.Interfaces
{
    public interface ISMSService
    {
        string getAccountBalance();
        string sendSingleMessage(SMSModel sms);
        string sendBulkMessage(List<SMSModel> smsList);
        string transferCredits(CreditTransferModel creditTransferModel);
    }
}
