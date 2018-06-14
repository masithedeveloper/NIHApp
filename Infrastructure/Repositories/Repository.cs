using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Interfaces;
using NIHApp.Infrastructure.Helpers;
using NIHApp.Infrastructure.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace NIHApp.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : class, IEntity
	{
		public Repository(ISession session)
		{
			Session = session;
		}

		public ISession Session { get; }

		public T Get(long id)
		{
			return Session.Get<T>(Convert.ToInt64(id));
		}

		public T Load(long id)
		{
			return Session.Load<T>(id);
		}

		public IList<T> FindAll()
		{
			return Session.CreateCriteria(typeof(T)).List<T>();
		}

		public IList<T> FindAllAndCacheLongTerm()
		{
			return
				Session.CreateCriteria(typeof(T))
					.SetCacheable(true)
					.SetCacheMode(CacheMode.Normal)
					.SetCacheRegion("LongTerm")
					.List<T>();
		}

		public long Save(T entity)
		{
			return Convert.ToInt64(Session.Save(entity));
		}

		public IList<T> FindBySpecification(ICriteriaSpecification<T> specification)
		{
			return specification.Criteria(Session).List<T>();
		}

		public IList<T> FindBySpecification(IMultiCriteriaSpecification<T> specification)
		{
			var results = specification.Criteria(Session).List();
			return ListConvert.ToGenericList<T>((IList)results[0]);
		}

		public T FindSingleBySpecification(IMultiCriteriaSpecification<T> specification)
		{
			var results = specification.Criteria(Session).List();
			return (T)((IList<object>)results[0]).First();
		}

		public void Update(T entity)
		{
			Session.Update(entity);
		}

		public void SaveOrUpdate(T item)
		{
			Session.SaveOrUpdate(item);
		}

		public void Delete(T item)
		{
			Session.Delete(item);
		}

		public long FindCountBySpecification(ICriteriaSpecification<T> specification)
		{
			return specification.Criteria(Session).SetProjection(Projections.Count(Projections.Id())).UniqueResult<int>();
		}
	}
}