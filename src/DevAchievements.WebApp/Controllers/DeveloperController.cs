using System;
using System.Linq;
using System.Web.Mvc;
using AppHarbor.Web.Security;
using DevAchievements.Application;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web;
using DevAchievements.Infrastructure.Web.Security;
using DevAchievements.Infrastructure.Web.UI;
using DevAchievements.WebApp.Helpers;
using DevTrends.MvcDonutCaching;
using ProxyApi;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Repositories;

namespace DevAchievements.WebApp.Controllers
{
	public class DeveloperController : FuncControllerBase<DeveloperCreateEditViewModel, long>
    {
		#region Constructors
		public DeveloperController() 
		{
			CreateNewEntityFunc = () => DeveloperCreateEditAppService.CreateNew ();
            GetEntityIdFunc = (model) => model.Id;
            GetEntityByIdFunc = (id) => DeveloperCreateEditAppService.GetById(id);
            DeleteEntityByIdFunc = (id) => DeveloperCreateEditAppService.Delete (id);
			GetEntitiesFunc = () => DeveloperCreateEditAppService.GetAll ();

			GetGridValuesFunc = (entity) => 
                                new object[] { DeveloperUI.GetAvatarUrl(entity.Email), entity.FullName, entity.Username, entity.Email, String.Join(", ", entity.AccountsAtIssuers.Select(r => r.AchievementIssuerId)) };
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
                return this.RedirectToHome();
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
			return this.Call (() => {                
				SaveEntity(entity);
				DependencyService.Create<IUnitOfWork>().Commit();
				ClearUserCache (entity.Username);

				return this.RedirectToDeveloperHome(entity.Username);
			}, (ex) => { 
				return View ("CreateEdit", entity);
			});
		}

		public override ActionResult Edit (long id)
		{
			if (DevInfo.Current != null && DevInfo.Current.Id.Equals (id)) {
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

            return this.RedirectToHome();
		}

		[ProxyName("existsDeveloperAccountAtIssuer")]
        public JsonResult ExistsDeveloperAccountAtIssuer(long achievementIssuerId, string username)
		{
			var service = new AchievementProviderService ();
	
            return Json (service.ExistsDeveloperAccountAtIssuer(achievementIssuerId, username), JsonRequestBehavior.AllowGet);
		} 

		[ProxyName("getAchievementHistory")]
		public JsonResult GetAchievementHistory(long developerId, long achievementId)
		{
			var service = new DeveloperService ();
			var developer = service.GetDeveloperById(developerId);
			var achievement = developer.GetAchievementById (achievementId);

			return Json (achievement.History.Select(h => new { DateTime = h.DateTime, Value = h.Value }), JsonRequestBehavior.AllowGet);
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