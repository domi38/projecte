using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Echk.Web.Helpers
{
    public class AuthorizationHelper<TModel> : AuthorizationHelper
    {
        public AuthorizationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
        }

        public AuthorizationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
        {
            ViewContext = viewContext;
            ViewData = new ViewDataDictionary<TModel>(viewDataContainer.ViewData);
        }

        public UrlHelper URLHelper { get; set; }

        public new ViewDataDictionary<TModel> ViewData
        {
            get;
            private set;
        }


    }
}
