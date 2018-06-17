using NIHApp.Domain.Entities;
using NIHApp.Implementation.Presentation.RestModels;

namespace NIHApp.Implementation.Interfaces
{
	public interface ITransportService
    {
        TransportModel CreateTransport(TransportModel transportAModel);
    }
}