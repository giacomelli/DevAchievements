using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Application;
using DevAchievements.Domain;
using Skahal.Infrastructure.Framework.Logging;

namespace DevAchievements.WebApp.Controllers
{
	public class HomeController : Controller
    {
        public ActionResult Index()
        {
			LogService.Debug ("Home.Index.Get access");
			return Index("giacomelli", "giacomelli", "giacomelli");
        }

        [HttpPost]
		public ActionResult Index(string gitHubUserName, string stackoverflowUserName, string vsaUserName)
        {
			LogService.Debug ("Home.Index.Post access");
			var achievementService = new AchievementService ();
            var account = new DeveloperAccount();
            account.AddAccountAtIssuer(new DeveloperAccountAtIssuer("github", gitHubUserName));
            account.AddAccountAtIssuer(new DeveloperAccountAtIssuer("stackoverflow", stackoverflowUserName));
			account.AddAccountAtIssuer(new DeveloperAccountAtIssuer("visual studio achievements", vsaUserName));
            var achievements = achievementService.GetAchievementsByDeveloper(account);

            var viewModel = new AchievementsViewModel(achievements);
            viewModel.GitHubUserName = gitHubUserName;
            viewModel.StackOverflowUserName = stackoverflowUserName;
			viewModel.VsaUserName = vsaUserName;

			return View(viewModel);
        }
    }
}
