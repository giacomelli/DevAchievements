using System;
using System.Collections.Generic;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Defines an interface for an achievement provider.
    /// An achievement provider is responsible to retrieve the achievements form achievements issuers.
    /// An provider, in most of case, will communicate with only one achievement provider, but can be more.
    /// </summary>
    public interface IAchievementProvider
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="DevAchievements.Domain.IAchievementProvider"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool Enabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is available.
        /// </summary>
        /// <value><c>true</c> if this instance is available; otherwise, <c>false</c>.</value>
        bool IsAvailable { get; }

        /// <summary>
        /// Gets the supported issuers.
        /// </summary>
        /// <value>The supported issuers.</value>
        AchievementIssuer[] SupportedIssuers { get; }
        #endregion
        #region Methods
        /// <summary>
        /// Checks the availability.
        /// </summary>
        void CheckAvailability();

        /// <summary>
        /// Gets the achievements.
        /// </summary>
        /// <returns>The achievements.</returns>
        /// <param name="account">The developer account at issuer.</param>
        IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account);

        /// <summary>
        /// Check if developer account exists at issuer.
        /// </summary>
        /// <param name="account">The developer account at issuer.</param>
        /// <returns>True if developer account exists at issuer.</returns>
        bool Exists(DeveloperAccountAtIssuer account);
        #endregion
    }
}