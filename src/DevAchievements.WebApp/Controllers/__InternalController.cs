using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Domain;

namespace DevAchievements.WebApp.Controllers
{
    public class __InternalController : Controller
    {
        public ActionResult Install()
        {
            var achievementProviderService = new AchievementProviderService();
            achievementProviderService.GetAchievementProviders();

            var achievementIssuerService = new AchievementIssuerService();
            var model = achievementIssuerService.GetAllAchievementIssuers();

            return View(model);
        }

        public ActionResult UpdateDevelopersAchievements()
        {
            var devService = new DeveloperService();
            var devs = devService.GetAllDevelopers().ToList();

            var achievementService = new AchievementService();

            foreach (var dev in devs)
            {
                achievementService.UpdateDeveloperAchievements(dev);
            }

            return View (devs);
        }
    }
}
