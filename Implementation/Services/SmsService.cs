using Newtonsoft.Json;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Infrastructure.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Implementation.Services
{
    public class SMSService : ISMSService
    {
        string email_address = "nihapp01@gmail.com";
        string token = "1b0fc83b-ba74-4ef7-b714-9a581ddb28f7";

        public SMSService(){

        }
        public string getAccountBalance()
        {

            var client = new RestClient();
            string zoomBaseUrl = "https://www.zoomconnect.com/app/";
            string zoomApiMethod = "api/rest/v1/account/balance";

            client.BaseUrl = new Uri(zoomBaseUrl);

            var request = new RestRequest(zoomApiMethod, Method.GET);
            request.AddHeader("email", email_address);
            request.AddHeader("token", token);

            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content; // raw content as string

            return content;
        }

        public string sendSingleMessage(SMSModel sms)
        {

            var client = new RestClient();
            string zoomBaseUrl = "https://zoomconnect.com/app";
            string zoomApiMethod = "/api/rest/v1/sms/send.json";

            client.BaseUrl = new Uri(zoomBaseUrl);

            var request = new RestRequest(zoomApiMethod, Method.POST);
            request.AddHeader("email", email_address);
            request.AddHeader("token", token);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(sms);

            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content; // raw content as string

            return content;
        }

        public string sendBulkMessage(List<SMSModel> smsList)
        {

            var client = new RestClient();
            string zoomBaseUrl = "https://zoomconnect.com/app";
            string zoomApiMethod = "/api/rest/v1/sms/send-bulk.json";

            client.BaseUrl = new Uri(zoomBaseUrl);

            var request = new RestRequest(zoomApiMethod, Method.POST);
            request.AddHeader("email", email_address);
            request.AddHeader("token", token);

            Console.Write(smsList);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { sendSmsRequests = smsList });

            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content; // raw content as string

            return content;
        }

        public string transferCredits(CreditTransferModel creditTransferModel)
        {

            var client = new RestClient();
            string zoomBaseUrl = "https://zoomconnect.com/app";
            string zoomApiMethod = "/api/rest/v1/account/transfer";

            client.BaseUrl = new Uri(zoomBaseUrl);

            var request = new RestRequest(zoomApiMethod, Method.POST);
            request.AddHeader("email", email_address);
            request.AddHeader("token", token);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(creditTransferModel);

            RestResponse response = (RestResponse)client.Execute(request);

            return response.StatusCode.ToString();
        }

  
    }
}