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
using DevAchievements.Application;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Repositories;

namespace DevAchievements.WebApp.Controllers
{
	public class DeveloperController : FuncControllerBase<DeveloperCreateEditViewModel, Guid>
    {
		#region Constructors
		public DeveloperController() 
		{
			CreateNewEntityFunc = () => DeveloperCreateEditAppService.CreateNew ();
			GetEntityIdFunc = (model) => (Guid)model.Key;
			GetEntityByIdFunc = (key) => DeveloperCreateEditAppService.GetByKey(key);
			DeleteEntityByIdFunc = (key) => DeveloperCreateEditAppService.Delete (key);
			GetEntitiesFunc = () => DeveloperCreateEditAppService.GetAll ();

			GetGridValuesFunc = (entity) => new object[] { DeveloperUI.GetAvatarUrl(entity.Email), entity.FullName, entity.Username, entity.Email, String.Join(", ", entity.AccountsAtIssuers.Select(r => r.IssuerName)) };
			SaveEntityFunc = (entity) =>
			{
				DeveloperCreateEditAppService.Save(entity);

				return entity;
			};
		}
		#endregion

		#region Actions
		public override ActionResult Create ()
		{
            var authenticationResult = TempData["authenticationResult"] as AuthenticationResult;
			var model = CreateNewEntity ();

			if (authenticationResult == null) {
				model.Provider = AuthenticationProvider.DevAchievements;
			} else {
				var dev = authenticationResult.Developer;
                model.Username = dev.Username;
                model.Email = dev.Email;
                model.FullName = dev.FullName;
				model.Provider = authenticationResult.Provider;
				model.ProviderUserKey = authenticationResult.ProviderUserKey;
			}

			return View ("CreateEdit", model);
		}

		public ActionResult Create(string username)
		{
			var model = CreateNewEntity ();
			model.Username = username;
		
			return View ("CreateEdit", model);
		} 

		[HttpPost]
		public override ActionResult Create (DeveloperCreateEditViewModel entity)
		{
			entity.Key = Guid.NewGuid();
         
			return this.Call (() => {                
				SaveEntity(entity);
				DependencyService.Create<IUnitOfWork>().Commit();
				ClearUserCache (entity.Username);

				return this.RedirectToDeveloperHome(entity.Username);
			}, (ex) => { 
				return View ("CreateEdit", entity);
			});
		}

		public override ActionResult Edit (Guid id)
		{
			if (DevInfo.Current != null && DevInfo.Current.Key.Equals (id)) {
				return base.Edit (id);
			}

			return new HttpUnauthorizedResult ("HAL: I think you know what the problem is just as well as I do.");
		}

		public override ActionResult Edit (DeveloperCreateEditViewModel entity)
		{
			var result = base.Edit (entity);
			ClearUserCache (entity.Username);

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
			var service = new DeveloperService ();
			var developer = service.GetDeveloperByKey(new Guid(developerKey));
			var achievement = developer.GetAchievementByKey (achievementKey);

			return Json (achievement.History, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region Helpers
		private static void ClearUserCache (string username)
		{
			var outputCacheManager = new OutputCacheManager ();
			outputCacheManager.RemoveItem ("Home", "Index", new {
				username = username
			});
		}
		#endregion
    }
}