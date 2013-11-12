using System;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.AchievementProviders.Vsa
{
	public class VsaResponse
	{
		public int TotalScore { get; set; }
		public List<VsaAchievementResponse> Achievements { get; set; }
	}
}

