using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            }

            return s_achievementProviders;
        }

        /// <summary>
        /// Gets the achievement provider by issuer name.
        /// </summary>
        /// <returns>The achievement provider by issuer name.</returns>
        /// <param name="issuerName">The issuer name.</param>
        public IAchievementProvider GetAchievementProviderByIssuerName(string issuerName)
        {
            return GetAchievementProviders()
                    .FirstOrDefault(p => p.SupportedIssuers.Any(i => i.Name.Equals(issuerName, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Checks if the developer account exists at issuer.
        /// </summary>
        /// <returns><c>true</c>, if developer account at issuer exists, <c>false</c> otherwise.</returns>
        /// <param name="issuerName">The issuer name.</param>
        /// <param name="username">The developer account username at issuer.</param>
        public bool ExistsDeveloperAccountAtIssuer(string issuerName, string username)
        {
            var provider = GetAchievementProviders()
                            .FirstOrDefault(p => p.SupportedIssuers.Any(i => i.Name.Equals(issuerName, StringComparison.OrdinalIgnoreCase)));

            if (provider == null)
            {
                return false;
            }

            return provider.Exists(new DeveloperAccountAtIssuer()
            {
                IssuerName = issuerName,
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
