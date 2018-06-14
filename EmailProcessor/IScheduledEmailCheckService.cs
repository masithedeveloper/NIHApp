namespace EmailProcessor
{
    public interface IScheduledEmailCheckService
    {
        void Stop();
        void Start();
    }
}
