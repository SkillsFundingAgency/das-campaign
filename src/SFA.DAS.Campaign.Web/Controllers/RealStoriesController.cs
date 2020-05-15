using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("real-stories")]
    public class RealStoriesController : Controller
    {
        private readonly IContentfulClient _contentfulClient;

        public RealStoriesController(IContentfulClient contentfulClient)
        {
            _contentfulClient = contentfulClient;
        }
        
        [Route("apprentice")]
        public async Task<IActionResult> Apprentice()
        {
            var realStories = await _contentfulClient.GetEntriesByType<RealStory>("realStory");
            
            return View(realStories);
        }
        [Route("employer")]
        public IActionResult Employer()
        {
            return View();
        }
    }

    public class RealStory
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Document Story { get; set; }
        public string AgeLocation { get; set; }
//        public string Thumbnail { get; set; }
        public string YoutubeLink { get; set; }
        public string ThumbnailUrl { get; set; }
        public string EmbedUrl { get; set; }
    }
}