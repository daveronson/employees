namespace EmployeeDirectory
{
    using HtmlTags;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class HtmlHelperExtensions
    {
        public static HtmlTag PrimaryButton(this IHtmlHelper helper, string text)
        {
            var tag = new HtmlTag("button")
                .Attr("type", "submit")
                .AddClass("btn")
                .AddClass("btn-primary")
                .Text(text);

            return tag;
        }

        public static HtmlTag LinkButton(this IHtmlHelper helper, string text, string url)
        {
            var tag = new HtmlTag("a")
                .Attr("href", url)
                .Attr("role", "button")
                .AddClass("btn")
                .AddClass("btn-default")
                .Text(text);

            return tag;
        }

        public static HtmlTag ButtonGroup(this IHtmlHelper helper,
            params HtmlTag[] buttons)
        {
            var outer = new DivTag()
                .AddClass("form-group");

            var inner = new DivTag()
                .AddClass("col-md-offset-2")
                .AddClass("col-md-10");

            inner.Append(buttons);

            outer.Append(inner);

            return outer;
        }
    }
}