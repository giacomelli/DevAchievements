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
            var account = new DeveloperAccount();
            account.AddAccountAtIssuer(new DeveloperAccountAtIssuer("Test", "DeveloperWithoutAchievements"));
            var actual = target.GetAchievementsByDeveloper(account);
			Assert.AreEqual (0, actual.Count);
		}

		[Test ()]
		public void GetAchievementsByDeveloper_ThereAreAchievementsForDeveloper_AchievementsFromProviders()
		{
			var target = new AchievementService ();
            var account = new DeveloperAccount();
            account.AddAccountAtIssuer(new DeveloperAccountAtIssuer("Test", "DeveloperWithAchievements"));
            var actual = target.GetAchievementsByDeveloper(account);
			Assert.AreEqual (2, actual.Count);

			Assert.AreEqual ("Achievement One", actual [1].Name);
			Assert.AreEqual ("Achievement Two", actual [0].Name);
		}
	}
}