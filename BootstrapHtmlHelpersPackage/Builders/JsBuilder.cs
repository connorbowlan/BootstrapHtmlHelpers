using System.Web.Mvc;
using BootstrapHtmlHelpersPackage.Helpers;

namespace BootstrapHtmlHelpersPackage.Builders
{
    public static class JsBuilder
    {
        public static MvcHtmlString CreateJsButton(HtmlHelper htmlHelper, string value, string onClick,
            string id = null, object htmlAttributes = null)
        {
            var builder = new TagBuilder("input");

            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            attributes.EnsureButtonStyleClass();
            id = AttributeHelper.EnsureId(id, value);

            builder.Attributes.Add("id", id);
            builder.Attributes.Add("name", id);
            builder.Attributes.Add("type", "button");
            builder.Attributes.Add("value", value);

            builder.Attributes.Add("onClick", onClick);

            builder.MergeAttributes(attributes);

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
