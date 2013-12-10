using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Domain;
using DevTrends.MvcDonutCaching;
using ProxyApi;
using DevAchievements.Infrastructure.Web;

namespace DevAchievements.WebApp.Controllers
{
	public class DeveloperController : FuncControllerBase<Developer, Guid>
    {
		public DeveloperController() 
		{
			var service = new DeveloperService ();

			GetEntitiesFunc = () => service.GetAllDevelopers ();

			CreateNewEntityFunc = () => new Developer ();
			GetEntityIdFunc = (entity) => (Guid)entity.Key;
			GetEntityByIdFunc = (id) => service.GetDeveloperByKey(id);
			DeleteEntityByIdFunc = (id) => service.DeleteDeveloper (id);
			GetEntitiesFunc = () => service.GetAllDevelopers ();
			GetGridValuesFunc = (entity) => new object[] { entity.FullName, entity.Username, entity.Email, String.Join(", ", entity.AccountsAtIssuers.Select(r => r.IssuerName)) };
			SaveEntityFunc = (entity) =>
			{
				// TODO: create a model binder.
				if(entity.Key != null)
				{
					entity.Key = new Guid(((string[]) entity.Key)[0]);
				}

				service.SaveDeveloper(entity);

				return entity;
			};
		}

		public override ActionResult Create ()
		{
			return Create (String.Empty);
		}

		public ActionResult Create(string username)
		{
			var model = new Developer ();
			model.Username = username;
			var service = new AchievementService ();

			var issuers = service.GetAllIssuers ();

			foreach (var issuer in issuers) {
				model.AccountsAtIssuers.Add (new DeveloperAccountAtIssuer (issuer.Name, ""));
			}

			return View ("CreateEdit", model);
		} 

		[HttpPost]
		public override ActionResult Create (Developer entity)
		{
			entity.Key = null;
			var developerService = new DeveloperService();

			return this.Call (() => {
				developerService.SaveDeveloper (entity);

				ClearUserCache (entity);

				return Redirect ("/" + entity.Username);
			});
		}

		public override ActionResult Edit (Developer entity)
		{
			var result = base.Edit (entity);
			ClearUserCache (entity);

			return result;
		}

		/*
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
        */

		[ProxyName("existsDeveloperAccountAtIssuer")]
		public JsonResult ExistsDeveloperAccountAtIssuer(string issuerName, string username)
		{
			var service = new AchievementProviderService ();
	
			return Json (service.ExistsDeveloperAccountAtIssuer(issuerName, username), JsonRequestBehavior.AllowGet);
		} 

		static void ClearUserCache (Developer entity)
		{
			var outputCacheManager = new OutputCacheManager ();
			outputCacheManager.RemoveItem ("Home", "Index", new {
				username = entity.Username
			});
		}
    }
}