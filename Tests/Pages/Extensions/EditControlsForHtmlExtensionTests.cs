﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Extensions
{
    [TestClass]

    public static class EditControlsForHtmlExtensionTests
    {
        public static IHtmlContent EditControlsFor<TModel, TResult>
            (this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            var s = htmlStrings(htmlHelper, expression);
            return new HtmlContentBuilder(s);
        }

        internal static List<object> htmlStrings<TModel, TResult>
            (IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            return new List<object>
            {
                new HtmlString("<div class=\"form group\">"),
                htmlHelper.LabelFor(expression, new {@class = "text-dark"}),
                htmlHelper.EditorFor(expression, new {htmlAttributes = new {@class = "form-control"}}),
                htmlHelper.ValidationMessageFor(expression, "", new {@class = "text-danger"}),
                new HtmlString("</div>")
            };
        }
    }
}