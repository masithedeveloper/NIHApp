namespace NIHApp.Implementation.Presentation.RestModels
{
	public class AuthModelLight
	{
		public string Desc { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public string SessionKey { get; set; }
		public long PersonId { get; set; }
	}
}