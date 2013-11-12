using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DevAchievements.Infrastructure.AchievementProviders;
using DevAchievements.Infrastructure.Web.Configuration;
using Skahal.Infrastructure.Framework.Logging;

[assembly: log4net.Config.XmlConfigurator(ConfigFile="Web.config", Watch = true)]

namespace DevAchievements.WebApp
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");


			routes.MapRoute (
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = "" }
			);

		}

		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		protected void Application_Start ()
		{
			LogService.Debug ("Application starting...");
			LogService.Debug ("Machine: {0}", Environment.MachineName);

			LogService.Debug ("Registering areas...");
			AreaRegistration.RegisterAllAreas ();
	
			LogService.Debug ("Registering global filters...");
			RegisterGlobalFilters (GlobalFilters.Filters);

			LogService.Debug ("Registering routes...");
			RegisterRoutes (RouteTable.Routes);

			LogService.Debug ("Bootstrap setup...");
			new WebBootstrap ().Setup ();

            LogService.Debug("Loading achievements provider assembly...");
            var dummy = typeof(AchievementProviderBase);
            LogService.Debug("{0} assembly loaded.", dummy.Assembly.GetName().Name);
            
			LogService.Debug ("Application started.");
		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			var ex = Server.GetLastError();

			LogService.Error ("Unhandled exception: {0}: {1}", ex.Message, ex.StackTrace);
		}
	}
}
