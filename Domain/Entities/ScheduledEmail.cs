using System;
using System.Collections.Generic;
using NIHApp.Domain.Enums;
using static System.Char;

namespace NIHApp.Domain.Entities
{
    public class ScheduledEmail : Entity
    {
        public virtual Person Person { get; set; }
        public virtual ScheduledEmailType SchType { get; set; }
        public virtual string SchToEmailAddress { get; set; }
        public virtual string SchCcEmailAddress { get; set; }
        public virtual string SchBccEmailAddress { get; set; }
        public virtual DateTime SchSendAt { get; set; }
        public virtual string SchContent { get; set; }
        public virtual string SchSubject { get; set; }
        public virtual bool SchIsHtml { get; set; }
        public virtual bool SchEmailed { get; set; }
        public virtual bool SchReady { get; set; }
        public virtual int SchFailureCount { get; set; }
        public virtual string SchLastFailureReason { get; set; }
        public virtual string SchFromAddress { get; set; }
        public virtual string SchFromName { get; set; }

        public virtual IEnumerable<string> ToEmailAddressList
        {
            get
            {
                var list = SchToEmailAddress.Split(Parse(";"), Parse(","), Parse("|"));
                return list;
            }
        }

        public virtual IEnumerable<string> CcEmailAddressList
        {
            get
            {
                var list = SchCcEmailAddress.Split(Parse(";"), Parse(","), Parse("|"));
                return list;
            }
        }

        public virtual IEnumerable<string> BccEmailAddressList
        {
            get
            {
                var list = SchBccEmailAddress.Split(Parse(";"), Parse(","), Parse("|"));
                return list;
            }
        }
    }
}
