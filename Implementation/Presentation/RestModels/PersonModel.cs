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
			ObjectId = person.Id;
			Name = person.PerFirstname;
            Email = person.PerEmail;
		}

		public long ObjectId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}