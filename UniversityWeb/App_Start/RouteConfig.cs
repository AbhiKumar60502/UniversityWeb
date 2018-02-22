﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UniversityWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "UniversityHome",
              url: "UniversityHome/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Shell", id = UrlParameter.Optional },
               namespaces: new[] { "UniversityWeb.Areas.UniversityHome.Controllers" }
               

           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Shell", id = UrlParameter.Optional },
                namespaces: new[] { "UniversityWeb.Areas.UniversityHome.Controllers" }
            );

        }
    }
}