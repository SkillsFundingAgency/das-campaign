using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Interface to mark which classes can be used as modules.
    /// </summary>
    public interface IModule
    {
        SystemProperties Sys { get; set; }
    }

    /// <summary>
    /// Interface to mark page modules.
    /// </summary>
    public interface IPageModule : IModule
    {
    }

    /// <summary>
    /// Interface to mark layout modules.
    /// </summary>
    public interface ILayoutModule : IModule
    {
    }

    /// <summary>
    /// Interface to mark layout modules.
    /// </summary>
    public interface IRealStoriesModule : IModule
    {
    }
}
