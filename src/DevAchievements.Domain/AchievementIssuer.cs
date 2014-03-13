using System;
using Skahal.Infrastructure.Framework.Domain;
using HelperSharp;
using System.Diagnostics;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents an achievements's issuer, like GitHub or Stack Overflow.
    /// </summary>
    [DebuggerDisplay("{Id} - {Name}")]
    public class AchievementIssuer : EntityWithIdBase<long>, IAggregateRoot
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.AchievementIssuer"/> class.
        /// </summary>
        public AchievementIssuer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.AchievementIssuer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AchievementIssuer(string name)
        {
            ExceptionHelper.ThrowIfNullOrEmpty("name", name);

            Name = name;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the logo URL.
        /// </summary>
        /// <value>The logo URL.</value>
        public virtual string LogoUrl { get; set; }
        #endregion
    }
}

