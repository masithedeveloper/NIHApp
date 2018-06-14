using System;
using NIHApp.Domain.Interfaces;

namespace NIHApp.Domain.Entities
{
	[Serializable]
	public class Entity : IEntity
	{
		public virtual DateTime? CreateDate { get; set; }
		public virtual DateTime? ModifiedDate { get; set; }
		public virtual long Id { get; set; }

		public virtual bool IsTransient()
		{
			return Id.Equals(default(long));
		}
	}
}