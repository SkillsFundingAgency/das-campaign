using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Shared.Components.Cookies;
using System;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Controller
{
    [TestFixture]
    public class CookieManagerTests
    {
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<HttpContext> _mockHttpContext;
        private ICookieManager _sut;

        [SetUp]
        public void Setup()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContext = GetMockHttpContext();
            _mockHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(_mockHttpContext.Object);

            _sut = new CookieManager(_mockHttpContextAccessor.Object);
        }

        [Test]
        public void Get_ShouldGetCookieFromHttpRequest_ForGivenCookieName()
        {
            var cookieName = "TestCookie";

            var result = _sut.Get(cookieName);

            _mockHttpContext.Verify(x => x.Request.Cookies[cookieName]);
        }

        [Test]
        public void Set_ShouldSetCookieValueInHttpsResponse_ForGivenCookie()
        {
            var cookieName = "TestCookie";
            var cookieValue = "Test Cookie Value";
            var expiry = DateTime.UtcNow.AddDays(90);

            _sut.Set(cookieName, cookieValue, expiry);

            _mockHttpContext.Verify(x => x.Response.Cookies.Append(cookieName, cookieValue, It.Is<CookieOptions>(a => CookieValuesSet(a, expiry))));
        }

        private static bool CookieValuesSet(CookieOptions a, DateTime expiry)
        {
            return a.Expires == expiry
                && a.HttpOnly == true
                && a.IsEssential == true
                && a.Secure == true;
        }

        private Mock<HttpContext> GetMockHttpContext()
        {
            Mock<HttpContext> mockContext = new Mock<HttpContext> { DefaultValue = DefaultValue.Mock };

            return mockContext;
        }
    }
}
