using System;
using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Presentation.RestModels
{
	public class PersonRegisterModel : PersonModel
	{
		public PersonRegisterModel()
		{

		}

		public PersonRegisterModel(Person person) : base(person)
		{
            Name = person.PerFirstname;
            Surname = person.PerLastname;
            Password = person.PerPassword;
            EmailAddress = person.PerEmail;
		}

		public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string DeviceCode { get; set; }
		public string DeviceDescription { get; set; }
		public string OS { get; set; }
        /*public string PerFirstname { get; set; }
        public string PerLastname { get; set; }
        public string PerFullname { get; set; }
        public string PerEmailAddress { get; set; }
        public DateTime PerDob { get; set; }
        public string PerIdNumber { get; set; }
        public string PerPassword { get; set; }
        public string PerHashPassword { get; set; }
        public bool PerPasswordReset { get; set; }
        public short PerLockCount { get; set; }
        public DateTime? PerLockedAt { get; set; }
        public short PerAccessType { get; set; }
        public short PerVerifyCode { get; set; }
        public bool PerEmailVerified { get; set; }*/
    }
}
