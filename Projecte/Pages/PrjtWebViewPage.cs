using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Prjt.Web.Helpers;

namespace Prjt.Web.Pages
{
    public abstract class PrjtWebViewPage : WebViewPage
    {
        public JavascriptHelper JS { get; set; }

        //public AuthorizationHelper Auth { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            JS = new JavascriptHelper(base.ViewContext, this);
            //JS.URLHelper = this.Url;
            //Auth = new AuthorizationHelper(base.ViewContext, this);
        }

        public override void Execute()
        {
            //throw new NotImplementedException();
        }
    }
}

