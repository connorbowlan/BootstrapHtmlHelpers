using System.Web.Mvc;
using BootstrapHtmlHelpersPackage.Builders;

namespace BootstrapHtmlHelpersPackage.Extensions
{
    public static class JsExtension
    {
        #region JsButton

        public static MvcHtmlString JsButton(this HtmlHelper htmlHelper, string value, string onClick, string id = null, object htmlAttributes = null)
        {
            return JsBuilder.CreateJsButton(htmlHelper, value, onClick, id, htmlAttributes);
        }

        #endregion
    }
}
