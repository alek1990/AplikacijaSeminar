using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacija.Helpers
{
    public static class MenuItemHelper
    {
        //Helper metoda za "osvjetljavanje" aktivnog linkova u navigation bar-u (Predbilježba, Predblježbe, Seminari)
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText,string actionName, string controllerName, string areaName)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            var currentArea = htmlHelper.ViewContext.RouteData.DataTokens["area"];

            var builder = new TagBuilder("li")
            {
                //InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
                InnerHtml = "<a href=\"" + new UrlHelper(htmlHelper.ViewContext.RequestContext).Action( actionName,controllerName, new { area = areaName }).ToString() + "\">" + linkText + "</a>"
            };

            if (String.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase))
                builder.AddCssClass("active");

            return new MvcHtmlString(builder.ToString());
        }
    }
}