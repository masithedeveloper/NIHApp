using System;

namespace NIHApp.Domain.Entities
{
    public class EmailCallResponse
    {
        public bool IsSuccess => CallResult.StartsWith("Sent.");

        public bool IsCallFailure => !CallResult.StartsWith("Sent.");

        public Exception LastException { get; set; }
        public string CallResult { get; set; }
    }
}
