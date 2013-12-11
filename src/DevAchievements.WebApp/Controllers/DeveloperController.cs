using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Domain;
using DevTrends.MvcDonutCaching;
using ProxyApi;
using DevAchievements.Infrastructure.Web;
using System.Security.Cryptography;
using DevAchievements.Infrastructure.Web.UI;

namespace DevAchievements.WebApp.Controllers
{
	public class DeveloperController : FuncControllerBase<Developer, Guid>
    {
		#region Constructors
		public DeveloperController() 
		{
			var service = new DeveloperService ();

			GetEntitiesFunc = () => service.GetAllDevelopers ();

			CreateNewEntityFunc = () => FillModel(new Developer ());
			GetEntityIdFunc = (entity) => (Guid)entity.Key;
			GetEntityByIdFunc = (id) => FillModel(service.GetDeveloperByKey (id));
			DeleteEntityByIdFunc = (id) => service.DeleteDeveloper (id);
			GetEntitiesFunc = () => service.GetAllDevelopers ();

			GetGridValuesFunc = (entity) => new object[] { DeveloperUI.GetAvatarUrl(entity), entity.FullName, entity.Username, entity.Email, String.Join(", ", entity.AccountsAtIssuers.Select(r => r.IssuerName)) };
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
		#endregion

		#region Actions
		public override ActionResult Create ()
		{
			return Create (String.Empty);
		}

		public ActionResult Create(string username)
		{
			var model = CreateNewEntity ();
			model.Username = username;
		
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

		[ProxyName("existsDeveloperAccountAtIssuer")]
		public JsonResult ExistsDeveloperAccountAtIssuer(string issuerName, string username)
		{
			var service = new AchievementProviderService ();
	
			return Json (service.ExistsDeveloperAccountAtIssuer(issuerName, username), JsonRequestBehavior.AllowGet);
		} 
		#endregion

		#region Helpers
		private static void ClearUserCache (Developer entity)
		{
			var outputCacheManager = new OutputCacheManager ();
			outputCacheManager.RemoveItem ("Home", "Index", new {
				username = entity.Username
			});
		}

		private static Developer FillModel (Developer model)
		{
			var service = new AchievementService ();
			var issuers = service.GetAllIssuers ();

			foreach (var issuer in issuers) {
				model.AddAccountAtIssuer (new DeveloperAccountAtIssuer (issuer.Name, ""));
			}

			model.AccountsAtIssuers = model.AccountsAtIssuers.OrderBy (a => a.IssuerName).ToList();

			return model;
		}
		#endregion
    }
}