using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreIdentityApp.HtmlHelpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent CustomWelcomeMessage(this IHtmlHelper htmlHelper)
        {
            var welcomeMessage = "<p>Custom Welcome Message</p>";
            return new HtmlString(welcomeMessage);
        }
    }

}
