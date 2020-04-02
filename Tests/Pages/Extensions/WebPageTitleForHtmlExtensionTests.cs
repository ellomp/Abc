using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Extensions
{
    [TestClass]

    public static class WebPageTitleForHtmlExtensionTests
    {

        public static IHtmlContent WebPageTitleFor(
            this IHtmlHelper htmlHelper, string title)
        {
            htmlHelper.ViewData["Title"] = title;
            var s = htmlStrings(title);
            return new HtmlContentBuilder(s);
        }

        internal static List<object> htmlStrings(string title)
        {
            return new List<object> {
                new HtmlString("<h1>"),
                new HtmlString(title),
                new HtmlString("</h1>")
            };
        }

    }
}