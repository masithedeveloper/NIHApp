using System.Collections.Generic;
using NHibernate;

namespace NIHApp.Infrastructure.Interfaces
{
	public interface IRepository<T>
	{
		ISession Session { get; }
		T Get(long id);
		T Load(long id);
		IList<T> FindAll();
		IList<T> FindAllAndCacheLongTerm();
		long Save(T entity);
		void SaveOrUpdate(T entity);
		void Delete(T entity);
		IList<T> FindBySpecification(ICriteriaSpecification<T> specification);
		IList<T> FindBySpecification(IMultiCriteriaSpecification<T> specification);
		T FindSingleBySpecification(IMultiCriteriaSpecification<T> specification);
		void Update(T entity);
	}
}