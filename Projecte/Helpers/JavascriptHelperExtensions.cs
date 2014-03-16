using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Prjt.Web.Helpers
{
    public static class JavascriptHelperExtensions
    {
        private static Dictionary<String, String> PendingModels(JavascriptHelper JS)
        {
            //if (JS.ViewData == null)
            //    JS.ViewData = new ViewDataDictionary();

            if (!JS.ViewData.ContainsKey("PendingModels"))
                JS.ViewData.Add("PendingModels", new Dictionary<string, string>());

            return JS.ViewData["PendingModels"] as Dictionary<string, string>;
        }

        private static JsonSerializerSettings _SerializerSettings;

        public static JsonSerializerSettings SerializerSettings
        {
            get
            {
                if (_SerializerSettings == null)
                {
                    _SerializerSettings = new JsonSerializerSettings();
                    IsoDateTimeConverter iso_8601 = new IsoDateTimeConverter();
                    iso_8601.DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal;
                    _SerializerSettings.Converters.Add(iso_8601);
                }
                return _SerializerSettings;
            }
        }

        /// <summary>
        /// Renders a script tag with a function <c>getModel</c>, which returns the json representation of the object passed as model
        /// </summary>
        /// <param name="JS"></param>
        /// <param name="model">The model object to include in the javascript</param>
        /// <param name="key">The optional key for the model. If supplied, the rendered function will be getModelForKey{KEY}</param>
        /// <returns>The sctipt block string</returns>
        public static IHtmlString IncludeModel(this JavascriptHelper JS, Object model, string key = null)
        {
            return new HtmlString(IncludeModelImplementation(model, key));
        }

        /// <summary>
        /// Stores a script tag with a function <c>getModel</c>, which returns the json representation of the object passed as model
        /// Later when the includePending models is called, the models added with this method will be rendered alltogether
        /// </summary>
        /// <param name="JS"></param>
        /// <param name="model">The model object to include in the javascript</param>
        /// <param name="key">The key for the model. If supplied, the rendered function will be getModelForKey{KEY}</param>
        public static void AddModel(this JavascriptHelper JS, Object model, string key, string wrap_model = null)
        {
            PendingModels(JS).Add(key, new HtmlString(RenderModel(model, key, wrap_model: wrap_model)).ToString());
        }

        private static string IncludeModelImplementation(Object model, string key = null, string wrap_model = null)
        {
            return IncludeModelImplementation(RenderModel(model, key, wrap_model: wrap_model));
        }

        private static string IncludeModelImplementation(String innerScript)
        {
            TagBuilder modelScriptTag = null;

            modelScriptTag = new TagBuilder("script");
            modelScriptTag.Attributes.Add("type", "text/javascript");
            modelScriptTag.Attributes.Add("language", "javascript");

            modelScriptTag.InnerHtml = innerScript;

            return modelScriptTag.ToString();
        }

        private static string RenderModel(Object model, string key, string wrap_model = null)
        {
            const string identity_fn = "(function(mod) { return mod; })";
            string crlf = "\r\n";

            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("var m{0} = ", key));
            sb.Append(JsonConvert.SerializeObject(model, Formatting.Indented, SerializerSettings));
            sb.Append(";").Append(crlf);

            sb.Append("function getModel");
            if (!String.IsNullOrEmpty(key))
            {
                sb.Append("ForKey").Append(key);
            }
            sb.Append("()").Append("{").Append(crlf);
            sb.Append(String.Format("\treturn {0}(m{1})", wrap_model ?? identity_fn, key)).Append(";").Append(crlf);
            //if (!string.IsNullOrEmpty(wrap_model))
            //{
            //    sb.Append(String.Format("\treturn {0}(m{1})", wrap_model, key)).Append(";").Append(crlf);
            //}
            //else
            //{
            //    sb.Append(String.Format("\treturn m{0}", key)).Append(";").Append(crlf);
            //}
            sb.Append("}").Append(crlf);
            return sb.ToString();
        }

        /// <summary>
        /// Renders a script include for the page, which looks for a file in the same
        /// directory as the action, with the extension .js instead of .cshtml. If a model
        /// object is passed if, it first renders a script tag with a function getModel,
        /// which returns the json representation of the object
        /// </summary>
        /// <param name="JS"></param>
        /// <param name="model">The optional model object to include in the javascript</param>
        /// <param name="key">The optional key for the model. If supplied, the rendered function will be getModelForKey{KEY}</param>
        /// <returns>The sctipt block string</returns>
        public static IHtmlString IncludeSelf(this JavascriptHelper JS, Object model = null, string key = null)
        {
            string modelString = "";
            if (model != null)
            {
                modelString = IncludeModelImplementation(model, key);
            }

            TagBuilder scriptTag = new TagBuilder("script");
            scriptTag.Attributes.Add("type", "text/javascript");
            scriptTag.Attributes.Add("language", "javascript");
            scriptTag.Attributes.Add("src", getScriptPath(JS.ViewContext, JS.URLHelper));

            return new HtmlString(string.Format("{0}\r\n{1}", modelString, scriptTag.ToString()));
        }

        /// <summary>
        /// Renders a script include for the page, which looks for a file in the same
        /// directory as the action, with the extension .js instead of .cshtml. If a model
        /// object is passed if, it first renders a script tag with a function getModel,
        /// which returns the json representation of the object
        /// </summary>
        /// <param name="JS"></param>
        /// <param name="model">The optional model object to include in the javascript</param>
        /// <param name="key">The optional key for the model. If supplied, the rendered function will be getModelForKey{KEY}</param>
        /// <returns>The sctipt block string</returns>
        public static IHtmlString IncludeSelf(this JavascriptHelper JS, bool includePendingModels)
        {
            TagBuilder scriptTag = new TagBuilder("script");
            scriptTag.Attributes.Add("type", "text/javascript");
            scriptTag.Attributes.Add("language", "javascript");
            scriptTag.Attributes.Add("src", getScriptPath(JS.ViewContext, JS.URLHelper));

            return new HtmlString(string.Format("{0}\r\n{1}", includePendingModels ? IncludePendingModels(JS).ToString() : "", scriptTag.ToString()));
        }

        /// <summary>
        /// Renders a script include for the page, which looks for a file in the same
        /// directory as the action, with the extension .js instead of .cshtml. If a model
        /// object is passed if, it first renders a script tag with a function getModel,
        /// which returns the json representation of the object
        /// </summary>
        /// <param name="JS"></param>
        /// <param name="model">The optional model object to include in the javascript</param>
        /// <param name="key">The optional key for the model. If supplied, the rendered function will be getModelForKey{KEY}</param>
        /// <returns>The sctipt block string</returns>
        public static IHtmlString IncludeSelf(this JavascriptHelper JS, Object Model, bool includePendingModels, string wrap_model = null)
        {
            TagBuilder scriptTag = new TagBuilder("script");
            scriptTag.Attributes.Add("type", "text/javascript");
            scriptTag.Attributes.Add("language", "javascript");
            scriptTag.Attributes.Add("src", getScriptPath(JS.ViewContext, JS.URLHelper));

            PendingModels(JS).Add("Model", RenderModel(Model, null, wrap_model: wrap_model));

            return new HtmlString(string.Format("{0}\r\n{1}", includePendingModels ? IncludePendingModels(JS).ToString() : "", scriptTag.ToString()));
        }

        public static IHtmlString IncludeSelfUserControl(this JavascriptHelper JS, Object Model, bool includePendingModels, string wrap_model = null)
        {
            TagBuilder scriptTag = new TagBuilder("script");
            scriptTag.Attributes.Add("type", "text/javascript");
            scriptTag.Attributes.Add("language", "javascript");
            scriptTag.Attributes.Add("src", getScriptPath(JS.ViewContext, JS.URLHelper).Replace("Index.js","UserControls/ucPlantsList.js"));
            
            PendingModels(JS).Add("Model", RenderModel(Model, null, wrap_model: wrap_model));
            string pendingModel = IncludePendingModels(JS).ToString();
            return new HtmlString(string.Format("{0}\r\n{1}", includePendingModels ? pendingModel : "", scriptTag.ToString()));
        }

        public static IHtmlString IncludePendingModels(this JavascriptHelper JS)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in PendingModels(JS).Values)
            {
                sb.Append(item).Append("\r\n");
            }

            return new HtmlString(IncludeModelImplementation(sb.ToString()));
        }

        private static string getScriptPath(ViewContext viewContext, UrlHelper helper)
        {
            helper = helper ?? new UrlHelper(viewContext.RequestContext);

            return helper.Content(((RazorView)viewContext.View).ViewPath.Replace("cshtml", "js"));
        }
    }
}