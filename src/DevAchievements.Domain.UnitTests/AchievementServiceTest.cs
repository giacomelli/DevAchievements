using NUnit.Framework;
using System;
using Rhino.Mocks;

namespace DevAchievements.Domain.UnitTests
{
	[TestFixture ()]
	public class AchievementServiceTest
	{
	
		[Test ()]
		public void GetAchievementsByDeveloper_NoAchievementsForDeveloper_EmptyList ()
		{
			var target = new AchievementService ();
			var actual = target.GetAchievementsByDeveloper (new DeveloperAchievementProviderAccount("DeveloperWithoutAchievements"));
			Assert.AreEqual (0, actual.Count);
		}

		[Test ()]
		public void GetAchievementsByDeveloper_ThereAreAchievementsForDeveloper_AchievementsFromProviders()
		{
			var target = new AchievementService ();
			var actual = target.GetAchievementsByDeveloper (new DeveloperAchievementProviderAccount("DeveloperWithAchievements"));
			Assert.AreEqual (2, actual.Count);

			Assert.AreEqual ("Achievement One", actual [0].Name);
			Assert.AreEqual ("Achievement Two", actual [1].Name);
		}
	}
}