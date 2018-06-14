namespace NIHApp.Domain.Interfaces
{
	public interface IEntity
	{
		long Id { get; }
		bool IsTransient();
	}
}