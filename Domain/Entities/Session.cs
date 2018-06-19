using System;
using NIHApp.Domain.Enums;

namespace NIHApp.Domain.Entities
{
    public class Session : EntityIsolated
    {
        internal Session()
        {
        }

        public Session(string sesDeviceActive, string sesKey, long sesPersonId, int sesTimeLimitInhours)
        {
            SesDeviceActive = sesDeviceActive;
            SesKey = sesKey;
            SesPersonId = sesPersonId;
            SesIsActive = true;
            SesCreatedDate = DateTime.Now;
            SesModifiedDate = DateTime.Now;
            SesValidDate = DateTime.Now.AddHours(sesTimeLimitInhours);
        }

        public virtual long SesId { get; set; }
        public virtual string SesDeviceActive { get; set; }
        public virtual string SesKey { get; set; }
        public virtual long SesPersonId { get; set; }
        public virtual bool SesIsActive { get; set; }
        public virtual DateTime SesCreatedDate { get; set; }
        public virtual DateTime SesValidDate { get ; set; }
        public virtual DateTime SesModifiedDate { get; set; }
        public override string Key => "Session";
    }
}