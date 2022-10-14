using MediatR;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandardByStandardUId
{
    public class GetStandardQueryHandler : IRequestHandler<GetStandardQuery, GetStandardQueryResult>
    {
        private readonly IStandardsRepository _repository;

        public GetStandardQueryHandler(IStandardsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetStandardQueryResult> Handle(GetStandardQuery request, CancellationToken cancellationToken)
        {
            var standard = await _repository.GetStandard(request.StandardUId);

            return new GetStandardQueryResult
            {
                Title = standard.Title,
                StandardUId = request.StandardUId,
                LarsCode = standard.LarsCode,
                Level = standard.Level,
                TimeToComplete = standard.TimeToComplete,
                MaxFundingAvailable = standard.MaxFundingAvailable
            };
        }
    }
}
