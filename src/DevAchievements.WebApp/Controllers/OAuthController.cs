//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Microsoft.Web.WebPages.OAuth;
//
//namespace DevAchievements.WebApp.Controllers
//{
//    public class OAuthController : Controller
//    {
//        public ActionResult Index()
//        {
//            return View ();
//        }
//
//		[HttpGet]
//		public void RequestTwitter() 
//		{
//			OAuthWebSecurity.RequestAuthentication ("Twitter", "http://127.0.0.1:8080/OAuth/Twitter");
//		}
//
//		[HttpGet]
//		public ActionResult Twitter(string oauth_token) 
//		{
//			TempData ["token"] = oauth_token;
//			return RedirectToAction("Create", "Developer");
//		}
//    }
//}
