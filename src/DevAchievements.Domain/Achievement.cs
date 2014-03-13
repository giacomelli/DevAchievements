using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents an achievement earned by a developer.
    /// </summary>
    [DebuggerDisplay("{Name} - {DateTime}: {Value}")]
    public class Achievement : EntityWithIdBase<long>, IAggregateRoot
    {       
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.Achievement"/> class.
        /// </summary>
        public Achievement()
        {
            History = new List<AchievementHistory>();
            DateTime = DateTime.UtcNow;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the developer.
        /// </summary>
        /// <value>The developer.</value>
        public virtual Developer Developer { get; set; }

        /// <summary>
        /// Gets or sets the achievement's issuer.
        /// </summary>
        public virtual AchievementIssuer Issuer { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public virtual int Value { get; set; }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>The date time.</value>
        public virtual DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public virtual string Link { get; set; }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        /// <value>The history.</value>
        public virtual IList<AchievementHistory> History { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the value change from specified day.
        /// </summary>
        /// <returns>The value change from.</returns>
        /// <param name="day">The day.</param>
        public virtual int GetValueChangeFrom(DateTime day)
        {
            var change = 0;
            var orderedHistory = History.OrderByDescending(h => h.DateTime);
            var nearest = orderedHistory.FirstOrDefault(h => h.DateTime <= day);

            if (nearest == null)
            {
                nearest = orderedHistory.LastOrDefault(h => h.DateTime > day);
            }

            if (nearest != null)
            {
                change = Value - nearest.Value;
            }

            return change;
        }
        #endregion
    }
}