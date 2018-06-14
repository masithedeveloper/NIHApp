using System;
using NIHApp.Domain.Enums;

namespace NIHApp.Domain.Entities
{
	public class Session : EntityIsolated
	{
		internal Session()
		{
		}

		public Session(string sesDeviceFirebaseToken, string sesKey, long sesPersonId, int sesTimeLimitInhours)
		{
            SesDeviceFirebaseToken = sesDeviceFirebaseToken;
            SesKey = sesKey;
            SesPersonId = sesPersonId;
			SesIsActive = true;
			CreateDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            SesValidDate = DateTime.Now.AddHours(sesTimeLimitInhours);
		}

		//public virtual long SessionId { get; set; }
		public virtual string SesDeviceFirebaseToken { get; set; }
		public virtual string SesKey { get; set; }
		public virtual long SesPersonId { get; set; }
		public virtual bool SesIsActive { get; set; }
		public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual DateTime SesValidDate { get; set; }

		public override string Key => "Session";
	}
}