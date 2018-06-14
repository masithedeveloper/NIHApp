using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using NIHApp.Domain.Entities;
using NIHApp.Domain.Enums;
using NIHApp.Implementation.Interfaces;
using NIHApp.Infrastructure.Interfaces;


namespace NIHApp.Implementation.Services
{
	public class ScheduledEmailService : IScheduledEmailService
	{
		private readonly IScheduledEmailRepository _scheduledEmailRepository;
		private readonly IEmailNotificationService _emailNotificationService;
		private readonly IPersonRepository _personRepository;
		public static int MaxFailureCount = 5;
		public ScheduledEmailService(IScheduledEmailRepository scheduledEmailRepository, IEmailNotificationService emailNotificationService, IPersonRepository personRepository)
		{
			_scheduledEmailRepository = scheduledEmailRepository;
			_emailNotificationService = emailNotificationService;
			_personRepository = personRepository;
		}

		public IList<ScheduledEmail> GetScheduledEmailsByPersonId(long personId)
		{
			var scheduledEmail = _scheduledEmailRepository.GetScheduledEmailsByPersonId(personId);
			return scheduledEmail;
		}

		public IList<ScheduledEmail> GetReadyToEmail()
		{
			return _scheduledEmailRepository.GetReadyToEmail();
		}

		public void Process(ScheduledEmail scheduledEmail)
		{
			var emailMessage = new EmailMessage
			{
				From = new MailAddress(scheduledEmail.SchFromAddress, string.IsNullOrEmpty(scheduledEmail.SchFromName) ? "NIHApp Admin" : scheduledEmail.SchFromName),
				Subject = scheduledEmail.SchSubject,
				Body = scheduledEmail.SchContent,
				IsBodyHtml = scheduledEmail.SchIsHtml
			};

			foreach (var toEmail in scheduledEmail.ToEmailAddressList.Where(toEmail => !string.IsNullOrWhiteSpace(toEmail)))
				emailMessage.To.Add(new MailAddress(toEmail));

			foreach (var ccEmail in scheduledEmail.CcEmailAddressList.Where(ccEmail => !string.IsNullOrWhiteSpace(ccEmail)))
				emailMessage.CC.Add(new MailAddress(ccEmail));

			foreach (var bccEmail in scheduledEmail.BccEmailAddressList.Where(bccEmail => !string.IsNullOrWhiteSpace(bccEmail)))
				emailMessage.Bcc.Add(new MailAddress(bccEmail));

			try
			{
				var emailSendResponse = _emailNotificationService.Send(emailMessage);
				if (emailSendResponse.IsSuccess)
				{
					scheduledEmail.SchEmailed = true;
				}
				else
					throw emailSendResponse.LastException;
			}
			catch (Exception emailException)
			{
				scheduledEmail.SchFailureCount++;
				scheduledEmail.SchLastFailureReason = emailException.Message;
				if (scheduledEmail.SchFailureCount > MaxFailureCount)
					scheduledEmail.SchReady = false;
				scheduledEmail.SchEmailed = false;
			}

			using (var transaction = _scheduledEmailRepository.Session.BeginTransaction())
			{
				_scheduledEmailRepository.SaveOrUpdate(scheduledEmail);
				transaction.Commit();
			}
		}

		public ScheduledEmail CreatScheduledEmail(string fromAddress, string fromName, string toAddress, string ccAddress, string bccAddress, string subject, string content, bool isHtml, ScheduledEmailType type, long personId)
		{
			var person = _personRepository.Get(personId);
			var scheduledEmail = new ScheduledEmail()
			{
				SchFromAddress = fromAddress,
                SchFromName = fromName,
                SchToEmailAddress = toAddress,
                SchCcEmailAddress = ccAddress,
                SchBccEmailAddress = bccAddress,
                SchSubject = subject,
                SchContent = content,
                SchFailureCount = 0,
                SchReady = true,
                SchEmailed = false,
                SchSendAt = DateTime.Now,
                SchType = type,
                SchIsHtml = isHtml,
				Person = person,
				CreateDate = DateTime.Now,
				ModifiedDate = DateTime.Now
			};

			using (var transaction = _scheduledEmailRepository.Session.BeginTransaction())
			{
				_scheduledEmailRepository.SaveOrUpdate(scheduledEmail);
				transaction.Commit();
			}

			return scheduledEmail;
		}
	}
}
