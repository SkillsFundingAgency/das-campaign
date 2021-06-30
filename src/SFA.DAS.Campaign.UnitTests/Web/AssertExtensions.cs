using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.UnitTests.Web
{
    public static class AssertExtensions
    {
        public static void AssertThatTheObjectResultIsValid(this ViewResult result)
        {
            result.Should().NotBeNull();
        }

        public static void AssertThatTheObjectValueIsValid<T>(this ViewResult result)
        {
            var value = (T)result.Model;
            value.Should().NotBeNull();
        }

        public static void AssertThatTheReturnedViewIsCorrect(this ViewResult result, string expected)
        {
            result.ViewName.Should().Be(expected);
        }
    }
}
