using System;

namespace NIHApp.Domain.Entities
{
	[Serializable]
	public class Device : Entity
	{
		public virtual long DevId { get; set; }
		public virtual Person Person { get; set; }
        public virtual string DevFirebaseToken { get; set; }
		public virtual string DevPlatform { get; set; }
        public virtual string DevOSVersion { get; set; }
    }
}
