using System;
using NIHApp.Domain.Interfaces;

namespace NIHApp.Domain.Entities
{
	[Serializable]
	public class EntityIsolated : IEntity
	{
		public EntityIsolated()
		{
			Id = 0;
		}

		public virtual string Key { get; set; }
		public virtual long Id { get; private set; }

		public virtual bool IsTransient()
		{
			return Id.Equals(default(long));
		}
	}
}