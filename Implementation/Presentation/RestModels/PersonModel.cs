using System;
using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Presentation.RestModels
{
	public class PersonModel
    {
		public PersonModel()
		{
		}

		public PersonModel(Person person)
		{
			PerId = person.Id;
			PerFirstname = person.PerFirstname;
			PerLastname = person.PerLastname;
            PerFullname = person.PerFullname;
            PerEmailAddress = person.PerEmail;
            PerTransportId = person.PerTransportId;
            CreateDate = person.CreateDate;
            ModifiedDate = person.ModifiedDate;
            PerDob = person.PerDob;
            PerIdNumber = PerIdNumber;
            PerPassword = person.PerPassword;
            PerType = person.PerType;
            PerHashPassword = person.PerHashPassword;
            PerPasswordReset = person.PerPasswordReset;
            PerLockCount = person.PerLockCount;
            PerLockedAt = person.PerLockedAt;
            PerAccessType = person.PerAccessType;
            PerVerifyCode = person.PerVerifyCode;
            PerEmailVerified = person.PerEmailVerified;
            PerCellPhone = person.PerCellPhone;
        }
        
		public string PerEmailAddress{ get; set; }
		public long PerTransportId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long PerId { get; set; }
		public string PerFirstname { get; set; }
		public string PerLastname { get; set; }
		public string PerFullname { get; set; }
        public string PerEmail { get; set; }
        public DateTime PerDob { get; set; }
		public string PerIdNumber { get; set; }
        public string PerPassword { get; set; }
        public bool PerType { get; set; }
        public string PerHashPassword { get; set; }
		public bool PerPasswordReset { get; set; }
		public short PerLockCount { get; set; }
		public DateTime? PerLockedAt { get; set; }
		public short PerAccessType { get; set; }
		public short PerVerifyCode { get; set; }
        public bool PerEmailVerified { get; set; }
		public string PerCellPhone { get; set; }
    }
}