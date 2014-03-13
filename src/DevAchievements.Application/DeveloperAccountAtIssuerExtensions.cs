using System;
using DevAchievements.Domain;

namespace DevAchievements.Application
{
    /// <summary>
    /// DeveloperAccountAtIssuer extensions.
    /// </summary>
    public static class DeveloperAccountAtIssuerExtensions
    {
        /// <summary>
        /// Gets the name of the achievement issuer.
        /// </summary>
        /// <returns>The achievement issuer name.</returns>
        /// <param name="developerAccountAtIssuer">Developer account at issuer.</param>
        public static string GetAchievementIssuerName(this DeveloperAccountAtIssuer developerAccountAtIssuer)
        {
            var service = new AchievementIssuerService();

            var issuer = service.GetAchievementIssuerById(developerAccountAtIssuer.AchievementIssuerId);

            if (issuer == null)
            {
                return String.Empty;
            }

            return issuer.Name;
        }
    }
}