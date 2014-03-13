using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevAchievements.Domain.Specifications;
using HelperSharp;
using KissSpecifications;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Domain layer achievement provider service.
    /// </summary>
    public class AchievementProviderService
    {
        #region Fields
        private static IList<IAchievementProvider> s_achievementProviders;
        #endregion

        #region Methods
        /// <summary>
        /// Gets the achievement providers.
        /// </summary>
        /// <returns>The achievement providers.</returns>
        public IList<IAchievementProvider> GetAchievementProviders()
        {
            if (s_achievementProviders == null)
            {
                var canditateAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var types = new List<Type>();

                foreach (var a in canditateAssemblies)
                {
                    try
                    {
                        types.AddRange(a.GetTypes());
                    }
                    catch (ReflectionTypeLoadException)
                    {
                        continue;
                    }
                }

                s_achievementProviders = types
                    .Where(t => FilterAchievementProviders(t))
                    .Select(t => Activator.CreateInstance(t) as IAchievementProvider)
                    .Where(p => p.Enabled)
                    .OrderBy(p => p.SupportedIssuers.First().Name)
                    .ToList();

                // Saves the new achievement issuers.
                var issuerService = new AchievementIssuerService();

                foreach (var provider in s_achievementProviders)
                {
                    foreach (var issuer in provider.SupportedIssuers)
                    {
                        var savedIssuer = issuerService.GetAchievementIssuerByName(issuer.Name);

                        if (savedIssuer == null)
                        {
                            savedIssuer = issuer;
                            issuerService.SaveAchievementIssuer(savedIssuer);
                        }

                        // Refresh the id.
                        issuer.Id = savedIssuer.Id;
                    }
                }
            }

            return s_achievementProviders;
        }

        /// <summary>
        /// Checks if the developer account exists at issuer.
        /// </summary>
        /// <returns><c>true</c>, if developer account at issuer exists, <c>false</c> otherwise.</returns>
        /// <param name="achievementIssuerId">The achievement issuer id.</param>
        /// <param name="username">The developer account username at issuer.</param>
        public bool ExistsDeveloperAccountAtIssuer(long achievementIssuerId, string username)
        {
            var provider = GetAchievementProviders()
                .FirstOrDefault(p => p.SupportedIssuers.Any(i => i.Id == achievementIssuerId));

            if (provider == null)
            {
                return false;
            }

            var service = new AchievementIssuerService();
            var issuer = service.GetAchievementIssuerById(achievementIssuerId);

            if (issuer == null)
            {
                throw new ArgumentException("The achievement issuer with id '{0}' does not exists.".With(achievementIssuerId));
            }

            return provider.Exists(new DeveloperAccountAtIssuer()
            {
                AchievementIssuerId = issuer.Id,
                Username = username
            });
        }
        #endregion
        #region Helpers
        private static bool FilterAchievementProviders(Type type)
        {
            var interfaceType = typeof(IAchievementProvider);

            try
            {
                return interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
