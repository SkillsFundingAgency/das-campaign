using Moq;
using NUnit.Framework;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Application.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using SFA.DAS.Campaign.Infrastructure.Mappers;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Infrastructure.Services;
using SFA.DAS.Campaign.Infrastructure.Models;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Repositories
{
    public class WhenGettingAStandardFromTheService
    {
        private StandardsRepository _standardsRepository;

        //Arrange
        string _routeId = "1";
        string _cachedKey = "FullStandardsAPI";

        [SetUp]
        public void Arrange()
        {
           
            _standardsRepository = new StandardsRepository();
        }

        
    }
}
