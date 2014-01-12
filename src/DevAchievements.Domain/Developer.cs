using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.People;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents a developer.
    /// This is the main entity inside the domain. A guess, someday, will be the main entity inside the world :).
    /// </summary>
    public class Developer : User
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.Developer"/> class.
        /// </summary>
        public Developer()
        {
            AccountsAtIssuers = new List<DeveloperAccountAtIssuer>();
            Achievements = new List<Achievement>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// <remarks>
        /// The username is unique.
        /// </remarks>
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the accounts at issuers.
        /// </summary>
        /// <value>The accounts at issuers.</value>
        public IList<DeveloperAccountAtIssuer> AccountsAtIssuers { get; set; }

        /// <summary>
        /// Gets or sets the achievements.
        /// </summary>
        /// <value>The achievements.</value>
        public IList<Achievement> Achievements { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Adds the account at issuer to accounts at issuer list if there none of the same issuer already.
        /// </summary>
        /// <param name="accountAtIssuer">The account at issuer.</param>
        public void AddAccountAtIssuer(DeveloperAccountAtIssuer accountAtIssuer)
        {
            if (!AccountsAtIssuers.Any(a => a.IssuerName.Equals(accountAtIssuer.IssuerName, StringComparison.OrdinalIgnoreCase)))
            {
                AccountsAtIssuers.Add(accountAtIssuer);
            }
        }

        /// <summary>
        /// Gets the account at issuer.
        /// </summary>
        /// <returns>The account at issuer.</returns>
        /// <param name="issuerName">The issuer name.</param>
        public DeveloperAccountAtIssuer GetAccountAtIssuer(string issuerName)
        {
            return AccountsAtIssuers.FirstOrDefault(a => a.IssuerName.Equals(issuerName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets the achievements at issuer.
        /// </summary>
        /// <returns>The achievements at issuer.</returns>
        /// <param name="issuerName">Issuer name.</param>
        public IList<Achievement> GetAchievementsAtIssuer(string issuerName)
        {
            return Achievements
                    .Where(a => a.Issuer.Name.Equals(issuerName, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(a => a.Name)
                    .ToList();
        }

        /// <summary>
        /// Gets an achievement by key.
        /// </summary>
        /// <param name="key">The achievement name.</param>
        /// <returns>The achievement.</returns>
        public Achievement GetAchievementByKey(object key)
        {
            return Achievements.FirstOrDefault(a => a.Key.Equals(key));
        }

        /// <summary>
        /// Gets an achievement by name.
        /// </summary>
        /// <param name="achievementName">The achievement name.</param>
        /// <returns>The achievement.</returns>
        public Achievement GetAchievementByName(string achievementName)
        {
            return Achievements.FirstOrDefault(a => a.Name.Equals(achievementName, StringComparison.OrdinalIgnoreCase));
        }
        #endregion
    }
}