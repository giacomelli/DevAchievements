using System;
using System.Diagnostics;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Achievement history.
    /// </summary>
    [DebuggerDisplay("{DateTime}: {Value}")]
    public class AchievementHistory : EntityWithIdBase<long>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.AchievementHistory"/> class.
        /// </summary>
        /// <param name="achievement">The achievement.</param>
        public AchievementHistory(Achievement achievement)
        {
            Achievement = achievement;
            DateTime = achievement.DateTime;
            Value = achievement.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.AchievementHistory"/> class.
        /// </summary>
        public AchievementHistory()
        {
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the achievement.
        /// </summary>
        /// <value>
        /// The achievement.
        /// </value>
        public virtual Achievement Achievement { get; set; }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>The date time.</value>
        public virtual DateTime DateTime { get; set; }
         
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public virtual int Value { get; set; }
        #endregion
    }
}
