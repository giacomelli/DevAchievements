using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skahal.Infrastructure.Framework.Logging;
using DevAchievements.Domain;
using DevAchievements.Application;

namespace DevAchievements.WebApp.Controllers
{
    public class DevelopersController : Controller
    {
		public ActionResult Index()
		{
			LogService.Debug ("Home.Index.Get access");
			return Index("giacomelli", "giacomelli");
		}

		[HttpPost]
		public ActionResult Index(string gitHubUserName, string stackoverflowUserName)
		{
			LogService.Debug ("Home.Index.Post access");
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
