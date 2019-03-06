using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoManager.Helpers.HtmlHelpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string src, string altText, string actionName, 
            string controllerName, object routeValues, object htmlAttributes, object imgHtmlAttributes)
        {
            var imgtag = Image(helper,src, altText, imgHtmlAttributes);
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            TagBuilder imglink = new TagBuilder("a");
            imglink.Attributes["href"] = urlHelper.Action(actionName, controllerName, routeValues);
            imglink.InnerHtml = imgtag.ToString();
            imglink.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            return MvcHtmlString.Create(imglink.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, object imgHtmlAttributes)
        {
            TagBuilder imgtag = new TagBuilder("img");
            imgtag.MergeAttribute("src", src);
            imgtag.MergeAttribute("alt", altText);
            imgtag.MergeAttributes(new RouteValueDictionary(imgHtmlAttributes), true);
            return MvcHtmlString.Create(imgtag.ToString(TagRenderMode.SelfClosing));
        }
    }
}
