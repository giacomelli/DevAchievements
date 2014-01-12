using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HelperSharp;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Repositories;

namespace DevAchievements.Domain
{
    public partial class AchievementService
    {
        #region Fields
        private AchievementProviderService m_providerService = new AchievementProviderService();
        #endregion
        #region Methods
        /// <summary>
        /// Gets the achievements by developer.
        /// </summary>
        /// <param name="developer">The developer.</param>
        public void UpdateDeveloperAchievements(Developer developer)
        {
            var providers = m_providerService.GetAchievementProviders();

            foreach (var provider in providers)
            {
                provider.CheckAvailability();

                if (provider.IsAvailable)
                {

                    foreach (var issuer in provider.SupportedIssuers)
                    {
                        var accountAtIssuer = developer.GetAccountAtIssuer(issuer.Name);
                        var oldAchievements = developer.GetAchievementsAtIssuer(issuer.Name);

                        if (accountAtIssuer != null)
                        {
                            try
                            {
                                var updatedAchievements = provider.GetAchievements(accountAtIssuer);

                                foreach (var updatedAchievement in updatedAchievements)
                                {
                                    var oldAchievement = oldAchievements.FirstOrDefault(o => o.Name.Equals(updatedAchievement.Name, StringComparison.OrdinalIgnoreCase));

                                    if (oldAchievement == null)
                                    {
                                        updatedAchievement.History.Add(new AchievementHistory(updatedAchievement));
                                        developer.Achievements.Add(updatedAchievement);
                                    }
                                    else
                                    {
                                        if (!oldAchievement.Value.Equals(updatedAchievement.Value))
                                        {
                                            developer.Achievements.Remove(oldAchievement);
                                            updatedAchievement.History = oldAchievement.History;
                                            updatedAchievement.History.Add(new AchievementHistory(updatedAchievement));
                                            developer.Achievements.Add(updatedAchievement);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogService.Error(
                                    "Error trying get achievements of developer '{0}' from issuer '{1}': {2}",
                                    developer.Username, issuer.Name, ex.Message);
                            }
                        }
                    }
                }
            }

            var devService = new DeveloperService();
            devService.SaveDeveloper(developer);
            DependencyService.Create<IUnitOfWork>().Commit();
        }

        /// <summary>
        /// Gets all achievements issuers.
        /// </summary>
        /// <returns>The all issuers.</returns>
        public IList<AchievementIssuer> GetAllIssuers()
        {
            var issuers = new List<AchievementIssuer>();
            var providers = m_providerService.GetAchievementProviders();

            foreach (var provider in providers)
            {
                issuers.AddRange(provider.SupportedIssuers);
            }

            return issuers;
        }
        #endregion
    }
}