using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace STM.Infrastructure
{
    public static class ControlHelper
    {
        public static HtmlString CreateText(this IHtmlHelper html, IDictionary<string, string> htmlAttributes = null)
        {
            TagBuilder tb = new TagBuilder("input");
            if (htmlAttributes != null)
            {
                foreach (var attr in htmlAttributes)
                {
                    tb.Attributes.Add(attr);
                }
            }

            tb.AddCssClass("stm-text");
            var writer = new System.IO.StringWriter();
            tb.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
