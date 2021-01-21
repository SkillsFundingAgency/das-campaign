﻿using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Core.Domain.Services;
using Sfa.Das.Sas.Infrastructure.Mapping;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Apprenticeships.Api.Types.Exceptions;
using SFA.DAS.NLog.Logger;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Sfa.Das.Sas.Infrastructure.Repositories
{
    public sealed class StandardApiRepository : IGetStandards
    {
        private readonly IStandardMapping _standardMapping;
        private readonly IStandardApiClient _standardApiClient;
        private readonly ILog _applicationLogger;

        public StandardApiRepository(
            IStandardMapping standardMapping,
            IStandardApiClient standardApiClient,
            ILog applicationLogger)
        {
            _standardMapping = standardMapping;
            _standardApiClient = standardApiClient;
            _applicationLogger = applicationLogger;
        }

        public Standard GetStandardById(string id)
        {
            try
            {
                var result = _standardApiClient.Get(id);
                return _standardMapping.MapToStandard(result);
            }
            catch (EntityNotFoundException ex)
            {
                _applicationLogger.Info($"404 trying to get standard with id {id}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Failed to get standard with id {id}", ex);
            }
        }
        
        public IEnumerable<Standard> GetStandardsByIds(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Standard> GetAllStandards()
        {
            throw new NotImplementedException();
        }

        public long GetStandardsAmount()
        {
            throw new NotImplementedException();
        }

        public long GetStandardsOffer()
        {
            throw new NotImplementedException();
        }
    }
}