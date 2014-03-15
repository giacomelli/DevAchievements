using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Application;
using DevAchievements.Domain;
using Skahal.Infrastructure.Framework.Logging;
using HelperSharp;
using DevTrends.MvcDonutCaching;

namespace DevAchievements.WebApp.Controllers
{
	public class HomeController : Controller
    {
		[DonutOutputCache(CacheProfile = "HomeControllerIndex")]
		public ActionResult Index(string username)
		{
			var service = new DeveloperService ();
			var dev = service.GetDeveloperByUsername (username);

			if (dev == null) {
				return View ("SiteHome", (object)username);
			}			

            var achievementService = new AchievementService();
            achievementService.UpdateDeveloperAchievements(dev);

            var viewModel = new DeveloperHomeViewModel (dev);
     
			return View ("DeveloperHome", viewModel);
		}
    }
}
