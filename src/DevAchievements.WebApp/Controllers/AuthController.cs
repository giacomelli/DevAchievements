using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.AspNet.Clients;
using DevAchievements.Domain;
using DevAchievements.WebApp.Helpers;
using DevAchievements.Infrastructure.Web.Security;
using System.Web.Security;

namespace DevAchievements.WebApp.Controllers
{
	public class AuthController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

        [HttpGet]
		public void SignInWith(AuthenticationProvider provider) 
        {
			AuthenticationService.Authenticate (provider);
        }

        [HttpGet]
		public ActionResult VerifySignInWith(AuthenticationProvider provider) 
        {
			var result = AuthenticationService.FinalizeAuthentication (provider);

			if(result.IsSuccessful)
			{
                if (result.IsRegisteredDeveloper)
                {
					FormsAuthentication.SetAuthCookie (result.Developer.Username, true);
                    return this.RedirectToDeveloperHome(result.Developer);
                }
                else
                {
                    TempData["authenticationResult"] = result;
                    return RedirectToAction("Create", "Developer");
                }
			}

			return new HttpUnauthorizedResult ("HAL: Without your space helmet, Dave? You're going to find that rather difficult.");
		}
	}
}
