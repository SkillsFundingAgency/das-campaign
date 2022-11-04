using MediatR;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandards
{
    public class GetStandardsQueryHandler : IRequestHandler<GetStandardsQuery, GetStandardsQueryResult>
    {
        private readonly IStandardsRepository _repository;

        public GetStandardsQueryHandler(IStandardsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetStandardsQueryResult> Handle(GetStandardsQuery request, CancellationToken cancellationToken)
        {
            var standards = await _repository.GetStandards();

            return new GetStandardsQueryResult
            {
                Standards = standards.Select(s => new Standard { Title = s.Title, StandardUId = s.StandardUId, Level = s.Level, LarsCode = s.LarsCode }).ToList()
            };
        }
    }
}
