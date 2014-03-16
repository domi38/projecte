using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Echk.Web.Helpers
{
    public class AuthorizationHelper
    {
        public AuthorizationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
            ;
        }

        public AuthorizationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
        {
            ViewContext = viewContext;
            ViewData = new ViewDataDictionary(viewDataContainer.ViewData);
        }

        public AuthorizationHelper()
        {
            ViewData = new ViewDataDictionary();
            ;
        }

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