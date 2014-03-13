using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents a developer account at an achievements issuer, like GitHub and Stack Overflow.
    /// </summary>
    public class DeveloperAccountAtIssuer : EntityWithIdBase<long>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.DeveloperAccountAtIssuer"/> class.
        /// </summary>
        public DeveloperAccountAtIssuer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.DeveloperAccountAtIssuer"/> class.
        /// </summary>
        /// <param name="achievementIssuerId">The achievement issuer id.</param>
        /// <param name="username">The developer username at issuer.</param>
        public DeveloperAccountAtIssuer(long achievementIssuerId, string username)
        {
            AchievementIssuerId = achievementIssuerId;
            Username = username;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the issuer id.
        /// </summary>
        public virtual long AchievementIssuerId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The developer username at issuer.</value>
        public virtual string Username { get; set; }
        #endregion
    }
}
