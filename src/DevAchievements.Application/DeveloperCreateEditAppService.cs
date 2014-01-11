using System;
using System.Collections.Generic;
using DevAchievements.Domain;
using System.Linq;
using DevAchievements.Infrastructure.Web.Security;
using AutoMapper;

namespace DevAchievements.Application
{
	/// <summary>
	/// Developer create edit app service.
	/// </summary>
	public static class DeveloperCreateEditAppService
    {
		/// <summary>
		/// Initializes the <see cref="DevAchievements.Application.DeveloperCreateEditAppService"/> class.
		/// </summary>
		static DeveloperCreateEditAppService()
		{
			AutoMapper.Mapper.CreateMap<Developer, DeveloperCreateEditViewModel> ().ReverseMap ();
		}

		/// <summary>
		/// Gets the domain service.
		/// </summary>
		/// <value>The domain service.</value>
		private static DeveloperService DomainService 
		{ 
			get 
			{
				return new DeveloperService ();
			}
		}

		/// <summary>
		/// Gets all developers.
		/// </summary>
		/// <returns>The all developers.</returns>
		public static IList<DeveloperCreateEditViewModel> GetAll()
		{
			return AutoMapper.Mapper.Map<IList<DeveloperCreateEditViewModel>>(DomainService.GetAllDevelopers ());
		} 

		/// <summary>
		/// Gets the by key.
		/// </summary>
		/// <returns>The by key.</returns>
		/// <param name="key">Key.</param>
		public static DeveloperCreateEditViewModel GetByKey(Guid key)
		{
			return FillModel (DomainService.GetDeveloperByKey (key));
		} 

		/// <summary>
		/// Creates the new.
		/// </summary>
		/// <returns>The new.</returns>
		public static DeveloperCreateEditViewModel CreateNew()
		{
			return FillModel (new Developer ());
		}

		/// <summary>
		/// Delete the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		public static void Delete(Guid key)
		{
			DomainService.DeleteDeveloper (key);
		}

		/// <summary>
		/// Save the specified model.
		/// </summary>
		/// <param name="model">Model.</param>
		public static void Save(DeveloperCreateEditViewModel model)
		{
			var entity = Mapper.Map<Developer> (model);
			var oldDeveloper = DomainService.GetDeveloperByKey (model.Key);

			if (oldDeveloper != null) {
				entity.Achievements = oldDeveloper.Achievements;
			}

			DomainService.SaveDeveloper (entity);
			AuthenticationService.SaveAuthenticationProviderUser(entity, model.Provider, model.ProviderUserKey);
		}

		private static DeveloperCreateEditViewModel FillModel (Developer model)
		{
			var service = new AchievementService ();
			var issuers = service.GetAllIssuers ();

			foreach (var issuer in issuers) {
				model.AddAccountAtIssuer (new DeveloperAccountAtIssuer (issuer.Name, ""));
			}

			model.AccountsAtIssuers = model.AccountsAtIssuers.OrderBy (a => a.IssuerName).ToList();

			return AutoMapper.Mapper.Map<DeveloperCreateEditViewModel> (model);
		}
    }
}

