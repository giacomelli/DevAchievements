using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Domain;

namespace DevAchievements.WebApp.Controllers
{
    public class DeveloperController : Controller
    {
        public ActionResult Index()
		{
			var developerService = new DeveloperService();
			var model = developerService.GetAllDevelopers ();

			return View (model);
        }

        public ActionResult Details(int id)
        {
            return View ();
        }

        public ActionResult Create()
        {
			var model = new Developer ();
			var service = new AchievementService ();

			var issuers = service.GetAllIssuers ();

			foreach (var issuer in issuers) {
				model.AccountsAtIssuers.Add (new DeveloperAccountAtIssuer (issuer.Name, ""));
			}

			return View (model);
        } 

        [HttpPost]
		public ActionResult Create(Developer developer)
        {
			var developerService = new DeveloperService();
			developerService.SaveDeveloper(developer);

            return RedirectToAction ("Index");
        }
        
        public ActionResult Edit(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }

        public ActionResult Delete(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }
    }
}