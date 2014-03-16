using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prjt.Web.Helpers
{
    public class JavascriptHelper<TModel> : JavascriptHelper
    {
        public JavascriptHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
        }

        public JavascriptHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
        {
            ViewContext = viewContext;
            ViewData = new ViewDataDictionary<TModel>(viewDataContainer.ViewData);
        }        

        public new ViewDataDictionary<TModel> ViewData
        {
            get;
            private set;
        }


    }
}
