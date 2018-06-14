using NHibernate;

namespace NIHApp.Infrastructure.Interfaces
{
	public interface IMultiCriteriaSpecification<T>
	{
		IMultiCriteria Criteria(ISession session);
	}
}