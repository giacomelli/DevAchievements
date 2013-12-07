using System;

namespace DevAchievements.Infrastructure.AchievementProviders.Vsa
{
	/// <summary>
	/// Represents the Visual Studio Achievements achievement response.
	/// </summary>
	public class VsaAchievementResponse
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the date earned.
		/// </summary>
		/// <value>The date earned.</value>
		public DateTime? DateEarned { get; set; }
	}
}