using System;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents an achievement earned by a developer.
    /// </summary>
	public class Achievement : EntityBase, IAggregateRoot
	{
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
		public string Name  { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value { get; set; }

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
		#endregion
	}
}