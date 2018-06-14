using System;
using System.Timers;
using log4net;
using NIHApp.Implementation.Interfaces;
using NHibernate;

namespace EmailProcessor
{
	public class ScheduledEmailCheckService : IScheduledEmailCheckService
	{
		private readonly IApplicationConfiguration _appConfig;
		private readonly IScheduledEmailService _scheduledEmailService;
		private readonly Timer _timer;
		private readonly ILog _logger;

		public void Stop()
		{
			_timer.Stop();
		}

		public void Start()
		{
			_timer.Start();
		}

		public ScheduledEmailCheckService(IApplicationConfiguration applicationConfiguration, IScheduledEmailService scheduledEmailService, ILog logger)
		{
			_appConfig = applicationConfiguration;
			_scheduledEmailService = scheduledEmailService;
			_logger = logger;
			_timer = new Timer(2500) { AutoReset = true };
			_timer.Elapsed += (sender, eventArgs) =>
				 {
					 try
					 {
						 _timer.Enabled = false;
						 _logger.Info($"Starting run at {DateTime.Now.ToString()}");
						 var readyToEmail = _scheduledEmailService.GetReadyToEmail();
						 if (readyToEmail.Count > 0)
						 {
							 _logger.Info(string.Format("Processing {0} emails that are ready to send", readyToEmail.Count));
							 foreach (var scheduledEmail in readyToEmail)
							 {
								 _logger.Info(string.Format("Processing scheduled email with Id {0}", scheduledEmail.Id));
								 try
								 {
									 scheduledEmailService.Process(scheduledEmail);
									 _logger.Info(string.Format("Scheduled email {0} sent successfully", scheduledEmail.Id));
								 }
								 catch (Exception exception)
								 {
									 _logger.Error(string.Format("Error sending email for id {0}. No more retries. ", scheduledEmail.Id), exception);
									 //SendAdminNotification(_emailNotificationService, _appConfig.GetSetting("support_address"), string.Format("Error emailing scheduled email {0}", scheduledEmail.Id), exception.Message + Environment.NewLine + Environment.NewLine + exception.StackTrace);
									 /*
									 using (var tx = scheduledEmailRepository.Session.BeginTransaction())
									 {
										  scheduledEmail.FailureCount++;
										  scheduledEmail.LastFailureReason = exception.Message + Environment.NewLine + exception.StackTrace;
										  scheduledEmailRepository.SaveOrUpdate(scheduledEmail);
										  tx.Commit();
									 }

									 if (scheduledEmail.FailureCount >= _appConfig.MaxScheduledEmailFailureRequests)
									 {
										  var message = string.Format("Scheduled email failure for email {0} has failed repeatedly with last exception {1}", scheduledEmail.Id, scheduledEmail.LastFailureReason);
										  SendAdminNotification(_emailNotificationService, _appConfig.GetSetting("support_address"), message, exception.Message + Environment.NewLine + Environment.NewLine + exception.StackTrace);
									 }
									 */
								 }
							 }
						 }
						 //scheduledEmailRepository.Session.Clear();
					 }
					 catch (Exception exception)
					 {
						 if (exception is ADOException)
						 {
							 Program.BurrowsFramework.CloseWorkSpace();
							 Program.BurrowsFramework.InitWorkSpace();
							 Program.SessionFactory = Program.BurrowsFramework.GetSessionFactory("PersistenceUnit1");
						 }
					 }
					 finally
					 {
						 _timer.Enabled = true;
					 }
				 };
		}
	}
}
