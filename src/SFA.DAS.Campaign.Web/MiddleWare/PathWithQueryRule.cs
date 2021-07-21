using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace SFA.DAS.Campaign.Web.MiddleWare
{
    public class PathWithQueryRule : IRule
    {
        private readonly string _requestRegexPattern;
        private readonly string _replacement;
        private readonly List<string> _originQueryParams;

        public PathWithQueryRule(string requestRegexPattern, string replacement, List<string> originQueryParams)
        {
            _requestRegexPattern = requestRegexPattern;
            _replacement = replacement;
            _originQueryParams = originQueryParams;
        }

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Path.StartsWithSegments(new PathString(_replacement)))
            {
                return;
            }

            if (!Regex.Match(request.Path.Value, _requestRegexPattern, RegexOptions.IgnoreCase).Success)
            {
                return;
            }

            var location = _replacement;

            for (var i = 0; i < _originQueryParams.Count; i++)
            {
                var queryStringValues = request.Query[_originQueryParams[i]];

                if (queryStringValues.Count == 1)
                {
                    var queryStringValue = queryStringValues.FirstOrDefault();

                    if (!string.IsNullOrEmpty(queryStringValue))
                    {
                        location = location.Replace($"${i}", queryStringValue);
                    }
                }
                else if (queryStringValues.Count > 1)
                {
                    var lastAmpIndex = location.LastIndexOf("&", StringComparison.CurrentCultureIgnoreCase);
                    var valIndex = location.IndexOf($"${i}", StringComparison.CurrentCultureIgnoreCase);

                    var subStringVal = location.Substring(lastAmpIndex, valIndex - lastAmpIndex);
                    var newQueryListParam = queryStringValues.Aggregate("", (current, stringValue) => current + subStringVal + stringValue);
                    location = location.Replace($"{subStringVal}${i}", newQueryListParam);
                }
            }

            while (location.Contains("$"))
            {
                var index = location.IndexOf("$", StringComparison.CurrentCultureIgnoreCase);
                location = location.Remove(index, 2);
            }


            var response = context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.PermanentRedirect;
            context.Result = RuleResult.EndResponse;
            response.Headers[HeaderNames.Location] = location;

        }
    }
}
