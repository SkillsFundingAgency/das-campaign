using System;
using Microsoft.AspNetCore.Html;

namespace SFA.DAS.Campaign.Web.Constants
{
    public static class ModalIdConsts
    {
        public const string RegisterThanksId = "modal-register-thanks";
    }

    public static class Helper
    {
    public static IHtmlContent Body(Func<object, IHtmlContent> body)
    {
        return body(null);
    }
    }
}
