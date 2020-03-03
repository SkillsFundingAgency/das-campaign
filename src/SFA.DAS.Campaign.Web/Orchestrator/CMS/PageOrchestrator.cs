using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using SFA.DAS.Campaign.Web.Configuration;
using SFA.DAS.Campaign.Web.Models.CMS;

namespace SFA.DAS.Campaign.Web.ViewModels.CMS
{
    public class PageOrchestrator : IPageOrchestrator
    {
        private readonly IContentfulClient _client;

        public PageOrchestrator(IContentfulClient client)
        {
            _client = client;
            _client.ContentTypeResolver = new ModulesResolver();
        }

        public async Task<PageViewModel> Get(string slug)
        {
            var model = new PageViewModel();

            var queryBuilder = QueryBuilder<Page>.New.ContentTypeIs("page").FieldEquals(f => f.Slug, slug).Include(4).LocaleIs("en-US");
            var page = (await _client.GetEntries(queryBuilder)).FirstOrDefault();

            model.Page = page;

            var systemProperties = new List<SystemProperties> { model.Page.Sys };
            if (model.Page.Modules != null && model.Page.Modules.Any())
            {
                systemProperties.AddRange(model.Page.Modules?.Select(c => c.Sys));
            }
            model.SystemProperties = systemProperties;

            return model;
        }

    }

    public interface IPageOrchestrator
    {
        Task<PageViewModel> Get(string slug);
    }
}
