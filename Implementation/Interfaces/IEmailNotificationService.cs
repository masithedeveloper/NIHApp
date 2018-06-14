using NIHApp.Domain.Entities;

namespace NIHApp.Implementation.Interfaces
{
	public interface IEmailNotificationService
	{
		EmailCallResponse Send(EmailMessage emailMessage);
		string GetRegisterMailBody(Person person);
		string GetForgotPasswordMailBody(Person person, string newPassword);
		string GetRegisterSubject();
	    string GetForgotPasswordSubject();
	}
}
