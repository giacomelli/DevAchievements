using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevAchievements.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				name: "Default_Developer",
				url: "Developer/{action}/{id}",
				defaults: new { controller = "Developer", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute (
				name: "Default_OAuth",
				url: "OAuth/{action}/{id}",
				defaults: new { controller = "OAuth", action = "Index", id = UrlParameter.Optional }
			);


			routes.MapRoute(
				"Developer_Profile", 
				"{username}", 
				new { controller = "Home", action = "Index", username = "" }
			);
        }
    }
}