using System.Security.Principal;

namespace NIHApp.Domain.Entities
{
	public class UserPrincipal : IPrincipal
	{
        public UserPrincipal(Person person)
		{
			User = person;
			Identity = new UserIdentity(person);
		}
        
        public Person User { get; }

		public bool IsInRole(string role)
		{
			return role == string.Empty;
		}

		public IIdentity Identity { get; }
	}
}