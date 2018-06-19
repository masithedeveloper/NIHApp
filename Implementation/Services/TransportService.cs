using System;
using System.Collections.Generic;
using System.Linq;
using NIHApp.Domain.Entities;
using NIHApp.Domain.Enums;
using NIHApp.Implementation.Helpers;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Infrastructure.Criteria;
using NIHApp.Infrastructure.Interfaces;
using NHibernate.Util;

namespace NIHApp.Implementation.Services
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly ISessionRepository _sessionRepository;
        //private readonly Random _random;

        public TransportService(ITransportRepository transportRepository, IDeviceRepository deviceRepository, ISessionRepository sessionRepository)
        {
            _transportRepository = transportRepository;
            _deviceRepository = deviceRepository;
            _sessionRepository = sessionRepository;
        }

        public TransportModel CreateTransport(TransportModel transportModel)
        {
            var transport = new Transport();

            transport.TraMake = transportModel.Make;
            transport.TraModel = transportModel.Model;
            transport.TraRegistration = transportModel.Registration;

            using (var transaction = _transportRepository.Session.BeginTransaction())
            {
                _transportRepository.Save(transport);
                transaction.Commit();
            }

            return new TransportModel(transport);
        }
    }
}

