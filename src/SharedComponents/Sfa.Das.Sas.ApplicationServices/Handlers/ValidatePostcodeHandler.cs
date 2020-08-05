using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using MediatR;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.ApplicationServices.Services;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    public class ValidatePostcodeHandler : IRequestHandler<ValidatePostcodeQuery, bool>
    {
        private readonly IPostcodeService _postcodeService;

        public ValidatePostcodeHandler(IPostcodeService postcodeService)
        {
            _postcodeService = postcodeService;
        }

        public async Task<bool> Handle(ValidatePostcodeQuery request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(request.Postcode))
            {
                return false;
            }

            var status = await _postcodeService.GetPostcodeStatus(request.Postcode);
            switch (status)
            {
                case "England":
                case "Wales":
                case "Scotland":
                case "Northern Ireland":
                    return true;
                default:
                    return false;
            }
        }
    }
}