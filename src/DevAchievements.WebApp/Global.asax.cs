using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.AchievementProviders;
using DevAchievements.Infrastructure.Repositories;
using DevAchievements.Infrastructure.Repositories.MongoDB;
using DevAchievements.Application.Configuration;
using MongoDB.Bson.Serialization;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Repositories;
using DevAchievements.WebApp.App_Start;
using DevAchievements.Infrastructure.Web.Configuration;
using DevAchievements.Infrastructure.Web.Security;
using Skahal.Infrastructure.Framework.People;
using Ninject;
using NHibernate.Context;

[assembly: log4net.Config.XmlConfigurator(ConfigFile="Web.config", Watch = true)]

namespace DevAchievements.WebApp
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		protected void Application_Start ()
		{
			LogService.Debug ("Application starting...");
			LogService.Debug ("Machine: {0}", Environment.MachineName);

			LogService.Debug ("Validating configuration...");
			ConfigValidator.Validate ();

			LogService.Debug ("Registering areas...");
			AreaRegistration.RegisterAllAreas ();
	
			LogService.Debug ("Registering global filters...");
			RegisterGlobalFilters (GlobalFilters.Filters);

			LogService.Debug ("Registering routes...");
			RouteConfig.RegisterRoutes (RouteTable.Routes);

			LogService.Debug ("Bootstrap setup...");
			new WebBootstrap ().Setup ();
	
            LogService.Debug("Loading achievements provider assembly...");
            var dummy = typeof(AchievementProviderBase);
            LogService.Debug("{0} assembly loaded.", dummy.Assembly.GetName().Name);

    		LogService.Debug ("Registering FluentUI...");
			FluentUIConfig.Register ();
         
			LogService.Debug ("Application started.");
		}
        /*
        protected void Application_BeginRequest()
        {
            var session = NinjectWebCommon.s_sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }
*/
        protected void Application_EndRequest()
        {
            DependencyService.Create<IUnitOfWork>().Commit();
        }

		protected void Application_Error(Object sender, EventArgs e)
		{
			var ex = Server.GetLastError();

			LogService.Error ("Unhandled exception: {0}: {1}", ex.Message, ex.StackTrace);
		}
	}
}
