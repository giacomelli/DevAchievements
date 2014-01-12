using System;
using Skahal.Infrastructure.Framework.Domain;
using HelperSharp;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents an achievements's issuer, like GitHub or Stack Overflow.
    /// </summary>
    public class AchievementIssuer : EntityBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.AchievementIssuer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AchievementIssuer(string name)
        {
            ExceptionHelper.ThrowIfNullOrEmpty("name", name);

            Key = name;
            Name = name;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the logo URL.
        /// </summary>
        /// <value>The logo URL.</value>
        public string LogoUrl { get; set; }
        #endregion
    }
}

