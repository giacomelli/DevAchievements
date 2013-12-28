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
using DevAchievements.Infrastructure.Repositories.MongoDB;
using MongoDB.Driver;
using DevAchievements.Infrastructure.Web.Security;
using DevAchievements.WebApp.Helpers;
using System.Web.Security;
using AppHarbor.Web.Security;

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
            var authenticationResult = TempData["authenticationResult"] as AuthenticationResult;

			if (authenticationResult == null) {
				return Create (String.Empty);
			} else {
				var model = CreateNewEntity ();
                var dev = authenticationResult.Developer;
                model.Username = dev.Username;
                model.Email = dev.Email;
                model.FullName = dev.FullName;
                ViewData["provider"] = authenticationResult.Provider;
                ViewData["providerUserKey"] = authenticationResult.ProviderUserKey;

				return View ("CreateEdit", model);
			}
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
			entity.Key = Guid.NewGuid();
            var provider  = (AuthenticationProvider) Enum.Parse(typeof(AuthenticationProvider), Request["provider"]);
            var providerUserKey = Request["providerUserKey"];

			var developerService = new DeveloperService();

			return this.Call (() => {                
				developerService.SaveDeveloper (entity);
                AuthenticationService.SaveAuthenticationProviderUser(entity, provider, providerUserKey);

				ClearUserCache (entity);

                return this.RedirectToDeveloperHome(entity);
			});
		}

		public override ActionResult Edit (Guid id)
		{
			if (DevInfo.Current != null && DevInfo.Current.Key.Equals (id)) {
				return base.Edit (id);
			}

			return new HttpUnauthorizedResult ("HAL: I think you know what the problem is just as well as I do.");
		}

		public override ActionResult Edit (Developer entity)
		{
			var result = base.Edit (entity);
			ClearUserCache (entity);

			return result;
		}

		public ActionResult Logout()
		{
			return View ();
		}

		[HttpPost]
		public ActionResult ConfirmLogout()
		{
			var authenticator = new CookieAuthenticator(); 
			authenticator.SignOut();

			return Redirect ("/");
		}

		// TODO: remover, apenas para teste.
		// TODO: ver se devo adicionar no Skahal.Infrastructure.Repositories.MongoDB
		public void RemoveAll() {
			var url = new MongoUrl(System.Configuration.ConfigurationManager.AppSettings.Get("MONGOLAB_URI"));
			var client = new MongoClient(url);
			var server = client.GetServer();
			var database = server.GetDatabase(String.IsNullOrEmpty(url.DatabaseName) ? "test" : url.DatabaseName);
			database.GetCollection ("Developers").RemoveAll ();
            database.GetCollection("AuthenticationProviderUsers").RemoveAll();
		}

		[ProxyName("existsDeveloperAccountAtIssuer")]
		public JsonResult ExistsDeveloperAccountAtIssuer(string issuerName, string username)
		{
			var service = new AchievementProviderService ();
	
			return Json (service.ExistsDeveloperAccountAtIssuer(issuerName, username), JsonRequestBehavior.AllowGet);
		} 

		[ProxyName("getAchievementHistory")]
		public JsonResult GetAchievementHistory(string developerKey, string achievementKey)
		{
			var developer = GetEntityById (new Guid(developerKey));
			var achievement = developer.GetAchievementByKey (achievementKey);

			return Json (achievement.History, JsonRequestBehavior.AllowGet);
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