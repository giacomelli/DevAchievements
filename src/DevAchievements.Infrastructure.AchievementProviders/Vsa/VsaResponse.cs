using System;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.AchievementProviders.Vsa
{
	/// <summary>
	/// Represents de Visual Studio Achievements response.
	/// </summary>
	public class VsaResponse
	{
		/// <summary>
		/// Gets or sets the total score.
		/// </summary>
		/// <value>The total score.</value>
		public int TotalScore { get; set; }

		/// <summary>
		/// Gets or sets the achievements.
		/// </summary>
		/// <value>The achievements.</value>
		public List<VsaAchievementResponse> Achievements { get; set; }
	}
}

