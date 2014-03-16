using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prjt.Web.Helpers
{
    public class JavascriptHelper
    {
        public JavascriptHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
            ;
        }

        public JavascriptHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
        {
            ViewContext = viewContext;
            ViewData = new ViewDataDictionary(viewDataContainer.ViewData);
        }

        public JavascriptHelper()
        {
            ViewData = new ViewDataDictionary();
            ;
        }

        public UrlHelper URLHelper { get; set; }

        public ViewDataDictionary ViewData
        {
            get;
            protected set;
        }

        public ViewContext ViewContext
        {
            get;
            protected set;
        }
    }
}