namespace NIHApp.Implementation.Interfaces
{
	public interface IApplicationConfiguration
	{
		string LocalTimeZone { get; }
		long MaxAllowedCallFailures { get; }
		string GetSetting(string key);
        //int MaxScheduledEmailFailureRequests { get; }
    }
}