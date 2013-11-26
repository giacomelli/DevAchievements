using System;
using System.Linq;
using DevAchievements.Domain;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.AchievementProviders.Memory
{
	public class MemoryAchievementProvider : AchievementProviderBase
	{
		#region Constructors
		public MemoryAchievementProvider()
			: base(
				new AchievementIssuer("GitHub"),
				new AchievementIssuer("StackOverflow"),
				new AchievementIssuer("Visual Studio Achievements"))
		{
			Enabled = false;
		}
		#endregion

		#region Methods
		public override void CheckAvailability()
		{

		}

		public override IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
		{
			var achievements = new List<Achievement> ();

			var issuer = SupportedIssuers.First (i => i.Name.Equals (account.IssuerName));
			AddAchievement (achievements, "Test 1", 1, "http://localhost", issuer);
			AddAchievement (achievements, "Test 2", 2, "http://localhost", issuer);
			AddAchievement (achievements, "Test 3", 3, "http://localhost", issuer);
	
			return achievements;
		}
		#endregion
	}
}

