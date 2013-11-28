using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Application;
using DevAchievements.Domain;
using Skahal.Infrastructure.Framework.Logging;
using HelperSharp;

namespace DevAchievements.WebApp.Controllers
{
	public class HomeController : Controller
    {
		[OutputCache(Duration = 3600)]
		public ActionResult Index(string username)
		{
			var service = new DeveloperService ();
			var dev = service.GetDeveloperByUsername (username);

			if (dev == null) {
				return View ("DeveloperNotFound", (object) username);
			}

			var achievementService = new AchievementService ();
			var achievements = achievementService.GetAchievementsByDeveloper(dev);

			var viewModel = new DeveloperHomeViewModel(dev, achievements);
         
			return View("DeveloperHome", viewModel);
		}
    }
}
