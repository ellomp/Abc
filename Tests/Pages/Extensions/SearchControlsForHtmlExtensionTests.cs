using System.Collections.Generic;
using Abc.Facade.Quantity;
using Abc.Pages.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Extensions {

    [TestClass] public class SearchControlsForHtmlExtensionTests : BaseTests {

        private const string Filter = "filter";
        private const string LinkToFullList = "url";
        private const string Text = "text";

        [TestInitialize] public virtual void TestInitialize() => type = typeof(SearchControlsForHtmlExtension);

        [TestMethod] public void SearchControlsForTest() {
            var obj = new htmlHelperMock<UnitView>().SearchControlsFor(Filter, LinkToFullList, Text);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod] public void HtmlStringsTest() {
            var expected = new List<string> {
                "<form asp-action=\"./Index\" method=\"get\">",
                "<div class=\"form-inline col-md-6\">",
                "Find by:",
                "&nbsp;",
                $"<input class=\"form-control\" type=\"text\" name=\"SearchString\" value=\"{Filter}\" />",
                "&nbsp;",
                "<input type=\"submit\" value=\"Search\" class=\"btn btn-default\" />",
                "&nbsp;",
                $"<a href=\"{LinkToFullList}\">{Text}</a>",
                "</div>",
                "</form>"
            };
            var actual = SearchControlsForHtmlExtension.htmlStrings(Filter, LinkToFullList, Text);
            TestHtml.Strings(actual, expected);
        }

    }

}