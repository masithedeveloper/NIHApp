using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Interfaces;
using System.Linq;
using System.Text;

namespace NIHApp.Implementation.Services
{
	public class EmailNotificationService : IEmailNotificationService
	{
		private readonly string _username;
		private readonly string _password;
		private readonly string _server;
		private readonly string _altServer;
		private readonly int _port;
		private readonly bool _enabled;
		private readonly string _url;
		private readonly IApplicationConfiguration _applicationConfiguration;

		public EmailNotificationService(IApplicationConfiguration applicationConfiguration)
		{
			_applicationConfiguration = applicationConfiguration;
			_server = _applicationConfiguration.GetSetting("smtp_server");
			_altServer = _applicationConfiguration.GetSetting("alt_smtp_server");
			_port = int.Parse(_applicationConfiguration.GetSetting("smtp_port"));
			_enabled = bool.Parse(_applicationConfiguration.GetSetting("smtp_enabled"));
			_url = _applicationConfiguration.GetSetting("website_url");
			_username = _applicationConfiguration.GetSetting("smtp_username");
			_password = _applicationConfiguration.GetSetting("smtp_password");
		}

		public EmailCallResponse Send(EmailMessage mailMessage)
		{
			try
			{
				if (mailMessage.IsBodyHtml)
				{
					var plainTextView = AlternateView.CreateAlternateViewFromString(mailMessage.Body.Trim(), new ContentType("text/html; charset=UTF-8"));
					plainTextView.TransferEncoding = TransferEncoding.SevenBit;
					mailMessage.AlternateViews.Add(plainTextView);
				}
				else
					mailMessage.BodyTransferEncoding = TransferEncoding.SevenBit;

				var client = !string.IsNullOrEmpty(_username) ? new SmtpClient(_server, _port) { Credentials = new NetworkCredential(_username, _password) } : new SmtpClient(_server, _port);

				if (RequiresAlternateRouting(mailMessage))
					client = new SmtpClient(_altServer, _port);

				if (_enabled)
				{
					client.Send(mailMessage);
				}
				return new EmailCallResponse { CallResult = "Sent." };
			}
			catch (Exception e)
			{
				return new EmailCallResponse { CallResult = "Failed.", LastException = e };
			}
		}

		private bool RequiresAlternateRouting(EmailMessage mailMessage)
		{
			var toContainsProblematicAddress = mailMessage.To.Count(x => x.Address.ToLower().Contains("@yahoo.com") || x.Address.ToLower().Contains("@aol.com")) > 0;
			var ccContainsProblematicAddress = mailMessage.CC.Count(x => x.Address.ToLower().Contains("@yahoo.com") || x.Address.ToLower().Contains("@aol.com")) > 0;
			var bccContainsProblematicAddress = mailMessage.Bcc.Count(x => x.Address.ToLower().Contains("@yahoo.com") || x.Address.ToLower().Contains("@aol.com")) > 0;
			return toContainsProblematicAddress || ccContainsProblematicAddress || bccContainsProblematicAddress;
		}

		public string GetRegisterMailBody(Person person)
		{
			var sbMail = new StringBuilder();
			sbMail.AppendLine("<h3>VERIFICATION</h3>");
			sbMail.AppendLine("Thank you for downloading the Mediclinic Baby App.<br /><br />");
			sbMail.AppendLine("In order to save your data stored in the App, please enter the code below in the App:<br /><br />");
			sbMail.AppendLine($"<b>Verification Code = {person.PerVerifyCode}</b><br /><br />");
			sbMail.AppendLine("Your email address will be your unique ID.<br /><br />");
			sbMail.AppendLine("We trust you will enjoy the many fantastic benefits that the Mediclinic Baby App has to offer!<br /><br />");
            sbMail.AppendLine("Wishing you many precious moments,<br />");
            sbMail.AppendLine("The Mediclinic Baby Team");
			return sbMail.ToString();
		}

		public string GetForgotPasswordMailBody(Person person, string newPassword)
		{
			var sbMail = new StringBuilder();
			sbMail.AppendLine("Hi " + person.PerFirstname + ",");
			sbMail.AppendLine("<br />");
			sbMail.AppendLine("<br />");
			sbMail.AppendLine("Seems like you've forgotten your password on the NIHApp App.");
			sbMail.AppendLine("<br />");
			sbMail.AppendLine("<br />");
            sbMail.AppendLine("Don't worry, we have assigned a temporary password for you, see below. Once you sign in you will be able to set a new password.");
            sbMail.AppendLine("<br />");
            sbMail.AppendLine("<br />");
            sbMail.AppendLine($"Temporary Password = {newPassword}");
			sbMail.AppendLine("<br />");
			sbMail.AppendLine("<br />");
            sbMail.AppendLine("Wishing you many precious moments,<br />");
            sbMail.AppendLine("The NIHApp Team");
			return sbMail.ToString();
		}

		public string GetRegisterSubject()
		{
			return "NIHApp App Registration";
		}

        public string GetForgotPasswordSubject()
        {
            return "NIHApp App Forgot Password";
        }
    }
}
