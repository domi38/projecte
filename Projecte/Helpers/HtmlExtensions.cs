using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Prjt.Web.Helpers
{
    /// <summary>
    /// Provides a method for registering and rendering scripts, even from partial views.
    ///
    /// More info: http://stackoverflow.com/questions/9655113/razor-section-inclusions-from-partial-view
    /// (but corrected because if you use an Stack you souldn't use a foreach, because scripts tend to have
    /// dependencies in previously registred scripts).
    /// </summary>
    public static class HtmlExtensions
    {
        public static IHtmlString RegisteredScripts(this HtmlHelper htmlHelper)
        {
            var ctx = htmlHelper.ViewContext.HttpContext;
            var registeredScripts = ctx.Items["_scripts_"] as List<string>;
            if (registeredScripts == null || registeredScripts.Count < 1)
            {
                return null;
            }
            var sb = new StringBuilder();
            foreach (var script in registeredScripts)
            {
                var scriptBuilder = new TagBuilder("script");
                scriptBuilder.Attributes["type"] = "text/javascript";
                string script_format = "{0}";
#if DEBUG
                script_format = string.Format("{{0}}?dt_={0}", DateTime.Now.Ticks);
#endif
                scriptBuilder.Attributes["src"] = string.Format(script_format, script);
                sb.AppendLine(scriptBuilder.ToString(TagRenderMode.Normal));
            }
            return new HtmlString(sb.ToString());
        }

        public static void RegisterScript(this HtmlHelper htmlHelper, string script)
        {
            var ctx = htmlHelper.ViewContext.HttpContext;
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var registeredScripts = ctx.Items["_scripts_"] as List<string>;
            if (registeredScripts == null)
            {
                registeredScripts = new List<string>();
                ctx.Items["_scripts_"] = registeredScripts;
            }
            var src = urlHelper.Content(script);
            if (!registeredScripts.Contains(src))
            {
                registeredScripts.Add(src);
            }
        }

        public static string GlobalResources(this WebViewPage page, string key)
        {
            var value= HttpContext.GetGlobalResourceObject("texts", key) as string ;
            return value ?? key;
        }
    }
}