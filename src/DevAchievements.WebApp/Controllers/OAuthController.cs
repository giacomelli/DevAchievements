using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.AspNet.Clients;

namespace DevAchievements.WebApp.Controllers
{
    public class OAuthController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

		[HttpGet]
		public void RequestTwitter() 
		{
			var client = new TwitterClient("cjk1CorYK48VkRl7rAKCdg", "qxXoqjLsKNihQoIRZ7a5JPBpCTYNbFoSx5WzA9crY");

			client.RequestAuthentication (HttpContext, new Uri("http://127.0.0.1:8080/OAuth/Twitter"));
		}

		[HttpGet]
		public ActionResult Twitter(string oauth_token) 
		{
			TempData ["token"] = oauth_token;
			return RedirectToAction("Create", "Developer");
		}
    }
}
