using System.Security.Principal;

namespace NIHApp.Domain.Entities
{
	public class UserIdentity : IIdentity
	{
		public UserIdentity(Person person)
		{
            Name = person.PerFullname;
            IsAuthenticated = true;
		}

		public string Name { get; }
		public string AuthenticationType { get; }
		public bool IsAuthenticated { get; }
	}
}