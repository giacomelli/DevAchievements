using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.People;
using System.Diagnostics;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents a developer.
    /// This is the main entity inside the domain. A guess, someday, will be the main entity inside the world :).
    /// </summary>
    [DebuggerDisplay("{Id} - {Username}")]
    public class Developer : EntityWithIdBase<long>, IAggregateRoot
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
        public virtual string FullName { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// <remarks>
        /// The username is unique.
        /// </remarks>
        /// </summary>
        /// <value>The username.</value>
        public virtual string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the accounts at issuers.
        /// </summary>
        /// <value>The accounts at issuers.</value>
        public virtual IList<DeveloperAccountAtIssuer> AccountsAtIssuers { get; set; }

        /// <summary>
        /// Gets or sets the achievements.
        /// </summary>
        /// <value>The achievements.</value>
        public virtual IList<Achievement> Achievements { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Adds the account at issuer to accounts at issuer list if there none of the same issuer already.
        /// </summary>
        /// <param name="accountAtIssuer">The account at issuer.</param>
        public virtual void AddAccountAtIssuer(DeveloperAccountAtIssuer accountAtIssuer)
        {
            if (!AccountsAtIssuers.Any(a => a.AchievementIssuerId.Equals(accountAtIssuer.AchievementIssuerId)))
            {
                AccountsAtIssuers.Add(accountAtIssuer);
            }
        }

        /// <summary>
        /// Gets the account at issuer.
        /// </summary>
        /// <returns>The account at issuer.</returns>
        /// <param name="achievementIssuerId">The issuer id.</param>
        public virtual DeveloperAccountAtIssuer GetAccountAtIssuer(long achievementIssuerId)
        {
            return AccountsAtIssuers.FirstOrDefault(a => a.AchievementIssuerId == achievementIssuerId);
        }

        /// <summary>
        /// Gets the achievements at issuer.
        /// </summary>
        /// <returns>The achievements at issuer.</returns>
        /// <param name="achievementIssuerId">The issuer id.</param>
        public virtual IList<Achievement> GetAchievementsAtIssuer(long achievementIssuerId)
        {
            var x = Achievements.ToList();

            return x
                    .Where(a => a.Issuer.Id == achievementIssuerId)
                    .OrderBy(a => a.Name)
                    .ToList();
        }

        /// <summary>
        /// Gets an achievement by key.
        /// </summary>
        /// <param name="key">The achievement name.</param>
        /// <returns>The achievement.</returns>
        public virtual Achievement GetAchievementById(long key)
        {
            return Achievements.FirstOrDefault(a => a.Id.Equals(key));
        }

        /// <summary>
        /// Gets an achievement by name.
        /// </summary>
        /// <param name="achievementName">The achievement name.</param>
        /// <returns>The achievement.</returns>
        public virtual Achievement GetAchievementByName(string achievementName)
        {
            return Achievements.FirstOrDefault(a => a.Name.Equals(achievementName, StringComparison.OrdinalIgnoreCase));
        }
        #endregion
    }
}