using System;

namespace DevAchievements.Domain
{
	/// <summary>
	/// Achievement history.
	/// </summary>
    public class AchievementHistory
    {
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.Domain.AchievementHistory"/> class.
		/// </summary>
		/// <param name="achievement">The achievement.</param>
		public AchievementHistory(Achievement achievement)
		{
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
		/// Gets or sets the date time.
		/// </summary>
		/// <value>The date time.</value>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value { get; set; }
		#endregion
    }
}

