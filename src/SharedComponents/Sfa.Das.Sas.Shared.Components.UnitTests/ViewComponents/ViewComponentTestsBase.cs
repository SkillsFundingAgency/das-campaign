using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat
{
    public class ViewComponentTestsBase
    {
        protected Mock<ICssViewModel> _cssClasses;

        protected ViewComponentContext _viewComponentContext;

        public void Setup()
        {
            var httpContext = new DefaultHttpContext();
            var viewContext = new ViewContext();
            viewContext.HttpContext = httpContext;
            _viewComponentContext = new ViewComponentContext();
            _viewComponentContext.ViewContext = viewContext;
       

            _cssClasses = new Mock<ICssViewModel>(MockBehavior.Strict);
            _cssClasses.Setup(s => s.ClassModifier).Returns("");
        }
    }
}
