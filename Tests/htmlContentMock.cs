using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace Abc.Tests.Pages.Extensions
{
    internal class htmlContentMock : IHtmlContent
    {
        private readonly string content;
        public htmlContentMock(string s) //constructor
        {
            content = s;
        }
        public void WriteTo(TextWriter writer, HtmlEncoder encoder) => writer.WriteLine(content);

        public override string ToString() => content;
    }
}