using System;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents an achievement earned by a developer.
    /// </summary>
	public class Achievement : EntityBase, IAggregateRoot
	{
		#region Fields
		private string m_name;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.Domain.Achievement"/> class.
		/// </summary>
		public Achievement()
		{
			History = new List<AchievementHistory> ();
			DateTime = DateTime.UtcNow;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the developer.
		/// </summary>
		/// <value>The developer.</value>
		public Developer Developer { get; set; }

		/// <summary>
		/// Gets or sets the achievement's issuer.
		/// </summary>
		public AchievementIssuer Issuer { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name 
		{
			get{
				return m_name; 
			}

			set {
				if (Key == null && value != null) {
					Key = value.Replace(" ", "_");
				}

				m_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public int Value { get; set; }

		/// <summary>
		/// Gets or sets the date time.
		/// </summary>
		/// <value>The date time.</value>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the link.
		/// </summary>
		/// <value>The link.</value>
        public string Link { get; set; }

		/// <summary>
		/// Gets or sets the history.
		/// </summary>
		/// <value>The history.</value>
		public IList<AchievementHistory> History { get; set; }
		#endregion

		#region Methods
		/// <summary>
		/// Gets the value change from specified day.
		/// </summary>
		/// <returns>The value change from.</returns>
		/// <param name="day">The day.</param>
		public int GetValueChangeFrom(DateTime day)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}