using System;
using System.Collections.Generic;

namespace NIHApp.Domain.Entities
{
	[Serializable]
	public class Person : Entity
	{
        
        public Person()
		{
			Sessions = new List<Session>();
			Devices = new List<Device>();
		}
        
        public virtual long PerId { get; set; }
		public virtual string PerFirstname { get; set; }
		public virtual string PerLastname { get; set; }
		public virtual string PerFullname { get; set; }
        public virtual string PerEmail { get; set; }
        public virtual DateTime PerDob { get; set; }
		public virtual string PerIdNumber { get; set; }
        public virtual string PerPassword { get; set; }
        public virtual bool PerType { get; set; }
        public virtual string PerHashPassword { get; set; }
		public virtual bool PerPasswordReset { get; set; }
		public virtual short PerLockCount { get; set; }
		public virtual DateTime? PerLockedAt { get; set; }
		public virtual short PerAccessType { get; set; }
		public virtual short PerVerifyCode { get; set; }
        public virtual bool PerEmailVerified { get; set; }
        public virtual IList<Session> Sessions { get; set; }
		public virtual IList<Device> Devices { get; set; }
		public virtual int PerTransportId { get; set; }
		public virtual string PerCellPhone { get; set; }
		public virtual bool PerIsLocked
		{
			get { return PerLockedAt != null; }
		}

		public virtual void CheckLockExpiration(int lockTimeout)
		{
			if (PerLockedAt != null && DateTime.Now.Subtract(PerLockedAt.Value).TotalMinutes >= lockTimeout)
			{
				Unlock();
			}
		}

		public virtual void Unlock()
		{
            PerLockCount = 0;
			PerLockedAt = null;
		}

		public virtual void Lock()
		{
			PerLockedAt = DateTime.Now;
		}
	}
}