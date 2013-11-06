using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Domain;

namespace DevAchievements.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			var achievementService = new AchievementService ();
			var achievements = achievementService.GetAchievementsByDeveloper (new DeveloperAchievementProviderAccount("giacomelli"));
            
			return View(achievements);
        }

    }
}
