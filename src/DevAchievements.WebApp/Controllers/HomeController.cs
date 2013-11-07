using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Application;
using DevAchievements.Domain;

namespace DevAchievements.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Index("giacomelli", "giacomelli");
        }

        [HttpPost]
        public ActionResult Index(string gitHubUserName, string stackoverflowUserName)
        {
			var achievementService = new AchievementService ();
            var account = new DeveloperAccount();
            account.AddAccountAtIssuer(new DeveloperAccountAtIssuer("github", gitHubUserName));
            account.AddAccountAtIssuer(new DeveloperAccountAtIssuer("stackoverflow", stackoverflowUserName));
            var achievements = achievementService.GetAchievementsByDeveloper(account);

            var viewModel = new AchievementsViewModel(achievements);
            viewModel.GitHubUserName = gitHubUserName;
            viewModel.StackOverflowUserName = stackoverflowUserName;

			return View(viewModel);
        }
    }
}
