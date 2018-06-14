using NHibernate;

namespace NIHApp.Infrastructure.Interfaces
{
	public interface ICriteriaSpecification<T>
	{
		ICriteria Criteria(ISession session);
	}
}