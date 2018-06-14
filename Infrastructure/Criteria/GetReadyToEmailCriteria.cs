using System;
using NIHApp.Domain.Entities;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Criteria
{
    public class GetReadyToEmailCriteria : ICriteriaSpecification<ScheduledEmail>
    {
        public ICriteria Criteria(ISession session)
        {
            return session.CreateCriteria(typeof(ScheduledEmail))
                 .Add(Restrictions.Eq("Ready", true))
                 .Add(Restrictions.Eq("Emailed", false))
                 .Add(Restrictions.Le("FailureCount", 5))
                 .Add(Restrictions.Le("SendAt", DateTime.Now));
        }
    }
}
